using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可创建塔的容器
/// </summary>
public class SpawnPanel : MonoBehaviour
{
    private TowerIcon[] _towerIcons;

    private void Awake()
    {
        _towerIcons = GetComponentsInChildren<TowerIcon>();
    }

    public void Show(GameModel gameModel, Vector3 pos,bool upSide)
    {
        //加载图标
        for (int i = 0; i < _towerIcons.Length; i++)
        {
            TowerInfo tower = Game.Instance.StaticData.GetTowerInfo(i);
            _towerIcons[i].Load(gameModel, tower, pos, upSide);
        }

        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
