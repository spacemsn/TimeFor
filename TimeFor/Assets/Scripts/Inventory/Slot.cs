using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public FoodItem foodItem;
    public int Id;
    public int amount;
    public bool isEmpty = true;
    public GameObject drag;
    public GameObject icon;
    public TMP_Text itemAmount;

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
}
