using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState { Idle, Move, Attack, Die }

public class Character : MonoBehaviour
{
    public Status status;

    [SerializeField] private CharacterState state;
    [SerializeField] private Character target = null;
    [SerializeField] private float attackRange;

    private NavMeshAgent agent;
    private bool isAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        InitialSetting();
    }

    private void Update()
    {
        FindNearestTarget();
        StateMachine();
    }

    private void InitialSetting()
    {
        agent.speed = status.SPD;
        status.ChangeStat(StatusType.CHP, status.MHP);
    }

    private void FindNearestTarget()
    {
        
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        target.status.ChangeStat(StatusType.CHP, -status.ATK);

        yield return new WaitForSeconds(1f);
        isAttacking = false;
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
        else if (target && Vector3.Distance(target.transform.position, transform.position) <= attackRange)
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
                StartCoroutine(Attack());
                break;
            case CharacterState.Die:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Now you are targeting " + name);
        GameManager.instance.CurCharacter = this;
    }
}
