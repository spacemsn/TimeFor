using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEnemy1 : StateMachineBehaviour
{
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            animator.transform.LookAt(player);

            float distacne = Vector3.Distance(animator.transform.position, player.transform.position);
            if (distacne > 3)
            {
                animator.SetBool("isAttack", false);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
