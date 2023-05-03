using UnityEngine;
using UnityEngine.UI;

public class attackCharacter : MonoCache
{
    [Header("Характеристики")]
    private bool attacking;
    [SerializeField] private float maxDistance;
    private Transform rightHand;

    [Header("Враги")]
    [SerializeField] Collider[] enemies;
    [SerializeField] LayerMask enemyLayer;

    [Header("Интерфейс")]
    [SerializeField] public indicatorCharacter indicators;
    [SerializeField] public GloballSetting globalSettings;
    [SerializeField] public mainCharacter status;
    [SerializeField] public moveCharacter move;
    [SerializeField] public Animator animator;

    [Header("UI")]
    [SerializeField] public GameObject InventoryPanel;
    [SerializeField] public Transform quickslotParent;
    [SerializeField] public GameObject DealthPanel;
    [SerializeField] public GameObject PausePanel;

    [SerializeField] public Sprite selectedSprite;
    [SerializeField] public Sprite notSelectedSprite;

    [Header("Виды атак")]
    [SerializeField] private skillItem WaterAttack;
    [SerializeField] private skillItem FireAttack;
    [SerializeField] private skillItem AirAttack;
    [SerializeField] private skillItem TerraAttack;
    private Collider currentEnemy;
    private skillItem currentAttack;
    private GameObject currentSpell;

    private enum listSpells { Water, Fire, Air, Terra, }
    [SerializeField] private listSpells spells;
    [SerializeField] public int currentQuickslotID = -1;
    [SerializeField] public int oldQuickslotID;

    private void Start()
    {
        indicators = GetComponent<indicatorCharacter>();
        move = GetComponent<moveCharacter>();
        status = GetComponent<mainCharacter>();
        animator = GetComponent<Animator>();

        if (globalSettings != null)
        {
            InventoryPanel = globalSettings.InventoryPanel;
            DealthPanel = globalSettings.DeathPanel;
            PausePanel = globalSettings.PausePanel;
        }
        rightHand = GameObject.Find("ArmSmall").transform;

    }

    private void Update()
    {
        // Используем цифры
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            // если мы нажимаем на клавиши 1 по 5 то...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
                if (currentQuickslotID == i)
                {
                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    }
                }
                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    oldQuickslotID = currentQuickslotID;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                }
            }
        }
        // Используем предмет по нажатию на левую кнопку мыши
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().foodItem != null && quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().foodItem)
        //    {
        //        if (!rayCharacter.isOpenPanel && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
        //        {
        //            // Применяем изменения к здоровью (будущем к голоду и жажде) 
        //            ChangeCharacteristics();

        //            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount <= 1)
        //            {
        //                quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
        //            }
        //            else
        //            {
        //                quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount--;
        //                quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().itemAmount.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount.ToString();
        //            }
        //        }
        //    }
        //}
    }

    private void FixedUpdate()
    {
        //switch (spells)
        //{
        //    case listSpells.Water:
        //        {
        //            currentAttack = WaterAttack;
        //        }
        //        break;

        //    case listSpells.Fire:
        //        {
        //            currentAttack = FireAttack;
        //        }
        //        break;

        //    case listSpells.Air:
        //        {
        //            currentAttack = AirAttack;
        //        }
        //        break;

        //    case listSpells.Terra:
        //        {
        //            currentAttack = TerraAttack;
        //        }
        //        break;

        //    default: { break; }
        //}

        switch (currentQuickslotID)
        {
            case 0:
                {
                    currentAttack = WaterAttack;
                    spells = listSpells.Water;
                    break;
                }

            case 1:
                {
                    currentAttack = FireAttack;
                    spells = listSpells.Fire;
                    break;
                }

            case 2:
                {
                    currentAttack = AirAttack;
                    spells = listSpells.Air;
                    break;
                }

            case 3:
                {
                    currentAttack = TerraAttack;
                    spells = listSpells.Terra;
                    break;
                }

            default: 
                {
                    currentQuickslotID = oldQuickslotID;
                    currentAttack = WaterAttack;
                    spells = listSpells.Water;
                    break;
                }
        }

        FindEnemies();
        Shoot();
    }

    public void FindEnemies()
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
        }
    }

    private void Shoot()
    {
        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentEnemy != null)
            {
                move.isManagement = false;
                transform.LookAt(currentEnemy.transform.position, Vector3.up);
                animator.SetBool("Attack1", true);
            }

            Invoke("ResetAttack", currentAttack.attackRollback);
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }

    public void StartAnimation()
    {
        // Выстрел самонаводящегося снаряда в ближайшего врага
        GameObject centerOfEnemy = currentEnemy.GetComponent<EnemyBehavior>().centerOfEnemy.gameObject;
        currentSpell = Instantiate(currentAttack.itemPrefab, rightHand.position, rightHand.transform.rotation);
        currentSpell.GetComponent<FireBall>().SetTarget(centerOfEnemy.transform, currentAttack.speed);

        // Запуск таймера для следующей атаки
        attacking = true;
    }   
    
    public void EndAnimation()
    {
        move.isManagement = true;
        animator.SetBool("Attack1", false);
    }
}
