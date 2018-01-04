using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellIcon : MonoBehaviour
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
    }

    private void OnMouseDown()
    {
        SendMessageUpwards("OnSellTower", _tower, SendMessageOptions.RequireReceiver);
    }
}
