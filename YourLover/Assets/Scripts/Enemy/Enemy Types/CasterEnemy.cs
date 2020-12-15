﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterEnemy : Enemy
{
    [SerializeField]
    private float actionRange = 25f;
    [SerializeField]
    private float attackRate;

    private float attackTime;

    public GameObject castObject;

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
            if (Vector2.Distance(transform.position, target.position) <= actionRange)
            {
                if (Time.time - attackTime >= 1 / attackRate)
                {
                    Cast(target);
                    attackTime = Time.time;
                }
            }
        }
    }

    public void Cast(Transform targetTransform)
    {
        triggerAttack = true;

        Instantiate(castObject, targetTransform.position, Quaternion.identity);
    }
}
