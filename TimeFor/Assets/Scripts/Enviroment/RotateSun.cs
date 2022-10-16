using UnityEngine;

public class RotateSun : MonoCache
{
    public float speed = 3.0f;
    [SerializeField] Vector3 rotate;

    void Update()
    {
        transform.Rotate(rotate * speed * Time.deltaTime);
    }
}
