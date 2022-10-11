using System.Collections;
using System.Collections.Generic;
using TF.Core;
using UnityEditor;
using UnityEngine;

namespace TF.Editor
{
    [CustomEditor(typeof(TFSettings))]
    public class TFSettingsEditor : UnityEditor.Editor
    {
        TFSettings t;
        bool layersDropdown;
        bool layerMatrixDropdown;
        bool[] layerMatrixsFoldout = new bool[32];
        List<bool[]> layersMatrixes = new List<bool[]>(32);

        private void Awake()
        {
            t = (TFSettings)target;

            layersMatrixes = new List<bool[]>();
            for(int i = 0; i < 32; i++)
            {
                layersMatrixes.Add(new bool[32]);
                BitArray ba = new BitArray(new int[]{ t.layerCollisionMatrix[i] });
                ba.CopyTo(layersMatrixes[i], 0);
            }
        }

        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            layersDropdown = EditorGUILayout.Foldout(layersDropdown, "Layers");
            if (layersDropdown)
            {
                EditorGUI.indentLevel++;

                for(int i = 0; i < t.layers.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"Layer {i}", GUILayout.Width(100));
                    t.layers[i] = EditorGUILayout.TextField(t.layers[i]);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel--;
            }

            layerMatrixDropdown = EditorGUILayout.Foldout(layerMatrixDropdown, "Collision Matrix");
            if (layerMatrixDropdown)
            {
                EditorGUI.indentLevel++;

                for(int i = 0; i < t.layerCollisionMatrix.Length; i++)
                {
                    if (string.IsNullOrEmpty(t.layers[i]))
                    {
                        continue;
                    }
                    layerMatrixsFoldout[i] = EditorGUILayout.Foldout(layerMatrixsFoldout[i], t.layers[i]);

                    if (layerMatrixsFoldout[i])
                    {
                        EditorGUI.indentLevel++;
                        for (int w = 0; w < t.layers.Length; w++)
                        {
                            if (string.IsNullOrEmpty(t.layers[w]) || t.layers[w] == t.layers[i])
                            {
                                continue;
                            }
                            layersMatrixes[i][w] = EditorGUILayout.Toggle(t.layers[w], layersMatrixes[i][w]);
                            if (GUI.changed)
                            {
                                // Set the equivalent to the same value.
                                layersMatrixes[w][i] = layersMatrixes[i][w];
                            }
                        }
                        EditorGUI.indentLevel--;
                    }
                }

                EditorGUI.indentLevel--;
            }

            if (GUI.changed)
            {
                for (int i = 0; i < 32; i++)
                {
                    t.layerCollisionMatrix[i] = (int)BitArrayToU64(new BitArray(layersMatrixes[i]));
                }
                EditorUtility.SetDirty(t);
            }
        }

        public ulong BitArrayToU64(BitArray ba)
        {
            var len = Mathf.Min(64, ba.Count);
            ulong n = 0;
            for (int i = 0; i < len; i++)
            {
                if (ba.Get(i))
                    n |= 1UL << i;
            }
            return n;
        }
    }
}