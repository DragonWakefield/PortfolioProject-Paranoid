using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterRngState
{
    Normal,
    Frenzy
}

public class EncounterManager : MonoBehaviour
{

    public static EncounterManager encounterManagerInstance;
    public float timer = 0;

    [Header("Timer Intervals")]
    [SerializeField] private int normalTimerInterval;
    [SerializeField] private int frenzyTimerInterval;

    private int rngValue = 0;
    public EncounterRngState rngState;

    private void Awake()
    {
        encounterManagerInstance = this;
    }
    void Start()
    {
        rngState = EncounterRngState.Normal;
    }
    
    // Update is called once per frame
    void Update()
    {
        TickEncounterTimer();
    }

    void TickEncounterTimer()
    {
        switch(rngState)
        {
            case EncounterRngState.Normal:
                timer += Time.deltaTime;
                if(timer >= normalTimerInterval)
                {
                    rngValue = 25;
                    SwitchState(EncounterRngState.Frenzy);
                }
                break;

            case EncounterRngState.Frenzy:
                timer += Time.deltaTime;
                if(timer >= frenzyTimerInterval)
                {
                    rngValue = 50;
                    SwitchState(EncounterRngState.Normal);
                }
                break;

            default:
                break;
        }
    }

    public void SwitchState(EncounterRngState state)
    {
        timer = 0;
        rngState = state;
    }

    public int GetRng()
    {
        return rngValue;
    }

    public float GetTimer()
    {
        return timer;
    }
}
