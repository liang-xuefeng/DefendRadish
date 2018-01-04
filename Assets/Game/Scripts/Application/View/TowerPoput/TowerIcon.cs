using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private TowerInfo _towerInfo;
    private Vector3 _createPos;
    /// <summary> 钱是否够建造 </summary>
    private bool _isEnough;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 加载建塔icon
    /// </summary>
    /// <param name="gameModel"></param>
    /// <param name="towerInfo">炮塔信息</param>
    /// <param name="createPos">建造位置</param>
    /// <param name="upSide">图标是否显视在上面</param>
    public void Load(GameModel gameModel, TowerInfo towerInfo, Vector3 createPos, bool upSide)
    {
        //_isEnough = gameModel.Gold >= towerInfo.BasePrice;
        _isEnough = true;

        string path = "Res/Roles/" + (_isEnough ? towerInfo.NormalIcon : towerInfo.DisabledIcon);
        _spriteRenderer.sprite = Resources.Load<Sprite>(path);

        _towerInfo = towerInfo;
        _createPos = createPos;

        //设置icon的摆放位置
        Vector3 locPos = transform.localPosition;
        locPos.y = upSide ? Mathf.Abs(locPos.y) : -Mathf.Abs(locPos.y);
        transform.localPosition = locPos;
    }

    private void OnMouseDown()
    {
        if (!_isEnough) return;

        int id = _towerInfo.Id;
        Vector3 pos = _createPos;

        object[] obj = {id, pos};

        //发送消息
        SendMessageUpwards("OnSpawnTower", obj, SendMessageOptions.RequireReceiver);
    }
}
