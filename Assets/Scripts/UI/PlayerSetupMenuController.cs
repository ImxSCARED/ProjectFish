using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField] private TextMeshProUGUI playerNumberText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject invertCheck;
    [SerializeField] private Slider sensitivity;
    [SerializeField] private GameObject readyCheck;
    

    private float ingoreInputTime = 1f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi) 
    { 
        playerIndex = pi;
        playerNumberText.SetText("P" + (playerIndex + 1).ToString());
        ingoreInputTime = Time.time + ingoreInputTime;
    }



    // Update is called once per frame
    void Update()
    {
        if(Time.time > ingoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetShip(GameObject ship)
    {
        if(!inputEnabled) { return; }

        PlayerConfigurationManager.instance.SetPlayerShip(playerIndex, ship);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.instance.ReadyPlayer(playerIndex);
        readyCheck.SetActive(!readyCheck.activeSelf);
    }
    public void SetInvert()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.instance.SetPlayerInvert(playerIndex);
        invertCheck.SetActive(!invertCheck.activeSelf);
    }
    public void SetSensitivity()
    {
        Debug.Log(sensitivity.value);
        PlayerConfigurationManager.instance.SetPlayerSensitivity(playerIndex, sensitivity.value + 0.01f);
    }
}
