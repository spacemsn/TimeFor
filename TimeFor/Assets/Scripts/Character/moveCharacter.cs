using UnityEngine;

public class moveCharacter : MonoBehaviour, IMoveBehavior
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Компоненты")]
    [SerializeField] private mainCharacter status;
    [SerializeField] private indicatorCharacter indicators;
    [SerializeField] private attackCharacter abilities;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] public Camera camera;

    [Header("Характеристики")]
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float maxStamina;
    public float debuff;
    public float smoothTime;
    private float smoothVelocity;
    Vector3 movement;
    public bool isManagement;
    private bool isGrounded = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        indicators = GetComponent<indicatorCharacter>();
        abilities = GetComponent<attackCharacter>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<mainCharacter>();
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        camera = uI.camera;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void FixedUpdate()
    {
        if (isManagement)
        {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (movement.magnitude > Mathf.Abs(0.05f))
            {
                float rotationAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                movement = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            }

            animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 0.55f).magnitude);

            float speed = Input.GetKey(KeyCode.LeftShift) && indicators.Stamina > 0 ? runSpeed : moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && indicators.Stamina > 0 && movement.magnitude > 0)
            {
                indicators.TakeStamina(debuff * 2);
                //animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(movement, 1).magnitude);
                animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 1).magnitude);
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && indicators.Stamina < maxStamina)
            {
                indicators.SetStamina(debuff);
                //animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(movement, 0.35f).magnitude);
                animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 0.55f).magnitude);
            }

            Vector3 velocity = movement.normalized * speed; velocity.y = rb.velocity.y; rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            animator.SetFloat("CharacterBehavior", Vector3.ClampMagnitude(movement, 0f).magnitude);
        }
    }

    private void LateUpdate()
    {
        if (isManagement)
        {
            // Прыжок
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jump", true);
            }
            else { animator.SetBool("Jump", false); }
        }
    }

    public void TransportPlayer(Vector3 _position)
    {
        this.transform.position = _position;
    }
}
