using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private CharacterStatus status;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterIndicators indicators;
    [SerializeField] private CharacterAbilities abilities;

    void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        abilities = GetComponent<CharacterAbilities>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    public void Move(Vector3 movement, float stamina, float runSpeed, float moveSpeed, float debuff, float maxStamina)
    {
        //animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(movement, 0.35f).magnitude);
        animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 0.55f).magnitude);

        float speed = Input.GetKey(KeyCode.LeftShift) && stamina > 0 ? runSpeed : moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && movement.magnitude > 0)
        {
            indicators.TakeStamina(debuff * 2);
            //animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(movement, 1).magnitude);
            animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 1).magnitude);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && stamina < maxStamina)
        {
            indicators.SetStamina(debuff);
            //animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(movement, 0.35f).magnitude);
            animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 0.55f).magnitude);
        }

        Vector3 velocity = movement.normalized * speed; velocity.y = rb.velocity.y; rb.velocity = velocity;
    }

    public void TransportPlayer(Vector3 _position)
    {
        this.transform.position = _position;
    }
}
