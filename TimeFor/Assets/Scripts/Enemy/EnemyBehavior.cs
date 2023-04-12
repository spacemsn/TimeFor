using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Характеристики Врага")]
    private float hp = 100;
    private int enemyDamage;
    private float viewAngle = 90f;
    private float viewDistance = 50f;
    [SerializeField] EnemyObject enemyParam;
    [SerializeField] private float chaseTime = 5f;
    [SerializeField] private float attackTime = 10f;
    [SerializeField] private float distanceMax = 30f;
    [SerializeField] private float originPos;
    [SerializeField] private float playerPos;

    [Header("Точки Врага")]
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private int currentPoint = 0;

    [Header("Компоненты Врага")]
    private Vector3 originalPosition;
    private NavMeshAgent navAgent;
    public CapsuleCollider rightHand;
    public CapsuleCollider leftHand;
    public Slider healthBar;
    private Animator animator;
    private Transform player;
    [HideInInspector] public Transform centerOfEnemy;

    private enum EnemyStage { Wait, Patrolling, Chase, Attack, Death, }
    [Header("Стадия поведения врага")]
    [SerializeField] private EnemyStage enemyStage;

    void Start()
    {
        SetParam();

        enemyStage = EnemyStage.Wait;

        //Находим компонент NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        //Находим игрока
        player = GameObject.FindGameObjectWithTag("Player").transform;
        centerOfEnemy = gameObject.transform.Find("CenterOfEnemy");

        rightHand.enabled = false;
        leftHand.enabled = false;

        //Запоминаем начальную позицию
        originalPosition = transform.position;
    }

    private void SetParam()
    {
        hp = enemyParam.hp;
        enemyDamage = enemyParam.enemyDamage;
        viewAngle = enemyParam.viewAngle;
        viewDistance = enemyParam.viewDistance;
    }

    void Update()
    {
        healthBar.value = hp;
        originPos = Vector3.Distance(transform.position, originalPosition);
        if (player != null) { playerPos = Vector3.Distance(transform.position, player.position); }

        switch (enemyStage)
        {
            case EnemyStage.Wait:
                {
                    if (CanSeePlayer() == EnemyStage.Patrolling)
                    {
                        enemyStage = EnemyStage.Patrolling;
                        animator.SetTrigger("Move");
                    }
                    else if (CanSeePlayer() == EnemyStage.Chase)
                    {
                        enemyStage = EnemyStage.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyStage.Patrolling:
                {
                    navAgent.destination = movePoints[currentPoint].position;

                    //Если достигли точки, то двигаемся к следующей
                    if (Vector3.Distance(transform.position, movePoints[currentPoint].position) < 1.5f)
                    {
                        currentPoint++;
                        if (currentPoint >= movePoints.Length)
                        {
                            currentPoint = 0;
                        }
                    }
                    if (CanSeePlayer() == EnemyStage.Chase)
                    {
                        enemyStage = EnemyStage.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyStage.Chase:
                {
                    if (playerPos <= navAgent.stoppingDistance)
                    {
                        enemyStage = EnemyStage.Attack;
                        animator.SetTrigger("Attack");
                    }
                    else if (originPos >= distanceMax)
                    { 
                        chaseTime -= Time.deltaTime;
                        navAgent.destination = player.position;

                        if (chaseTime <= 0)
                        { 
                            enemyStage = EnemyStage.Patrolling;
                            animator.SetTrigger("Move");
                        }
                    }
                    else
                    {
                        chaseTime = 5f;
                        navAgent.destination = player.position;
                    }
                }
                break;

            case EnemyStage.Attack:
                {
                    if (playerPos <= navAgent.stoppingDistance)
                    {
                        gameObject.transform.LookAt(player.transform);
                    }
                    else if(playerPos > navAgent.stoppingDistance)
                    { 
                        enemyStage = EnemyStage.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyStage.Death:
                {
                    navAgent.Stop();
                    animator.SetTrigger("Death");
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    healthBar.gameObject.SetActive(false);
                    Destroy(this.gameObject, 10f);
                }
                break;

            default:
                { break; }
        }
    }

    private EnemyStage CanSeePlayer()
    {
        if (player != null)
        {
            //Находим направление до игрока
            Vector3 direction = player.position - transform.position;

            //Находим угол между направлением до игрока и направлением взгляда врага
            float angle = Vector3.Angle(direction, transform.forward);

            //Если угол меньше или равен углу обзора и расстояние до игрока меньше или равно расстоянию обнаружения, то игрок обнаружен
            if (angle <= viewAngle && Vector3.Distance(transform.position, player.position) <= viewDistance || Vector3.Distance(transform.position, player.position) <= viewDistance)
            {
                return EnemyStage.Chase;
            }

            return EnemyStage.Patrolling;
        }
        else { return EnemyStage.Patrolling; }
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;
        enemyStage = EnemyStage.Chase;
        animator.SetTrigger("Chase");

        if (hp <= 0)
        {
            enemyStage = EnemyStage.Death;
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterIndicators indicators = other.gameObject.GetComponent<CharacterIndicators>();
        if (indicators)
        {
            indicators.TakeHit(enemyDamage);
        }
    }

    public void CollidersTrue()
    {
        //rightHand.enabled = true;
        leftHand.enabled = true;
    }

    public void CollidersFalse()
    {
        //rightHand.enabled = false;
        leftHand.enabled = false;
    }
}
