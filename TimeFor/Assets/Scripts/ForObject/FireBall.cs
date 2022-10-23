using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoCache
{
    [SerializeField] SkillObject skill;

    [Header("Поиск врага")]
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float step;
    [SerializeField] float progress;
    [SerializeField] float radius;

    [SerializeField] Collider[] colliders;
    public Transform rightHand;

    public override void OnTick()
    {
        colliders = Physics.OverlapSphere(startPosition, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject enemy = colliders[i].gameObject;
            if (enemy.tag == "Enemy")
            {
                endPosition = enemy.transform.GetChild(0).position;

                transform.position = Vector3.Lerp(startPosition, endPosition, progress);
                progress += step;
            }
        }
    }

    private void Start()
    {
        rightHand = GameObject.Find("ArmSmall").transform;
        transform.position = rightHand.position;
        startPosition = rightHand.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyScript>())
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
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
