using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class CharacterStatus : MonoCache
{
    [Header("Компоненты игрока")]
    [HideInInspector] public Animator _animator;

    [Header("CharacterStatus")]
    public Vector3 position;

    [Header("CharacterPrefab")]
    public GameObject _characterPrefab; 

    [Header("CharacterLevel")]
    public int levelId;

    [Header("CharacterIndicators")]
    public CharacterIndicators _indicators;
    public int health;
    public int mana;
    public float stamina;

    [Header("CharacterMove")]
    public CharacterMove _move;

    [Header("Характеристики персонажа")]
    public float aimModeSpeed = 4.5f;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float normallSpeed;
    public float jumpValue = 8.0f;
    public float gravity = 20.0f;
    public float smoothTime;
    public float debuff = 0.15f;
    public float smoothVelocity;
    public bool charMenegment = true;

    [HideInInspector] Vector3 moveDirection;
    [SerializeField] Vector3 playerVelocity;

    private void Start()
    {
        #region Components

        _animator = GetComponent<Animator>();

        #endregion

        #region GameObject

        _characterPrefab = this.gameObject;

        #endregion

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

        _move = this.GetComponent<CharacterMove>();

        #endregion

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

        _move = this.GetComponent<CharacterMove>();

        #endregion
    }

    public override void OnTick()
    {
        Movement();
        Status();
        UpdateStatus();
        JumpInput();
    }

    private void Movement()
    {
        float deltaH = Input.GetAxisRaw("Horizontal");
        float deltaV = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(deltaH, 0, deltaV).normalized;
        _move.Move(moveDirection, stamina, jumpValue, gravity, smoothTime, smoothVelocity, walkingSpeed, runningSpeed, normallSpeed, debuff, charMenegment);
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && _move.controller.isGrounded) // Прыжок
        {
            playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
            _move.Jump(playerVelocity, charMenegment);
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
            _move.Jump(playerVelocity, charMenegment);
        }
    }

    private void Status()
    {
        _indicators.Indicators(health, mana, stamina);
    }

    #region Save and Load Json
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        levelId = data.level;
        health = data.health;
        mana = data.mana;
        stamina = data.stamina;
        position = data.position;
        transform.position = data.position;
    }
    #endregion
}
