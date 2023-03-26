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

    Collider[] colliders;
    public Transform rightHand;

    public override void OnTick()
    {
        colliders = Physics.OverlapSphere(startPosition, radius);
        StartCoroutine(Countdown());
        
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject enemy = colliders[i].gameObject;
            if (enemy.tag == "Enemy")
            {
                endPosition = enemy.transform.position;

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
        if (other.GetComponent<AI_Monster>())
        {
            AI_Monster enemy = other.GetComponent<AI_Monster>();
            if (enemy != null)
            {
                enemy.TakeDamage(skill.damage);
                Destroy(gameObject);
                StartCoroutine(Countdown());
            }
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
