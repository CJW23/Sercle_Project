using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Pawn, Knight, Bishop, Rook, Queen, King }

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

    public void AttackExpectation(int actionNum, Unit targetUnit)
    {
        int damage = (int)(myActions[actionNum].Damage * (1 - (float)targetUnit.Stat.Defense / 100));
        int probability = stat.Critical - targetUnit.stat.Dodge;

        int criticalProb = 0;
        int dodgeProb = 0;
        if (probability > 0) criticalProb = probability;
        else if (probability < 0) dodgeProb = probability;

        Debug.Log("예상 데미지 : " + damage);
        Debug.Log("크리티컬 확률 : " + criticalProb + "%");
        Debug.Log("회피 확률 : " + dodgeProb + "%");
    }
}