using UnityEngine;

public class DamageScript : MonoCache
{
    public WeaponItem weapon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<AI_Monster>().TakeDamage(weapon.damage);
        }
    }
}
