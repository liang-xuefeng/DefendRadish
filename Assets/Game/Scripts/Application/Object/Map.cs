using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 描述一个关卡地图的状态
/// </summary>
public class Map : MonoBehaviour
{
    public const int RowCount = 8;
    public const int ColumeCount = 12;
    public bool DrawGizmos = true;

    #region 属性

    public Level Level { get { return _level; } }

    /// <summary> 设置背景图片 </summary>
    public string BackgroundImage
    {
        set
        {
            SpriteRenderer sprite = transform.Find("BackGround").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, sprite));
        }
    }

    public Rect MapRect
    {
        get { return new Rect(-_mapWidth / 2, -_mapHeight / 2, _mapWidth, _mapHeight); }
    }

    /// <summary> 设置路径图片 </summary>
    public string RoadImage
    {
        set
        {
            SpriteRenderer sprite = transform.Find("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, sprite));
        }
    }

    /// <summary> 所有格子的集合 </summary>
    public List<Tile> Grid { get { return _grid; } }
    /// <summary> 所有路径的集合 </summary>
    public List<Tile> Road { get { return _road; } }

    /// <summary> 获取怪物行走的世界坐标 </summary>
    public Vector3[] Path
    {
        get
        {
            List<Vector3> path = new List<Vector3>();
            foreach (Tile tile in _road)
            {
                Vector3 point = GetPosition(tile);
                path.Add(point);
            }
            return path.ToArray();
        }
    }

    #endregion

    #region 字段

    //地图的宽，高
    private float _mapWidth;
    private float _mapHeight;

    //格子的宽，高
    private float _tileWidth;
    private float _tileHeight;

    private List<Tile> _grid = new List<Tile>();
    private List<Tile> _road = new List<Tile>();

    //要编辑的关卡信息
    private Level _level;

    #endregion

    #region 事件

    /// <summary> 鼠标点击格子事件 </summary>
    public event EventHandler<TileClickEventArgs> OnTileClick;

    #endregion

    #region 事件回调

    private void Map_OnTileClick(object sender, TileClickEventArgs e)
    {
        if (gameObject.scene.name == "LevelBuilder") return;

        if (_level == null) return;

        if (e.MouseButton == 0 && !_road.Contains(e.Tile))
        {
            e.Tile.CanHold = !e.Tile.CanHold;
        }
        else if (e.MouseButton == 1 && !e.Tile.CanHold)
        {
            if (_road.Contains(e.Tile)) _road.Remove(e.Tile);
            else _road.Add(e.Tile);
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 加载关卡信息
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(Level level)
    {
        Clear();
        _level = level;

        //设置图片
        BackgroundImage = "file://" + Consts.MapDir + "/" + level.Background;
        RoadImage = "file://" + Consts.MapDir + "/" + level.Road;

        //寻路路径
        foreach (Point point in level.Path)
        {
            Tile tile = GetTile(point.X, point.Y);
            _road.Add(tile);
        }

        //塔空地
        foreach (Point point in level.Holders)
        {
            Tile tile = GetTile(point.X, point.Y);
            tile.CanHold = true;
        }

    }

    /// <summary>
    /// 清理所有塔位信息
    /// </summary>
    public void ClearHolder()
    {
        foreach (Tile tile in _grid)
        {
            tile.CanHold = false;
        }
    }

    /// <summary>
    /// 清理怪物行走路径
    /// </summary>
    public void ClearRoad()
    {
        _road.Clear();
    }

    /// <summary>
    /// 清理所有信息
    /// </summary>
    public void Clear()
    {
        _level = null;
        ClearHolder();
        ClearRoad();
    }

    #endregion

    #region 私有帮助方法

    /// <summary>
    /// 计算地图与格子的大小
    /// </summary>
    private void CalculateSize()
    {
        Vector3 p1 = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 p2 = Camera.main.ViewportToWorldPoint(Vector3.one);

        _mapWidth = p2.x - p1.x;
        _mapHeight = p2.y - p1.y;

        _tileWidth = _mapWidth / ColumeCount;
        _tileHeight = _mapHeight / RowCount;
    }

    /// <summary>
    /// 得到一个格子在地图中的位置(世界)
    /// </summary>
    /// <param name="tile">传入的格子</param>
    /// <returns></returns>
    public Vector3 GetPosition(Tile tile)
    {
        return new Vector3(-_mapWidth / 2 + (tile.X + 0.5f) * _tileWidth, -_mapHeight / 2 + (tile.Y + 0.5f) * _tileHeight);
    }

    /// <summary>
    /// 根据世界坐标，获得格子
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public Tile GetTile(Vector3 worldPos)
    {
        int col = (int)((worldPos.x + _mapWidth / 2) / _tileWidth);
        int row = (int)((worldPos.y + _mapHeight / 2) / _tileHeight);
        return GetTile(col, row);
    }

    /// <summary>
    /// 根据索引，获得格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Tile GetTile(int x, int y)
    {
        int index = x + y * ColumeCount;
        if (index < 0 || index >= _grid.Count) return null;
        return _grid[index];
    }

    /// <summary>
    /// 获取鼠标下的格子
    /// </summary>
    /// <returns></returns>
    private Tile GetTileUndermouse()
    {
        Vector2 worldPos = GetWorldPositon();
        return GetTile(worldPos);
    }

    /// <summary>
    /// 获得鼠标所在的世界坐标
    /// </summary>
    /// <returns></returns>
    private Vector3 GetWorldPositon()
    {
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        return worldPos;
    }

    #endregion

    #region Unity回调方法

    private void Awake()
    {
        //计算地图大小
        CalculateSize();

        for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColumeCount; j++)
                _grid.Add(new Tile(j,i));

        OnTileClick += Map_OnTileClick;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = GetTileUndermouse();
            if (tile != null && OnTileClick != null)
            {
                TileClickEventArgs tileClick = new TileClickEventArgs(0, tile);
                OnTileClick(this, tileClick);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Tile tile = GetTileUndermouse();
            if (tile != null && OnTileClick != null)
            {
                TileClickEventArgs tileClick = new TileClickEventArgs(1, tile);
                OnTileClick(this, tileClick);
            }
        }
    }

    /// <summary>
    /// 编辑器时，在屏目上画
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!DrawGizmos) return;

        //计算地图大小
        CalculateSize();

        Gizmos.color = Color.green;
        //绘制行
        for (int row = 0; row < RowCount; row++)
        {
            Vector2 from = new Vector2(-_mapWidth/2,-_mapHeight/2 + row * _tileHeight);
            Vector2 to = new Vector2(-_mapWidth/2 + _mapWidth, -_mapHeight/2 + row * _tileHeight);
            Gizmos.DrawLine(from, to);
        }

        //绘制列
        for (int col = 0; col < ColumeCount; col++)
        {
            Vector2 from = new Vector2(-_mapWidth / 2 + col * _tileWidth, _mapHeight / 2);
            Vector2 to = new Vector2(-_mapWidth / 2 + col * _tileWidth, -_mapHeight / 2);
            Gizmos.DrawLine(from, to);
        }

        //绘制建塔点
        foreach (Tile tile in _grid)
        {
            if (tile.CanHold)
            {
                Vector3 pos = GetPosition(tile);
                Gizmos.DrawIcon(pos, "holder.png", true);
            }
        }

        //绘制路径
        Gizmos.color = Color.red;
        for (int i = 0; i < _road.Count; i++)
        {
            if(i == 0)
                Gizmos.DrawIcon(GetPosition(_road[i]), "start.png", true);
            if(_road.Count > 1 && i == _road.Count - 1)
                Gizmos.DrawIcon(GetPosition(_road[i]), "end.png", true);

            if (_road.Count > 1 && i != 0)
            {
                Vector3 from = GetPosition(_road[i - 1]);
                Vector3 to = GetPosition(_road[i]);
                Gizmos.DrawLine(from, to);
            }
        }
    }

    #endregion
}
