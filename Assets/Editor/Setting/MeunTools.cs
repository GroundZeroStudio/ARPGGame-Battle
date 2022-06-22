/****************************************************
    文件：MeunTools.cs
    作者：Olivia
    日期：2022/3/10 22:6:24
    功能：Nothing
*****************************************************/
using System;
using UnityEditor;

public class MeunTools
{
    [MenuItem("Tool/Setting")]
    static void OpenSettingWindow()
    {
        WindowSetting windowSetting = EditorWindow.GetWindow<WindowSetting>("全局设置");
        windowSetting.Show();
    }


}