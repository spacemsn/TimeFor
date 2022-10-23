using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UsingAbilities : MonoCache
{
    [Header("Характеристики")]
    [SerializeField] float ButtonTimer = 0;
    [SerializeField] float timer = 0;
    [SerializeField] Transform rightHand;
    GameObject _attack;

    [Header("Интерфейс")]
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject DealthPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CenterScreen;
    [SerializeField] Slider ChargeAttack;
    [SerializeField] Health health;

    [Header("Виды атак")]
    [SerializeField] bool aimMode = false;
    [SerializeField] SkillObject attackOne;
    [SerializeField] SkillObject attackTwo;
    [SerializeField] SkillObject attackThree;
    [SerializeField] SkillObject attackFour;

    QuickslotInventory inventory;
    CharacterMove character;
    [SerializeField] Camera _camera;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        health = GetComponent<Health>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMove>();
        inventory = GameObject.Find("SkillsPanel").GetComponent<QuickslotInventory>();        
        CenterScreen = GameObject.Find("CenterScreen");
        rightHand = GameObject.Find("ArmSmall").transform;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public override void OnTick()
    {
        ChargeAttack.value = ButtonTimer;

        if (inventory.currentQuickslotID == 0) { ShootOne(); }
        else if (inventory.currentQuickslotID == 1) { ShootTwo(); }
        else if (inventory.currentQuickslotID == 2) { ShootThree(); }
        else if(inventory.currentQuickslotID == 3) { AimModel(); }
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

    void AimModel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (aimMode == false)
            {
                aimMode = true;
                virtualCamera.Priority = 11;
                CenterScreen.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (aimMode == true)
            {
                aimMode = false;
                virtualCamera.Priority = 9;
                CenterScreen.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        Cinemachine3rdPersonFollow PersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (PersonFollow.CameraSide == 0) { PersonFollow.CameraSide = 1; }
            else if (PersonFollow.CameraSide == 1) { PersonFollow.CameraSide = 0; }
        }

        timer += Time.deltaTime;

        if (timer >= attackTwo.attackRollback)
        {
            if (InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0;
                    AimAttack();
                }
            }
        }
    }

    void ThrowFireBall()
    {
        if (health.mana >= attackOne.consumption)
        {
            _attack = Instantiate(attackOne.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackOne.consumption);
            _attack.GetComponent<FireBall>();
        }
    }

    void ThrowRay()
    {
        if (health.mana >= attackTwo.consumption)
        {
            _attack = Instantiate(attackTwo.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackTwo.consumption);
            _attack.GetComponent<Explosion>().Shoot();
        }
    }

    void SuperAttack()
    {
        if (health.mana >= attackThree.consumption)
        {
            _attack = Instantiate(attackThree.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(attackThree.consumption);
            _attack.GetComponent<FireBall>();
        }
    }

    void AimAttack()
    {
        if (health.mana >= attackFour.consumption)
        {
            _attack = Instantiate(attackFour.objectPrefab, rightHand.position, _camera.transform.rotation);
            health.TakeMana(attackFour.consumption);
            _attack.GetComponent<Aimball>().Fire();
        }
    }

    public void StartAnimation()
    {
        character.charMenegment = false;
    }   
    
    public void EndAnimation()
    {
        character.charMenegment = true;
    }
}
