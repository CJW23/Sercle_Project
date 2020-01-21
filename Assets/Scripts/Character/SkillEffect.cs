using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Attack, Heal, Temp }

[System.Serializable]
public struct EffectResult
{
    public StatusType type;
    public float amount;
    public float duration;

    public EffectResult(StatusType type, float amount, float duration = 0)
    {
        this.type = type;
        this.amount = amount;
        this.duration = duration;
    }
}

public abstract class SkillEffect : ScriptableObject
{
    public abstract List<EffectResult> GetEffectResult(Character caster, Character target);
}

[CreateAssetMenu(fileName = "New Attack Skill", menuName = "Skill/Attack Skill")]
public class AttackSkill : SkillEffect
{
    [SerializeField] private float damageCoefficient = 1f;

    public override List<EffectResult> GetEffectResult(Character caster, Character target)
    {
        List<EffectResult> effects = new List<EffectResult>();

        float damage = damageCoefficient * caster.status.ATK * (1 - target.status.DEF / 100);
        int rand = Random.Range(0, 100);

        float prob = caster.status.CRT - target.status.DDG;

        if (prob > 0 && rand <= prob) damage *= caster.status.CC;
        else if (prob < 0 && rand <= Mathf.Abs(prob)) damage = 0;

        effects.Add(new EffectResult(StatusType.CHP, -damage));

        return effects;
    }
}

[CreateAssetMenu(fileName = "New Healing Skill", menuName = "Skill/Healing Skill")]
public class HealingSkill : SkillEffect
{
    [SerializeField] private float healCoefficient = 1f;

    public override List<EffectResult> GetEffectResult(Character caster, Character target)
    {
        List<EffectResult> effects = new List<EffectResult>();

        float healAmount = healCoefficient * caster.status.ATK;

        effects.Add(new EffectResult(StatusType.CHP, healAmount));

        return effects;
    }
}

[CreateAssetMenu(fileName = "New Temp Skill", menuName = "Skill/Temp Skill")]
public class TempSkill : SkillEffect
{
    [SerializeField] private List<EffectResult> effects;

    public override List<EffectResult> GetEffectResult(Character caster, Character target)
    {
        return effects;
    }
}