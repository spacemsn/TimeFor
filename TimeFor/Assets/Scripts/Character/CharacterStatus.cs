using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoCache
{
    [Header("Игрок")]
    public string playerName;

    [Header("Компоненты игрока")]
    [SerializeField] private Animator animator;
    [SerializeField] public Camera camera;
    [SerializeField] private Rigidbody rb;

    [Header("CharacterStatus")]
    public Vector3 position;

    [Header("Сохранение")]
    public SaveData saveData;

    [Header("Инвентарь")]
    public InventoryScript inventory;

    [Header("CharacterLevel")]
    public int levelId;

    [Header("CharacterIndicators")]
    public CharacterIndicators _indicators;
    public int levelPlayer;
    public int health;
    public float stamina;

    [Header("PlayerMove")]
    public CharacterMove move;

    [Header("CharacterAbilities")]
    public CharacterAbilities characterAbilities;

    [Header("Характеристики персонажа")]
    public float damageBase = 20f;
    public float damagePercent = 1f;
    public float moveSpeed = 1.5f;
    public float runSpeed = 3.5f;
    public float jumpForce = 5f;
    private float maxStamina = 100f;
    public float debuff = 0.15f;
    public float smoothTime;
    private float smoothVelocity;
    [SerializeField] Vector3 movement;
    public bool charMenegment = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        #region Components

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        #endregion

        #region CharacterLevel

        levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        position = this.transform.position;

        #endregion

        #region InventoryScript

        inventory = GetComponent<InventoryScript>();

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        #endregion

        #region CharacterAbilities

        characterAbilities = this.GetComponent<CharacterAbilities>();

        #endregion

        #region CharacterMove

        move = this.GetComponent<CharacterMove>();

        #endregion

        LoadPlayer();
    }

    private void UpdateStatus()
    {
        #region CharacterLevel

        levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        position = this.transform.position;

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        #endregion

        #region CharacterMove

        move = this.GetComponent<CharacterMove>();

        #endregion
    }

    private void Update()
    {
        Status();
    }

    private void FixedUpdate()
    {
        Movement(); UpdateStatus();
    }

    private void Movement()
    {
        if (charMenegment)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (movement.magnitude > Mathf.Abs(0.05f))
            {
                float rotationAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                movement = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            }

            // Прыжок
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jump", true);
            }
            else { animator.SetBool("Jump", false); }

            move.Move(movement, stamina, runSpeed, moveSpeed, debuff, maxStamina);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void Status()
    {
        _indicators.Indicators(health, stamina, levelPlayer);
    }

    #region Save and Load

    public void SavePlayer()
    {
        saveData.SetSave(this);
    }

    public void LoadPlayer()
    {
        saveData.LoadSave(this);
    }

    #endregion
}
