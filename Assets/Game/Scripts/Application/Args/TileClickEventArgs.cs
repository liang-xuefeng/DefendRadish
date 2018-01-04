using System;

/// <summary>
/// 鼠标点击参数类
/// </summary>
public class TileClickEventArgs : EventArgs
{
    /// <summary> 点击的那个键 </summary>
    public int MouseButton;
    /// <summary> 点击的那个格子 </summary>
    public Tile Tile;

    public TileClickEventArgs(int mouseButton, Tile tile)
    {
        MouseButton = mouseButton;
        Tile = tile;
    }
}
