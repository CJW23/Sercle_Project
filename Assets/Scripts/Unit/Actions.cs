using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Actions
{
    [SerializeField] private int cost;
    [SerializeField] private string description;
    [SerializeField] private List<Action> myAction;

    public int Cost { get { return cost; } }
    public string Description { get { return description; } }

    public void Activate()
    { }

    public List<Vector2> GetActionRange()
    {
        return myAction[0].MyRange();
    }
}
