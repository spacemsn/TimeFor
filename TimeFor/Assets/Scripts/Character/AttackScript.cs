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
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }
}
