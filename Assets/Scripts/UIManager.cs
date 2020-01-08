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

    private void Update()
    {
        if (nowUnit != null)
        {
            ShowBasicInfo(nowUnit);
        }
        else
        {
            basicInfoPanel.SetActive(false);
        }
    }

    private void ShowBasicInfo(Unit unit)
    {
        basicInfoPanel.SetActive(true);

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
        List<Vector2> actionRange = actions.GetActionRange();

        // actionRange를 보여주기!
    }
}