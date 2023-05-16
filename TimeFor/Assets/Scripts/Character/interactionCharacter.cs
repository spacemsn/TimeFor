using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
{
    [Header("Кнопки")]
    [SerializeField] private Image imageE;
    [SerializeField] private Image imageT;

    [Header("Радиус взаимодействия")]
    [SerializeField] private float radius;
    [SerializeField] private bool isGizmos = true;

    [Header("Дальность взаимодействия")]
    [SerializeField] private float maxDistance;
    private Ray ray;
    private RaycastHit hit;

    [Header("Компоненты")]
    public GameObject GlobalSettings;
    private moveCharacter move;
    public Collider[] Interactions;
    public Collider[] NPC;

    public LayerMask maskNPC;
    public LayerMask maskInteractions;

    [Header("Диалог")]
    private DialogManager dialog;

    public List<Button> selectButtons;
    public Button oldButton;
    public Button currentButton;
    public Transform buttonParent;
    public int selectedIndex;

    public KeyCode keyOne;
    public KeyCode keyTwo;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
        dialog = GetComponent<DialogManager>();
    }

    private void Ray()
    {
        ray = new Ray(transform.position + new Vector3(0, 1f, 0), transform.forward);
    }

    private void DrawRay()
    {
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        }

        if (hit.transform == null)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // Метод взаимодействия с действиями 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            //imageE.enabled = true;
            //imageT.enabled = true;

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }

    private void Radius() // Метод взаимодействия с объектами через радиус 
    {
        var Inventory = gameObject.GetComponent<bookCharacter>();

        Interactions = Physics.OverlapSphere(transform.position, radius, maskInteractions);

        for (int i = 0; i < Interactions.Length; i++)
        {
            Rigidbody rigidbodyInteractions = Interactions[i].attachedRigidbody;
            if (rigidbodyInteractions != null)
            {
                imageT.enabled = true;

                if (Input.GetKeyDown(KeyCode.E)) // Взять в руку предмет
                {
                    if (rigidbodyInteractions.GetComponent<Rigidbody>() != null)
                    {
                        //rigidbodyInteractions.GetComponent<Interactions>().PickUp();
                    }
                }
                //if (Input.GetKeyDown(KeyCode.T)) // Взять предмет в инвентарь
                //{
                //    ItemPrefab item = rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>();
                //    if (rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>() != null)
                //    {
                //        Inventory.AddItem(item.item, item.amount);
                //    }
                //    Destroy(rigidbodyInteractions.gameObject);
                //}
            }
            else { imageT.enabled = false; }
        }

        NPC = Physics.OverlapSphere(transform.position, radius, maskNPC);

        for (int i = 0; i < NPC.Length; i++)
        {
            Rigidbody rigidbodyInteractions = NPC[i].attachedRigidbody;
            if (rigidbodyInteractions != null)
            {
                imageE.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (NPC[i].GetComponent<NPCBehaviour>())
                    {
                        NPCBehaviour NPCbehaviour = NPC[i].GetComponent<NPCBehaviour>();
                        transform.LookAt(NPCbehaviour.transform, new Vector3(0, transform.position.y, 0));
                        dialog.StartDialog(NPCbehaviour);
                    }
                }
            }
            else { imageE.enabled = false; }
        }
    }

    private void OnDrawGizmos()
    {
        if (isGizmos == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 1f, 0), radius);
        }
    }

    public override void OnUpdate()
    {
        Ray();
        DrawRay();
        Interact();
        Radius();

        selectButtons = buttonParent.GetComponentsInChildren<Button>().ToList();


        if (Input.GetKeyDown(keyOne))
        {
            // Получаем индекс выбранной кнопки меню
            selectedIndex = selectButtons.IndexOf(currentButton);

            // Если выбрана последняя кнопка, выбираем первую
            if (selectedIndex >= selectButtons.Count - 1)
            {
                selectedIndex = 0;
            }
            else
            {
                // Выбираем следующую кнопку
                selectedIndex++;
            }

            // Выбираем объект, связанный с выбранной кнопкой
            SelectObject(selectButtons[selectedIndex]);
        }
        else if (Input.GetKeyDown(keyTwo))
        {
            // Получаем индекс выбранной кнопки меню
            selectedIndex = selectButtons.IndexOf(currentButton);

            // Если выбрана первая кнопка, выбираем последнюю
            if (selectedIndex <= 0)
            {
                selectedIndex = selectButtons.Count - 1;
            }
            else
            {
                // Выбираем предыдущую кнопку
                selectedIndex--;
            }

            // Выбираем объект, связанный с выбранной кнопкой
            SelectObject(selectButtons[selectedIndex]);
        }

    }

    void SelectObject(Button obj)
    {
        // Сбрасываем выделение всех объектов
        foreach (Button selectableObj in selectButtons)
        {
            // Меняем цвет на обычный
            selectableObj.transform.GetComponent<Image>().color = Color.white;
        }

        // Выбираем новый объект
        if(currentButton != null) { oldButton = currentButton; oldButton.GetComponent<SelectObjectButton>().isSelect(); }
        currentButton = obj; currentButton.GetComponent<SelectObjectButton>().isSelect();

        // Выделяем выбранный объект
        currentButton.GetComponent<Image>().color = Color.green;

    }
}
