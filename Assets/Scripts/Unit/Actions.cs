using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { Attack, Move, Special }

[System.Serializable]
public class Actions
{
    [SerializeField] private ActionType type;
    [SerializeField] private int cost;
    [SerializeField] private string description;
    [SerializeField] private List<Action> myAction;

    public int Cost { get { return cost; } }
    public string Description { get { return description; } }
    public int Damage
    {
        get
        {
            foreach (Action action in myAction)
            {
                Skill skill = action.GetSkill;

                if (skill.Type == SkillType.Attack)
                    return skill.Value;
            }
            return 0;
        }
    }

    public void Activate()
    { }

    public List<Vector2> GetActionRange()
    {
        return myAction[0].MyRange();
    }
}
