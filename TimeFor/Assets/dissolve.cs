using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolve : MonoBehaviour
{
    public MeshRenderer Mesh;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;


    private Material[] meshMaterials;


    // Start is called before the first frame update
    void Start()
    {
        if (Mesh != null)
            meshMaterials = Mesh.materials;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            StartCoroutine(DissolveCo());
        }

         IEnumerator DissolveCo() 
        {
            if (meshMaterials.Length > 0) 
            {
                float counter = 0;
                while (meshMaterials[0].GetFloat("_Float") < 1)
                {
                    counter += dissolveRate;
                    for (int i = 0; i < meshMaterials.Length; i++) 
                    {
                        meshMaterials[i].SetFloat("_Float", counter);
                    }

                    yield return new WaitForSeconds(refreshRate);

                }
            }
        }
    }
}
