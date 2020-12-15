﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    private float attackRange = 2f;
    [SerializeField]
    private float attackSpeed = 5f;
    [SerializeField]
    private float attackRate = 2;
    [SerializeField]
    private int attackDamage = 1;

    private float attackTime;

    public override void Start()
    {
        base.Start();
        FindPlayer();

        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public override void Update()
    {
        base.Update();
        AttackTarget();
    }

    private void AttackTarget()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= attackRange)
            {
                if (Time.time - attackTime >= 1 / attackRate)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.GetComponent<PlayerInfo>().TakeDamage(attackDamage);
        }
        else
        {
            target.GetComponent<Entity>().TakeDamage(attackDamage);
        }

        triggerAttack = true;

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = target.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += attackSpeed * Time.deltaTime;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
