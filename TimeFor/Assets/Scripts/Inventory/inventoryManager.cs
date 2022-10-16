using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoCache
{
    public Transform inventoryPanel;
    public List<Slot> slots = new List<Slot>();
    [SerializeField] bool isOpenPanel;

    private void Start()
    {
        for(int i = 0; i < inventoryPanel.childCount; i++)
        {
            if(inventoryPanel.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isOpenPanel == true)
            {
                inventoryPanel.gameObject.SetActive(false); 
                isOpenPanel = false;
            }
            else if(isOpenPanel == false)
            {
                inventoryPanel.gameObject.SetActive(true);
                isOpenPanel = true;
            }
        }
    }
}
