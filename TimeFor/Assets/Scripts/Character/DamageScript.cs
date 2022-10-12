using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public WeaponItem weapon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyScript>().TakeDamage(weapon.damage);
        }
    }
}
