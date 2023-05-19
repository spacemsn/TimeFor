using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjectButton : MonoBehaviour
{
    public ItemPrefab item;
    public NPCBehaviour npc;

    private GameObject player;
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
    }

    public void OnButtonItem()
    {
        if (isSelected)
        {
            var Inventory = player.GetComponent<bookCharacter>();
            if (item != null)
            {
                Inventory.AddItem(item.item, item.amount);
                Destroy(item.gameObject);
            }
        }
    }

    public void OnButtonNPC()
    {
        if (isSelected)
        {
            var Dialog = player.GetComponent<DialogManager>();
            if (npc != null)
            {
                NPCBehaviour NPCbehaviour = npc.GetComponent<NPCBehaviour>();
                player.transform.LookAt(NPCbehaviour.transform, new Vector3(0, transform.position.y, 0));
                Dialog.StartDialog(NPCbehaviour);
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
}
