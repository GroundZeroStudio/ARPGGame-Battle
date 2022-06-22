/****************************************************
    文件：GenerateResourceConfig.cs
    作者：Olivia
    日期：2022/2/7 22:25:6
    功能：Application.persostemtDataPath 路径可以在运行时进行读写操作
*****************************************************/
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateResourceConfig
{

    [MenuItem("Tools/Resources/Generate ResConfig File")]
    private static void Generate()
    {
        //读取Resource文件下所有预制体,文件名-文件路径
        string[] files = AssetDatabase.FindAssets("t:prefab", new[] { "Assets/Resources" });
        for (int i = 0; i < files.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(files[i]);
            string fileName = Path.GetFileNameWithoutExtension(assetPath);
            string filePath = assetPath.Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            files[i] = $"{fileName}={filePath}";
        }
        //生成配置文件
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllLines(Application.streamingAssetsPath + "/ConfigMap.txt", files);
        AssetDatabase.Refresh();
    }
}