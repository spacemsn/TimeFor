using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoCache
{
    [Header("Дальность взаимодействия")]
    private float maxDistance;
    [SerializeField] private Image imageE;
    [SerializeField] private Image imageT;
    private Ray ray;
    private RaycastHit hit;

    [Header("Радиус взаимодействия")]
    [SerializeField] private float radius;
    [SerializeField] private bool isGizmos = true;

    [SerializeField] GameObject GlobalSettings;
    private CharacterMove character;
    public Collider[] Interactions;
    public Collider[] NPC;

    public LayerMask maskNPC;
    public LayerMask maskInteractions;

    private void Start()
    {
        character = GetComponent<CharacterMove>();
        GlobalSettings = GameObject.Find("Global Settings");

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
            imageE.enabled = false;
            imageT.enabled = false;

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // Метод взаимодействия с действиями 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            imageE.enabled = true;
            imageT.enabled = true;

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }

    private void Radius() // Метод взаимодействия с объектами для передвижения
    {
        var Inventory = gameObject.GetComponent<InventoryScript>();

        Interactions = Physics.OverlapSphere(transform.position, radius, maskInteractions);

        for (int i = 0; i < Interactions.Length; i++)
        {
            Rigidbody rigidbodyInteractions = Interactions[i].attachedRigidbody;
            if (rigidbodyInteractions != null)
            {
                imageE.enabled = true;
                imageT.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (rigidbodyInteractions.GetComponent<Rigidbody>() != null)
                    {
                        rigidbodyInteractions.GetComponent<Interactions>().PickUp();
                    }
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ItemPrefab item = rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>();
                    if (rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>() != null)
                    {
                        Inventory.AddItem(item.item, item.amount);
                    }
                    Destroy(rigidbodyInteractions.gameObject);
                }
            }
        }

        NPC = Physics.OverlapSphere(transform.position, radius, maskNPC);

        for (int i = 0; i < NPC.Length; i++)
        {
            imageE.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(NPC[i].GetComponent<NPCBehaviour>())
                {
                    NPCBehaviour NPCbehaviour = NPC[i].GetComponent<NPCBehaviour>();
                    transform.LookAt(NPCbehaviour.transform, new Vector3( 0, transform.position.y, 0));
                    NPCbehaviour.StartDialog();
                }
            }
        }
    }

    public void ButtonE()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.GetComponent<Interactions>().PickUp();
            }

        }
    }

    public void ButtonT()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.GetComponent<Interactions>().Release();
            }

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

    public override void OnTick()
    {
        Ray();
        DrawRay();
        Interact();
        Radius();
    }
}
