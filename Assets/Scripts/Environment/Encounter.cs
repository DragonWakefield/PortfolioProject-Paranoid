using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public Material tempMaterial1;
    public Material tempMaterial2;
    [SerializeField]
    private float movSpeed;

    [SerializeField]
    private GameObject model;
    private Renderer modelRenderer;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 _distanceToPlayer;

    private Vector3 initialPosition;
    private bool _activated;
    private float initialAngle;
    private bool startEncounter;
    private bool fadeIn;

    private float FADE_IN_TIME = 1f;
    private float FADE_IN_DISTANCE = 2f;
    private float FADE_OUT_TIME = 4f;
    private float FADE_OUT_DISTANCE = 5f;
    private void Start(){
        _activated = false;
        movSpeed = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player");
        model = transform.GetChild(1).GetChild(0).gameObject;
        modelRenderer = model.GetComponent<Renderer>();
        initialAngle = 0;
        startEncounter = false;
        initialPosition = model.transform.position;
        fadeIn = false;
        InitializeNewPosition();
    }

    private void Update(){
        Debug.DrawRay(model.transform.position, model.transform.TransformDirection(Vector3.forward) * 10, Color.green);

        if(_activated){

            float angle = GetAngle();
            Debug.Log(angle);
            if (angle > 40f){
                startEncounter = true;
                modelRenderer.material = tempMaterial2; // Testing Primatives
            }
        }

        if (startEncounter){
            if (!fadeIn){
               StartCoroutine(FadeIn(FADE_IN_TIME)); 
            }
            
            TrackAndFade();
        }
    }

    private void InitializeNewPosition(){
        model.transform.position = model.transform.position - model.transform.forward * FADE_IN_DISTANCE;
    }

    private float GetAngle(){

        _distanceToPlayer = Camera.main.transform.position - model.transform.position;
        float angle = Vector3.Angle(_distanceToPlayer, -Camera.main.transform.forward);

        return angle;
    }

    private void TrackAndFade(){

        /* Track */
        float angle = GetAngle();

        /* Fade */
        if (initialAngle == 0){
            initialAngle = angle;
        }
        else{
            float percentage = angle / initialAngle;

            var modelColor = modelRenderer.material.color;
            modelColor.a = percentage;
            modelRenderer.material.color = modelColor;

            if (percentage < 0.20f){
                StartCoroutine(FadeOut(FADE_OUT_TIME));
            }
        }

    }

    public void ActivateEncounter(){
        // var rngValue = Random.Range(0, 100);
        // if(rngValue >= EncounterManager.encounterManagerInstance.GetRng())
        // {
        //    _activated = true;
        // }

        _activated = true;
    }

    private IEnumerator FadeIn(float fadeTime){
        fadeIn = true;
        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = model.transform.position + model.transform.forward * FADE_IN_DISTANCE;
        float elaspedTime = 0;

        while(elaspedTime < fadeTime){

            model.transform.position = Vector3.Lerp(startPosition, endPosition, (elaspedTime/ fadeTime));

            elaspedTime += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator FadeOut(float fadeTime){
        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = model.transform.position - model.transform.forward * FADE_OUT_DISTANCE;
        float elaspedTime = 0;

        while(elaspedTime < fadeTime){

            model.transform.position = Vector3.Lerp(startPosition, endPosition, (elaspedTime/ fadeTime));

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        
    }
}
