using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedEye.Character.Basics;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterEntity : MonoBehaviour, IInteraction, IDamageable
{
    //Attributes
    protected Enum_CharacterState characterState;
    public Enum_CharacterState CharacterState { get { return characterState; } }
    [SerializeField] protected Struct_BasicAttributes attributes;
    public Struct_BasicAttributes Attributes { get { return attributes; } }

    //Attributes Regeneration
    [Header("Regen Values")]
    [SerializeField] protected float staminaRegen = 2;
    public float StaminaRegen { get { return staminaRegen; } }

    [SerializeField] protected float staminaRegenTime = 1;
    public float StaminaRegenTime { get { return staminaRegenTime; } }

    //interactions
    [Header("Interactions")]
    [SerializeField] private Transform interactionPoint;
    public Transform InteractionPoint { get { return interactionPoint; } }
    [SerializeField] private Vector3 interactionMaxDistance;
    public Vector3 InteractionMaxDistance { get { return interactionMaxDistance; } }
    [SerializeField] private float interactionRange = 1;
    public float InteractionRange { get { return interactionRange; } }

    //Movementation
    [Header("Movementation")]
    [SerializeField] protected float moveSpeed = 5;
    protected Vector2 moveDirection = new Vector2();
    public Vector2 MoveDirection { get { return moveDirection; } }

    [Header("Skills")]
    [SerializeField] protected Skill MeleeBasicAttack;
    [SerializeField] protected Skill DashSkill;
    [SerializeField] private Transform skillParent;
    public Transform SkillParent { get { return skillParent; } }

    protected List<Skill> m_SkillList = new List<Skill>();
    protected List<Skill> m_InstantiatedSkillList = new List<Skill>();

    //Physics
    private Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody { get { return rigidbody; } }

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

    //Initialization Methods
    /// <summary>
    /// it setup skill list with all skills present on skill parent transform
    /// </summary>
    private void SetupSkillList()
    {
        foreach (Skill item in m_SkillList)
        {
            m_InstantiatedSkillList.Add(Instantiate<Skill>(item, SkillParent));
        }
        if (m_InstantiatedSkillList.Contains(MeleeBasicAttack)) { MeleeBasicAttack = Instantiate<Skill>(MeleeBasicAttack, SkillParent); }
        if (m_InstantiatedSkillList.Contains(DashSkill)) { DashSkill = Instantiate<Skill>(DashSkill, SkillParent); }
    }

    //Abstract Methods to instantiate.

    public abstract void OnMove(Vector2 direction);
    public abstract IEnumerator OnAttack(float coolDown, float Damage);
    public abstract void OnUseSkill();
    public abstract void OnDash(Skill dashSkill);

    //Virtual methods to instance
    /// <summary>
    /// It updates the state of this character
    /// </summary>
    public virtual void UpdateCharacterState(Enum_CharacterState newState)
    {
        characterState = newState;
    }
    /// <summary>
    /// It will verify if character is dead
    /// </summary>
    protected virtual void VerifyDeath()
    { //it will verify character is dead or not
        if (attributes.lP < attributes.minimumLP)
        {
            this.gameObject.SetActive(false);
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
                if (attributes.stamina + value <= attributes.maxStamina)
                {
                    attributes.stamina += value;
                }
                else
                {
                    attributes.stamina = attributes.maxStamina;
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
                valueToValidate = attributes.lP;
                minValueToValidate = attributes.minimumLP;
                break;
            case 2:
                valueToValidate = attributes.stamina;
                minValueToValidate = attributes.minimumStamina;
                break;
            case 3:
                valueToValidate = attributes.mana;
                minValueToValidate = attributes.minimumMana;
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
    /// Return a skill from character skill list
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public virtual Skill GetSkillFromList(Skill skill)
    {
        foreach (Transform child in SkillParent)
        {
            if (child.TryGetComponent<Skill>(out Skill childSkill))
            {
                if (childSkill.SkillName == skill.SkillName) return childSkill;
            }
        }
        return null;
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
                attributes.lP -= value;
                VerifyDeath();
                break;
            case 2:
                attributes.stamina -= value;
                break;
            case 3:
                attributes.mana -= value;
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
        Gizmos.DrawWireSphere(interactionPoint.position, MeleeBasicAttack.ContactRange);
    }

    /// <summary>
    /// Returns an IInteraction object, that can be used to interact
    /// </summary>
    /// <param name="point"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public virtual IInteraction InteractableObjectOnRange(Transform point, float range)
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
    /// <summary>
    /// return all Damageable Objects On Range
    /// </summary>
    /// <param name="point"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public virtual List<IDamageable> DamageableObjectsOnRange(Transform point, float range)
    {
        List<IDamageable> values = new List<IDamageable>();
        foreach (GameObject value in GetAllObjectsInRange(point, range))
        {
            if (value.TryGetComponent<IDamageable>(out IDamageable dobject))
            {
                values.Add(dobject);
            }
        }
        return values;
    }
    //Methods from Interact Interface
    public virtual void OnBeingInteract()
    {
        print("Hello my name is :" + gameObject.name);
    }

    public virtual void OnInteract()
    {
        IInteraction value = InteractableObjectOnRange(interactionPoint, interactionRange);

        if (value != null)
        {
            value.OnBeingInteract();
        }
    }

    //Methods from Damagable Interface
    public virtual void OnTakeDamage(float damage)
    {
        ReduceAtribute(damage, 1);
        print(attributes.lP);
    }
}
