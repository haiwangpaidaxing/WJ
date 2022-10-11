using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TF.Core.Editor
{
    [CustomPropertyDrawer(typeof(TFLayerAttribute))]
    public class TFLayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //TFLayerAttribute range = (TFLayerAttribute)attribute;
            TFLayerAttribute range = attribute as TFLayerAttribute;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            property.intValue = EditorGUI.Popup(position,
                          property.intValue, TFPhysics.instance.settings.layers);

        }
    }
}