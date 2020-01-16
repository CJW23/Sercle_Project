using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Tile selectedTile;
    private Tile targetTile;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AssignSelectedTile(Tile selected)
    {
        selectedTile = selected;
        UIManager.instance.ShowBasicInfo(selectedTile);
    }

    public void AssignTargetTile(Tile target)
    {
        targetTile = target;

        //target.unitInfo.
    }

    public bool TryAction(Actions actions)
    {
        // 액션을 수행할 수 있는 행동력이 있는지 계산
        // 행동력이 부족하다면 return false;
        // 행동력이 충분하다면 행동력을 차감하고 return true;
        return true;
    }
}
