using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Temp Skill", menuName = "Skill/Temp Skill")]
public class TempSkill : SkillEffect
{
    [SerializeField] private List<EffectResult> effects;

    public override List<EffectResult> GetEffectResult(Character caster, Character target)
    {
        return effects;
    }
}
