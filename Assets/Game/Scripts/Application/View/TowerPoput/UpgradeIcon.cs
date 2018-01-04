using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIcon : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Tower _tower;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Load(Tower tower)
    {
        _tower = tower;

        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.Id);
        string path = "Res/Roles/" + (tower.IsTopLevel ? info.DisabledIcon : info.NormalIcon);
        _spriteRenderer.sprite = Resources.Load<Sprite>(path);
    }

    private void OnMouseDown()
    {
        SendMessageUpwards("OnUpgradeTower", _tower, SendMessageOptions.RequireReceiver);
    }
}
