using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FixedPointy;


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FixConst))]
public class FixConstPropertyDrawer : PropertyDrawer
{
    double val;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var valueRect = new Rect(position.x, position.y, 45, position.height);
        var unitRect = new Rect(position.x + 50, position.y, 45, position.height);
        var buttonRect = new Rect(position.x + 100, position.y, 20, position.height);

        EditorGUI.LabelField(valueRect, FixConst.RawToString(property.FindPropertyRelative("raw").longValue));
        val = EditorGUI.DoubleField(unitRect, val);
        if (GUI.Button(buttonRect, "A"))
        {
            property.FindPropertyRelative("raw").longValue = ((FixConst)val).raw;
        }

        EditorGUI.EndProperty();
    }
}
#endif
