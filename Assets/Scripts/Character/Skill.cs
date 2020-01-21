using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { Self, Friend, Enemy }

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Skill")]
public class Skill : ScriptableObject
{
    private Character caster;
    public Character Caster { set { caster = value; } }
    public string description;
    public float targetRange;
    public TargetType targetType;
    public float coolDown;
    public SkillEffect skillEffect;
    public bool isCooling = false;

    public IEnumerator Activate()
    {
        Debug.Log("Start Skill : " + description);
        if(isCooling)
        {
            Debug.Log("This skill is cooling");
            yield break;
        }

        isCooling = true;

        List<Character> targets = new List<Character>();

        GameManager.instance.InputState = InputState.Action;

        while (targets.Count == 0)
        {
            targets = FindTargets();
            yield return new WaitForFixedUpdate();
        }

        GameManager.instance.InputState = InputState.Normal;

        string log = "현재 지정된 타겟은 ";
        foreach (Character target in targets)
        {
            log += target.name + ", ";
        }
        log += "입니다.";
        Debug.Log(log);

        foreach (Character target in targets)
        {
            List<EffectResult> effects = skillEffect.GetEffectResult(caster, target);
            GameManager.instance.ApplySkill(target, effects);
        }

        yield return new WaitForSeconds(coolDown);

        isCooling = false;
    }

    private List<Character> FindTargets()
    {
        List<Character> targets = new List<Character>();

        switch (targetType)
        {
            case TargetType.Self:
                targets.Add(caster);
                break;
            case TargetType.Friend:
                Debug.Log("타겟 찾는 중");
                // 스킬의 범위를 보여준다.
                

                // 마우스 클릭에 의한 캐릭터를 지정받는다.
                Character target = GameManager.instance.ClickedCharacter();

                if (isValidTarget(target, true)) targets.Add(target);
              
                break;
            case TargetType.Enemy:
                Debug.Log("타겟 찾는 중");
                // 스킬의 범위를 보여준다.


                // 마우스 클릭에 의한 캐릭터를 지정받는다.
                target = GameManager.instance.ClickedCharacter();

                if (isValidTarget(target, false)) targets.Add(target);

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
    private bool isValidTarget(Character target, bool shouldFriend)
    {
        if (target == null) return false;
        if (target.isFriend != shouldFriend) return false;
        if (Vector3.Distance(caster.transform.position, target.transform.position) > targetRange) return false;

        return true;
    }
}
