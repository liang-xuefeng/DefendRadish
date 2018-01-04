using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPopup : View
{
    public SpawnPanel SpawnPanel;
    public UpgradePanel UpgradePanel;

    public override string Name
    {
        get { return Consts.V_TowerPopup; }
    }

    public override void RegisterEvents()
    {
        AttationEvents.Add(Consts.E_ShowSpawnPanel);
        AttationEvents.Add(Consts.E_ShowUpgradePanel);
        AttationEvents.Add(Consts.E_HiedPopup);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ShowSpawnPanel:
                ShowSpawnPanelArgs showSpawn = data as ShowSpawnPanelArgs;
                if (showSpawn != null) ShowSpawnPanel(showSpawn.Pos, showSpawn.UpSide);
                break;
            case Consts.E_ShowUpgradePanel:
                ShowUpgradePanelArgs showUpgrade = data as ShowUpgradePanelArgs;
                if (showUpgrade != null) ShowUpgradePanel(showUpgrade.Tower);
                break;
            case Consts.E_HiedPopup:
                HiedPopup();
                break;
            default:
                break;
        }
    }

    private void ShowSpawnPanel(Vector3 pos, bool upSide)
    {
        HiedPopup();
        SpawnPanel.Show(GetModel<GameModel>(), pos, upSide);
    }

    private void ShowUpgradePanel(Tower tower)
    {
        HiedPopup();
        UpgradePanel.Show(tower);
    }

    private void HiedPopup()
    {
        SpawnPanel.Hide();
        UpgradePanel.Hide();
    }

    /// <summary>
    /// 创建炮塔的消息
    /// </summary>
    private void OnSpawnTower(object[] obj)
    {
        int id = (int) obj[0];
        Vector3 pos = (Vector3) obj[1];

        SendEvent(Consts.E_SpawnTower, new SpawnTowerArgs { TowerId = id, Pos = pos});
    }

    /// <summary>
    /// 升级塔的消息
    /// </summary>
    private void OnUpgradeTower(object obj)
    {
        Tower tower = (Tower) obj;
        SendEvent(Consts.E_UpgradeTower, new UpgradeTowerArgs {Tower = tower});
    }

    /// <summary>
    /// 塔出售
    /// </summary>
    private void OnSellTower(object obj)
    {
        Tower tower = (Tower)obj;
        SendEvent(Consts.E_SellTower, new SellTowerArgs {Tower = tower});
    }
}
