using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private int enemyDamage;
    public Animator animator;
    public Slider healthBar;

    // Update is called once per frame
    void Update()
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
            Destroy(this.gameObject, 15f);
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.TakeHit(enemyDamage);
        }
    }
}
