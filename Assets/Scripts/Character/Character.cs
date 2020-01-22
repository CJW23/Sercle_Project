using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState { Idle, Move, Attack, Die }

public class Character : MonoBehaviour
{
    public Status status;
    public bool isFriend;

    private CharacterState state;
    private Character target = null;
    [SerializeField] private LayerMask contactLayer;

    [Header("Skills")]
    public Skill basicAttack;
    public List<Skill> skills;

    [Header("Skill UI")]
    [SerializeField] private CircleRenderer skillRangeCircle;

    private NavMeshAgent agent;
    private bool isAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        InitialSetting();
    }

    private void Update()
    {
        agent.speed = status.SPD;

        FindNearestTarget();
        StateMachine();
    }

    private void InitialSetting()
    {
        status.ChangeStat(StatusType.CHP, status.MHP);
        basicAttack.isCooling = false;
        foreach (Skill skill in skills)
        {
            skill.isCooling = false;
        }
    }

    private void FindNearestTarget()
    {
        target = null;

        Collider[] colls = Physics.OverlapSphere(transform.position, basicAttack.targetRange, contactLayer);

        float nearestDis = 9999999;

        foreach (Collider coll in colls)
        {
            if (coll.gameObject == gameObject) continue;

            if (target == null || Vector3.Distance(coll.transform.position, transform.position) < nearestDis)
            {
                nearestDis = Vector3.Distance(coll.transform.position, transform.position);
                target = coll.GetComponent<Character>();
            }
        }
    }

    private void BasicAttackActivate()
    {
        StartCoroutine(basicAttack.Activate(this, target));
    }

    public void SkillActivate(int skillNum)
    {
        StartCoroutine(skills[skillNum].Activate(this));
    }

    private void StateMachine()
    {
        if (status.CHP <= 0)
        {
            state = CharacterState.Die;
        }
        else if (agent.remainingDistance > agent.stoppingDistance)
        {
            state = CharacterState.Move;
        }
        else if (target)
        {
            state = CharacterState.Attack;
        }
        else
        {
            state = CharacterState.Idle;
        }

        StateAction();
    }

    private void StateAction()
    {
        switch (state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Move:
                break;
            case CharacterState.Attack:
                BasicAttackActivate();
                break;
            case CharacterState.Die:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void OnMouseEnter()
    {
        MouseCursor.instance.State = MouseState.Select;
    }

    /// <summary>
    /// 마우스가 이 캐릭터의 콜라이더를 클릭하면 불리는 함수.
    /// GameManager에게 자신이 클릭됐음을 전달.
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("You click " + name);
        GameManager.instance.ClickedCharacter(this);
    }

    private void OnMouseExit()
    {
        MouseCursor.instance.State = MouseState.Idle;
    }

    /// <summary>
    /// 파라미터로 들어온 효과들을 자신에게 적용하는 함수.
    /// duration이 0이면 영구적, 아니라면 일시적으로 적용한다.
    /// </summary>
    /// <param name="effects">적용할 효과들</param>
    public void Apply(List<EffectResult> effects)
    {
        foreach (EffectResult effect in effects)
        {
            Debug.Log(name + "에게 " + effect.type.ToString() + "을 " + effect.amount + "만큼 변화시키는 효과가 " + effect.duration + "동안 적용!");
            // 알아서 이 effect를 적용시키시오!
            if (effect.duration == 0)
            {
                status.ChangeStat(effect.type, effect.amount);
            }
            else
            {
                StartCoroutine(TempEffect(effect));
            }
        }
    }

    /// <summary>
    /// 캐릭터가 선택된 상태라는 것을 알려주기 위해 임시로 만든 함수.
    /// 선택되면 빨간색이 된다.
    /// </summary>
    /// <param name="isClicked">true면 선택된 것.</param>
    public void ChangeColor(bool isClicked)
    {
        if (isClicked) transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        else transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
    }

    /// <summary>
    /// 일시적인 효과(버프/디버프)를 적용하는 함수.
    /// </summary>
    /// <param name="effect"></param>
    /// <returns></returns>
    private IEnumerator TempEffect(EffectResult effect)
    {
        status.ChangeStat(effect.type, effect.amount);

        yield return new WaitForSeconds(effect.duration);

        status.ChangeStat(effect.type, -effect.amount);
    }

    /// <summary>
    /// 해당 스킬의 사거리를 표시해주는 함수.
    /// </summary>
    /// <param name="show">true면 표시, false면 표시하지 않는다.</param>
    /// <param name="range">스킬의 사거리. 기본값은 0이다.</param>
    public void ShowSkillRnage(bool show, float range = 0)
    {
        skillRangeCircle.SetupCircle(range);

        if (show) skillRangeCircle.gameObject.SetActive(true);
        else skillRangeCircle.gameObject.SetActive(false);
    }
}
