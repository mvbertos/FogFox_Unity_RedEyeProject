using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity
{
    public Struct_PlayerInput PlayerInputs; 
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

        if (Input.GetKeyDown(PlayerInputs.Dash))
        {
            StartCoroutine(OnDash(moveDirection, dashActionTime));
            return;
        }

        if(Input.GetKeyDown(PlayerInputs.Interact)){
            OnInteract();
        }
        OnMove(moveDirection);
    }

    protected override void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// makes character dash
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="actionTime"></param>
    /// <returns></returns>
    protected override IEnumerator OnDash(Vector2 direction, float actionTime)
    {

        float dashExecutionTime = Time.time + actionTime;

        if (canDash)
        {
            canDash = false;

            do
            {
                print(dashExecutionTime + "/" + Time.time);
                OnMove(direction * dashSpeed);
                yield return new WaitForEndOfFrame();
                print("Dahsed");

            } while ((Time.time <= dashExecutionTime) == true);
        }
        else
        {
            dashExecutionTime = 0;
            yield return new WaitForSeconds(DashCoolDown);
            canDash = true;
        }

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
