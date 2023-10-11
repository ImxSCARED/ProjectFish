using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerCongifuration> playerConfigs;

    private float allReadyTimer = 3;

    public static PlayerConfigurationManager instance { get; private set; }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerCongifuration>();
        }
    }

    public void SetPlayerShip(int index, GameObject ship)
    {
        playerConfigs[index].PlayerShip = ship;
    }
    public void SetPlayerInvert(int index)
    {
        playerConfigs[index].InvertControls = !playerConfigs[index].InvertControls;
    }
    public void SetPlayerSensitivity(int index, float amount)
    {
        playerConfigs[index].Sensitivity = amount;
    }
    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = !playerConfigs[index].IsReady;
        
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined: " + pi.playerIndex);
        DontDestroyOnLoad(pi.gameObject);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerCongifuration(pi));
        }
    }

    private void Update()
    {
        if (playerConfigs.Count > 1 && playerConfigs.All(p => p.IsReady == true))
        {
            allReadyTimer -= Time.deltaTime;
            if (allReadyTimer < 0)
            {
                SceneManager.LoadScene("SampleScene");
                playerConfigs.All(p => p.IsReady = false); // Set ready to false so we only load the scene once
            }    
        }
        else
            allReadyTimer = 3f;
    }

    public void GameStart()
    {
        foreach(var player in playerConfigs)
        {
            player.Input.camera.gameObject.SetActive(true);
            player.Input.SwitchCurrentActionMap("Player");
        }
    }
}

public class PlayerCongifuration
{
    //Back stuff for player management
    public PlayerCongifuration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }

    //Settings they chose when making player
    public GameObject PlayerShip { get; set; }
    public bool InvertControls { get; set; }
    public float Sensitivity { get; set; }
    public bool IsReady { get; set; }
    
    
}