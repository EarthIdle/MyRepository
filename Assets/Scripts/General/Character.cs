using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]

    public float maxHealth;
    public float currentHealth;

    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;

    public bool invulerable;

    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent onDie;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulerable)
            return;
        if(currentHealth > attacker.damage)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();

            //ִ������
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //��������
            onDie?.Invoke();

        }
    }

   /// <summary>
   /// ���������޵�
   /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulerable)
        {
            invulerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
