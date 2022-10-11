using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TF.Core.Editor
{
    [CustomPropertyDrawer(typeof(TFLayerMask))]
    public class TFLayermaskPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (!TFPhysics.instance)
            {
                EditorGUI.EndProperty();
                return;
            }

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var valueRect = new Rect(position.x, position.y, 75, position.height);

            property.FindPropertyRelative("mask").intValue = EditorGUI.MaskField(valueRect, 
                property.FindPropertyRelative("mask").intValue, TFPhysics.instance.settings.layers);

            EditorGUI.EndProperty();
        }
    }
}
