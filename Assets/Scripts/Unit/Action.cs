using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{
    [Tooltip("Direction / Area / MaxRange")]
    [SerializeField] private Range range;
    [SerializeField] private Skill skill;

    public Skill GetSkill { get { return skill; } }

    public List<Vector2> MyRange()
    {
        return range.GetRange();
    }

    public void Activate()
    {

    }
}