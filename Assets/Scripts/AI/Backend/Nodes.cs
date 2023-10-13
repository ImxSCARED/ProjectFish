using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public enum NodeStatus
{
    success,
    running,
    failure
}

// --NODE BASE--
public interface IBehaviourNode
{
    // For node editor
    public Rect WindowSize { get; set; }
    public string Name { get; set; }

    NodeStatus Tick();
}

public interface INodeWithChildren
{
    void AddChild(IBehaviourNode child);
    void RemoveChild(IBehaviourNode child);
}

// --CONTROL FLOW NODES--
public class RootNode : IBehaviourNode, INodeWithChildren
{
    List<IBehaviourNode> m_children;

    public Rect WindowSize { get; set; }
    public string Name { get; set; }

    public RootNode()
    {
        Name = "Root";
    }

    public virtual NodeStatus Tick()
    {
        foreach (IBehaviourNode child in m_children)
        {
            child.Tick();
        }

        return NodeStatus.success;
    }

    public void AddChild(IBehaviourNode child)
    {
        m_children.Add(child);
    }

    public void RemoveChild(IBehaviourNode child)
    {
        m_children.Remove(child);
    }
}

public class SelectorNode : IBehaviourNode, INodeWithChildren
{
    List<IBehaviourNode> m_children;

    public Rect WindowSize { get; set; }
    public string Name { get; set; }

    public SelectorNode(string name)
    {
        Name = name;
    }

    public virtual NodeStatus Tick()
    {
        foreach (IBehaviourNode child in m_children)
        {
            // Tick the child, then, if it didn't fail, jump out of the loop and return the result
            NodeStatus result = child.Tick();
            if (result != NodeStatus.failure) { return result; }
        }

        return NodeStatus.failure;
    }

    public void AddChild(IBehaviourNode child)
    {
        m_children.Add(child);
    }

    public void RemoveChild(IBehaviourNode child)
    {
        m_children.Remove(child);
    }
}

public class SequenceNode : IBehaviourNode, INodeWithChildren
{
    List<IBehaviourNode> m_children;

    public Rect WindowSize { get; set; }
    public string Name { get; set; }

    public SequenceNode(string name)
    {
        Name = name;
    }

    public virtual NodeStatus Tick()
    {
        foreach (IBehaviourNode child in m_children)
        {
            // Tick the child, then, if it didn't fail, jump out of the loop and return the result
            NodeStatus result = child.Tick();
            if (result != NodeStatus.success) { return result; }
        }

        return NodeStatus.success;
    }

    public void AddChild(IBehaviourNode child)
    {
        m_children.Add(child);
    }

    public void RemoveChild(IBehaviourNode child)
    {
        m_children.Remove(child);
    }
}

// --EXECUTION NODES--

public class ActionNode : IBehaviourNode
{
    Func<NodeStatus> m_action;

    public Rect WindowSize { get; set; }
    public string Name { get; set; }

    public ActionNode(string name)
    {
        m_action = null;
        Name = name;
    }

    public virtual NodeStatus Tick()
    {
        if (m_action != null)
        {
            return m_action();
        }
        return NodeStatus.failure;
    }
}

