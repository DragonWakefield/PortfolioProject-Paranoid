using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    Encounter encounterScript;
    
    private void Start(){
        encounterScript = transform.parent.gameObject.GetComponent<Encounter>();
    }

    private void OnTriggerEnter(Collider other){
        
        if (other.gameObject.layer == 7){
            encounterScript.ActivateEncounter();
            Debug.Log("Encounter Triggered");
        }
    }
}
