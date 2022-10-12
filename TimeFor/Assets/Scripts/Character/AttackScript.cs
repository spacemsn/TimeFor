using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator animator;
  
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) 
        { 
            animator.SetBool("isAttack", true);
        }
        else
        { 
            animator.SetBool("isAttack", false);
        }
    }
}
