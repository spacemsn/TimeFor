using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;

public class Interactions : MonoCache
{
    [Header("Рука")]
    public GameObject armSmall;
    public GameObject armLarge;
    public bool flag = false;
    private Rigidbody rb;
    public BoxCollider boxCollider;

    [Header("Rig's")]
    public GameObject player;
    public RigBuilder rbBuilder;

    

    public enum sizeItem
    {
        small = 0,
        large = 1
    }
    public sizeItem size = sizeItem.small;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        armSmall = GameObject.Find("ArmSmall");
        armLarge = GameObject.Find("ArmLarge");

        player = GameObject.FindGameObjectWithTag("Player");
        rbBuilder = player.GetComponent<RigBuilder>();
    }

    public void PickUp()
    {
        if (size == sizeItem.small)
        {
            if (armSmall.transform.childCount < 1 && armLarge.transform.childCount < 1)
            {
                if (this.gameObject.GetComponent<ItemPrefab>().item.type == ItemType.Weapon)
                {
                    //player.GetComponent<CharacterMove>().walkType = CharacterMove.WalkType.withWeapon;
                    player.GetComponent<Animator>().SetBool("GetWeapon", true);
                    boxCollider.isTrigger = true;
                }
                rbBuilder.layers[2].active = false;
                rbBuilder.layers[1].active = true;
                transform.SetParent(armSmall.transform);
                transform.position = armSmall.transform.position;
                transform.rotation = armSmall.transform.rotation;
                rb.isKinematic = true;
                flag = true;
            }
        }
        else if (size == sizeItem.large)
        {
            if (armLarge.transform.childCount < 1 && armSmall.transform.childCount < 1)
            {
                if (this.gameObject.GetComponent<ItemPrefab>().item.type == ItemType.Weapon)
                {
                    //splayer.GetComponent<CharacterMove>().walkType = CharacterMove.WalkType.withWeapon;
                    player.GetComponent<Animator>().SetBool("GetWeapon", true);
                    boxCollider.isTrigger = true;

                }
                rbBuilder.layers[1].active = false;
                rbBuilder.layers[2].active = true;
                transform.SetParent(armLarge.transform);
                transform.position = armLarge.transform.position;
                transform.rotation = armLarge.transform.rotation;
                rb.isKinematic = true;
                flag = true;
            }
        }
    }

    public void Throw()
    {
        if(flag == true)
        {
            if (this.gameObject.GetComponent<ItemPrefab>().item.type == ItemType.Weapon)
            {
                //player.GetComponent<CharacterMove>().walkType = CharacterMove.WalkType.noWeapon;
                player.GetComponent<Animator>().SetBool("GetWeapon", false);
                boxCollider.isTrigger = false;
            }
            transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 100);
            flag = false;
        }
    }

    public void Release()
    {
        if (flag == true)
        {
            if (this.gameObject.GetComponent<ItemPrefab>().item.type == ItemType.Weapon)
            {
                //player.GetComponent<CharacterMove>().walkType = CharacterMove.WalkType.noWeapon;
                player.GetComponent<Animator>().SetBool("GetWeapon", false);
                boxCollider.isTrigger = false;
            }
            rbBuilder.layers[1].active = false;
            rbBuilder.layers[2].active = false;
            transform.parent = null;
            rb.isKinematic = false;
            flag = false;
        }
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.T))
    //    {
    //        Release();
    //    }
    //}

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Release();
        }
    }
}
