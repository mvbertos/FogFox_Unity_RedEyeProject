using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimePeriodController : MonoBehaviour
{
    public Enum_TimePeriod CurrentTimePeriod;//ACtual world period
    public List<TimePeriodStruct> PeriodInfo = new List<TimePeriodStruct>();//PeriodInfo
    public Grid WorldGrid;

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
        if (CurrentTimePeriod <= Enum_TimePeriod.Night)
        {
            CurrentTimePeriod++;
        }
        else
        {
            CurrentTimePeriod = 0;
        }

        OnChangePeriod();
    }
    private void OnChangePeriod()
    {
        UpdateStores();
        UpdateAmbient();
        //Call Events
    }
    private void UpdateAmbient()
    {
        Tilemap[] tilesmaps = WorldGrid.GetComponentsInChildren<Tilemap>();
        
        foreach (Tilemap tiles in tilesmaps)
        {
            foreach (TimePeriodStruct time in PeriodInfo)
            {
                if (time.period == CurrentTimePeriod)
                {
                    tiles.color = time.periodColor;
                }
            }

        }
    }
    //Maybe set this on StoreEntitys Script
    private void UpdateStores()
    {
        StoreEntity store = null;
        store = FindObjectOfType<StoreEntity>();

        if (store)
        {
            store.UpdateStore(store.OpenPeriod.Contains(CurrentTimePeriod));
        }
        print(store.StoreOpen);
    }
}
