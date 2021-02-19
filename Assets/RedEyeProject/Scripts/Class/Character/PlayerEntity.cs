using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    public Struct_PlayerInput PlayerInputs;

    void Update()
    {
        //Update MeleeAttackPoint
        MeleeAttackPoint.localPosition = UpdateMeleeAttackPoint();
        //Receive Movement Input
        ReceivePlayeMovementInput();
    }

    /// <summary>
    /// It defines position to where the meleeAttackpoint shold be
    /// </summary>
    private Vector3 UpdateMeleeAttackPoint()
    {

        Vector3 maxRange = MeleeMaxRange;
        Vector3 mouseWorldPosition = this.transform.position - GetMouseWorldPosition(Input.mousePosition);
        Vector3 meleePoint = MeleeAttackPoint.position;
        Vector3 newMeleePoint = new Vector3();

        //Defines limit for X position
        if (mouseWorldPosition.x < maxRange.x * -1)
        {
            newMeleePoint.x = maxRange.x * -1;
        }
        else if (mouseWorldPosition.x > maxRange.x)
        {
            newMeleePoint.x = maxRange.x;
        }
        else
        {
            newMeleePoint.x = mouseWorldPosition.x;
        }

        //Defins Limit for Y position
        if (mouseWorldPosition.y < maxRange.y * -1)
        {
            newMeleePoint.y = maxRange.y * -1;
        }
        else if (mouseWorldPosition.y > maxRange.y)
        {
            newMeleePoint.y = maxRange.y;
        }
        else
        {
            newMeleePoint.y = mouseWorldPosition.y;
        }

        return Vector3.Scale(newMeleePoint, new Vector3(-1, -1, 1));
    }

    /// <summary>
    /// This method will update moveDirection input and then execute OnMove Method
    /// </summary>
    /// <param name="input"></param>
    private void ReceivePlayeMovementInput()
    {
        //Movementation!!!

        _moveDirection = new Vector2();

        //Vertical Movementation
        if (Input.GetKey(PlayerInputs.MoveUp))
        {
            _moveDirection.y = 1;
        }
        else if (Input.GetKey(PlayerInputs.MoveDown))
        {
            _moveDirection.y = -1;
        }

        //Horizontal Movementation
        if (Input.GetKey(PlayerInputs.MoveLeft))
        {
            _moveDirection.x = -1;
        }
        else if (Input.GetKey(PlayerInputs.MoveRight))
        {
            _moveDirection.x = 1;
        }

        //Dash 
        if (Input.GetKeyDown(PlayerInputs.Dash))
        {
            foreach (Skill skill in m_InstantiatedSkillList)
            {
                if (skill.TryGetComponent<Skill_Dash>(out Skill_Dash dash)) { OnDash(dash); }
            }

            return;
        }

        //Interactions!!!

        //Interact
        if (Input.GetKeyDown(PlayerInputs.Interact))
        {
            OnInteract();
        }
        OnMove(_moveDirection);

        //Combat!!!
        if (characterState == Enum_CharacterState.Attacking) { return; }
        
        if (Input.GetKeyDown(PlayerInputs.MeleeFast))
        {
            StartCoroutine(OnAttack(MeleeCoolDown, MeleeDamage));
        }
        else if (Input.GetKeyDown(PlayerInputs.MeleeStrong))
        {
            StartCoroutine(OnAttack(MeleeCoolDown, MeleeDamage));
        }
    }



    public override void OnMove(Vector2 direction)
    {
        direction *= moveSpeed;

        rigidbody.velocity = direction;

        characterState = Enum_CharacterState.Walking;
    }

    public override void OnUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDash(Skill dashSkill)
    {
        StartCoroutine(dashSkill.GetComponent<Skill_Dash>().OnDash(this.gameObject.GetComponent<CharacterEntity>()));
    }

    public override IEnumerator OnAttack(float coolDown, float Damage)
    {
        characterState = Enum_CharacterState.Attacking;
        foreach (IDamageable damagables in DamageableObjectsOnRange(MeleeAttackPoint, MeleeAttackRange))
        {
            damagables.OnTakeDamage(MeleeDamage);
        }
        yield return new WaitForSeconds(coolDown);
        characterState = Enum_CharacterState.Idle;
    }
}
