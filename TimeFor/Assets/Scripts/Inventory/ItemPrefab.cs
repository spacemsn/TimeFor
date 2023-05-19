using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab : MonoBehaviour
{
    public Item item;
    public int amount;

    public float radius;
    public LayerMask maskPlayer;

    public Collider[] Player;

    public Button buttonPrefab;
    public Transform buttonParent;
    public Button currentButton;

    private void Update()
    {
        Player = Physics.OverlapSphere(transform.position, radius, maskPlayer);
        if (Player.Length > 0 && currentButton == null)
        {
            currentButton = Instantiate(buttonPrefab, buttonParent); 
            currentButton.GetComponent<SelectObjectButton>().GetComponentItem(this, Player[0].gameObject); 
            currentButton.transform.GetChild(0).GetComponent<Text>().text = "(F)    " + item.name + " " + amount + "רע";

        }
        else if (Player.Length == 0 && currentButton != null)
        {
            Destroy(currentButton.gameObject);
        }
    }

    public void OnDestroy()
    {
        if (currentButton != null)
        {
            Destroy(currentButton.gameObject);
        }
    }
}
