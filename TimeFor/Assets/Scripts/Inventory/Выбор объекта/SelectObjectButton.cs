using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjectButton : MonoBehaviour
{
    public ItemPrefab item;

    private GameObject player;
    [SerializeField] private bool isSelected = false;

    private void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnButtonSelect(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && player != null) // Взять предмет в инвентарь
        {
            OnButtonSelect();
        }
    }

    public void OnButtonSelect()
    {
        if (isSelected)
        {
            var Inventory = player.GetComponent<bookCharacter>();
            if (item != null)
            {
                Inventory.AddItem(item.item, item.amount);
            }
            Destroy(item.gameObject);
        }
    }

    public void isSelect()
    {
        isSelected = !isSelected;
    }

    public void GetComponent(ItemPrefab item, GameObject player)
    {
        this.item = item;
        this.player = player;
    }
}
