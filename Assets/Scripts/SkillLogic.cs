using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLogic
{
    public static List<EffectResult> EffectCalculate(Character caster, Character target, SkillEffect effect)
    {
        List<EffectResult> results = new List<EffectResult>();

        switch (effect.type)
        {
            case SkillType.Attack:
                float damage = effect.damageCoefficient * caster.status.ATK * (1 - target.status.DEF / 100);
                int rand = Random.Range(0, 100);

                float prob = caster.status.CRT - target.status.DDG;

                if (prob > 0 && rand <= prob) damage *= caster.status.CC;
                else if (prob < 0 && rand <= Mathf.Abs(prob)) damage = 0;

                results.Add(new EffectResult(StatusType.CHP, -damage));
                break;
            case SkillType.Heal:
                float heal = effect.healCoefficeint * caster.status.ATK;

                results.Add(new EffectResult(StatusType.CHP, heal));
                break;
            case SkillType.Temp:
                results.AddRange(effect.effectResults);
                break;
        }

        return results;
    }
}