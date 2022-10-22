using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Explosion : MonoCache
{
    [SerializeField] SkillObject skill;

    [Header("Поиск врага")]
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float step;
    [SerializeField] float progress;
    [SerializeField] float radius;
    [SerializeField] float force;

    [SerializeField] Collider[] colliders;
    public Transform rightHand;
    Rigidbody rigidbody;

    public override void OnTick()
    {
        Destroy(gameObject, 15f);
    }

    void Shoot()
    {
        transform.position = rightHand.position;
        startPosition = rightHand.transform.position;

        colliders = Physics.OverlapSphere(startPosition, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject enemy = colliders[i].gameObject;
            if (enemy.tag == "Enemy")
            {
                endPosition = enemy.transform.GetChild(0).position;
                var wave = endPosition - startPosition;
                rigidbody.velocity = wave;
            }
        }
    }

    private void Start()
    {
        rightHand = GameObject.Find("ArmSmall").transform;
        rigidbody = GetComponent<Rigidbody>();

        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyScript>())
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(skill.damage);
                Destroy(gameObject);
            }
        }
        else if(other.tag == "Player") { }
        //else Destroy(gameObject);
    }
}
