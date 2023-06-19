using UnityEngine;

public class FireBall : MonoCache, IElementBehavior
{
    [SerializeField] private skillItem skill;
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

    public void SetTarget(Transform target)
    {
        this.target = target;
        this.speed = skill.speed;
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
            EnemyDamage enemyHealth = other.GetComponent<EnemyDamage>();
            if (enemyHealth != null)
            {
                enemyHealth.Reaction(skill.element, skill.elementObject.basePersent, skill.elementObject.baseDamage);
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

    public void Reaction(Elements secondary, float buff, float damage)
    {

    }
}
