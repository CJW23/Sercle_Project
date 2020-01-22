using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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