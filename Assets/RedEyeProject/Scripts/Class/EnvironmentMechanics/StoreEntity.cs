using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoreEntity : MonoBehaviour //TODO:Review this class
{

    //NPC Dono
    [SerializeField]
    public List<Enum_TimePeriod> OpenPeriod = new List<Enum_TimePeriod>();
    private bool storeOpen;
    public GameObject entranceTile;
    public bool StoreOpen
    {
        get
        {
            return storeOpen;
        }
    }

    void Awake()
    {
        foreach (Transform item in this.transform)
        {
            if (item.name.Equals("Entrance"))
            {
                entranceTile = item.gameObject;
            }
        }
    }
    public void UpdateStore(bool open)
    {
        storeOpen = open;
        if (storeOpen)
        {
            entranceTile.SetActive(false);
        }
        else
        {
            entranceTile.SetActive(true);
        }
        //If Open
        //Do open store stuff
    }
}