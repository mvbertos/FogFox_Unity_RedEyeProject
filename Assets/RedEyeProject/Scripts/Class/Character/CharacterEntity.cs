﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterEntity : MonoBehaviour, IInteraction
{
    //Atributes
    [Header("Atributes")]
    [SerializeField]
    protected float lP = 5;
    [SerializeField]
    protected float maxLP = 5;
    [SerializeField]
    protected float minimumLP = 0;
    [SerializeField]
    protected float stamina = 5;
    [SerializeField]
    protected float maxStamina = 5;
    [SerializeField]
    protected float minimumStamina = 0;
    [SerializeField]
    protected float mana = 5;
    [SerializeField]
    protected float maxMana = 5;

    [Header("Regen Values")]
    [SerializeField]
    protected float staminaRegen = 2;
    [SerializeField]
    protected float staminaRegenTime = 1;


    //Movementation
    protected Enum_CharacterState characterState;
    [Header("Movementation")]
    [SerializeField]
    protected float moveSpeed = 5;

    [Header("Dash")]
    [SerializeField]
    protected float dashSpeed = 7;
    [SerializeField]
    protected float dashActionTime = 2;
    [SerializeField]
    protected float dashCost = 2;
    protected float DashCoolDown = 2;
    protected bool canDash = true;

    [Header("Interaction")]
    [SerializeField]
    protected Transform interactionPoint;
    [SerializeField]
    protected float interactionRange;

    //Physics
    protected Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody
    {
        get
        {
            return rigidbody;
        }
    }

    //Default Methods
    private void Awake()
    {
        //Rigidbody 2D
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Setting up character state
        characterState = Enum_CharacterState.Idle;

        //Start Regen
        StartCoroutine(RegenStamina(staminaRegenTime));
    }

    //Abstract Methods to instantiate.

    protected abstract void OnMove(Vector2 direction);
    protected abstract void OnAttack();
    protected abstract void OnUseSkill();
    protected abstract IEnumerator OnDash(Vector2 direction, float actionTime);

    //Virtual methods to instance

    /// <summary>
    /// It will verify if character is dead
    /// </summary>
    protected virtual void VerifyDeath()
    { //it will verify character is dead or not
        if (lP < minimumLP)
        {
            print("Dead");
        }
    }

    //Health

    /// <summary>
    /// It will reduce lifepoints and then verify if character is dead
    /// </summary>
    /// <param name="LifePoints"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual float ReduceLP(float LifePoints, float value)
    {
        LifePoints -= value;
        VerifyDeath();

        return LifePoints;
    }

    //Stamina 

    protected virtual IEnumerator RegenStamina(float regenTime)
    {
        do
        {
            stamina = AddStamina(stamina, staminaRegen);
            yield return new WaitForSeconds(regenTime);
        } while (true);
    }
    protected virtual float AddStamina(float staminaPoints, float value)
    {

        if (staminaPoints + value <= maxStamina)
        {
            staminaPoints += value;
        }
        else
        {
            staminaPoints = maxStamina;
        }

        return staminaPoints;
    }

    /// <summary>
    /// Verify if can use Stamina
    /// </summary>
    protected virtual bool VerifyStamina(float staminaPoints, float value)
    {
        if ((staminaPoints - value) >= minimumStamina)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Reduce Stamina 
    /// </summary>
    /// <param name="StaminaPoints"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual float ReduceStamina(float StaminaPoints, float value)
    {
        StaminaPoints -= value;
        return StaminaPoints;
    }
    /// <summary>
    /// Get every object in range
    /// </summary>
    /// <param name="point"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<GameObject> GetAllObjectsInRange(Transform point, float range)
    {

        List<GameObject> objects = new List<GameObject>();

        RaycastHit2D[] ray = Physics2D.CircleCastAll(point.position, range, point.forward);

        foreach (RaycastHit2D hit in ray)
        {
            if (hit.transform.gameObject != this.gameObject) { objects.Add(hit.transform.gameObject); }
        }

        return objects;
    }

    /// <summary>
    /// Debug Ranges
    /// </summary>
    private void OnDrawGizmos()
    {
        //Draw Interaction Circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }

    /// <summary>
    /// Returns an IInteraction object, that can be used to interact
    /// </summary>
    /// <param name="point"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public virtual IInteraction InteractableObjects(Transform point, float range)
    {
        foreach (GameObject value in GetAllObjectsInRange(point, range))
        {
            if (value.TryGetComponent<IInteraction>(out IInteraction iobject))
            {
                return iobject;
            }
        }
        return null;
    }

    //Methods from Interact Interface
    public virtual void OnBeingInteract()
    {
        print("Hello my name is :" + gameObject.name);
    }

    public virtual void OnInteract()
    {
        IInteraction value = InteractableObjects(interactionPoint, interactionRange);

        if (value != null)
        {
            value.OnBeingInteract();
        }
    }
}
