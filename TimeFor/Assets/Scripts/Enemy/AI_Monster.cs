using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Monster : MonoCache
{
    [Header("Характеристики Врага")]
    [SerializeField] private float hp = 100;
    [SerializeField] private int enemyDamage;
    [SerializeField] public float Dist_Player;

    [Header("Компоненты Врага")]
    public CapsuleCollider rightHand;
    public CapsuleCollider leftHand;
    public Animator animator;
    public Slider healthBar;
    public NavMeshAgent AI_Agent;
    private GameObject player;
    public GameObject panel_GaveOver;
    public Transform[] WayPoints;
    public int Current_Patch;

    public enum AI_State { Patrol, Stay, Chase, Attack };
    public AI_State AI_Enemy;

    void Start()
    {
        rightHand.enabled = false;
        leftHand.enabled = false;

        animator = gameObject.GetComponent<Animator>();
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnTick()
    {
        Dist_Player = Vector3.Distance(player.transform.position, gameObject.transform.position);
        healthBar.value = hp;

        if (AI_Enemy == AI_State.Patrol)
        {
            AI_Agent.Resume();
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isChase", false);
            AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
            float Patch_Dist = Vector3.Distance(WayPoints[Current_Patch].transform.position, gameObject.transform.position);
            if (Patch_Dist < 2)
            {
                Current_Patch++;
                Current_Patch = Current_Patch % WayPoints.Length;
            }
        }
        else if (AI_Enemy == AI_State.Stay)
        {
            animator.SetBool("isPatrolling", false);
            animator.SetBool("isChase", false);
            AI_Agent.Stop();
        }
        else if (AI_Enemy == AI_State.Chase)
        {
            animator.SetBool("isChase", true);
            animator.SetBool("isAttack", false);
            AI_Agent.SetDestination(player.transform.position);
        }
        else if (AI_Enemy == AI_State.Attack)
        {
            animator.SetBool("isAttack", true);
            gameObject.transform.LookAt(player.transform);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            healthBar.gameObject.SetActive(false);
            Destroy(this.gameObject, 10f);
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