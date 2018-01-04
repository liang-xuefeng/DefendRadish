using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundModel : Model
{
    /// <summary> 回合出怪间隔 </summary>
    public const float Round_Interval = 3f;
    /// <summary> 回合内出怪间隔 </summary>
    public const float Spawn_Interval = 1f;

    private List<Round> _rounds = new List<Round>();
    public int _roundIndex = -1;
    private bool _allRoundsComplete = false;

    public override string Name
    {
        get { return Consts.M_RoundModel; }
    }

    /// <summary> 当前回合索引 </summary>
    public int RoundIndex { get { return _roundIndex; } }
    /// <summary> 总回合数 </summary>
    public int RoundTotal { get { return _rounds.Count; } }
    /// <summary> 是否所有怪都出完 </summary>
    public bool AllRoundsComplete { get { return _allRoundsComplete; } }
    /// <summary> 所有回合信息 </summary>
    public List<Round> Rounds { get { return _rounds; } }

    /// <summary>
    /// 获得当前所选关卡的回合信息
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(Level level)
    {
        _rounds = level.Rounds;
        _roundIndex = -1;
        _allRoundsComplete = false;
    }

    /// <summary>
    /// 开始回合
    /// </summary>
    public void StartRound()
    {
        Game.Instance.StartCoroutine(RunRound());
    }

    /// <summary>
    /// 取消回合
    /// </summary>
    public void StopRound()
    {
        Game.Instance.StopCoroutine(RunRound());
    }

    /// <summary>
    /// 回合开始，开始出怪
    /// </summary>
    private IEnumerator RunRound()
    {
        _allRoundsComplete = false;
        for (int i = 0; i < _rounds.Count; i++)
        {
            //回合信息
            StartRoundArgs startRoundArgs = new StartRoundArgs();
            startRoundArgs.RoundIndex = i;
            startRoundArgs.RoundTotal = RoundTotal;

            SendEvent(Consts.E_StartRound, startRoundArgs);

            Round round = _rounds[i];

            for (int j = 0; j < round.Count; j++)
            {
                //出怪信息
                SpawnMonsterArgs spawnMonsterArgs = new SpawnMonsterArgs();
                spawnMonsterArgs.MosterType = round.Monster;

                SendEvent(Consts.E_SpawnMonster, spawnMonsterArgs);

                yield return new WaitForSeconds(Spawn_Interval);
            }

            _roundIndex++;

            if (i < round.Count -1)
                yield return new WaitForSeconds(Round_Interval);
        }
        _allRoundsComplete = true;
    }
}
