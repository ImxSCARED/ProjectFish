using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeWindow
{
    IBehaviourNode m_node;

    Rect m_window;

    public NodeWindow(IBehaviourNode m_node, Rect window)
    {
        m_window = window;
    }

    public void Rename(string name)
    {
        m_node.Name = name;
    }

    public void Move(Vector2 newPos)
    {
        m_window.x = newPos.x;
        m_window.y = newPos.y;
    }
}

public class BehaviourTreeEditor : EditorWindow
{
    List<Rect> m_windows = new List<Rect>();

    [MenuItem("Tools/BehaviourTree")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor editor = GetWindow<BehaviourTreeEditor>();
        editor.titleContent = new GUIContent("Behaviour Tree");

        //editor.Init();
    }

    public static void OpenWindow(string treePath)
    {
        BehaviourTreeEditor editor = GetWindow<BehaviourTreeEditor>();
        editor.titleContent = new GUIContent("Behaviour Tree");

        editor.LoadTree(treePath);
    }

    private void LoadTree(string treePath)
    {
        BehaviourTreeSO treeObject = AssetDatabase.LoadAssetAtPath<BehaviourTreeSO>(treePath);

        
    }

    public void CreateGUI()
    {
    }

    void OnGUI()
    {
        BeginWindows();
        for (int i = 0; i < m_windows.Count; i++)
        {
            m_windows[i] = GUI.Window(i, m_windows[i], DrawNodeWindow, "Window " + i);
        }
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        GUI.DragWindow();
    }
}
