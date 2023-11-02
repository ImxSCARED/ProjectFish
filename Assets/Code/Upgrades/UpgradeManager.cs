using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Fishing m_fishing;
    private MovementController m_movementController;

    [SerializeField] private Upgrade[] m_upgrades;
    [SerializeField] private UpgradeData m_upgradeData;

    void Start()
    {
        m_fishing = GetComponent<Fishing>();
        m_movementController = GetComponent<MovementController>();
    }

    public void ImplementUpgrade(Upgrade UpgradeToAdd)
    {
        switch (UpgradeToAdd.Type)
        {
            case UpgradeData.UpgradeType.AMMO:
                m_fishing.ammoUpgrade = m_upgradeData.AmmoIncreaseAmount * UpgradeToAdd.Level;
                break;

            case UpgradeData.UpgradeType.SPEED:

                break;

            case UpgradeData.UpgradeType.TURN:

                break;

            case UpgradeData.UpgradeType.WRANGLE:
                m_fishing.wrangleUpgrade = m_upgradeData.WrangleIncreaseAmount * UpgradeToAdd.Level;
                break;

            case UpgradeData.UpgradeType.RANGE:
                m_fishing.rangeUpgrade = m_upgradeData.RangeIncreaseAmount * UpgradeToAdd.Level;
                m_fishing.ChangeFishingRangeSize();
                break;
        }
    }
    void GetSavedStats()
    {

    }
}
