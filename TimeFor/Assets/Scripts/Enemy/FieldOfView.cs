using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoCache
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject player;
    public AI_Monster enemy;
    Animator animator;

    public LayerMask targetMask;
    public LayerMask ObstacleMask;
    public bool canSeePlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemy = gameObject.GetComponent<AI_Monster>();

        float distance = Vector3.Distance(animator.transform.position, player.transform.position);

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    public override void OnTick()
    {
        if (enemy.Dist_Player <= 2 && canSeePlayer == true)
        {
            enemy.AI_Enemy = AI_Monster.AI_State.Attack;
        }
        else if (canSeePlayer == false)
        {
            enemy.AI_Enemy = AI_Monster.AI_State.Patrol;
        }
        else
        {
            enemy.AI_Enemy = AI_Monster.AI_State.Chase;
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                {
                    canSeePlayer = true;
                } else { canSeePlayer = false; }
            } else { canSeePlayer = false; }
        } else if (canSeePlayer) { canSeePlayer = false; } 
    }
}