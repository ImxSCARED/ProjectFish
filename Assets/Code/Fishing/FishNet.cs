using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNet : MonoBehaviour
{
    public Fishing m_fishing;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fish")
        {
            if (m_fishing.fishing)
            {
                m_fishing.FishCaught();
            }
        }
    }
}
