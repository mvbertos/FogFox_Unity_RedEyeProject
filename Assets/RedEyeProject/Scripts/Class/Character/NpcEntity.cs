using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class NpcEntity : CharacterEntity
{
    private AIPath _AIPath;
    private AIDestinationSetter _AIDestinationSetter;

    private void Start(){
        _AIPath = gameObject.GetComponent<AIPath>();
        _AIDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
    }
    
    protected override void OnAttack()
    {
        throw new System.NotImplementedException();
    }


    protected override IEnumerator OnDash(Vector2 direction, float actionTime)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnMove(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnUseSkill()
    {
        throw new System.NotImplementedException();
    }
}
