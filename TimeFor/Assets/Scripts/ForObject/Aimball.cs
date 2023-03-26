using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimball : MonoCache
{
    [SerializeField] SkillObject skill;
    [SerializeField] float force;
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fire()
    {
        rb.AddForce(transform.forward * (force), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AI_Monster>())
        {
            AI_Monster enemy = other.GetComponent<AI_Monster>();
        if (enemy != null)
        {
                enemy.TakeDamage(skill.damage);
                StartCoroutine(Countdown());
            }
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
