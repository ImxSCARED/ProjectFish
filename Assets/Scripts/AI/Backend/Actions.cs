using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actions are small, clearly defined functions that can be run by an action node, and return some kind of status
/// </summary>
public abstract class Action
{
    /// <summary>
    /// Wrapper for ActionBehaviour. Ensures that agent is not null.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    public NodeStatus DoAction(Agent agent, out string errorMessageOut)
    {
        if (agent != null)
        {
            return ActionBehaviour(agent, out errorMessageOut);
        }

        errorMessageOut = "Agent is null";
        return NodeStatus.error;
    }

    /// <summary>
    /// Action to be performed. Returns an error if not implemented.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    protected virtual NodeStatus ActionBehaviour(Agent agent, out string errorMessageOut)
    {
        errorMessageOut = ToString() + " has no defined action.";
        return NodeStatus.error;
    }
}

/// <summary>
/// Just for testing. Remove later when actual actions exist.
/// </summary>
public class TemplateAction : Action
{
    /// <summary>
    /// Prints the name of the agent's parent object, then returns success.
    /// </summary>
    /// <param name="agent">Reference to the agent that this behaviour tree is connected to</param>
    /// <param name="errorMessageOut">Contains error message. If there were no errors, returns null</param>
    /// <returns></returns>
    protected override NodeStatus ActionBehaviour(Agent agent, out string errorMessageOut)
    {
        errorMessageOut = null;

        Debug.Log(agent.name);     
        return NodeStatus.success;
    }
}