using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjectButton : MonoBehaviour
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    public ItemPrefab item;
    public NPCBehaviour npc;
    public Chest chest;

    private GameObject player;
    public GameObject book;
    [SerializeField] private bool isSelected = false;

    private void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnButtonItem(); });
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player != null) // Взять предмет в инвентарь
        {
            OnButtonItem();
        }
        if (Input.GetKeyDown(KeyCode.F) && player != null) // Поговорить с npc
        {
            OnButtonNPC();
        }
        if (Input.GetKeyDown(KeyCode.F) && player != null) // Поговорить с npc
        {
            OnButtonChest();
        }
    }

    public void OnButtonItem()
    {
        if (isSelected)
        {
            book = GameObject.Find("Book");
            if (item != null)
            {
                var Inventory = book.GetComponent<bookCharacter>();
                Inventory.AddItem(item.item, item.amount);
                Destroy(item.gameObject);
            }
        }
    }

    public void OnButtonNPC()
    {
        if (isSelected)
        {
            if (npc != null)
            {
                var Dialog = player.GetComponent<DialogManager>();
                NPCBehaviour NPCbehaviour = npc.GetComponent<NPCBehaviour>();
                player.transform.LookAt(NPCbehaviour.transform, new Vector3(0, transform.position.y, 0));
                Dialog.StartDialog(NPCbehaviour);
            }
        }
    }

    public void OnButtonChest()
    {
        if (isSelected)
        {
            if (chest != null)
            {
                chest.GetComponent<Chest>().OpenChest();
                Destroy(chest.gameObject);
            }
        }
    }

    public void isSelect()
    {
        isSelected = !isSelected;
    }

    public void GetComponentItem(ItemPrefab item, GameObject player)
    {
        this.item = item;
        this.player = player;
    }

    public void GetComponentNPC(NPCBehaviour npc, GameObject player)
    {
        this.npc = npc;
        this.player = player;
    }

    public void GetComponentChest(Chest chest, GameObject player)
    {
        this.chest = chest;
        this.player = player;
    }
}
