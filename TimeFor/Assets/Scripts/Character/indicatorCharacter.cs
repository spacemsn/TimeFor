using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class indicatorCharacter : MonoCache, IElementBehavior, IDamageBehavior
{
    public static Action onLevelUp;

    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Показатели персонажа")]
    [SerializeField] private float health;
    [SerializeField] private float stamina;
    [SerializeField] private float healthMax;
    [SerializeField] private float staminaMax;

    [Header("Прокачка персонажа")]
    public int level; // текущий уровень персонажа
    public int experience; // текущий опыт
    public int experienceRequired; // требуемый опыт для нового уровня

    [SerializeField] Slider healthBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Image reactionImage;
    [SerializeField] TMP_Text damageText;
    [SerializeField] DeathScript dealthCharacter;
    [SerializeField] mainCharacter status;

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


    public float Health
    {
        get { return health; }
        set 
        {
            health = value;

            if(value > healthMax)
            {
                value = healthMax;
            }
            if (value <= 0)
            {
                dealthCharacter.OpenMenu();
            }
        }
    }

    public float Stamina
    {
        get { return stamina; }
        set 
        {
            stamina = value;

            if (value > staminaMax)
            {
                value = staminaMax;
            }
            else if(value < 0)
            {
                value = 0;
            }
        }
    }

    private void Start()
    {
        status = GetComponent<mainCharacter>();

        #region Resources 

        WaterSprite = Resources.Load<Sprite>("UI/Sprites/Elements/waterIcon");
        FireSprite = Resources.Load<Sprite>("UI/Sprites/Elements/fireIcon");
        AirSprite = Resources.Load<Sprite>("UI/Sprites/Elements/airIcon");
        TerraSprite = Resources.Load<Sprite>("UI/Sprites/Elements/terraIcon");

        damageUpSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/damageUpImage");
        VisionDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/visionDebuffImage");
        MovementDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/movementDebuffImage");

        #endregion

        EnemyBehavior.onDeadEnemy += GainExperience;
        Chest.onOpenChest += GainExperience;
        QuestReward.onQuestReward += GainExperience;
    }

    private void OnDisable()
    {
        EnemyBehavior.onDeadEnemy -= GainExperience;
        Chest.onOpenChest -= GainExperience;
        QuestReward.onQuestReward -= GainExperience;
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        healthBar = uI.healthBar;
        staminaBar = uI.staminaBar;
        reactionImage = uI.reactionImage;
        damageText = uI.damageText;

        reactionImage.enabled = false;
        damageText.enabled = false;
    }

    private void Update()
    {
        SetIcon();
    }

    public void GainExperience(int amount) // метод получения уровня 
    {
        experience += amount;
        if (experience >= experienceRequired)
        {
            LevelUp();
        }
    }

    private void LevelUp() // метод прокачки уровня 
    {
        level++;
        experience -= experienceRequired;
        experienceRequired = Mathf.RoundToInt(experienceRequired * 1.5f);
        healthMax += 50;

        UpdateStats();

        Debug.Log("Level Up! Current Level: " + level);

        onLevelUp.Invoke();
    }

    public void UpdateStats()
    {
        // обновить максимальные показатели в UI 
        healthBar.maxValue = healthMax;
        staminaBar.maxValue = staminaMax;

        health = healthMax; stamina = staminaMax;
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
        healthBar.value = Health; staminaBar.value = Stamina;
    }

    public void Reaction(IElementBehavior.Elements secondary, float buff, float damage)
    {
        if ((currentStatus == IElementBehavior.Elements.Water && secondary == IElementBehavior.Elements.Fire) || (currentStatus == IElementBehavior.Elements.Fire && secondary == IElementBehavior.Elements.Water))
        {
            Debug.Log("Реация увеличения урона!");
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
            Debug.Log("Реация оглушения движения");
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
            Debug.Log("Реация оглушения зрения");
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

    void SetDefauntStatus()
    {
        currentStatus = IElementBehavior.Elements.Null;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void SetHealth(float bonushealth)
    {
        Health += bonushealth;
    }

    public void TakeStamina(float amount)
    {
            Stamina -= amount;
    }

    public void SetStamina(float bonusstamina)
    {
        Stamina += bonusstamina;
    }
}
