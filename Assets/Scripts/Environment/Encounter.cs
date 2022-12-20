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
    private GameObject playerCentre;

    [SerializeField]
    private Vector3 _distanceToPlayer;

    private bool _activated;
    private float initialAngle;
    private bool startEncounter;
    
    private void Start(){
        _activated = false;
        movSpeed = 0.5f;
        playerCentre = GameObject.FindGameObjectWithTag("EyeTracker");
        model = transform.GetChild(1).GetChild(0).gameObject;
        modelRenderer = model.GetComponent<Renderer>();
        initialAngle = 0;
        startEncounter = false;
        
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
            TrackAndFade();
        }
    }

    private float GetAngle(){
        _distanceToPlayer = player.transform.position - model.transform.position;

        Vector3 playerToCentre = playerCentre.transform.position - player.transform.position ;

        float angle = Vector3.Angle(_distanceToPlayer, playerToCentre);    

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
                StartCoroutine(FadeOut(4f));
            }
        }


    }

    public void ActivateEncounter(){
        Debug.Log("Activated");
        _activated = true;
    }

    private IEnumerator FadeOut(float fadeTime){
        Vector3 startPosition = model.transform.position;
        Vector3 endPosition = model.transform.position - model.transform.forward * 5f;
        float elaspedTime = 0;

        while(elaspedTime < fadeTime){

            model.transform.position = Vector3.Lerp(startPosition, endPosition, (elaspedTime/ fadeTime));

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        
    }
}
