using UnityEngine;

public class FireBall : MonoCache
{
    [SerializeField] float speed;
    [SerializeField] float damage;

    [Header("Взрыв")]
    [SerializeField] private float raduis;
    [SerializeField] private float force;
    [SerializeField] private float second;

    public override void OnTick()
    {
        //transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyScript>())
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null) enemy.TakeDamage(damage);
        }
        else Destroy(gameObject);
    }

    void Explode() // Взрыв
    {
        Collider[] overLapped = Physics.OverlapSphere(transform.position, raduis);

        for (int i = 0; i < overLapped.Length; i++)
        {
            Rigidbody rigidbody = overLapped[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(force, transform.position, raduis);
                Destroy(gameObject, 5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raduis);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raduis / 2f);
    }
}
