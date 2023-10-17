using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CreateAssetMenu(fileName = "Behaviour Tree", menuName = "BehaviourTree/BehaviourTreeScriptableObject", order = 1)]
public class BehaviourTreeScriptableObject : ScriptableObject
{
    public RootNode m_root;

    // Runs when object is created
    private void Awake()
    {
        m_root = new();
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        string assetPath = AssetDatabase.GetAssetPath(instanceID);
        bool isBTAsset = AssetDatabase.LoadAssetAtPath<BehaviourTreeScriptableObject>(assetPath) != null;

        if (isBTAsset)
        {
            BehaviourTreeEditor.OpenWindow();
            return true;
        }

        return false;
    }
}
