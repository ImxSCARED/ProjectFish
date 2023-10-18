using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(BehaviourTreeSO))]
public class BehaviourTreeInspector : Editor
{
    public VisualTreeAsset m_inspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new();

        // Load UXML tree
        m_inspectorXML.CloneTree(inspector);

        inspector.Query<Button>("OpenEditor").First().clicked += BehaviourTreeEditor.OpenWindow;

        return inspector;
    }
}
