using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAttack : MonoCache
{
    public float speed = 3.0f;
    [SerializeField] Vector3 rotate;

    void Start()
    {

    }

    public override void OnTick()
    {
        transform.Rotate(rotate * speed * Time.deltaTime);
    }
}
