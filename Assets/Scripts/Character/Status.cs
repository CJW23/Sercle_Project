using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType { MHP, CHP, SPD, ATK, DEF, CRT, DDG }

[System.Serializable]
public class Status
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private float spd;
    [SerializeField] private float atk;
    [SerializeField] private float def;
    [SerializeField] private float crt;
    [SerializeField] private float ddg;

    public int MHP { get { return maxHp; } }
    public int CHP { get { return curHp; } }
    public float SPD { get { return spd; } }
    public float ATK { get { return atk; } }
    public float DEF { get { return def; } }
    public float CRT { get { return crt; } }
    public float DDG { get { return ddg; } }

    /// <summary>
    /// Critical Coefficient, 크리티컬 계수
    /// </summary>
    private float cc;

    /// <summary>
    /// 해당 유닛의 Status를 변경합니다. amount가 양수일 경우 더해지고 음수일 경우 뺍니다.
    /// </summary>
    /// <param name="type">변경하고자 하는 StatusType</param>
    /// <param name="amount">변경하고자 하는 양</param>
    public void ChangeStat(StatusType type, float amount)
    {
        switch(type)
        {
            case StatusType.MHP:
                maxHp += (int)amount;
                break;
            case StatusType.CHP:
                curHp += (int)amount;
                break;
            case StatusType.SPD:
                spd += amount;
                break;
            case StatusType.ATK:
                atk += amount;
                break;
            case StatusType.DEF:
                def += amount;
                break;
            case StatusType.CRT:
                crt += amount;
                break;
            case StatusType.DDG:
                ddg += amount;
                break;
            default:
                Debug.LogError("Something is Wrong at Changing Status");
                break;
        }
    }
}