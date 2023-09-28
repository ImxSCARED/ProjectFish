using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // EDITOR VARIABLES
    [SerializeField]
    PlayerInput m_playerInput;
    [SerializeField]
    MovementController m_playerController;

    // CODE VARIABLES
    InputAction m_forwardAction;
    InputAction m_yawAction;

    void Awake()
    {
        m_forwardAction = m_playerInput.actions["forward"];
        m_yawAction = m_playerInput.actions["yaw"];
    }

    void Update()
    {
        if (m_forwardAction.inProgress)
        {
            m_playerController.AddVelocity(m_forwardAction.ReadValue<float>());
        }

        if (m_yawAction.inProgress)
        {
            m_playerController.Turn(m_yawAction.ReadValue<float>());
        }
    }
}
