using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
{
    [Header("Кнопки")]
    [SerializeField] private Image imageE;
    [SerializeField] private Image imageT;
    private Ray ray;
    private RaycastHit hit;

    [Header("Радиус взаимодействия")]
    [SerializeField] private float radius;
    [SerializeField] private bool isGizmos = true;

    [Header("Дальность взаимодействия")]
    [SerializeField] private float maxDistance;

    [SerializeField] GameObject GlobalSettings;
    private moveCharacter character;
    public Collider[] Interactions;
    public Collider[] NPC;

    public LayerMask maskNPC;
    public LayerMask maskInteractions;

    [Header("Диалог")]
    [SerializeField] private DialogManager dialog;

    private void Start()
    {
        character = GetComponent<moveCharacter>();
        dialog = GetComponent<DialogManager>();

        imageE = GameObject.Find("Button E").GetComponent<Image>();
        imageT = GameObject.Find("Button T").GetComponent<Image>();
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

    private void Radius() // Метод взаимодействия с объектами для передвижения
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
                if (Input.GetKeyDown(KeyCode.T)) // Взять предмет в инвентарь
                {
                    ItemPrefab item = rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>();
                    if (rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>() != null)
                    {
                        Inventory.AddItem(item.item, item.amount);
                    }
                    Destroy(rigidbodyInteractions.gameObject);
                }
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
                        dialog.StartDialog();
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
    }
}
