using UnityEngine;

public class AttackScript : MonoCache
{
    Animator animator;
  
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Attack1", true);
        }
        //else
        //{
        //    animator.SetBool("Attack1", false);
        //}
    }
}
