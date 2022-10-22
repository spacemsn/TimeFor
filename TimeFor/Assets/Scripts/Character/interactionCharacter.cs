using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEditor.SceneManagement;

public class interactionCharacter : MonoCache
{
    [Header("Дальность взаимодействия")]
    [SerializeField] private float maxDistance;
    public Image imageE;
    public Image imageT;
    public Button buttonE;
    public Button buttonT;
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
        
        if(hit.transform == null)
        {
            if (characterMove.move == CharacterMove.Move.PC)
            {
                imageE.enabled = false;
                imageT.enabled = false;

            }
            else if (characterMove.move == CharacterMove.Move.Android)
{
                buttonE.image.enabled = false;
                buttonT.image.enabled = false;
            }
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // Метод взаимодействия с действиями 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            if (characterMove.move == CharacterMove.Move.PC)
            {
                imageE.enabled = true;
                imageT.enabled = true;

            }
            else if (characterMove.move == CharacterMove.Move.Android)
            {
                buttonE.image.enabled = true;
                buttonT.image.enabled = true;
            }
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
            if(rigidbody)
            {
                if (characterMove.move == CharacterMove.Move.PC)
                {
                    imageE.enabled = true;
                    imageT.enabled = true;

                }
                else if (characterMove.move == CharacterMove.Move.Android)
                {
                    buttonE.image.enabled = true;
                    buttonT.image.enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (rigidbody.GetComponent<Rigidbody>() != null)
                    {
                        rigidbody.GetComponent<Interactions>().PickUp();
                    }
                }
                if(Input.GetKeyDown(KeyCode.F))
                {
                    if(rigidbody.gameObject.GetComponent<ItemPrefab>().food != null) { Inventory.AddItemFood(rigidbody.gameObject.GetComponent<ItemPrefab>().food, rigidbody.gameObject.GetComponent<ItemPrefab>().amount); }
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

        if (characterMove.move == CharacterMove.Move.PC)
        {
            buttonE.gameObject.SetActive(false);
            buttonT.gameObject.SetActive(false);
            imageE.gameObject.SetActive(true);
            imageT.gameObject.SetActive(true);

        }
        else if (characterMove.move == CharacterMove.Move.Android)
        {
            imageE.gameObject.SetActive(false);
            imageT.gameObject.SetActive(false);
            buttonE.gameObject.SetActive(true);
            buttonT.gameObject.SetActive(true);
        }
    }
}
