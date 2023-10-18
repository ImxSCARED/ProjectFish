using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [SerializeField]
    PlayerInput m_playerInput;
    [SerializeField]
    MovementController m_playerController;
    [SerializeField]
    Firing m_playerFire;
    // --CODE VARIABLES--
    InputAction m_forwardAction;
    InputAction m_yawAction;
    InputAction m_dashAction;
    InputAction m_fireAction;

    // --UNITY METHODS--
    void Awake()
    {
        m_forwardAction = m_playerInput.actions["Forward"];
        m_yawAction = m_playerInput.actions["Yaw"];
        m_dashAction = m_playerInput.actions["Dash"];
        m_fireAction = m_playerInput.actions["Fire"];

        m_dashAction.performed += Dash;
    }

    void FixedUpdate()
    {
        if (m_forwardAction.inProgress)
        {
            m_playerController.AddVelocity(m_forwardAction.ReadValue<float>());
        }

        if (m_yawAction.inProgress)
        {
            m_playerController.Turn(m_yawAction.ReadValue<float>());
        }

        if (m_fireAction.WasPressedThisFrame())
        {
            Debug.Log("Huh");
            m_playerFire.FireCannons();
        }
    }

    void Dash(CallbackContext context)
    {
        m_playerController.Dash();
    }
}
