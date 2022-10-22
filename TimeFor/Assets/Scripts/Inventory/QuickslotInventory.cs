using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoCache
{
    // Объект у которого дети являются слотами
    public Transform quickslotParent;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    // Update is called once per frame
    public override void OnTick()
    {
        // Используем цифры
        for(int i = 0; i < quickslotParent.childCount; i++)
        {
            // если мы нажимаем на клавиши 1 по 5 то...
            if (Input.GetKeyDown((i + 1).ToString())) {
                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
                if (currentQuickslotID == i)
                {
                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    }
                    //else
                    //{
                    //    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    //}
                }
                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                }
            }
        }
        // Используем предмет по нажатию на левую кнопку мыши
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().foodItem != null && quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().foodItem)
        //    {
        //        if (!rayCharacter.isOpenPanel && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
        //        {
        //            // Применяем изменения к здоровью (будущем к голоду и жажде) 
        //            ChangeCharacteristics();

        //            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount <= 1)
        //            {
        //                quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
        //            }
        //            else
        //            {
        //                quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount--;
        //                quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().itemAmount.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<Slot>().amount.ToString();
        //            }
        //        }
        //    }
        //}
    }
}
