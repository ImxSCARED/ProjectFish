using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerCongifuration playerConfig;
    private Cannon cannon;

    private GameActions controls;
    [SerializeField] 
    private void Awake()
    {
        cannon = GetComponent<Cannon>();
        controls = new GameActions();
    }

    //public void initializePlayer(PlayerCongifuration pc)
    //{
    //    playerConfig = pc;
    //    playerConfig.Input = ;
    //}

    //private void Input_onActionTriggered1(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    if(obj.action.name == controls.Pla)
    //}
    void Update()
    {
        
    }
}
