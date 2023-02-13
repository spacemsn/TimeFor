using UnityEngine;
using UnityEngine.UI;

public class CharacterIndicators : MonoCache
{
    float timer;

    private int health;
    public int maxHealth = 100;

    private int mana;
    public int maxMana = 1000;
    public int timeRecoveryMana = 10;

    private float stamina;
    public float maxStamina = 100;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider manaBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] DealthCharacter dealthCharacter;
    [SerializeField] CharacterStatus status;

    private void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        manaBar = GameObject.Find("ManaBar").GetComponent<Slider>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        dealthCharacter = GameObject.Find("Global Settings").GetComponent<DealthCharacter>();
        status = GetComponent<CharacterStatus>();
    }

    public void Indicators(int health, int mana, float stamina)
    {
        this.health = health; this.mana = mana; this.stamina = stamina;
        healthBar.value = health; manaBar.value = mana; staminaBar.value = stamina;

        RecoveryMana();
    }

    public void TakeHit(int damage)
    {
        status.health -= damage;

        if (status.health <= 0)
        {
            dealthCharacter.OpenMenu();
        }
    }

    public void SetHealth(int bonushealth)
    {
        status.health += bonushealth;

        if (status.health > maxHealth)
        {
            status.health = maxHealth;
        }
    }

    public void TakeMana(int amount)
    {
        status.mana -= amount;
    }

    public void SetMana(int bonusmana)
    {
        status.mana += bonusmana;

        if (status.mana > maxMana)
        {
            status.mana = maxMana;
        }
    }

    public void RecoveryMana()
    {
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            timer = 0;
            if (status.mana < maxMana)
            {
                status.mana += timeRecoveryMana;
            }
        }
    }

    public void TakeStamina(float amount)
    {
        if (status.stamina > 0)
        {
            status.stamina -= amount;
        }
    }

    public void SetStamina(float bonusstamina)
    {
        status.stamina += bonusstamina;

        if (status.stamina > maxStamina)
        {
            status.stamina = maxStamina;
        }
    }
}
