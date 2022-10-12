using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    void Update()
    {
        for(int i = 0; i < MonoCache.allUpdate.Count; i++) { MonoCache.allUpdate[i].Tick(); }
    }
}
