using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    //Information
    public List<Struct_Effect> Effects = new List<Struct_Effect>();
    public Enum_AtributeCostType AtributeCostType;
    public float Cost = 5;
    public float Cooldown = 5;
    public float ActionTime = 5;

    //Execution
    public Sprite RangeSprite;
    protected bool executing = false;
}
