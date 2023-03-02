using UnityEngine;
using System.Collections;

public class CharacterMove : MonoCache
{
    [Header("Компоненты")]
    [SerializeField] CharacterStatus status;
    [SerializeField] public CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] Camera _camera;
    [SerializeField] CharacterIndicators indicators;
    [SerializeField] CharacterAbilities abilities;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Vector3 playerVelocity;

    private void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        abilities = GetComponent<CharacterAbilities>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
        cameraManager = GetComponent<CameraManager>();
    }

    public void Move(Vector3 moveDirection, float stamina, float jumpValue, float gravity, float smoothTime, float smoothVelocity, float walkingSpeed, float runningSpeed, float normallSpeed, float debuff, bool charMenegment)
    {
        _camera = cameraManager._camera;
        if (charMenegment == true)
        {
            animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

            if (abilities.aimMode == false)
            {
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }
            else if (abilities.aimMode == true)
            {
                float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) + _camera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > Mathf.Abs(0.05f)) // Бег
            {
                indicators.TakeStamina(debuff * 2);
                if (stamina > 0)
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
        }
        else if (charMenegment == false)
        {
            
        }

    } // Движение персонажа ПК версии

    public void Jump(Vector3 playerVelocity, bool charMenegment)
    {
        if (charMenegment == true)
        {
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

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
