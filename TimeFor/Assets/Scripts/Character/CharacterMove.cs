using UnityEngine;

public class CharacterMove : MonoCache
{
    [Header("Компоненты")]
    [HideInInspector] private CharacterController controller;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform _camera;
    [HideInInspector] CharacterIndicators indicators;
    [HideInInspector] UsingAbilities UsingAbilities;
    [HideInInspector] Vector3 moveDirection;
    [HideInInspector] Vector3 playerVelocity;

    [Header("Характеристики персонажа")]
    [HideInInspector] public float aimModeSpeed = 4.5f;
    [HideInInspector] public float walkingSpeed = 7.5f;
    [HideInInspector] public float runningSpeed = 11.5f;
    [HideInInspector] public float normallSpeed;
    [HideInInspector] public float jumpValue = 8.0f;
    [HideInInspector] public float gravity = 20.0f;
    [HideInInspector] public float smoothTime;
    [HideInInspector] public float debuff = 0.15f;
    [HideInInspector] public float smoothVelocity;
    [HideInInspector] public bool charMenegment = true;

    private void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        UsingAbilities = GetComponent<UsingAbilities>();    
        controller = this.gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public override void OnTick()
    {
        MovePC();
    }

    public void MovePC()
    {
        if (charMenegment == true)
        {
            float deltaH = Input.GetAxisRaw("Horizontal");
            float deltaV = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(deltaH, 0, deltaV).normalized;
            animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

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

            if (Input.GetKey(KeyCode.LeftShift)) // Бег
            {
                indicators.TakeStamina(debuff * 2);
                if (indicators.stamina > 0)
                {
                    controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                }
            }
            else // Обычное состояние
            {
                controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                indicators.SetStamina(debuff);
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
            
        }

    } // Движение персонажа ПК версии

    //private void SimpleMove() // Управление персонажем без прыжков и тд
    //{

    //    deltaH = Input.GetAxisRaw("Horizontal");
    //    deltaV = Input.GetAxisRaw("Vertical");


    //    Vector3 moveDirection = new Vector3(deltaH, 0, deltaV);
    //    animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

    //    if (moveDirection.magnitude > Mathf.Abs(0.05f))
    //    {
    //        float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //        Vector3 moveCharacter = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

    //        controller.SimpleMove(Vector3.ClampMagnitude(moveCharacter, 1) * walkingSpeed);
    //    }
    //}
}
