using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [SerializeField]
    PlayerInput m_playerInput;
    [SerializeField]
    MovementController m_playerController;
    [SerializeField]
    Fishing m_playerFish;

    // --CODE VARIABLES--
    InputAction m_forwardAction;
    InputAction m_yawAction;
    InputAction m_fireAction;
    InputAction m_MMAction;

    // --UNITY METHODS--
    void Awake()
    {
        m_forwardAction = m_playerInput.actions["Forward"];
        m_yawAction = m_playerInput.actions["Yaw"];
        m_fireAction = m_playerInput.actions["Fire"];
        m_MMAction = m_playerInput.actions["MinigameMover"];
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

        if (m_fireAction.WasPressedThisFrame())
        {
            m_playerFish.FishMinigame();
        }

        if (m_MMAction.inProgress)
        {
            m_playerFish.MoveMM(m_MMAction.ReadValue<Vector2>());
        }
    }
}
