using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    public Struct_PlayerInput PlayerInputs;

    void Update()
    {
        //Receive Movement Input
        ReceivePlayeMovementInput();
    }

    /// <summary>
    /// This method will update moveDirection input and then execute OnMove Method
    /// </summary>
    /// <param name="input"></param>
    private void ReceivePlayeMovementInput()
    {
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

        if (Input.GetKeyDown(PlayerInputs.Dash))
        {
            foreach (Skill skill in m_InstantiatedSkillList)
            {
                if (skill.TryGetComponent<Skill_Dash>(out Skill_Dash dash)) { OnDash(dash); }
            }

            return;
        }

        if (Input.GetKeyDown(PlayerInputs.Interact))
        {
            OnInteract();
        }
        OnMove(_moveDirection);
    }

    public override void OnAttack()
    {
        throw new System.NotImplementedException();
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
}
