#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;


public class SoundEnumCreater : MonoBehaviour
{

    private const string DirectoryPath = "Assets/AutoCreate";
    private const string SEEnumPath = "/SETypeEnums.cs";
    private const string BGMEnumPath = "/BGMTypeEnums.cs";
    private const string JingleEnumPath = "/JingleTypeEnums.cs";

    [MenuItem("Tools/CreateEnum/SESounds")]
    public static void CreateSEEnum()
    {
        var scriptableSE = Resources.Load("SEData") as ScriptableSEDatas;
        List<SEData> seDatas;

        if (scriptableSE == null)//存在しない場合空のリストにする
            seDatas = new List<SEData>();
        else
            seDatas = scriptableSE.seDatas;

        var sb = new StringBuilder();
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// スクリプトから生成されたEnumです");
        sb.AppendLine("/// 再生成する場合、画面上のtool->CreateEnum->SESounds から再生成してください");
        sb.AppendLine("/// 使わないからと言って消さないこと！コンパイルエラーになります！");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("public enum SETypeEnum");
        sb.AppendLine("{");

        foreach (var seData in seDatas)
        {
            sb.AppendLine($"    {seData.seName}　,");
        }

        sb.AppendLine("}");

        if (Directory.Exists(DirectoryPath) == false)
            AssetDatabase.CreateFolder("Assets","AutoCreate");
        File.WriteAllText(DirectoryPath+SEEnumPath, sb.ToString());

        AssetDatabase.Refresh();
        Debug.Log($"SEのEnumを{DirectoryPath+SEEnumPath}に作成しました");
    }

    [MenuItem("Tools/CreateEnum/BGMSounds")]
    public static void CreateBGMEnum()
    {
        var scriptableBGM = Resources.Load("BGMData") as ScriptableBGMDatas;
        List<BGMData> bgmDatas;

        if (scriptableBGM == null)//存在しない場合空のリストにする
            bgmDatas = new List<BGMData>();
        else
            bgmDatas = scriptableBGM.bgmDatas;

        //StringBuilderで複数行を作れる
        var sb = new StringBuilder();
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// スクリプトから生成されたEnumです");
        sb.AppendLine("/// 再生成する場合、画面上のtool->CreateEnum->BGMSounds から再生成してください");
        sb.AppendLine("/// 使わないからと言って消さないこと！コンパイルエラーになります！");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("public enum BGMTypeEnum");
        sb.AppendLine("{");

        foreach (var bgmData in bgmDatas)
        {
            sb.AppendLine($"    {bgmData.bgmName} ,");
        }

        sb.AppendLine("}");

        //ディレクトリがない場合は作る
        if (Directory.Exists(DirectoryPath) == false)
            AssetDatabase.CreateFolder("Assets","AutoCreate");
        File.WriteAllText(DirectoryPath+BGMEnumPath, sb.ToString());

        //これでUnityを再読み込みさせる
        AssetDatabase.Refresh();
        Debug.Log($"BGMのEnumを{DirectoryPath+BGMEnumPath}に作成しました");
    }

    [MenuItem("Tools/CreateEnum/JingleSounds")]
    public static void CreateJingleEnum()
    {
        var scriptableJingle = Resources.Load("JingleData") as ScriptableJingleDatas;
        List<JingleData> jingleDatas;

        if (scriptableJingle == null)//存在しない場合空のリストにする
            jingleDatas = new List<JingleData>();
        else
            jingleDatas = scriptableJingle.jingleDatas;

        //StringBuilderで複数行を作れる
        var sb = new StringBuilder();
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// スクリプトから生成されたEnumです");
        sb.AppendLine("/// 再生成する場合、画面上のtool->CreateEnum->JingleSounds から再生成してください");
        sb.AppendLine("/// 使わないからと言って消さないこと！コンパイルエラーになります！");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("public enum JingleTypeEnum");
        sb.AppendLine("{");

        foreach (var jingleData in jingleDatas)
        {
            sb.AppendLine($"    {jingleData.jingleName} ,");
        }

        sb.AppendLine("}");

        //ディレクトリがない場合は作る
        if (Directory.Exists(DirectoryPath) == false)
            AssetDatabase.CreateFolder("Assets","AutoCreate");
        File.WriteAllText(DirectoryPath+JingleEnumPath, sb.ToString());

        //これでUnityを再読み込みさせる
        AssetDatabase.Refresh();
        Debug.Log($"JingleのEnumを{DirectoryPath+JingleEnumPath}に作成しました");
    }
}
#endif