using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class EnemyDamage : MonoBehaviour, IElementBehavior, IDamageBehavior
{
    [Header("Характеристики Врага")]
    [SerializeField] EnemyObject enemyParam;
    [SerializeField] EnemyBehavior enemyBehavior;
    public float hp;
    public int enemyDamage;

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
    public bool runStatusCorouutine;

    [Header("Время наложения реакции")]
    public float timeReaction;
    public bool runReactionCorouutine;

    private void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        animator = GetComponent<Animator>();

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

        enemyParam.SetDamage(this);
    }

    private void Update()
    {
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
        hp -= damage; healthBar.value = hp;

        enemyBehavior.currentState = EnemyBehavior.EnemyStage.Chase;

        if (hp <= 0)
        {
            enemyBehavior.currentState = EnemyBehavior.EnemyStage.Death;
            enemyBehavior.DeadEnemy();
            enemyUI.gameObject.SetActive(false);
            
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void SetHealth(float bonus)
    {
        hp += bonus; healthBar.value = hp;
    }

    public void HealthUp()
    {
        if (hp >= healthBar.value)
        {
            hp = Mathf.RoundToInt(hp * 1.5f);
            healthBar.maxValue = hp;
            healthBar.value = hp;
        }
        else
        {
            healthBar.maxValue = Mathf.RoundToInt(hp * 1.5f);
        }
        enemyParam.LevelUp(this);
    }    

    private void OnTriggerEnter(Collider other)
    {
        indicatorCharacter player = other.gameObject.GetComponent<indicatorCharacter>();
        if (player)
        {
            player.Reaction(currentStatus, 1, enemyDamage);
        }
    }

    public void CollidersTrue()
    {
        rightHand.enabled = true;
        leftHand.enabled = true;
    }

    public void CollidersFalse()
    {
        rightHand.enabled = false;
        leftHand.enabled = false;
    }

    public void Reaction(IElementBehavior.Elements secondary, float buff, float damage)
    {
        if ((currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Fire) || (currentStatus == IElementBehavior.Elements.Fire && secondary == IElementBehavior.Elements.Water))
        {
            reaction = IElementBehavior.Reactions.DamageUp;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            damage *= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == IElementBehavior.Elements.Terra && secondary == IElementBehavior.Elements.Fire) || (currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Terra))
        {
            reaction = IElementBehavior.Reactions.MovementDown;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            //navAgent.speed -= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == IElementBehavior.Elements.Fire && secondary == IElementBehavior.Elements.Air) || (currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Air) || (currentStatus == IElementBehavior.Elements.Terra && secondary == IElementBehavior.Elements.Air))
        {
            reaction = IElementBehavior.Reactions.VisionDown;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            //viewAngle -= buff;
            TakeDamage(damage);
        }
        else
        {
            currentStatus = secondary;
            TakeDamage(damage);
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitStatus(timeStatus));
            }
            else
            {
                StopCoroutine(WaitStatus(timeStatus));
                StartCoroutine(WaitStatus(timeStatus));
            }
        }
    }

    IEnumerator WaitStatus(float timeStatus)
    {
        runStatusCorouutine = true;

        yield return new WaitForSeconds(timeStatus);
        currentStatus = IElementBehavior.Elements.Null;
        runStatusCorouutine = false;
    }

    IEnumerator WaitReaction(float timeStatus)
    {
        runReactionCorouutine = true;

        yield return new WaitForSeconds(timeStatus);
        reaction = IElementBehavior.Reactions.Null;
        runReactionCorouutine = false;
    }

    private void SetDefauntStatus()
    {
        currentStatus = IElementBehavior.Elements.Null;
    }
}
