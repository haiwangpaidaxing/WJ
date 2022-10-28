//using System.CodeDom;
//using System.CodeDom.Compiler;
//using System.IO;
//using UnityEditor;
//using UnityEngine;

//public static class GenTool
//{
//    public enum GenType
//    {
//        Class, Interface, Struct, Enum,
//    }
//    /// <summary>
//    /// ����һ������
//    /// </summary>
//    /// <param name="className">��������</param>
//    /// <param name="genType">����������</param>
//    /// <param name="Attributes">�������η�</param>
//    /// <param name="baseType">��������</param>
//    /// <returns></returns>
//    public static System.CodeDom.CodeTypeDeclaration CreateCodeTypeDeclaration(string className, GenType genType = GenType.Class, System.CodeDom.MemberAttributes Attributes = System.CodeDom.MemberAttributes.Public, System.Type baseType = null)
//    {
//        CodeTypeDeclaration createTtyep = new CodeTypeDeclaration(className)
//        {
//            Attributes = MemberAttributes.Public// public claas AA
//        };
//        if (baseType != null)
//        {
//            createTtyep.BaseTypes.Add(baseType);
//        }
//        switch (genType)
//        {
//            case GenType.Class:
//                createTtyep.IsClass = true;
//                break;
//            case GenType.Interface:
//                createTtyep.IsInterface = true;
//                break;
//            case GenType.Struct:
//                createTtyep.IsStruct = true;
//                break;
//            case GenType.Enum:
//                createTtyep.IsEnum = true;
//                break;
//        }
//        return createTtyep;

//    }
//    /// <summary>
//    /// ������Ա
//    /// </summary>
//    /// <param name="type"></param>
//    /// <param name=""></param>
//    public static void CodeTypeDeclarationAddMembers(CodeTypeDeclaration type, CodeTypeMember value)
//    {
//        type.Members.Add(value);
//    }

//    /// <summary>
//    /// �����һ������
//    /// </summary>
//    public static CodeMemberMethod ClassAddMethod(CodeTypeDeclaration type, string methodName, MemberAttributes Attributes)
//    {
//        CodeMemberMethod method = CreateMemberMethod(methodName, Attributes);
//        type.Members.Add(method);
//        return method;
//    }

//    /// <summary>
//    /// �����һ���ֶ�
//    /// </summary>
//    public static void ClassAddField<T>(CodeTypeDeclaration type, string fieldName, MemberAttributes Attributes = MemberAttributes.Public)
//    {
//        CodeMemberField codeMemberField = CreateFields<T>(fieldName, Attributes);
//        type.Members.Add(codeMemberField);
//    }
//    public static void ClassAddField<T>(CodeTypeDeclaration type, string fieldName, MemberAttributes Attributes, CodeExpression value)
//    {
//        CodeMemberField codeMemberField = CreateFields<T>(fieldName, Attributes);
//        codeMemberField.Attributes = Attributes;
//        codeMemberField.InitExpression = value;
//        // CodeVariableDeclarationStatement decl = new CodeVariableDeclarationStatement(typeof(int), "n", new CodePrimitiveExpression(98000));
//        //CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression(codeMemberField,CodeBinaryOperatorType.Assign,);
//        //CreateCodeAssignStatement(codeMemberField,value);
//        type.Members.Add(codeMemberField);
//    }

//    /// <summary>
//    /// ����һ���ֶ�
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="type">�ֶ�����</param>
//    /// <param name="fieldName">�ֶ���</param>
//    /// <returns></returns>
//    public static CodeMemberField CreateFields<T>(string fieldName, MemberAttributes Attributes = MemberAttributes.Public)
//    {
//        CodeMemberField codeMemberField = new CodeMemberField(typeof(T), fieldName);//����һ���ֶ�
//        codeMemberField.Attributes = Attributes;
//        return codeMemberField;
//    }





//    /// <summary>
//    /// ��ӳ�Ա 
//    /// </summary>
//    /// <param name="codeTypeDeclaration"></param>
//    public static void AddMembers(CodeTypeDeclaration typeDeclaration, CodeTypeMember member)
//    {
//        typeDeclaration.Members.Add(member);
//    }

//    /// <summary>
//    /// ����һ��û�з���ֵ�ķ���
//    /// </summary>
//    /// <returns></returns>
//    public static CodeMemberMethod CreateMemberMethod(string methodName, MemberAttributes Attributes = MemberAttributes.Public)
//    {
//        CodeMemberMethod codeMemberMethod = new CodeMemberMethod()
//        {
//            Name = methodName,
//            Attributes = Attributes,
//        };
//        CodeMethodReferenceExpression codeMethodReferenceExpression = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetComponent");
//        {

//        };
//        return codeMemberMethod;
//    }

//    /// <summary>
//    /// ����һ����������
//    /// </summary>
//    /// <returns></returns>
//    public static CodeMethodInvokeExpression MethodInvocation(CodeMethodReferenceExpression codeMethodReferenceExpression, string callname)
//    {
//        //    codeMethodReferenceExpression.TypeArguments.Add(typeof(Button));
//        CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(codeMethodReferenceExpression, callname);
//        return invoke;
//    }

//    /// <summary>
//    /// ������ֵ���
//    /// </summary>
//    /// <returns></returns>
//    public static CodeAssignStatement CreateCodeAssignStatement(CodeExpression left, CodeExpression right)
//    {
//        CodeAssignStatement codeAssignStatemen = new CodeAssignStatement(left, right);
//        return codeAssignStatemen;
//    }
//    /// <summary>
//    /// ������ӱ��ʽ
//    /// </summary>
//    /// <param name="codeMemberMethod"></param>
//    /// <param name="codeStatement">���ʽ</param>
//    public static void CodeMemberMethodStatementsAdd(CodeMemberMethod codeMemberMethod, CodeStatement codeStatement)
//    {
//        codeMemberMethod.Statements.Add(codeStatement);
//    }

//    public static void Gen(string path, string name, CodeTypeDeclaration codeTypeDeclaration)
//    {
//        //using (StreamWriter sw = File.AppendText(path + name + ".cs"))
//        //{
//        //    CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");//����cs����
//        //    provider.GenerateCodeFromType(codeTypeDeclaration, sw, null);
//        //}
//        using (StreamWriter streamWriter = File.CreateText(path + name + ".cs"))
//        {
//            //��ʾ����C#����
//            CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");//����cs����
//            provider.GenerateCodeFromType(codeTypeDeclaration, streamWriter, null);
//        }
//        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
//    }
//}


/////// <summary>
/////// �и�
/////// </summary>
////public static class ImageSlicer
////{
////    [MenuItem("Assets/ImageSlicer/Process to Sprites")]
////    static void ProcessToSprite()
////    {
////        Texture2D image = Selection.activeObject as Texture2D;//��ȡ��ת�Ķ���
////        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//��ȡ·������
////        string path = rootPath + "/" + image.name + ".PNG";//ͼƬ·������


////        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//��ȡͼƬ���


////        AssetDatabase.CreateFolder(rootPath, image.name);//�����ļ���


////        foreach (SpriteMetaData metaData in texImp.spritesheet)//����Сͼ��
////        {
////            Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

////            //abc_0:(x:2.00, y:400.00, width:103.00, height:112.00)
////            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y������
////            {
////                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
////                    myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
////            }


////            //ת������EncodeToPNG���ݸ�ʽ
////            if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
////            {
////                Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
////                newTexture.SetPixels(myimage.GetPixels(0), 0);
////                myimage = newTexture;
////            }
////            var pngData = myimage.EncodeToPNG();


////            //AssetDatabase.CreateAsset(myimage, rootPath + "/" + image.name + "/" + metaData.name + ".PNG");
////            File.WriteAllBytes(rootPath + "/" + image.name + "/" + metaData.name + ".PNG", pngData);
////            // ˢ����Դ���ڽ���
////            AssetDatabase.Refresh();
////        }
////    }
////}