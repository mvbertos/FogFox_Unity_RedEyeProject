using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    //Information
    [SerializeField]
    private string skillName;
    public string SkillName
    {
        get { return skillName; }
    }
    public List<Struct_Effect> Effects = new List<Struct_Effect>();
    public Enum_AtributeCostType AtributeCostType;
    public float Cost = 5;
    public float Cooldown = 5;
    public float ActionTime = 5;
    public float ContactRange = 1;

    //Execution
    public Sprite RangeSprite;
    protected bool executing = false;
    private void Awake()
    {
        executing = false;
    }
    public virtual Struct_Effect GetEffect(Enum_EffectType effectType)
    {
        foreach (Struct_Effect effect in Effects)
        {
            if (effect.EffectType == effectType)
            {
                return effect;
            }
        }
        return new Struct_Effect();
    }
}
