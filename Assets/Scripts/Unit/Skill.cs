using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum SkillType { Move, Attack, Defend, Heal, Grow, Castling }

[System.Serializable]
public class Skill
{
    [SerializeField] private SkillType type;
    [SerializeField] private int value;

    public SkillType Type { get { return type; } }
    public int Value { get { return value; } }

    public void SkillActivate()
    {
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Skill))]
public class SkillDrawerUIE : PropertyDrawer
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
        float typeRectWidth = (position.width - 5) * 0.8f;
        float valueRectWidth = (position.width - 5) * 0.2f;
        var typeRect = new Rect(position.x, position.y, typeRectWidth, position.height);
        var valueRect = new Rect(position.x + typeRectWidth + 5, position.y, valueRectWidth, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif
