﻿using System.Collections;
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