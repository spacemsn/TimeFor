using UnityEngine;
using UnityEngine.UI;

public class CharacterIndicators : MonoCache
{
    float timer;

    private int health;
    public int maxHealth = 100;

    private float stamina;
    public float maxStamina = 100;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] dealthPanel dealthCharacter;
    [SerializeField] CharacterStatus status;

    private void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        dealthCharacter = GameObject.Find("Global Settings").GetComponent<dealthPanel>();
        status = GetComponent<CharacterStatus>();
    }

    public void Indicators(int health, float stamina)
    {
        this.health = health; this.stamina = stamina;
        healthBar.value = health; staminaBar.value = stamina;
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
