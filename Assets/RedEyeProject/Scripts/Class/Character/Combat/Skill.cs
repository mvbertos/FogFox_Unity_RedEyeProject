using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour 
{
    //Information
    public Enum_SkillType SkillType;
    public Enum_AtributeCostType AtributeCostType;
    public float Cost;

    //Execution
    public Sprite RangeSprite;

    public abstract void OnUseSkill();
}
