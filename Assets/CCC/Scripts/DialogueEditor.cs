using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueEditor : MonoBehaviour
{
    [MenuItem("Tools/CCC/DialogueEditor")]
    public void OpenBlankWindow()
    {
        // 创建一个空白的EditorWindow实例
        EditorWindow.GetWindow<DialogueEditorWindow>("Blank Window");
    }
}

// 定义一个继承自EditorWindow的类，用于创建空白窗体
public class DialogueEditorWindow : EditorWindow
{
    // 窗体GUI
    void OnGUI()
    {



    }
}

/// <summary>
/// 对话框数据
/// </summary>
[System.Serializable]

public enum DialogueType
{
    BasicDia,
    TODODia
}
public class BasicDia
{
    public Sprite SpeakerImage;
    public Color SpeakerColor;
    public int RankCount;
    public int StarsCount;
    public GameObject mytargetBadge;
}
