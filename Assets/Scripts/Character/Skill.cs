﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { Auto, Self, Friend, Enemy }

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public string description;

    public float targetRange;
    public TargetType targetType;

    /// <summary>
    /// 스킬 명령이 들어온 후 실제로 스킬 효과가 적용될 때까지 걸리는 시간.
    /// </summary>
    public float preDelay;
    /// <summary>
    /// 스킬 효과 적용 후 조작불가 시간.
    /// </summary>
    public float postDelay;
    public float coolDown;
    public SkillEffect skillEffect;

    public bool isCooling;

    /// <summary>
    /// 스킬의 효과를 발동시키는 함수.
    /// </summary>
    /// <param name="caster">시전자</param>
    /// <returns></returns>
    public IEnumerator Activate(Character caster)
    {
        #region 쿨타임 체크
        if (isCooling)
        {
            yield break;
        }
        #endregion

        Debug.Log("Start Skill : " + description);

        #region 타겟 지정
        GameManager.instance.InputState = InputState.Action;
        List<Character> targets = new List<Character>();

        // 타겟을 지정할 때까지 매 Fixed Update 마다 FindTargets 함수를 돌린다.
        while (targets.Count == 0)
        {
            targets = FindTargets(caster);
            yield return new WaitForFixedUpdate();
        }

        GameManager.instance.InputState = InputState.Normal;
        caster.ShowSkillRnage(false);
        #endregion

        string log = "현재 지정된 타겟은 ";
        foreach (Character target in targets) log += target.name + ", ";
        log += "입니다.";
        Debug.Log(log);

        #region 스킬 효과 적용
        isCooling = true;

        foreach (Character target in targets)
        {
            List<EffectResult> effects = skillEffect.GetEffectResult(caster, target);
            GameManager.instance.ApplySkill(target, effects);
        }
        #endregion

        yield return new WaitForSeconds(coolDown);

        isCooling = false;
    }

    /// <summary>
    /// 기본 자동 공격을 위한 발동 함수.
    /// 시전자와 타겟을 지정해서 바로 발동한다.
    /// </summary>
    /// <param name="caster">기본 공격을 시전하는 캐릭터</param>
    /// <param name="target">대상</param>
    /// <returns></returns>
    public IEnumerator Activate(Character caster, Character target)
    {
        #region 쿨타임 체크
        if (isCooling)
        {
            yield break;
        }
        #endregion

        Debug.Log("Start Skill : " + description);

        Debug.Log("현재 지정된 타겟은 " + target.name + "입니다.");

        #region 스킬 효과 적용
        isCooling = true;

        List<EffectResult> effects = skillEffect.GetEffectResult(caster, target);
        GameManager.instance.ApplySkill(target, effects);
        #endregion

        yield return new WaitForSeconds(coolDown);

        isCooling = false;
    }

    private List<Character> FindTargets(Character caster)
    {
        List<Character> targets = new List<Character>();

        switch (targetType)
        {
            case TargetType.Auto:
                break;
            case TargetType.Self:
                targets.Add(caster);
                break;
            case TargetType.Friend:
                //Debug.Log("타겟 찾는 중");
                // 스킬의 범위를 보여준다.
                caster.ShowSkillRnage(true, targetRange);

                // 마우스 클릭에 의한 캐릭터를 지정받는다.
                Character target = GameManager.instance.ClickedCharacter();

                if (isValidTarget(caster, target, true)) targets.Add(target);
              
                break;
            case TargetType.Enemy:
                //Debug.Log("타겟 찾는 중");
                // 스킬의 범위를 보여준다.
                caster.ShowSkillRnage(true, targetRange);

                // 마우스 클릭에 의한 캐릭터를 지정받는다.
                target = GameManager.instance.ClickedCharacter();

                if (isValidTarget(caster, target, false)) targets.Add(target);

                break;
            default:
                break;
        }

        return targets;
    }

    /// <summary>
    /// 타겟이 유효한지 검사한다.
    /// target이 null값인지 확인하고,
    /// 적군, 아군인지 확인하고,
    /// 거리가 스킬 사거리 이내인지 확인한다.
    /// </summary>
    /// <param name="target">유효성 판정 대상</param>
    /// <param name="shouldFriend">아군을 향하는 스킬이라면 true, 적군을 향하는 스킬이라면 false</param>
    /// <returns></returns>
    private bool isValidTarget(Character caster, Character target, bool shouldFriend)
    {
        if (target == null) return false;
        if (target.isFriend != shouldFriend) return false;
        if (Vector3.Distance(caster.transform.position, target.transform.position) > targetRange) return false;

        return true;
    }
}
