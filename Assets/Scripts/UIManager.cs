using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Basic Info Panel")]
    [SerializeField] private GameObject basicInfoPanel;
    [SerializeField] private Text unitName;
    [SerializeField] private Text basicStatText;
    [SerializeField] private Text talentText;

    [Header("Action Panel")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private Text cost;
    [SerializeField] private Text description;

    private Unit nowUnit = null;

    public Unit NowUnit { set { nowUnit = value; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // 초기 상태 설정
        basicInfoPanel.SetActive(false);
        actionPanel.SetActive(false);
    }

    public void ShowBasicInfo(Tile tile)
    {
        basicInfoPanel.SetActive(true);

        Unit unit = tile.Unit;
        unitName.text = unit.UnitType.ToString();
        basicStatText.text = unit.Stat.Health + "/" + unit.Stat.Defense + "/" + unit.Stat.Critical + "/" + unit.Stat.Dodge;
    }

    public void ShowActionDescription(int num)
    {
        actionPanel.SetActive(true);

        Actions actions = nowUnit.Actions(num);
        cost.text = actions.Cost.ToString();
        description.text = actions.Description;
    }

    public void SelectAction(int num)
    {
        Actions actions = nowUnit.Actions(num);

        if (GameManager.instance.TryAction(actions) == false)
        {
            // 행동력이 부족해서 액션을 수행할 수 없을 경우
        }
        else
        {
            List<Vector2> actionRange = actions.GetActionRange();

            // actionRange와 actionType을 타일 매니저에게 넘겨주면 됨.
        }
    }

    public void ShowAttackExpectation(int actionNum, Unit targetUnit)
    {
        nowUnit.AttackExpectation(actionNum, targetUnit);
    }
}