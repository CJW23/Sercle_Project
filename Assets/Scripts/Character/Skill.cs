using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { Self, Friend, Enemy }

[System.Serializable]
public class Skill
{
    private Character caster;
    public Character Caster { set { caster = value; } }
    public string description;
    public float targetRange;
    public TargetType targetType;
    public float coolDown;
    public SkillEffect skillEffect;
    
    private bool isCooling = false;

    public IEnumerator Activate()
    {
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

        GameManager.instance.ApplySkill(caster, targets, skillEffect);

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
                if (!target) break;

                // Valid Check
                if (Vector3.Distance(caster.transform.position, target.transform.position) > targetRange)
                    target = null;

                // 그 값을 targets에 저장한다.
                if (target) targets.Add(target);                
                break;
            case TargetType.Enemy:
                break;
            default:
                break;
        }

        return targets;
    }
}
