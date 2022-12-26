using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EncounterState
{
    Disabled,
    Enabled,
    Encounter,
    Finished
}
public class Encounter1 : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 _distanceToPlayer;

    [Header("Target Position")]
    [SerializeField] private Transform targetPos;

    private EncounterState state;

    private float initialAngle;

    private Renderer modelRenderer;


    [Header("Materials")]
    public Material tempMaterial1;
    public Material tempMaterial2;


    private void Start(){
        modelRenderer = GetComponent<Renderer>();
        initialAngle = 0;
    }

    private void Update(){
        
        switch (state)
        {
            case EncounterState.Disabled:
                break;

            case EncounterState.Enabled:
                float angle = GetAngle();
                if (angle > 40f)
                {
                    state = EncounterState.Encounter;
                    modelRenderer.material = tempMaterial2; // Testing Primatives
                }
                break;

            case EncounterState.Encounter:
                TrackAndFade();
                break;

            case EncounterState.Finished:
                break;

        }
    }

    private float GetAngle(){

        _distanceToPlayer = transform.position - Camera.main.transform.position;

        float angle = Vector3.Angle(_distanceToPlayer, Camera.main.transform.forward);

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
                Debug.Log("DEBUG");
                state = EncounterState.Finished;
            }
        }
    }

    public void ActivateEncounter(){

        if(state == EncounterState.Finished) return;
        
        state = EncounterState.Enabled;

    }

    private IEnumerator FadeOut(float fadeTime){
        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetPos.position;
        float elaspedTime = 0;

        while(elaspedTime < fadeTime){

            transform.position = Vector3.Lerp(startPosition, endPosition, (elaspedTime/ fadeTime) * moveSpeed);

            elaspedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
