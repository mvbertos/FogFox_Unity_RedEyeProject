using System.Collections;
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
    protected float mana = 5;
    [SerializeField]
    protected float maxMana = 5;


    //Movementation
    [Header("Movementation")]
    [SerializeField]
    protected float moveSpeed = 5;

    [Header("Dash")]
    [SerializeField]
    protected float dashSpeed = 7;
    [SerializeField]
    protected float dashActionTime = 2;
    [SerializeField]
    protected float DashCoolDown = 2;
    protected bool canDash = true;

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
        rigidbody = GetComponent<Rigidbody2D>();
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

    //Methods from Interact Interface
    public virtual void OnBeingInteract()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnInteract()
    {
        //Get objects on range
        //If has IInteraction Interface
        //Execute interaction
    }
}
