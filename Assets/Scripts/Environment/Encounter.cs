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
    
    private void Start(){
        _activated = false;
        movSpeed = 2f;
        playerCentre = GameObject.FindGameObjectWithTag("EyeTracker");
        model = transform.GetChild(1).GetChild(0).gameObject;
        modelRenderer = model.GetComponent<Renderer>();
        initialAngle = 0;
    }

    private void Update(){
        if(_activated){
            TrackAndFade();
        }
    }

    private void TrackAndFade(){

        /* Track */
        Debug.DrawLine(model.transform.position, player.transform.position, Color.green);
        _distanceToPlayer = player.transform.position - model.transform.position;

        Vector3 playerToCentre = playerCentre.transform.position - player.transform.position ;

        float angle = Vector3.Angle(_distanceToPlayer, playerToCentre);

        //Debug.Log(angle);

        /* Fade */
        if (initialAngle == 0){
            initialAngle = angle;
        }
        else{
            float percentage = angle / initialAngle;
            Debug.Log(percentage);
            var modelColor = modelRenderer.material.color;
            modelColor.a = percentage;
            modelRenderer.material.color = modelColor;

            if (percentage < 0.20f){
                StartCoroutine(FadeOut(1f));
            }
        }


    }

    public void ActivateEncounter(){
        Debug.Log("Activated");
        _activated = true;
        modelRenderer.material = tempMaterial2;
    }

    private IEnumerator FadeOut(float fadeTime){
        Vector3 startPosition = model.transform.position;
        float elaspedTime = 0;

        while(elaspedTime < fadeTime){
            model.transform.position -= Vector3.forward * Time.deltaTime * movSpeed;
            elaspedTime += Time.deltaTime;
        }

        Destroy(gameObject);
        yield return null;
    }
}
