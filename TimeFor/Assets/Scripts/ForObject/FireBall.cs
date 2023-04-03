using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoCache
{
    [SerializeField] private SkillObject skill;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AnimationCurve ScaleCurve;
    [SerializeField] private Vector3 originalScale;

    [Range(0, 1)]
    [SerializeField] private float timeOfScale;
    [SerializeField] private float scaleDuraction;
    [SerializeField] private float speed;
    [SerializeField] private float liveTime;
    [SerializeField] private Vector3 EnemyPos;

    public void SetTarget(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    private void Start()
    {
        originalScale = transform.localScale;
        Destroy(gameObject, liveTime);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            timeOfScale += Time.deltaTime / scaleDuraction;

            if (timeOfScale >= 1)
            {
                timeOfScale = 1;
            }

            originalScale = originalScale * ScaleCurve.Evaluate(timeOfScale);

            Vector3 direction = target.position - transform.position;
            transform.LookAt(target.position);
            //transform.Translate(direction.normalized * speed * Time.deltaTime);
            transform.position += (direction * speed * Time.deltaTime).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyGTP enemyHealth = other.GetComponent<EnemyGTP>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(skill.damage);
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
        else if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
