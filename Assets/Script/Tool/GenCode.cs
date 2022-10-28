//using System;
//using System.CodeDom;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using UnityEditor;
//using UnityEditor.Callbacks;
//using UnityEngine;
//using UnityEngine.UI;

//public class GenCode : EditorWindow
//{
//    //删除所有Miss的脚本

//    public class DeleteMissingScripts
//    {
//        [MenuItem("Tool/Cleanup Missing Scripts")]
//        static void CleanupMissingScripts()
//        {
//            for (int i = 0; i < Selection.gameObjects.Length; i++)
//            {
//                var gameObject = Selection.gameObjects[i];

//                // We must use the GetComponents array to actually detect missing components
//                var components = gameObject.GetComponents<Component>();

//                // Create a serialized object so that we can edit the component list
//                var serializedObject = new SerializedObject(gameObject);
//                // Find the component list property
//                var prop = serializedObject.FindProperty("m_Component");

//                // Track how many components we've removed
//                int r = 0;

//                // Iterate over all components
//                for (int j = 0; j < components.Length; j++)
//                {
//                    // Check if the ref is null
//                    if (components[j] == null)
//                    {
//                        // If so, remove from the serialized component array
//                        prop.DeleteArrayElementAtIndex(j - r);
//                        // Increment removed count
//                        r++;
//                    }
//                }

//                // Apply our changes to the game object
//                serializedObject.ApplyModifiedProperties();
//                EditorUtility.SetDirty(gameObject);
//            }
//        }

//    }
//    /// <summary>
//    /// 生成路径
//    /// </summary>
//    public string genUIPath = @"D:\GetHubProject\Dream\Assets\Script\Gen";
//    /// <summary>
//    /// 生成UI路径类名
//    /// </summary>
//    public string classUIPathName = "类名";


//    /// <summary>
//    /// 生成面板路径
//    /// </summary>
//    public string genUIPanelPath = @"D:\GetHubProject\Dream\Assets\Script\Gen";
//    /// <summary>
//    /// 生成UI面板类名
//    /// </summary>
//    public string classUIPanelName = "类名";
//    [MenuItem("Gen/UI")]
//    public static void GenUIPath()
//    {
//        EditorWindow editorWindow = GetWindow(typeof(GenCode), true);
//    }
//    static GenSelectPanelData genSelectPanelData = new GenSelectPanelData();

//    private void OnGUI()
//    {

//        //输入框控件
//        genUIPath = EditorGUILayout.TextField("生成脚本的路径:", genUIPath);
//        classUIPathName = EditorGUILayout.TextField("ClassName:", classUIPathName);
//        //EditorGUILayout.PropertyField(typeof(GemPathParameter));
//        if (GUILayout.Button("生成路径", GUILayout.Width(500)))
//        {
//            UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

//            CodeTypeDeclaration genClass = GenTool.CreateCodeTypeDeclaration(classUIPathName, GenTool.GenType.Class, System.CodeDom.MemberAttributes.Static);
//            for (int i = 0; i < arr.Length; i++)
//            {
//                string path = AssetDatabase.GetAssetPath(arr[i]);

//                path = path.Replace("Assets/Resources/", "");

//                path = path.Replace(".prefab", "");
//                Debug.Log(path);
//                GenTool.ClassAddField<string>(genClass, arr[i].name, MemberAttributes.Static | MemberAttributes.Public, new CodePrimitiveExpression(path));//创建路径
//            }
//            GenTool.Gen(genUIPath, classUIPathName, genClass);
//            this.ShowNotification(new GUIContent("生成完毕"));
//        }

//        ///生成UI面板
//        genUIPanelPath = EditorGUILayout.TextField("生成面板的路径:", genUIPanelPath);
//        //  classUIPanelName = EditorGUILayout.TextField("ClassName:", classUIPanelName);
//        if (GUILayout.Button("生成面板脚本", GUILayout.Width(500)))
//        {
//            genSelectPanelData.selectPath.Clear();
//            SaveelectPanelData();
//            UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
//            for (int i = 0; i < arr.Length; i++)
//            {

//                string panelPath = AssetDatabase.GetAssetPath(arr[i]);
//                GameObject panel = AssetDatabase.LoadAssetAtPath(panelPath, typeof(GameObject)) as GameObject;
//                genSelectPanelData.selectPath.Add(panelPath);
//                if (panel != null)
//                {
//                    //获取子类组件

//                    CodeTypeDeclaration panelClass = GenTool.CreateCodeTypeDeclaration(panel.name, GenTool.GenType.Class, MemberAttributes.Public, typeof(BasePanel));

//                    CodeMemberMethod initGetCompoent = GenTool.ClassAddMethod(panelClass, "InitGetCompoent", MemberAttributes.Public);

//                    GenTool.ClassAddMethod(panelClass, "Init", MemberAttributes.Public | MemberAttributes.Override);

//                    GenTool.ClassAddMethod(panelClass, "Clear", MemberAttributes.Public | MemberAttributes.Override);

//                    for (int ci = 0; ci < panel.transform.childCount; ci++)
//                    {
//                        GameObject component = panel.transform.GetChild(ci).gameObject;
//                        if (component.name.StartsWith("FC"))//查找子类
//                        {
//                            CodeMethodReferenceExpression transformFind = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("transform"), "Find");//transform.Find
//                            CodeMethodInvokeExpression transformFindInvoke = new CodeMethodInvokeExpression(transformFind, new CodePrimitiveExpression(component.name));//方法添加参数

//                            CodeVariableDeclarationStatement temporaryVariable
//       = new CodeVariableDeclarationStatement(typeof(Transform), component.name.ToLower(), transformFindInvoke);

//                            initGetCompoent.Statements.Add(temporaryVariable);
//                            for (int a = 0; a < component.transform.childCount; a++)
//                            {
//                                GameObject cComponent = component.transform.GetChild(a).gameObject;
//                                //cPl.name = cPl.name.Substring(1);

//                                FinComponentType(cComponent, panelClass, initGetCompoent, component.name.ToLower());
//                            }
//                        }
//                        else if (component.name.StartsWith("F"))
//                        {
//                            FinComponentType(component, panelClass, initGetCompoent);

//                        }

//                    }
//                    GenTool.Gen(genUIPanelPath, panel.name, panelClass);//生成脚本

//                    //// 1.Load(命名空间名称)，GetType(命名空间.类名)
//                    //Type type = ClassName.GetType();
//                    //        //2.GetMethod(需要调用的方法名称)
//                    //        MethodInfo method = type.GetMethod(“MethodFunc” });
//                    //// 3.调用的实例化方法（非静态方法）需要创建类型的一个实例
//                    //object obj = Activator.CreateInstance(type);
//                    ////4.方法需要传入的参数
//                    //object[] parameters = new object[] { };
//                    //// 5.调用方法，如果调用的是一个静态方法，就不需要第3步（创建类型的实例）
//                    //// 相应地调用静态方法时，Invoke的第一个参数为null
//                    //method.Invoke(obj, null);
//                    //Type type = typen(panel.name + "Test");
//                    //Debug.Log(type == null);
//                    //if (panel.GetComponent(type) != null)
//                    //{
//                    //    // Component component = panel.GetComponent(type);
//                    //    DestroyImmediate(panel.GetComponent(type), true);
//                    //}
//                    //Component component1 = panel.gameObject.AddComponent(type);

//                    //MethodInfo method = component1.GetType().GetMethod("InitGetCompoent");
//                    //method.Invoke(component1, null);
//                }
//            }
//            SaveelectPanelData();
//            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
//        }
//        if (GUILayout.Button("关闭窗口", GUILayout.Width(200)))
//        {
//            //关闭窗口
//            this.Close();
//        }
//    }
//    [DidReloadScripts]
//    public static void OnCompileScripts()
//    {
//        UpdatePanel();
//        //    Debug.Log("Update");
//    }
//    public static void SaveelectPanelData()
//    {
//        string pathJson = EditorJsonUtility.ToJson(genSelectPanelData, true);
//        using (StreamWriter streamWriter = File.CreateText(Application.streamingAssetsPath + "/GenSelectPanelData.json"))
//        {
//            //表示生成C#代码
//            streamWriter.Write(pathJson);
//            streamWriter.Close();
//        }
//    }
//    public static void UpdatePanel()
//    {
//        string data = File.ReadAllText(Application.streamingAssetsPath + "/GenSelectPanelData.json");
//        GenSelectPanelData gData = JsonUtility.FromJson<GenSelectPanelData>(data);
//        for (int i = 0; i < gData.selectPath.Count; i++)
//        {
//            GameObject panel = AssetDatabase.LoadAssetAtPath<GameObject>(gData.selectPath[i]);
//            if (panel != null)
//            {
//                //ArchivePanelTest
//                Type type = typen(panel.name);
//                Debug.Log("面板名称" + panel.name + type == null);
//                if (panel.GetComponent(type) != null)
//                {
//                    DestroyImmediate(panel.GetComponent(type), true);
//                }
//                try
//                {
//                    Component component1 = panel.AddComponent(type);
//                    /////初始化组件
//                    MethodInfo method = component1.GetType().GetMethod("InitGetCompoent");
//                    method.Invoke(component1, null);
//                }
//                catch (Exception a)
//                {
//                    Debug.Log(a);
//                }
//                /////添加组件

//            }
//        }
//        genSelectPanelData.selectPath.Clear();
//        SaveelectPanelData();
//    }
//    public static Type typen(string typeName)
//    {
//        Type type = null;
//        Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
//        int assemblyArrayLength = assemblyArray.Length;
//        for (int i = 0; i < assemblyArrayLength; ++i)
//        {
//            type = assemblyArray[i].GetType(typeName);
//            if (type != null)
//            {
//                return type;
//            }
//        }

//        for (int i = 0; (i < assemblyArrayLength); ++i)
//        {
//            Type[] typeArray = assemblyArray[i].GetTypes();
//            int typeArrayLength = typeArray.Length;
//            for (int j = 0; j < typeArrayLength; ++j)
//            {
//                if (typeArray[j].Name.Equals(typeName))
//                {
//                    return typeArray[j];
//                }
//            }
//        }
//        return type;
//    }

//    public void FinComponentType(GameObject component, CodeTypeDeclaration panelClass, CodeMemberMethod initMethod, string fcName = "")
//    {
//        string cName = component.name.Replace("F", "");
//        if (component.GetComponent<UnityEngine.UI.Button>() != null)
//        {
//            AddField<Button>(component, panelClass, cName, initMethod, fcName);
//        }
//        else if (component.GetComponent<UnityEngine.UI.ScrollRect>() != null)
//        {
//            AddField<ScrollRect>(component, panelClass, cName, initMethod, fcName);
//            // ScrollRect scrollRect = component.GetComponent<UnityEngine.UI.ScrollRect>();

//            GenTool.ClassAddField<Transform>(panelClass, (cName + "Content"));

//            CodeMethodReferenceExpression getContent = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(cName), "content");//transform.Find

//            CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression(cName + "Content"), getContent);
//            initMethod.Statements.Add(codeAssignStatement);
//            //Transform conente = scrollRect.content;
//            //AddField<Transform>(component, panelClass, cName + "Conente", initMethod);
//        }
//        else if (component.GetComponent<UnityEngine.UI.InputField>() != null)
//        {
//            AddField<InputField>(component, panelClass, cName, initMethod, fcName);
//        }
//        else if (component.GetComponent<UnityEngine.UI.Slider>() != null)
//        {
//            AddField<Slider>(component, panelClass, cName, initMethod, fcName);
//        }
//        else if (component.GetComponent<UnityEngine.UI.Image>() != null)
//        {
//            AddField<Image>(component, panelClass, cName, initMethod, fcName);
//        }
//        else if (component.GetComponent<UnityEngine.UI.Text>() != null)
//        {
//            AddField<Text>(component, panelClass, cName, initMethod, fcName);
//        }
//        else if (component.GetComponent<UnityEngine.UI.Toggle>() != null)
//        {
//            AddField<Toggle>(component, panelClass, cName, initMethod, fcName);
//        }
//    }


//    public void AddField<T>(GameObject component, CodeTypeDeclaration panelClass, string fieldname, CodeMemberMethod initMethod, string fcName = "")
//    {
//        GenTool.ClassAddField<T>(panelClass, fieldname);
//        string finField = "transform";
//        if (fcName != "")
//        {
//            finField = fcName;
//        }
//        CodeMethodReferenceExpression transformFind = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(finField), "Find");//transform.Find

//        CodeMethodInvokeExpression transformFindInvoke = new CodeMethodInvokeExpression(transformFind, new CodePrimitiveExpression(component.name));//方法添加参数

//        CodeVariableDeclarationStatement temporaryVariable
//         = new CodeVariableDeclarationStatement(typeof(Transform), component.name.ToLower(), transformFindInvoke);
//        initMethod.Statements.Add(temporaryVariable);

//        //this.GetComponent
//        CodeMethodReferenceExpression thisGetComponent = new CodeMethodReferenceExpression(new CodeVariableReferenceExpression(component.name.ToLower()), "GetComponent");
//        //this.GetComponent<T>
//        thisGetComponent.TypeArguments.Add(typeof(T));//泛型
//                                                      //this.GetComponent<T>("")
//        CodeMethodInvokeExpression invokeThisGetComponent = new CodeMethodInvokeExpression(thisGetComponent);

//        //MyTestButton = this.GetComponent<UnityEngine.UI.Button>();
//        CodeAssignStatement thisGetComponentAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression(component.name.Replace("F", "")), invokeThisGetComponent);
//        initMethod.Statements.Add(thisGetComponentAssignStatement);
//    }
//}
//[Serializable]
//public class GenSelectPanelData
//{
//    public List<string> selectPath = new List<string>();
//}

