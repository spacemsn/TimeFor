using UnityEngine;
using UnityEngine.UIElements;

public class CharacterAbilities : MonoCache
{
    [Header("Характеристики")]
    private float timer = 0;
    private Transform rightHand;
    private bool attacking; // буль переменная атаки
    [SerializeField] private float maxDistance; // дисстанция ближайщего противника

    [Header("Враги")]
    [SerializeField] Collider[] enemies;
    [SerializeField] LayerMask enemyLayer;


    [Header("Интерфейс")]
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject DealthPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private CharacterStatus status;
    [SerializeField] private CharacterIndicators indicators;
    [SerializeField] private QuickslotInventory inventory;
    [SerializeField] private GameObject GlobalSettings;
    [SerializeField] private Animator animator;

    [Header("Виды атак")]
    [SerializeField] private SkillObject attackOne;
    [SerializeField] private SkillObject attackTwo;
    [SerializeField] private SkillObject attackThree;
    [SerializeField] private SkillObject attackFour;
    private Collider currentEnemy;
    private SkillObject currentAttack;
    private GameObject currentSpell;

    private enum listSpells { Fire, Water, Air, Ground, }
    [SerializeField] private listSpells spells;

    private void Start()
    {
        GlobalSettings = GameObject.Find("Global Settings");
        if (GlobalSettings != null)
        {
            InventoryPanel = GlobalSettings.GetComponent<GloballSetting>().inventoryPanel.gameObject;
            DealthPanel = GlobalSettings.GetComponent<GloballSetting>().dealthPanel.gameObject;
            PausePanel = GlobalSettings.GetComponent<GloballSetting>().pausePanel.gameObject;
        }
        rightHand = GameObject.Find("ArmSmall").transform;
        //inventory = GameObject.Find("SkillsPanel").GetComponent<QuickslotInventory>();

        indicators = GetComponent<CharacterIndicators>();
        status = GetComponent<CharacterStatus>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        switch (spells)
        {
            case listSpells.Fire:
                {
                    currentAttack = attackOne; Shoot();
                }
                break;

            case listSpells.Water:
                {
                    currentAttack = attackTwo; Shoot();
                }
                break;

            case listSpells.Air:
                {
                    currentAttack = attackThree; Shoot();
                }
                break;

            case listSpells.Ground:
                {
                    currentAttack = attackFour; Shoot();
                }
                break;

            default: { break; }
        }
    }

    private void Shoot()
    {
        if (!attacking && timer > currentAttack.attackRollback)
        {
            // Поиск ближайшего врага
            enemies = Physics.OverlapSphere(transform.position, maxDistance, enemyLayer);
            if (enemies.Length > 0)
            {
                currentEnemy = enemies[0];
                float closestDistance = Vector3.Distance(transform.position, currentEnemy.transform.position);
                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        currentEnemy = enemy;
                        closestDistance = distance;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0f;
                    status.charMenegment = false;
                    transform.LookAt(currentEnemy.transform.position, Vector3.up);
                    animator.SetBool("Attack1", true);
                }

                Invoke("ResetAttack", currentAttack.attackRollback);
            }
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }

    public void StartAnimation()
    {
        // Выстрел самонаводящегося снаряда в ближайшего врага
        GameObject centerOfEnemy = currentEnemy.GetComponent<EnemyGTP>().centerOfEnemy.gameObject;
        currentSpell = Instantiate(attackOne.objectPrefab, rightHand.position, rightHand.transform.rotation);
        currentSpell.GetComponent<FireBall>().SetTarget(centerOfEnemy.transform, currentAttack.speed);

        // Запуск таймера для следующей атаки
        attacking = true;
    }   
    
    public void EndAnimation()
    {
        status.charMenegment = true;
        animator.SetBool("Attack1", false);
    }
}
