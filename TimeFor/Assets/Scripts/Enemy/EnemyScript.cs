using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoCache
{
    [SerializeField] private float hp = 100;
    [SerializeField] private int enemyDamage;
    public CapsuleCollider rightHand;
    public CapsuleCollider leftHand;
    public Animator animator;
    public Slider healthBar;

    private void Start()
    {
        rightHand.enabled = false;
        leftHand.enabled = false;
    }

    public override void OnTick()
    {
        healthBar.value = hp;
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;

        if(hp <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            healthBar.gameObject.SetActive(false);
            Destroy(this.gameObject, 10f);
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterIndicators indicators = other.gameObject.GetComponent<CharacterIndicators>();
        if (indicators)
        {
            indicators.TakeHit(enemyDamage);
        }
    }

    public void CollidersTrue()
    {
        //rightHand.enabled = true;
        leftHand.enabled = true;
    }

    public void CollidersFalse()
    {
        //rightHand.enabled = false;
        leftHand.enabled = false;
    }
}
