using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private UpgradeIcon _upgradeIcon;
    private SellIcon _sellIcon;

    private void Awake()
    {
        _upgradeIcon = GetComponentInChildren<UpgradeIcon>();
        _sellIcon = GetComponentInChildren<SellIcon>();
    }

    public void Show(Tower tower)
    {
        _upgradeIcon.Load(tower);
        _sellIcon.Load(tower);

        transform.position = tower.transform.position;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
