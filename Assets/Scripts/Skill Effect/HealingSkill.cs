using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
