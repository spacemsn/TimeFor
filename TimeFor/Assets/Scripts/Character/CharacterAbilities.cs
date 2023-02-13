using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbilities : MonoCache
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
    [SerializeField] CharacterIndicators indicators;

    [Header("Виды атак")]
    [SerializeField] public bool aimMode = false;
    [SerializeField] SkillObject attackOne;
    [SerializeField] SkillObject attackTwo;
    [SerializeField] SkillObject attackThree;
    [SerializeField] SkillObject attackFour;

    QuickslotInventory inventory;
    [SerializeField] GameObject GlobalSettings;

    [HideInInspector] public CharacterStatus status;
    [HideInInspector] public Camera _camera;
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        status = GetComponent<CharacterStatus>();
        //inventory = GameObject.Find("SkillsPanel").GetComponent<QuickslotInventory>();
        //ChargeAttack = GameObject.Find("ChargeAttack").GetComponent<Slider>();
        CenterScreen = GameObject.Find("CenterScreen");
        rightHand = GameObject.Find("ArmSmall").transform;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        GlobalSettings = GameObject.Find("Global Settings");
        InventoryPanel = GlobalSettings.GetComponent<InventoryPanel>().inventoryPanel.gameObject;
        DealthPanel = GlobalSettings.GetComponent<DealthCharacter>().DealthPanel.gameObject;
        PausePanel = GlobalSettings.GetComponent<PauseMenu>().PausePanel.gameObject;
    }

    //public override void OnTick()
    //{
    //    ChargeAttack.value = ButtonTimer;

    //    if (inventory.currentQuickslotID == 0) { ShootOne(); }
    //    else if (inventory.currentQuickslotID == 1) { ShootTwo(); }
    //    else if (inventory.currentQuickslotID == 2) { ShootThree(); }
    //    else if(inventory.currentQuickslotID == 3) { AimModel(); }
    //}

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
                status._animator.SetBool("aimMode", true);
                status.normallSpeed = status.walkingSpeed;
                status.walkingSpeed = status.aimModeSpeed;
                status.smoothTime = 0.075f;
            }
            else if (aimMode == true)
            {
                aimMode = false;
                virtualCamera.Priority = 9;
                CenterScreen.transform.GetChild(0).gameObject.SetActive(false);
                status._animator.SetBool("aimMode", false);
                status.walkingSpeed = status.normallSpeed;
                status.smoothTime = 0.075f;
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
        if (status.mana >= attackOne.consumption)
        {
            _attack = Instantiate(attackOne.objectPrefab, rightHand.position, rightHand.rotation);
            indicators.TakeMana(attackOne.consumption);
            _attack.GetComponent<FireBall>();
        }
    }

    void ThrowRay()
    {
        if (status.mana >= attackTwo.consumption)
        {
            _attack = Instantiate(attackTwo.objectPrefab, rightHand.position, rightHand.rotation);
            indicators.TakeMana(attackTwo.consumption);
            _attack.GetComponent<Explosion>();
        }
    }

    void SuperAttack()
    {
        if (status.mana >= attackThree.consumption)
        {
            _attack = Instantiate(attackThree.objectPrefab, rightHand.position, rightHand.rotation);
            indicators.TakeMana(attackThree.consumption);
            _attack.GetComponent<FireBall>();
        }
    }

    void AimAttack()
    {
        if (status.mana >= attackFour.consumption)
        {
            _attack = Instantiate(attackFour.objectPrefab, rightHand.position, _camera.transform.rotation);
            indicators.TakeMana(attackFour.consumption);
            _attack.GetComponent<Aimball>().Fire();
        }
    }

    public void StartAnimation()
    {
        status.charMenegment = false;
    }   
    
    public void EndAnimation()
    {
        status.charMenegment = true;
    }
}
