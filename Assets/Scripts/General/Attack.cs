using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    public float attackRange;       //攻击范围

    public float attackRate;        //攻击频率

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this); //加问号用来判断对方是否携带这个组件
    }
}
