using UnityEngine;
using UnityEngine.UI;

public class WanderingAI : MonoCache
{
    [Header("Характеристики")]
    [SerializeField] float range;
    [SerializeField] int consumption;
    [SerializeField] GameObject fireBallPref;
    [SerializeField] GameObject CenterPlayer;
    GameObject _fireball;

    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject DealthPanel;
    [SerializeField] GameObject PausePanel;

    [SerializeField] Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public override void OnTick()
    {
        if(InventoryPanel.activeSelf == false && DealthPanel.activeSelf == false && PausePanel.activeSelf == false)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowFireBall();
        }
    }

    void ThrowFireBall()
    {
        if(health.mana >= consumption)
        {
            _fireball = Instantiate(fireBallPref);
            _fireball.transform.position = CenterPlayer.transform.position;
            _fireball.transform.rotation = CenterPlayer.transform.rotation;
            _fireball.GetComponent<Rigidbody>().AddForce(transform.forward * range);

            health.TakeMana(consumption);
        }
    }

}
