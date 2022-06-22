/****************************************************
    文件：WindowSetting.cs
    作者：Olivia
    日期：2022/3/10 22:6:46
    功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowSetting : EditorWindow
{
    List<MacorItem> macorItems = new List<MacorItem>();

    public WindowSetting()
    {
        macorItems.Clear();

    }

    private void OnGUI()
    {
        //标题
        GUILayout.Label("全局设置");
    }

    public class MacorItem
    {
        public string Name;
        public string DisplayName;
        public string IsDebug;
        public string IsRelease;
    }
}

