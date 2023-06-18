using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Slot oldSlot;
    [SerializeField] private Transform player;
    [SerializeField] GameObject itemObject;

    private void OnEnable()
    {
        player = GameObject.FindObjectOfType<PlayerEntryPoint>().currentPlayer.transform;
        oldSlot = transform.GetComponentInParent<Slot>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Если слот пустой, то мы не выполняем то что ниже return;
        if (oldSlot.isEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        //Делаем картинку прозрачнее
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        if (eventData.pointerCurrentRaycast.gameObject.name == "InventarPanel")
        {
            for (int i = 0; i < oldSlot.amount; i++)
            {
                // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
                itemObject = Instantiate(oldSlot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                itemObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * 5, ForceMode.Impulse);
            }
            // убираем значения InventorySlot
            NullifySlotData();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>());
        }
       
    }
    public void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.item = null;
        oldSlot.foodItem = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.icon.GetComponent<Image>().sprite = null;
        oldSlot.itemAmount.text = "";
    }
    void ExchangeSlotData(Slot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        Item item = newSlot.item;
        foodItem foodItem = newSlot.foodItem;
        ArtifactsObject artifacts = newSlot.artifact;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject icon = newSlot.icon;
        TMP_Text itemAmountText = newSlot.itemAmount;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.item = oldSlot.item;
        newSlot.foodItem = oldSlot.foodItem;
        newSlot.artifact = oldSlot.artifact;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.icon.GetComponent<Image>().sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.icon.GetComponent<Image>().sprite = null;
            newSlot.itemAmount.text = "";
        }
        
        newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.item = item;
        oldSlot.amount = amount;
        oldSlot.foodItem = foodItem;
        oldSlot.artifact = artifacts;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(icon.GetComponent<Image>().sprite);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.icon.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }
        
        oldSlot.isEmpty = isEmpty;
    }
}
