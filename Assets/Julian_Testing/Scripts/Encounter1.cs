using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public enum EncounterState
{
    Disabled,
    Ready,
    Enabled,
    Encounter,
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
    public float cooldownTimer = 10;
    private float timer = 0;

    private Renderer modelRenderer;

    private Material material;
    private MeshRenderer mesh;

    public VisualEffect vfx;

    [Header("Dissolve Properties")]
    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;

    private void Start(){
        modelRenderer = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        initialAngle = 0;
        material = mesh.material;
    }

    private void Update(){
        
        switch (state)
        {
            case EncounterState.Disabled:
                DoDisableTimer();
                break;
                
            case EncounterState.Ready:

                break;

            case EncounterState.Enabled:
                float angle = GetAngle();
                if (angle > 40f)
                {
                    state = EncounterState.Encounter;
                }
                break;

            case EncounterState.Encounter:
                TrackAndFade();
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

            if (percentage < 0.20f){
                StartCoroutine(Dissolve());
               
            }
        }
    }

    public void ActivateEncounter(){
        var rngValue = (int)Random.Range(0, 100);
        Debug.Log(rngValue);
        if (state == EncounterState.Disabled) return;
        if(rngValue >= EncounterManager.encounterManagerInstance.GetRng())
        {
            state = EncounterState.Enabled;
        }
        else
        {
            state = EncounterState.Disabled;
            RefreshTimer();
        }
    }
    void DoDisableTimer()
    {
        if (state != EncounterState.Disabled) return;
        timer += Time.deltaTime;
        if(timer >= cooldownTimer)
        {
            state = EncounterState.Ready;
        }
    }
    IEnumerator Dissolve()
    {
        vfx.Play();
        float counter = 0;
        while(material.GetFloat("_DissolveAmount") < 1)
        {
            counter += dissolveRate;
            material.SetFloat("_DissolveAmount", counter);
            yield return new WaitForSeconds(refreshRate);
        }
        state = EncounterState.Disabled;
        RefreshTimer();
    }
    public EncounterState GetState()
    {
        return state;
    }
    private void RefreshTimer()
    {
        timer = 0;
    }
}
