using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoreEntity : MonoBehaviour
{

    //NPC Dono
    [SerializeField]
    public List<Enum_TimePeriod> OpenPeriod = new List<Enum_TimePeriod>();
    private bool _StoreOpen;
    public bool StoreOpen
    {
        get
        {
            return _StoreOpen;
        }
    }

    public void UpdateStore(bool open)
    {
        _StoreOpen = open;
        //If Open
        //Do open store stuff
    }
}