using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
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
    private CharacterMove characterMove;

    private void Start()
    {
        characterMove = GetComponent<CharacterMove>();
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
        var Inventory = GlobalSettings.GetComponent<InventoryPanel>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody && rigidbody.GetComponent<Rigidbody>().GetComponent<Interactions>() != null)
            {
                imageE.enabled = true;
                imageT.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (rigidbody.GetComponent<Rigidbody>() != null)
                    {
                        rigidbody.GetComponent<Interactions>().PickUp();
                    }
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (rigidbody.gameObject.GetComponent<ItemPrefab>().food != null) { Inventory.AddItemFood(rigidbody.gameObject.GetComponent<ItemPrefab>().food, rigidbody.gameObject.GetComponent<ItemPrefab>().amount); }
                    else { Inventory.AddItem(rigidbody.gameObject.GetComponent<ItemPrefab>().item, rigidbody.gameObject.GetComponent<ItemPrefab>().amount); }
                    Destroy(rigidbody.gameObject);
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
