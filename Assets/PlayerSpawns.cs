using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawns : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        player.transform.position = startingPoints[players.Count - 1].position;
    }
}
