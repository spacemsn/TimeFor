using UnityEngine;

public class Ballistics : MonoCache
{
    [SerializeField] float Power = 100;

    Camera mainCamera;
    [SerializeField] GameObject player;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public override void OnTick()
    {
        float enter;
        Ray ray = Camera.main.ScreenPointToRay(Input.mouseScrollDelta);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        player.transform.rotation = Quaternion.LookRotation(speed);

    }
}
