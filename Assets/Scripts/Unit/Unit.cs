using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Pawn, Knigh, Bishop, Rook, Queen, King }

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitType unitType;
    [Tooltip("health/defense/critical/dodge")]
    [SerializeField] private BasicStatus stat;
    private Talent talent;
    [SerializeField] private List<Actions> myActions;

    public UnitType UnitType { get { return unitType; } }
    public BasicStatus Stat { get { return stat; } }
    public Actions Actions(int num) { return myActions[num]; }

    public void ShowActionDescription(int actionNum)
    {

    }
}