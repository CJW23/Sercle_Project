using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Direction { Self, Front, Sun, Straight, Diagonal, FrontDiagonal, All }
public enum Area { Edge, All, AllExceptSelf };

[System.Serializable]
public class Range
{
    [SerializeField] private Direction dir;
    [SerializeField] private Area area;
    [SerializeField] private int maxRange;

    public List<Vector2> GetRange()
    {
        List<Vector2> ret = new List<Vector2>();
        List<int> areaList = GetArea();

        List<Vector2> dirVecs = new List<Vector2>();

        switch (dir)
        {
            case Direction.Self:
                ret.Add(Vector2.zero);
                break;
            case Direction.Front:
                dirVecs = new List<Vector2> { new Vector2(0, 1) };
                break;
            case Direction.Sun:
                dirVecs = new List<Vector2> { new Vector2(2, 1), new Vector2(1, 2), new Vector2(-1, 2), new Vector2(-2, 1),
                                              new Vector2(-2, -1), new Vector2(-1, -2), new Vector2(1, -2), new Vector2(2, -1)};
                break;
            case Direction.Straight:
                dirVecs = new List<Vector2> { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
                break;
            case Direction.Diagonal:
                dirVecs = new List<Vector2> { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
                break;
            case Direction.FrontDiagonal:
                dirVecs = new List<Vector2> { new Vector2(1, 1), new Vector2(-1, 1) };
                break;
            case Direction.All:
                dirVecs = new List<Vector2> { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0),
                                              new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
                break;
            default:
                Debug.LogError("Sth wrong when get range of skill");
                break;
        }

        for (int i = 0; i < dirVecs.Count; i++)
        {
            for (int j = 0; j < areaList.Count; j++)
            {
                ret.Add(dirVecs[i] * areaList[j]);
            }
        }

        return ret;
    }

    private List<int> GetArea()
    {
        List<int> ret = new List<int>();

        switch (area)
        {
            case Area.Edge:
                ret.Add(maxRange);
                break;
            case Area.All:
                for (int i = 0; i <= maxRange; i++) ret.Add(i);
                break;
            case Area.AllExceptSelf:
                for (int i = 1; i <= maxRange; i++) ret.Add(i);
                break;
            default:
                Debug.LogError("Sth wrong when get range of skill");
                break;
        }

        return ret;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Range))]
public class RangeDrawerUIE : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        float rectWidth = (position.width - 10) / 3;
        var dirRect = new Rect(position.x, position.y, rectWidth, position.height);
        var areaRect = new Rect(position.x + rectWidth + 5, position.y, rectWidth, position.height);
        var maxRangeRect = new Rect(position.x + rectWidth * 2 + 10, position.y, rectWidth, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(dirRect, property.FindPropertyRelative("dir"), GUIContent.none);
        EditorGUI.PropertyField(areaRect, property.FindPropertyRelative("area"), GUIContent.none);
        EditorGUI.PropertyField(maxRangeRect, property.FindPropertyRelative("maxRange"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif