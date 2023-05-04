using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using static EnemyBehavior;

public class EnemyDamage : MonoBehaviour, IElementBehavior, IDamageBehavior
{
    [Header("Характеристики Врага")]
    [SerializeField] EnemyObject enemyParam;
    [SerializeField] EnemyBehavior enemyBehavior;
    [HideInInspector] public float hp;
    [HideInInspector] public int enemyDamage;

    [Header("Компоненты Врага")]
    public Slider healthBar;
    public Image reactionImage;
    public TMP_Text damageText;
    public CapsuleCollider rightHand;
    public CapsuleCollider leftHand;
    public Animator animator;

    public Transform player;
    public Transform enemyUI;
    public Transform camera;

    [Header("Картинки стихии")]
    public Sprite WaterSprite;
    public Sprite FireSprite;
    public Sprite AirSprite;
    public Sprite TerraSprite;

    [Header("Картинки реакции стихии")]
    public Sprite damageUpSprite;
    public Sprite VisionDownSprite;
    public Sprite MovementDownSprite;

    [Header("Статус Стихии")]
    public IElementBehavior.Elements currentStatus;

    [Header("Реакция Стихии")]
    public IElementBehavior.Reactions reaction;

    [Header("Время наложения статуса")]
    public float timeStatus;

    [Header("Время наложения реакции")]
    public float timeReaction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        enemyBehavior = GetComponent<EnemyBehavior>();
        animator = GetComponent<Animator>();

        enemyParam.SetDamage(this);

        rightHand.enabled = false;
        leftHand.enabled = false;
        reactionImage.enabled = false;
        damageText.enabled = false;

        WaterSprite = Resources.Load<Sprite>("UI/Sprites/Elements/waterIcon");
        FireSprite = Resources.Load<Sprite>("UI/Sprites/Elements/fireIcon");
        AirSprite = Resources.Load<Sprite>("UI/Sprites/Elements/airIcon");
        TerraSprite = Resources.Load<Sprite>("UI/Sprites/Elements/terraIcon");

        damageUpSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/damageUpImage");
        VisionDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/visionDebuffImage");
        MovementDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/movementDebuffImage");
    }

    private void Update()
    {
        healthBar.value = hp;
        SetIcon();
    }

    private void SetIcon()
    {
        if (reaction == IElementBehavior.Reactions.Null)
        {
            switch (currentStatus)
            {
                case IElementBehavior.Elements.Water:
                    reactionImage.sprite = WaterSprite;
                    reactionImage.enabled = true;
                    break;

                case IElementBehavior.Elements.Fire:
                    reactionImage.sprite = FireSprite;
                    reactionImage.enabled = true;
                    break;

                case IElementBehavior.Elements.Air:
                    reactionImage.sprite = AirSprite;
                    reactionImage.enabled = true;
                    break;

                case IElementBehavior.Elements.Terra:
                    reactionImage.sprite = TerraSprite;
                    reactionImage.enabled = true;
                    break;

                case IElementBehavior.Elements.Null:
                    reactionImage.enabled = false;
                    break;
            }
        }
        if (currentStatus == IElementBehavior.Elements.Null)
        {
            switch (reaction)
            {
                case IElementBehavior.Reactions.DamageUp:
                    {
                        reactionImage.sprite = damageUpSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case IElementBehavior.Reactions.MovementDown:
                    {
                        reactionImage.sprite = MovementDownSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case IElementBehavior.Reactions.VisionDown:
                    {
                        reactionImage.sprite = VisionDownSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case IElementBehavior.Reactions.Null:
                    {
                        reactionImage.enabled = false;
                        break;
                    }
            }
        }
    }

    private void LateUpdate()
    {
        enemyUI.LookAt(camera.position, Vector3.up);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        enemyBehavior.enemyStage = EnemyBehavior.EnemyStage.Chase;

        if (hp <= 0)
        {
            enemyBehavior.enemyStage = EnemyBehavior.EnemyStage.Death;
            enemyUI.gameObject.SetActive(false);
            
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void SetHealth(float bonus)
    {
        hp += bonus;
    }

    private void OnTriggerEnter(Collider other)
    {
        indicatorCharacter player = other.gameObject.GetComponent<indicatorCharacter>();
        if (player)
        {
            player.Reaction(currentStatus, 0, enemyDamage);
        }
    }

    public void CollidersTrue()
    {
        //rightHand.enabled = true;
        leftHand.enabled = true;
    }

    public void CollidersFalse()
    {
        //rightHand.enabled = false;
        leftHand.enabled = false;
    }

    public void Reaction(IElementBehavior.Elements secondary, float buff, float damage)
    {
        if ((currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Fire) || (currentStatus == IElementBehavior.Elements.Fire && secondary == IElementBehavior.Elements.Water))
        {
            Debug.Log("Реация увеличения урона!");
            reaction = IElementBehavior.Reactions.DamageUp;
            SetDefauntStatus();
            StartCoroutine(WaitReaction(timeReaction));
            damage *= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == IElementBehavior.Elements.Terra && secondary == IElementBehavior.Elements.Fire) || (currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Terra))
        {
            Debug.Log("Реация оглушения движения");
            reaction = IElementBehavior.Reactions.MovementDown;
            SetDefauntStatus();
            StartCoroutine(WaitReaction(timeReaction));
            //navAgent.speed -= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == IElementBehavior.Elements.Fire && secondary == IElementBehavior.Elements.Air) || (currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Air) || (currentStatus == IElementBehavior.Elements.Terra && secondary == IElementBehavior.Elements.Air))
        {
            Debug.Log("Реация оглушения зрения");
            reaction = IElementBehavior.Reactions.VisionDown;
            SetDefauntStatus();
            StartCoroutine(WaitReaction(timeReaction));
            //viewAngle -= buff;
            TakeDamage(damage);
        }
        else
        {
            currentStatus = secondary;
            TakeDamage(damage);
            StartCoroutine(WaitStatus(timeStatus));
        }
    }

    IEnumerator WaitStatus(float timeStatus)
    {
        yield return new WaitForSeconds(timeStatus);
        currentStatus = IElementBehavior.Elements.Null;
    }

    IEnumerator WaitReaction(float timeStatus)
    {
        yield return new WaitForSeconds(timeStatus);
        reaction = IElementBehavior.Reactions.Null;
    }

    private void SetDefauntStatus()
    {
        currentStatus = IElementBehavior.Elements.Null;
    }
}
