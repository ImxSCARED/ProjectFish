using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isDocked = false;

    
    public void Dock()
    {
        GetComponent<InputManager>().ChangeActionMap("UI");
        isDocked = true;

    }

    public void SellFish()
    {

    }
}
