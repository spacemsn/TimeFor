using UnityEngine;
using UnityEngine.UI;

public class UsingAbilities : MonoCache
{
    [Header("Характеристики")]
    [SerializeField] float ButtonTimer = 0;
    [SerializeField] float timer = 0;
    [SerializeField] Transform rightHand;
    GameObject _fireball;

    [Header("Интерфейс")]
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject DealthPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Slider ChargeAttack;
    [SerializeField] Health health;

    [Header("Виды атак")]
    [SerializeField] SkillObject attackOne;
    [SerializeField] SkillObject attackTwo;
    [SerializeField] SkillObject attackThree;

    QuickslotInventory inventory;

    private void Start()
    {
        health = GetComponent<Health>();
        inventory = GameObject.Find("SkillsPanel").GetComponent<QuickslotInventory>();
        rightHand = GameObject.Find("ArmSmall").transform;
    }

    public override void OnTick()
    {
        ChargeAttack.value = ButtonTimer;

        if (inventory.currentQuickslotID == 0) { ShootOne(); }
        else if (inventory.currentQuickslotID == 1) { ShootTwo(); }
        else if (inventory.currentQuickslotID == 2) { ShootThree(); }
    }

    void ShootOne()
    {
        timer += Time.deltaTime;

        if (timer >= attackOne.attackRollback)
        {
            if (InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0;
                    ThrowFireBall();
                }
            }
        }
    }

    void ShootTwo()
    {
        timer += Time.deltaTime;

        if (timer >= attackTwo.attackRollback)
        {
            if (InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0;
                    ThrowRay();
                }
            }
        }
    }

    void ShootThree()
    {
        timer += Time.deltaTime;

        if (timer >= attackThree.attackRollback)
        {
            if (InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    ButtonTimer += Time.deltaTime * 33.3f;
                    if (ChargeAttack.value == 100)
                    {
                        timer = 0;
                        ButtonTimer = 0;
                        SuperAttack();
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    timer = 0;
                    ButtonTimer = 0;
                }
            }
        }
    }

    void ThrowFireBall()
    {
        if (health.mana >= attackOne.consumption)
        {
            _fireball = Instantiate(attackOne.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackOne.consumption);
        }
    }

    void ThrowRay()
    {
        if (health.mana >= attackTwo.consumption)
        {
            _fireball = Instantiate(attackTwo.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackTwo.consumption);
        }
    }

    void SuperAttack()
    {
        if (health.mana >= attackThree.consumption)
        {
            _fireball = Instantiate(attackThree.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackThree.consumption);
        }
    }
}
