using System.Collections;
using System.Collections.Generic;
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

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        BehaviourTreeEditor.OpenWindow();
        return false;
    }
}
