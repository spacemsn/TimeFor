using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMove : MonoCache
{
    [Header("??????????")]
    public Joystick joystick;
    private CharacterController controller;
    [HideInInspector] public Animator animator;
    public Transform _camera;
    Health health;
    UsingAbilities UsingAbilities;

    [HideInInspector] public Vector3 moveDirection;
    Vector3 playerVelocity;

    [Header("?????????????? ?????????")]
    [SerializeField] public float aimModeSpeed = 4.5f;
    [SerializeField] public float walkingSpeed = 7.5f;
    [SerializeField] public float runningSpeed = 11.5f;
    [SerializeField] public float normallSpeed;
    [SerializeField] private float jumpValue = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] public float smoothTime;
    [SerializeField] private float debuff = 0.15f;
    public float smoothVelocity;

    [Header("????? ??????????")]
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
        health = GetComponent<Health>();
        UsingAbilities = GetComponent<UsingAbilities>();    

        controller = this.gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public override void OnTick()
    {
        if (move == Move.PC) { MovePC(); }
        else if (move == Move.Android) { MoveAndroid(); }
        else if (move == Move.Simple) { SimpleMove(); }
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

            if (UsingAbilities.aimMode == false)
            {
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }
            else if (UsingAbilities.aimMode == true)
            {
                float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift)) // ???
            {
                health.TakeStamina(debuff * 2);
                if (health.stamina > 0)
                {
                    controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                }
            }
            else // ??????? ?????????
            {
                controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                health.SetStamina(debuff);
            }

            if (Input.GetButton("Jump") && controller.isGrounded) // ??????
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

    } // ???????? ????????? ?? ??????

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

    } // ???????? ????????? ??????? ??????

    private void SimpleMove() // ?????????? ?????????? ??? ??????? ? ??
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
