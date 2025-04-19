using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : EnemyAbstract
{
    public int currentHP;
    public int maxHP;
    [SerializeField] protected bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;
    }

    public virtual int Deduct(int hp)
    {
        this.currentHP = this.currentHP - hp;
        Debug.Log(transform.parent.name + " " + this.currentHP);
        if (this.currentHP <= 0)
        {
            this.IsDead();
        }
        return this.currentHP;
    }

    protected virtual bool IsDead()
    {
        Debug.Log(gameObject.transform.parent.name + " is dead");
        Die();
        return isDead = true;
    }

    protected override void LoadEnemyControl()
    {
        if (this.enemyControl != null) return;
        enemyControl = gameObject.transform.parent.GetComponent<EnemyControl>();
    }
    protected override void LoadEnemyMoving()
    {
        if (this.enemyMoving != null) return;
        enemyMoving = gameObject.transform.parent.GetComponentInChildren<EnemyMoving>();
    }

    // set die animation, can move = false, disable game object
    private void Die()
    {
        this.enemyControl.Animator.SetBool(Const.ENEMY_DIE, this.isDead);
        this.enemyMoving.canMove = false;
    }
}
