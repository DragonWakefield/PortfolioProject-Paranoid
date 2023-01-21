using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger1 : MonoBehaviour
{
    [SerializeField] private Encounter1[] encounters;

    [SerializeField] private LayerMask playerMask;
    
    private void OnTriggerEnter(Collider other){

        if (playerMask == (playerMask | (1 << other.gameObject.layer))){
            foreach(var encounter in encounters)
            {
                if (encounter == null)
                    continue;
                Debug.Log(encounter.GetState());
                if(encounter.GetState() == EncounterState.Ready)
                 
                encounter.ActivateEncounter();
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach(var encounter in encounters)
        {
            Gizmos.DrawLine(gameObject.transform.position, encounter.gameObject.transform.position);
        }
    }
}
