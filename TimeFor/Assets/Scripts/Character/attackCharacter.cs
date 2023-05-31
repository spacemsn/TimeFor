using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class attackCharacter : MonoCache
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Характеристики")]
    private bool attacking;
    [SerializeField] private float maxDistance;
    private Transform rightHand;

    [Header("Враги")]
    [SerializeField] Collider[] enemies;
    [SerializeField] LayerMask enemyLayer;

    [Header("Интерфейс")]
    [SerializeField] public moveCharacter move;
    [SerializeField] public Animator animator;

    [Header("UI")]
    [SerializeField] public Transform QuickslotPanel;

    [SerializeField] public Sprite FireSelectSprite;
    [SerializeField] public Sprite WaterSelectSprite;
    [SerializeField] public Sprite AirSelectSprite;
    [SerializeField] public Sprite TerraSelectSprite;

    [SerializeField] public Sprite selectedSprite;
    [SerializeField] public Sprite notSelectedSprite;

    [Header("Слоты атак")]
    [SerializeField] public SkillSlot WaterSlot;
    [SerializeField] public SkillSlot FireSlot;
    [SerializeField] public SkillSlot AirSlot;
    [SerializeField] public SkillSlot TerraSlot;

    [Header("Виды атак")]
    [SerializeField] private skillItem WaterAttack;
    [SerializeField] private skillItem FireAttack;
    [SerializeField] private skillItem AirAttack;
    [SerializeField] private skillItem TerraAttack;
    private Collider currentEnemy;
    private skillItem currentAttack;
    private GameObject currentSpell;

    [SerializeField] public int currentQuickslotID = -1;
    [SerializeField] public int oldQuickslotID;

    [Header("Кнопки")]
    [SerializeField] KeyCode KeyCode1;
    [SerializeField] KeyCode KeyCode2;
    [SerializeField] KeyCode KeyCode3;
    [SerializeField] KeyCode KeyCode4;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
        animator = GetComponent<Animator>();

        rightHand = GameObject.Find("ArmSmall").transform;
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        WaterSlot = uI.WaterSlot;
        FireSlot = uI.FireSlot;
        AirSlot = uI.AirSlot;
        TerraSlot = uI.TerraSlot;
        QuickslotPanel = uI.QuickslotPanel;
            
        WaterSlot.SetIcon(WaterAttack.icon);
        FireSlot.SetIcon(FireAttack.icon);
        AirSlot.SetIcon(AirAttack.icon);
        TerraSlot.SetIcon(TerraAttack.icon);
    }

    private void Update()
    {
        // Используем цифры
        for (int i = 0; i < QuickslotPanel.childCount; i++)
        {
            // если мы нажимаем на клавиши 1 по 5 то...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
                if (currentQuickslotID == i)
                {
                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
                    if (QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    }
                    //else
                    //{
                    //    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    //}
                }
                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
                else
                {
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    oldQuickslotID = currentQuickslotID;

                    currentQuickslotID = i;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
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
        switch (currentQuickslotID)
        {
            case 0:
                {
                    currentAttack = WaterAttack;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = FireSelectSprite;
                    break;
                }

            case 1:
                {
                    currentAttack = FireAttack;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = WaterSelectSprite;
                    break;
                }

            case 2:
                {
                    currentAttack = AirAttack;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = AirSelectSprite; break;
                }

            case 3:
                {
                    currentAttack = TerraAttack;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = TerraSelectSprite; break;
                }

            default:
                {
                    currentQuickslotID = oldQuickslotID;
                    currentAttack = WaterAttack;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = WaterSelectSprite; break;
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
