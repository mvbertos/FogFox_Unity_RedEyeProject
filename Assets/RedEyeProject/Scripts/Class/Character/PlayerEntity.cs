using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    public PlayerInputStruct PlayerInputs;
    private Vector2 moveDirection = new Vector2();

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
        moveDirection = new Vector2();

        //Vertical Movementation
        if (Input.GetKey(PlayerInputs.MoveUp))
        {
            moveDirection.y = 1;
        }
        else if (Input.GetKey(PlayerInputs.MoveDown))
        {
            moveDirection.y = -1;
        }

        //Horizontal Movementation
        if (Input.GetKey(PlayerInputs.MoveLeft))
        {
            moveDirection.x = -1;
        }
        else if (Input.GetKey(PlayerInputs.MoveRight))
        {
            moveDirection.x = 1;
        }

        if (Input.GetKey(PlayerInputs.Dash))
        {
            StartCoroutine(OnDash(moveDirection, DashActionTime));
            return;
        }
        OnMove(moveDirection);
    }

    protected override void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator OnDash(Vector2 direction, float actionTime)
    {
        float dashActionTime = Time.time + actionTime;
        do
        {

            OnMove(direction * DashSpeed);
            yield return new WaitForEndOfFrame();
            print("Dahsed");

        } while (dashActionTime <= actionTime);
    }

    protected override void OnMove(Vector2 direction)
    {
        direction *= moveSpeed;

        rigidbody.velocity = direction;
    }

    protected override void OnUseSkill()
    {
        throw new System.NotImplementedException();
    }
}
