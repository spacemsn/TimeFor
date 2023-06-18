using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class Slot : MonoBehaviour
{
    public static Action<Slot> onPuttingArtifact;
    public static Action<Slot> onTakingArtifact;

    public static Action<Slot> onUseQuickSlot;

    public Item item;
    public foodItem foodItem;
    public ArtifactsObject artifact;
    public int Id;
    public int amount;
    public bool isEmpty = true;
    public GameObject drag;
    public GameObject icon;
    public TMP_Text itemAmount;

    public ItemType slotType;
    public ArtifactType artifactType;
    public bool isPut = false;

    public Color isPutColor;
    public Color isTakeColor;

    private void Start()
    {
        drag = transform.GetChild(0).gameObject;
        icon = drag.transform.GetChild(0).gameObject;
        itemAmount = drag.transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite sprite)
    {
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        icon.GetComponent<Image>().sprite = sprite;
    }

    public void OnPutItem()
    {
        if(item.type == ItemType.Food && item.type == slotType)
        {
            foodItem = (foodItem)item;
            onPuttingArtifact.Invoke(this);
            isPut = true;
            SetBuffIcon();
            Debug.Log("Надели на " + gameObject.name);
        }
        if(item.type == ItemType.Artifact && item.type == slotType)
        {
            artifact = (ArtifactsObject)item;
            if(artifact.artifact == artifactType)
            {
                onPuttingArtifact.Invoke(this);
                isPut = true;
                SetBuffIcon();
                Debug.Log("Надели на " + gameObject.name);
            }
        }
    }

    public void OnTakeItem()
    {
        if (slotType == ItemType.Food)
        {
            onTakingArtifact.Invoke(this);
            Debug.Log("Сняли с " + gameObject.name);
            isPut = false;
            SetDebuffIcon();
        }
        if (slotType == ItemType.Artifact)
        {
            onTakingArtifact.Invoke(this);
            Debug.Log("Сняли с " + gameObject.name);
            isPut = false;
            SetDebuffIcon();
        }
    }

    private void LateUpdate()
    {
        if (item != null && isPut == false)
        {
            OnPutItem();
        }
        else if (item == null && isPut == true)
        {
            OnTakeItem();
        }

        if(slotType == ItemType.Food && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
        {
            if(foodItem != null && amount >= 1)
            {
                onUseQuickSlot.Invoke(this);
                amount--; itemAmount.text = amount.ToString();
            }
            else
            {
                GetComponentInChildren<DragAndDropItem>().NullifySlotData(); 
            } 
        }
    }

    public void SetBuffIcon()
    {
        gameObject.GetComponent<Image>().color = isPutColor;
    }

    public void SetDebuffIcon()
    {
        gameObject.GetComponent<Image>().color = isTakeColor;
    }

}
