using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelArgs
{
    /// <summary> 加载那个关卡 </summary>
	public int LevelIndex { get; set; }

    /// <summary> 是否成功过关 </summary>
    public bool IsSuccess;
}
