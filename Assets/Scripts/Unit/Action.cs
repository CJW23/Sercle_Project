using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { Attack, Move, Special }

[System.Serializable]
public class Action
{
    [SerializeField] private ActionType type;
    [Tooltip("Direction / Area / MaxRange")]
    [SerializeField] private Range range;
    [SerializeField] private Skill skill;

    public List<Vector2> MyRange()
    {
        return range.GetRange();
    }

    public void Activate()
    {

    }
}