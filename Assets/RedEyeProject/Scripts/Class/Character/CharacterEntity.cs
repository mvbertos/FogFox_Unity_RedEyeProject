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
    protected float minimumStamina = 0;
    [SerializeField]
    protected float mana = 5;
    [SerializeField]
    protected float maxMana = 5;
    protected float minimumMana = 0;

    [Header("Regen Values")]
    [SerializeField]
    protected float staminaRegen = 2;
    [SerializeField]
    protected float staminaRegenTime = 1;

    //Combat
    [Header("Combat")]
    public Transform MeleeAttackPoint;
    public float MeleeAttackRange;
    public Vector3 MeleeMaxRange;
    public Transform SkillParent;
    public List<Skill> m_SkillList = new List<Skill>();
    [HideInInspector]
    public List<Skill> m_InstantiatedSkillList = new List<Skill>();


    //Movementation
    [HideInInspector]
    public Enum_CharacterState characterState;

    [Header("Movementation")]
    [SerializeField]
    protected float moveSpeed = 5;
    protected Vector2 _moveDirection = new Vector2();
    public Vector2 moveDirection
    {
        get { return _moveDirection; }
    }

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

        //Setting up character
        characterState = Enum_CharacterState.Idle;
        SetupSkillList();

        //Start Regen
        StartCoroutine(RegenAttribute(staminaRegenTime, staminaRegen, 2));
    }
    private void SetupSkillList()
    {
        foreach (Skill item in m_SkillList)
        {
            print(item);
            m_InstantiatedSkillList.Add(Instantiate<Skill>(item, SkillParent));
        }
    }

    //Abstract Methods to instantiate.

    public abstract void OnMove(Vector2 direction);
    public abstract void OnAttack();
    public abstract void OnUseSkill();
    public abstract void OnDash(Skill dashSkill);

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
    /// Regen atribute to it max value as possible
    /// 1)LP
    /// 2)Stamina
    /// 3)Mana
    /// </summary>
    /// <param name="regenTime"></param>
    /// <returns></returns>
    protected virtual IEnumerator RegenAttribute(float regenTime, float value, int op)
    {
        switch (op)
        {
            case 1:
                //Decide What to do with LP if nedded
                break;
            case 2:
                do
                {
                    AddAttributeValue(staminaRegen, 2);
                    yield return new WaitForSeconds(regenTime);
                } while (true);

            case 3:
                //Decide What to do with Mana if nedded
                break;
            default:
                break;
        }


    }

    /// <summary>
    /// Incremend certain amout to the seleced value
    /// 1)LP
    /// 2)Stamina
    /// 3)Mana
    /// </summary>
    /// <param name="value"></param>
    /// <param name="op"></param>
    protected virtual void AddAttributeValue(float value, int op)
    {
        switch (op)
        {
            case 1:
                break;
            case 2:
                if (stamina + value <= maxStamina)
                {
                    stamina += value;
                }
                else
                {
                    stamina = maxStamina;
                }
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Verify atributes
    /// 1)LP
    /// 2)Stamina
    /// 3)Mana
    /// </summary>
    public virtual bool VerifyAtribute(float value, int op)
    {
        float valueToValidate = 0;
        float minValueToValidate = 0;

        //Initialize validation var
        switch (op)
        {
            case 1:
                valueToValidate = lP;
                minValueToValidate = minimumLP;
                break;
            case 2:
                valueToValidate = stamina;
                minValueToValidate = minimumStamina;
                break;
            case 3:
                valueToValidate = mana;
                minValueToValidate = minimumMana;
                break;
            default:
                break;
        }

        //Validate
        if ((valueToValidate - value) >= minValueToValidate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Reduce Attribute 
    /// 1)LP
    /// 2)Stamina
    /// 3)Mana
    /// </summary>
    /// <param name="StaminaPoints"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual void ReduceAtribute(float value, int op)
    {
        switch (op)
        {
            case 1:
                lP -= value;
                VerifyDeath();
                break;
            case 2:
                stamina -= value;
                break;
            case 3:
                mana -= value;
                break;
            default:
                break;
        }
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
    protected virtual Vector3 GetMouseWorldPosition(Vector3 mousePosition)
    {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    /// <summary>
    /// Debug Ranges
    /// </summary>
    private void OnDrawGizmos()
    {
        //Draw Interaction Circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);

        //Draw Melee Circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(MeleeAttackPoint.position, MeleeAttackRange);
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
