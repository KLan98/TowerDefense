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
        //Debug.Log(transform.parent.name + " " + this.currentHP);
        if (this.currentHP <= 0)
        {
            this.IsDead();
        }
        return this.currentHP;
    }

    private void IsDead()
    {
        //Debug.Log(gameObject.transform.parent.name + " is dead");
        isDead = true;
        Die();
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

    protected override void LoadEnemyTargetable()
    {
        if (this.enemyTargetable != null) return;
        enemyTargetable = gameObject.transform.parent.GetComponentInChildren<EnemyTargetable>();
    }

    // set die animation, can move = false, disable game object
    private void Die()
    {
        this.enemyMoving.canMove = false;
        this.enemyTargetable.gameObject.layer = LayerMask.NameToLayer("Ignored");
        this.enemyControl.Animator.SetBool(Const.ENEMY_DIE, this.isDead);
        StartCoroutine(DespawnEnemy());
    }

    private IEnumerator DespawnEnemy()
    {
        // wait 2s before disable gameobject
        yield return new WaitForSeconds(4.6f);
        this.enemyControl.gameObject.SetActive(false);  
    }
}
