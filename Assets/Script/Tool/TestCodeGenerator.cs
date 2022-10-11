using System.Collections;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.CodeDom;
using System.CodeDom.Compiler;
using System;
using UnityEngine.UI;

public class TestCodeGenerator
{
    //CodePrimitiveExpression 一般用于指定基础类型的常量值，如int、string、double等
    //CodeTypeOfExpression typeof1 = new CodeTypeOfExpression(typeRef1);
    //表示 typeof 表达式，该表达式返回指定类型名称的 Type。

    // Represents a string.
    // CodePrimitiveExpression stringPrimitive = new CodePrimitiveExpression("Test String");
    // Represents an integer.
    // CodePrimitiveExpression intPrimitive = new CodePrimitiveExpression(10);
    // Represents a floating point number.
    // CodePrimitiveExpression floatPrimitive = new CodePrimitiveExpression(1.03189);
    // Represents a null value expression.
    //  CodePrimitiveExpression nullPrimitive = new CodePrimitiveExpression(null)
    //  ;
    [MenuItem("Test/T")]
    public static void Init()
    {
        CodeTypeDeclaration testClass = new CodeTypeDeclaration("AaaA")
        {
            IsClass = true,//表示类
            Attributes = MemberAttributes.Public// public claas AA
        };
        testClass.BaseTypes.Add(typeof(BasePanel));//基础
        //CodeVariableReferenceExpression left = new CodeVariableReferenceExpression("a");//表示局部变量的引用。
        //CodeVariableReferenceExpression right = new CodeVariableReferenceExpression("b");
        //CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression();//表示一个表达式，该表达式包含在两个表达式间进行的二进制运算。
        //opt.Operator = CodeBinaryOperatorType.Add;//操作类型是加法的运算
        //opt.Left = left;
        //opt.Right = right;
        //(a + b)

        // CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(typeof(Button), "MyButton");
        //testClass.Members.Add(variable);
        CodeParameterDeclarationExpression bl = new CodeParameterDeclarationExpression(typeof(string), "value");
        CodeConstructor codeConstructor = new CodeConstructor();
        codeConstructor.Parameters.Add(bl);//构造函数

        CodeMemberMethod codeMemberMethod = new CodeMemberMethod()
        {
            Name = "MYTETS",
            Attributes = MemberAttributes.Public,
        };//创建一个方法
        CodeMemberField codeMemberField = new CodeMemberField(typeof(Button), "MyTestButton");
        codeMemberField.Attributes = MemberAttributes.Public;
        testClass.Members.Add(codeMemberField);//创建一个字段

        codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "name"));
        CodeMethodReferenceExpression codeMethodReferenceExpression = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetComponent");
        {

        };
        codeMethodReferenceExpression.TypeArguments.Add(typeof(Button));
        CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(codeMethodReferenceExpression);

        CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("MyTestButton"), invoke);
        codeMemberMethod.Statements.Add(codeAssignStatement);
        testClass.Members.Add(codeMemberMethod);//添加一个方法
        testClass.Members.Add(codeConstructor);

        CodeTypeDeclaration cl = GenTool.CreateCodeTypeDeclaration("TestClass2");
        cl.BaseTypes.Add(typeof(BasePanel));
        CodeMemberMethod init = GenTool.CreateMemberMethod("Init", MemberAttributes.Public | MemberAttributes.Override);
        CodeMethodInvokeExpression invoke1 = GenTool.MethodInvocation(new CodeMethodReferenceExpression() { TypeArguments = { typeof(Button) }, }, "GetComponent");
        //    GenClass.AddMembers(GenClass.CreateCodeAssignStatement(,));
        GenTool.ClassAddMethod(cl, "Init", MemberAttributes.Public | MemberAttributes.Override);

        CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
        using (StreamWriter streamWriter = File.CreateText(@"E:\GitProject\Dream\Assets\Script\00Common\" + "MyTestCode.cs"))
        {
            //表示生成C#代码
            provider.GenerateCodeFromType(testClass, streamWriter, null);
        }
        using (StreamWriter streamWriter = File.CreateText(@"E:\GitProject\Dream\Assets\Script\00Common\" + "TestClass2.cs"))
        {
            //表示生成C#代码
            provider.GenerateCodeFromType(cl, streamWriter, null);
        }
        AssetDatabase.Refresh();
    }
}
//public static class GenTool
//{
//    public enum GenType
//    {
//        Class, Interface, Struct, Enum,
//    }
//    /// <summary>
//    /// 创建一个类型
//    /// </summary>
//    /// <param name="className">类型名称</param>
//    /// <param name="genType">创建的类型</param>
//    /// <param name="Attributes">访问修饰符</param>
//    /// <param name="baseType">基础类型</param>
//    /// <returns></returns>
//    public static CodeTypeDeclaration CreateCodeTypeDeclaration(string className, GenType genType = GenType.Class, MemberAttributes Attributes = MemberAttributes.Public, System.Type baseType = null)
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
//    /// 添加类成员
//    /// </summary>
//    /// <param name="type"></param>
//    /// <param name=""></param>
//    public static void CodeTypeDeclarationAddMembers(CodeTypeDeclaration type, CodeTypeMember value)
//    {
//        type.Members.Add(value);
//    }

//    /// <summary>
//    /// 类添加一个方法
//    /// </summary>
//    public static void ClassAddMethod(CodeTypeDeclaration type, string methodName, MemberAttributes Attributes)
//    {
//        CodeMemberMethod method = CreateMemberMethod(methodName, Attributes);
//        type.Members.Add(method);
//    }

//    /// <summary>
//    /// 类添加一个字段
//    /// </summary>
//    public static void ClassAddField(CodeTypeDeclaration type, string fieldName, MemberAttributes Attributes = MemberAttributes.Public)
//    {
//        CodeMemberField codeMemberField = CreateFields<string>(fieldName, Attributes);
//        type.Members.Add(codeMemberField);
//    }
//    public static void ClassAddField(CodeTypeDeclaration type, string fieldName, MemberAttributes Attributes, CodeExpression value)
//    {
//        CodeMemberField codeMemberField = CreateFields<string>(fieldName, Attributes);
//        codeMemberField.Attributes = Attributes;
//        codeMemberField.InitExpression = value;
//        // CodeVariableDeclarationStatement decl = new CodeVariableDeclarationStatement(typeof(int), "n", new CodePrimitiveExpression(98000));
//        //CodeBinaryOperatorExpression opt = new CodeBinaryOperatorExpression(codeMemberField,CodeBinaryOperatorType.Assign,);
//        //CreateCodeAssignStatement(codeMemberField,value);
//        type.Members.Add(codeMemberField);
//    }

//    /// <summary>
//    /// 创建一个字段
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="type">字段类型</param>
//    /// <param name="fieldName">字段名</param>
//    /// <returns></returns>
//    public static CodeMemberField CreateFields<T>(string fieldName, MemberAttributes Attributes = MemberAttributes.Public)
//    {
//        CodeMemberField codeMemberField = new CodeMemberField(typeof(T), fieldName);//创建一个字段
//        codeMemberField.Attributes = Attributes;
//        return codeMemberField;
//    }





//    /// <summary>
//    /// 添加成员 
//    /// </summary>
//    /// <param name="codeTypeDeclaration"></param>
//    public static void AddMembers(CodeTypeDeclaration typeDeclaration, CodeTypeMember member)
//    {
//        typeDeclaration.Members.Add(member);
//    }

//    /// <summary>
//    /// 创建一个没有返回值的方法
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
//    /// 创建一个方法引用
//    /// </summary>
//    /// <returns></returns>
//    public static CodeMethodInvokeExpression MethodInvocation(CodeMethodReferenceExpression codeMethodReferenceExpression, string callname)
//    {
//        //    codeMethodReferenceExpression.TypeArguments.Add(typeof(Button));
//        CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(codeMethodReferenceExpression, callname);
//        return invoke;
//    }

//    /// <summary>
//    /// 创建赋值语句
//    /// </summary>
//    /// <returns></returns>
//    public static CodeAssignStatement CreateCodeAssignStatement(CodeExpression left, CodeExpression right)
//    {
//        CodeAssignStatement codeAssignStatemen = new CodeAssignStatement(left, right);
//        return codeAssignStatemen;
//    }
//    /// <summary>
//    /// 方法添加表达式
//    /// </summary>
//    /// <param name="codeMemberMethod"></param>
//    /// <param name="codeStatement">表达式</param>
//    public static void CodeMemberMethodStatementsAdd(CodeMemberMethod codeMemberMethod, CodeStatement codeStatement)
//    {
//        codeMemberMethod.Statements.Add(codeStatement);
//    }

//    public static void Gen(string path, string name, CodeTypeDeclaration codeTypeDeclaration)
//    {

//        using (StreamWriter streamWriter = File.CreateText(@"E:\GitProject\Dream\Assets\" + path + name + ".cs"))
//        {
//            //表示生成C#代码
//            CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");//生成cs代码
//            provider.GenerateCodeFromType(codeTypeDeclaration, streamWriter, null);
//        }
//        AssetDatabase.Refresh();
//    }
//}
