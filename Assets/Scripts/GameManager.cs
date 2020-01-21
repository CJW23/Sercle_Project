﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum InputState { Normal, Action }

public class GameManager : MonoBehaviour
{
    [Tooltip("초당 CP 증가량")]
    [SerializeField] private float cps;
    [SerializeField] private float myCP;

    [SerializeField] private InputState inputState = InputState.Normal;
    public InputState InputState { set { inputState = value; } }
    [SerializeField] private Character curCharacter;
    [SerializeField] private Character clickedCharacter;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        myCP = 50;
    }

    private void Update()
    {
        if (!curCharacter) return;

        if (Input.GetKeyDown(KeyCode.Z))
            Upgrade();
        if (Input.GetMouseButtonDown(1))
            ClickToMove();
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(curCharacter.skills[0].Activate());
    }

    private void FixedUpdate()
    {
        // 매초 cps만큼 CP 충전
        myCP += cps * Time.fixedDeltaTime;
    }

    public Character ClickedCharacter()
    {
        switch (inputState)
        {
            case InputState.Normal:
                Debug.LogError("Normal 상태인데 Clicked Character 정보가 필요해?");
                return null;
            case InputState.Action:
                Character temp = clickedCharacter;
                clickedCharacter = null;
                return temp;
            default:
                return null;
        }
    }

    public void ClickedCharacter(Character character)
    {
        switch (inputState)
        {
            case InputState.Normal:
                if (curCharacter) curCharacter.ChangeColor(false);
                curCharacter = character;
                curCharacter.ChangeColor(true);
                break;
            case InputState.Action:
                clickedCharacter = character;
                break;
        }
    }
    

    /// <summary>
    /// 선택된 캐릭터가 있고 마우스 오른쪽 클릭을 했을 때 해당 캐릭터의 목표지점을 설정
    /// </summary>
    private void ClickToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            curCharacter.GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
    
    /// <summary>
    /// 임시로 만들어둔 캐릭터를 강화시키는 함수. 현재는 Z키를 누르면 CP를 20 소모하고 현재 선택된 캐릭터의 공격력이 20 증가.
    /// </summary>
    private void Upgrade()
    {
        if (myCP < 20) return;

        curCharacter.status.ChangeStat(StatusType.ATK, 20);
        myCP -= 20;
    }

    public void ApplySkill(Character caster, List<Character> targets, SkillEffect effect)
    {
        foreach (Character target in targets)
        {
            List<EffectResult> effects = new List<EffectResult>();

            effects = SkillLogic.EffectCalculate(caster, target, effect);

            target.Apply(effects);
        }
    }
}
