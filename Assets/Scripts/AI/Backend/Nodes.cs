using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

/// <summary>
/// Success: The node or its child(ren) have succeeded in their function.
/// <br/>
/// Running: The node or its child(ren) are performing some ongoing action.
/// <br/>
/// Failure: The node or its child(ren) have failed in their function.
/// <br/>
/// Error: The node or its child(ren) have performed an illegal action or were improperly configured.
/// </summary>
public enum NodeStatus
{
    success,
    running,
    failure,
    error
}

// --NODE BASE--
/// <summary>
/// Describes the methods required for any behaviour node.
/// </summary>
public interface IBehaviourNode
{
    public string Name { get; set; }

    /// <summary>
    /// Performs whatever function is required for a node to function properly.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    NodeStatus Tick(Agent agent, out string errorMessageOut);
}

/// <summary>
/// Describes the methods required for a control flow node.
/// </summary>
public interface IControlFlowNode : IBehaviourNode
{
    /// <summary>
    /// Adds a node to this control flow node's list of children.
    /// </summary>
    /// <param name="child">Node to add</param>
    void AddChild(IBehaviourNode child);
    /// <summary>
    /// Removes a node from this control flow node's list of children.
    /// </summary>
    /// <param name="child">Node to add</param>
    void RemoveChild(IBehaviourNode child);
}

/// <summary>
/// Describes the methods required for a prefix node.
/// </summary>
public interface IPrefixNode : IBehaviourNode
{
    /// <summary>
    /// Sets this prefix node's child to the provided node.
    /// </summary>
    /// <param name="child">Node to set</param>
    void SetChild(IBehaviourNode child);
    /// <summary>
    /// Sets this prefix node's child to null.
    /// </summary>
    void RemoveChild();
}

// --CONTROL FLOW NODES--
/// <summary>
/// The beginning of a behaviour tree or subtree. Runs through all children regardless of their result.
/// </summary>
public class RootNode : IControlFlowNode
{
    List<IBehaviourNode> m_children;

    public string Name { get; set; }

    public RootNode()
    {
        Name = "Root";
    }

    /// <summary>
    /// Tick all children, then return success.
    /// <br/>
    /// If there are no children, return an error.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public virtual NodeStatus Tick(Agent agent, out string errorMessageOut)
    {
        NodeStatus status = NodeStatus.success;
        errorMessageOut = null;

        if (m_children.Count == 0)
        {
            status = NodeStatus.error;
            errorMessageOut = Name + ": Root has no children.";
        }

        foreach (IBehaviourNode child in m_children)
        {
            child.Tick(agent, out errorMessageOut);
        }

        return status;
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

/// <summary>
/// Runs through each node, "selecting" the first node that succeeds and ignoring all following nodes.
/// <br/>
/// If no node succeeds, this node fails.
/// </summary>
public class SelectorNode : IControlFlowNode
{
    List<IBehaviourNode> m_children;

    public string Name { get; set; }

    public SelectorNode(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Tick through children until a success or running is encountered, then return that status. Otherwise, return failure.
    /// <br/>
    /// If there are no children, return an error.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public virtual NodeStatus Tick(Agent agent, out string errorMessageOut)
    {
        NodeStatus status = NodeStatus.failure;
        errorMessageOut = null;

        if (m_children.Count == 0)
        {
            status = NodeStatus.error;
            errorMessageOut = Name + ": Selector node has no children.";
        }

        foreach (IBehaviourNode child in m_children)
        {
            // Tick the child, then, if it didn't fail, jump out of the loop and return the result
            NodeStatus result = child.Tick(agent, out errorMessageOut);
            if (result != NodeStatus.failure) { return result; }
        }

        return status;
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

/// <summary>
/// Runs through each node until one fails. This breaks the "sequence", so this node will fail as well.
/// <br/>
/// If no node fails, this node succeeds.
/// </summary>
public class SequenceNode : IControlFlowNode
{
    List<IBehaviourNode> m_children;

    public string Name { get; set; }

    public SequenceNode(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Tick through children until a failure or running is encountered, then return that status. Otherwise, return success.
    /// <br/>
    /// If there are no children, return an error.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public virtual NodeStatus Tick(Agent agent, out string errorMessageOut)
    {
        NodeStatus status = NodeStatus.success;
        errorMessageOut = null;

        if (m_children.Count == 0)
        {
            status = NodeStatus.error;
            errorMessageOut = Name + ": Sequence node has no children.";
        }

        foreach (IBehaviourNode child in m_children)
        {
            // Tick the child, then, if it didn't fail, jump out of the loop and return the result
            NodeStatus result = child.Tick(agent, out errorMessageOut);
            if (result != NodeStatus.success) { return result; }
        }

        return status;
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

// --PREFIX NODES--

/// <summary>
/// Negates the result of its child node, provided that result makes sense to negate.
/// </summary>
public class NegationNode : IPrefixNode
{
    IBehaviourNode m_child;

    public string Name { get; set; }

    public NegationNode(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Tick child, then return the opposite of that child's status. Returns as normal if status is not success or failure.
    /// <br/>
    /// If there is no child, return an error.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public virtual NodeStatus Tick(Agent agent, out string errorMessageOut)
    {
        errorMessageOut = null;

        if (m_child != null)
        {
            NodeStatus result = m_child.Tick(agent, out errorMessageOut);

            // Return the opposite of result
            if (result == NodeStatus.success) { return NodeStatus.failure; }
            if (result == NodeStatus.failure) { return NodeStatus.success; }

            // If we didn't get a success or failure, return whatever we did get
            return result;
        }
        else
        {
            errorMessageOut = Name + ": Negation node has no child.";
            return NodeStatus.error;
        }
    }

    public void SetChild(IBehaviourNode child)
    {
        m_child = child;
    }

    public void RemoveChild()
    {
        m_child = null;
    }
}

// --EXECUTION NODES--

/// <summary>
/// Performs some provided action, defined in Actions.cs.
/// </summary>
public class ActionNode : IBehaviourNode
{
    Action m_action;

    public string Name { get; set; }

    public ActionNode(string name)
    {
        m_action = null;
        Name = name;
    }

    /// <summary>
    /// Performs an action, then returns the result of that action as a NodeStatus.
    /// <br/>
    /// If there is no assigned action, return an error.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public virtual NodeStatus Tick(Agent agent, out string errorMessageOut)
    {
        if (m_action != null)
        {
            NodeStatus result = m_action.DoAction(agent, out errorMessageOut);

            errorMessageOut = Name + ": " + errorMessageOut;
            return result;
        }
        else
        {
            errorMessageOut = Name + ": Action node has no assigned action.";
            return NodeStatus.error;
        }
    }
}