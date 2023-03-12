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

    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector3 playerVelocity;

    private CharacterController controller;
    private Vector3 _playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

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
        //JumpInput();
    }

    private void Movement()
    {
        if (_move.controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space) && _move.controller.isGrounded) // Прыжок
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpValue * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        _move.Move(moveDirection, playerVelocity, stamina, jumpValue, gravity, smoothTime, smoothVelocity, walkingSpeed, runningSpeed, normallSpeed, debuff, charMenegment);
    }

    //private void JumpInput()
    //{
    //    if (_move.controller.isGrounded && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space) && _move.controller.isGrounded) // Прыжок
    //    {
    //        playerVelocity.y = 0f;
    //        playerVelocity.y += Mathf.Sqrt(jumpValue * -3.0f * gravity);
    //        _move.Jump(playerVelocity, charMenegment);
    //    }

    //    playerVelocity.y += gravity * Time.deltaTime;
    //    _move.Jump(playerVelocity, charMenegment);
    //}

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
