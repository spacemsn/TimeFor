using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Компоненты")]
    public Joystick joystick;
    private CharacterController controller;
    private Animator animator;
    public Transform _camera;

    private Vector3 moveDirection;
    Vector3 playerVelocity;

    [Header("Характеристики персонажа")]
    [SerializeField] public float walkingSpeed = 7.5f;
    [SerializeField] public float runningSpeed = 11.5f;
    [SerializeField] private float jumpValue = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float smoothTime;
    float smoothVelocity;

    [Header("Выбор управления")]
    public Move move = Move.PC;
    public WalkType walkType = WalkType.noWeapon;

    [HideInInspector] public bool charMenegment = true;

    private float deltaH;
    private float deltaV;

    public enum Move
    {
        PC = 0,
        Android = 1, 
        Simple = 2
    }

    public enum WalkType
    {
        noWeapon = 0,
        withWeapon = 1,
    }

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        controller = this.gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (move == Move.PC) { MovePC(); }
        else if (move == Move.Android) { MoveAndroid(); }
        else if(move == Move.Simple) { SimpleMove(); }
    }

    private void MovePC()
    {
        if (charMenegment == true)
        {
            deltaH = Input.GetAxisRaw("Horizontal");
            deltaV = Input.GetAxisRaw("Vertical");
            joystick.gameObject.SetActive(false);

            moveDirection = new Vector3(deltaH, 0, deltaV).normalized;

            if (walkType == WalkType.noWeapon) { animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(moveDirection, 1).magnitude); }
            else if (walkType == WalkType.withWeapon) { animator.SetFloat("Sword And Shield", Vector3.ClampMagnitude(moveDirection, 1).magnitude); }

            if (moveDirection.magnitude > Mathf.Abs(0.05f))
            {
                float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            }

            if (Input.GetKey(KeyCode.LeftShift)) // Бег
            {
                controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Mouse0)) // Удар
            {
                controller.Move(moveDirection.normalized * 0 * Time.deltaTime);
                animator.SetBool("isAttack", true);
            }
            else // Обычное состояние
            {
                controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                animator.SetBool("isAttack", false);
            }

            if (Input.GetButton("Jump") && controller.isGrounded) // Прыжок
            {
                playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
                controller.Move(playerVelocity * Time.deltaTime);
            }
            else
            {
                playerVelocity.y += gravity * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);
            }
        }
        else if (charMenegment == false)
        {
            if (walkType == WalkType.noWeapon) { animator.SetFloat("StandartMotion", 0); }
            else if (walkType == WalkType.withWeapon) { animator.SetFloat("StaffMotion", 0); }
        }

    } // Движение персонажа ПК версии

    private void MoveAndroid()
    {
        deltaH = joystick.Horizontal;
        deltaV = joystick.Vertical;
        joystick.gameObject.SetActive(true);

        float movementDirectionY = moveDirection.y;

        moveDirection = new Vector3(deltaH, 0, deltaV).normalized;
        animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
        }
        else { controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime); }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 1;
        }
        else
        {
            controller.height = 2;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    } // Движение персонажа андройд версии

    private void SimpleMove() // Управление персонажем без прыжков и тд
    {
        if (move == Move.PC)
        {
            deltaH = Input.GetAxisRaw("Horizontal");
            deltaV = Input.GetAxisRaw("Vertical");
            joystick.gameObject.SetActive(false);
        }
        else if (move == Move.Android)
        {
            deltaH = joystick.Horizontal;
            deltaV = joystick.Vertical;
            joystick.gameObject.SetActive(true);
        }

        Vector3 moveDirection = new Vector3(deltaH, 0, deltaV);
        animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

        if (moveDirection.magnitude > Mathf.Abs(0.05f))
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveCharacter = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            controller.SimpleMove(Vector3.ClampMagnitude(moveCharacter, 1) * walkingSpeed);
        }
    }
}
