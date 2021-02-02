using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriodController : MonoBehaviour
{
    public Enum_TimePeriod TimePeriod;
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //TODO:Delete after finish debug
        if (Input.GetKey(KeyCode.F))
        {
            ChangeTimePeriod();
        }
    }
    public void ChangeTimePeriod()
    {
        if (TimePeriod <= Enum_TimePeriod.Night)
        {
            TimePeriod++;
        }else
        {
            TimePeriod=0;
        }
    }
    private void OnChangePeriod()
    {
        //ChangeAmbience
        //Call Events
        //Open Close Stores
    }
}
