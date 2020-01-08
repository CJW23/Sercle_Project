using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class BasicStatus
{
    [SerializeField] private int health;
    [SerializeField] private int defense;
    [SerializeField] private int critical;
    [SerializeField] private int dodge;

    public int Health { get { return health; } }
    public int Defense { get { return defense; } }
    public int Critical { get { return critical; } }
    public int Dodge { get { return dodge; } }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(BasicStatus))]
public class BasicStatusDrawerUIE : PropertyDrawer
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
        float rectWidth = (position.width - 10) / 4;
        var healthRect = new Rect(position.x, position.y, rectWidth, position.height);
        var defenseRect = new Rect(position.x + rectWidth + 5, position.y, rectWidth, position.height);
        var criticalRect = new Rect(position.x + rectWidth * 2 + 10, position.y, rectWidth, position.height);
        var dodgeRect = new Rect(position.x + rectWidth * 3 + 15, position.y, rectWidth, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(healthRect, property.FindPropertyRelative("health"), GUIContent.none);
        EditorGUI.PropertyField(defenseRect, property.FindPropertyRelative("defense"), GUIContent.none);
        EditorGUI.PropertyField(criticalRect, property.FindPropertyRelative("critical"), GUIContent.none);
        EditorGUI.PropertyField(dodgeRect, property.FindPropertyRelative("dodge"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif