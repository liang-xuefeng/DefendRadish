using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    [HideInInspector]
    public Map Map = null;

    /// <summary> 所有关卡列表 </summary>
    private List<FileInfo> _fileInfos = new List<FileInfo>();
    /// <summary> 要编辑的当前关卡索引 </summary>
    private int _selectIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            //存储要编辑的类型
            Map = target as Map;

            //绘制下拉菜单
            EditorGUILayout.BeginHorizontal();
            int currentIndex = EditorGUILayout.Popup(_selectIndex, GetFileInfoName(_fileInfos));
            if (currentIndex != _selectIndex)
            {
                _selectIndex = currentIndex;
                LoadLevel();
            }

            if (GUILayout.Button("读取列表"))
                LoadLevelFile();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("清除塔点"))
                Map.ClearHolder();
            if (GUILayout.Button("清除路径"))
                Map.ClearRoad();
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("保存数据"))
                SaveLevel();
        }

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }

    /// <summary>
    /// 获取所有关卡的配置文件名字
    /// </summary>
    /// <param name="fileInfos"></param>
    /// <returns></returns>
    private string[] GetFileInfoName(List<FileInfo> fileInfos)
    {
        List<string> names = new List<string>();
        foreach (FileInfo info in fileInfos)
            names.Add(info.Name);

        return names.ToArray();
    }

    /// <summary>
    /// 加载关卡
    /// </summary>
    private void LoadLevel()
    {
        FileInfo fileInfo = _fileInfos[_selectIndex];

        Level level = new Level();
        Tools.FillLevel(fileInfo.FullName, ref level);
        Map.LoadLevel(level);
    }

    /// <summary>
    /// 加载单个关卡文件
    /// </summary>
    private void LoadLevelFile()
    {
        Clear();
        _fileInfos = Tools.GetLevelFileInfos();
        if (_fileInfos.Count > 0)
        {
            _selectIndex = 0;
            LoadLevel();
        }
    }

    /// <summary>
    /// 保存关卡信息文件
    /// </summary>
    private void SaveLevel()
    {
        Level level = Map.Level;

        List<Point> points = new List<Point>();
        //收集放塔点
        foreach (Tile tile in Map.Grid)
        {
            if (tile.CanHold)
            {
                Point point = new Point(tile.X, tile.Y);
                points.Add(point);
            }
        }
        level.Holders = points;

        points = new List<Point>();
        //收集路径点
        foreach (Tile tile in Map.Road)
        {
            Point point = new Point(tile.X, tile.Y);
            points.Add(point);
        }
        level.Path = points;

        //保存
        string fileName = _fileInfos[_selectIndex].FullName;
        Tools.SaveLevel(fileName, level);

        EditorUtility.DisplayDialog("数据保存", "保存成功", "确定");
    }

    /// <summary>
    /// 清理状态
    /// </summary>
    private void Clear()
    {
        _fileInfos.Clear();
        _selectIndex = -0;
    }
}
