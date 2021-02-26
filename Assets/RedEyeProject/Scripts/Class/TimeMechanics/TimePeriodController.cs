using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class TimePeriodController : MonoBehaviour
{
    public Enum_TimePeriod CurrentTimePeriod;//ACtual world period
    public List<Struct_TimePeriod> m_PeriodInfo = new List<Struct_TimePeriod>();//PeriodInfo
    private Dictionary<Enum_TimePeriod, Struct_TimePeriod> m_PeriodInfoDictionary = new Dictionary<Enum_TimePeriod, Struct_TimePeriod>();
    private List<StoreEntity> m_Stores = new List<StoreEntity>();
    public Light2D GlobalLight;//Focused on filter the colors of the world

    private void Start()
    {

        //Load m_PeriodInfoDictionary
        foreach (Struct_TimePeriod item in m_PeriodInfo)
        {
            m_PeriodInfoDictionary.Add(item.period, item);
        }
        //Load Stores
        foreach (StoreEntity stores in FindObjectsOfType<StoreEntity>())
        {
            m_Stores.Add(stores);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //TODO:Delete after finish debug
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeTimePeriod();
        }
    }
    /// <summary>
    /// Change world period
    /// Morning,Day,Night,Dawn
    /// </summary>
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
    /// <summary>
    /// update game world after change period
    /// Morning,Day,Night,Dawn
    /// </summary>
    private void OnChangePeriod()
    {
        UpdateStores();
        UpdateAmbient();
        //Call Events
    }

    /// <summary>
    /// It will update ambient PalletColor
    /// Reminder - Think about change sprite too
    /// </summary>
    private void UpdateAmbient()
    {


        if (!GlobalLight) { return; } // Check if worldPallet is null.

        m_PeriodInfoDictionary.TryGetValue(CurrentTimePeriod, out Struct_TimePeriod periodStruct);

        GlobalLight.color = periodStruct.periodColor;
    }

    //Maybe set this on StoreEntitys Script
    private void UpdateStores()
    {
        foreach (StoreEntity store in m_Stores)
        {
            store.UpdateStore(store.OpenPeriod.Contains(CurrentTimePeriod));//TODO: make this verification in StoreEntityClass
            print(store.OpenPeriod.Contains(CurrentTimePeriod));
        }

    }
}
