using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoCache
{
    float timer;

    public int health;
    public int maxHealth = 100;

    public int mana;
    public int maxMana = 100;
    public int timeRecoveryMana = 1;

    public float stamina;
    public float maxStamina = 100;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider manaBar;
    [SerializeField] Slider staminaBar;

    public DealthCharacter dealthCharacter;

    public override void OnTick()
    {
        healthBar.value = health;
        manaBar.value = mana;
        staminaBar.value = stamina;

        RecoveryMana();
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            dealthCharacter.OpenMenu();
        }
    }

    public void SetHealth(int bonushealth)
    {
        health += bonushealth;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeMana(int amount)
    {
        mana -= amount;
    }

    public void SetMana(int bonusmana)
    {
        mana += bonusmana;

        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }

    public void RecoveryMana()
    {
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            timer = 0;
            if (mana < maxMana)
            {
                mana += timeRecoveryMana;
            }
        }
    }

    public void TakeStamina(float amount)
    {
        if (stamina > 0)
        {
            stamina -= amount;
        }
    }

    public void SetStamina(float bonusstamina)
    {
        stamina += bonusstamina;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
}
