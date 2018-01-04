using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luobo : Role
{
    private Animator _animator;

    private readonly int _isDamageId = Animator.StringToHash("IsDamage");
    private readonly int _isDead = Animator.StringToHash("IsDead");

    private readonly int _idel = Animator.StringToHash("Luobo_Idel");

    public override void Damage(int hit)
    {
        if (IsDead) return;
        base.Damage(hit);

        _animator.SetTrigger(_isDamageId);
    }

    protected override void Die(Role role)
    {
        base.Die(role);
        _animator.SetBool(_isDead, true);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        _animator = GetComponent<Animator>();
        _animator.Play(_idel);

        LuoboInfo info = Game.Instance.StaticData.GetLuoboInfo();
        MaxHp = info.Hp;
        Hp = info.Hp;
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
        _animator.SetBool(_isDead, false);
        _animator.ResetTrigger(_isDamageId);

        Hp = 0;
        MaxHp = 0;
    }
}
