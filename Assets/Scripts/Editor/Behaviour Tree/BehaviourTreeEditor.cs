using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    List<Rect> m_windows = new List<Rect>();

    [MenuItem("Tools/BehaviourTree")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor editor = GetWindow<BehaviourTreeEditor>();
        editor.titleContent = new GUIContent("Behaviour Tree");

        editor.Init();
    }

    private void Init()
    {
        m_windows.Add(new Rect(10, 10, 100, 100));
        m_windows.Add(new Rect(200, 10, 354, 23));
        m_windows.Add(new Rect(10, 200, 144, 200));
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
