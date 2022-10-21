using UnityEngine;
using UnityEngine.UI;

public class WanderingAI : MonoCache
{
    [Header("Характеристики")]
    [SerializeField] float timer = 0;
    [SerializeField] GameObject fireBallPref;
    [SerializeField] Transform rightHand;
    GameObject _fireball;

    [Header("Интерфейс")]
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject DealthPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Health health;

    [SerializeField] SkillObject skill;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public override void OnTick()
    {
        timer += Time.deltaTime;

        if (timer >= skill.attackRollback)
        {
            if (InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0;
                    ThrowFireBall();
                }
            }
        }
    }

    void ThrowFireBall()
    {
        if(health.mana >= skill.consumption)
        {
            _fireball = Instantiate(skill.objectPrefab, rightHand.position, rightHand.rotation);
            health.TakeMana(skill.consumption);
        }
    }

}
