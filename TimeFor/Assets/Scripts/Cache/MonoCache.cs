using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allUpdate = new List<MonoCache>(10001);
    public static List<MonoCache> allFixedUpdate = new List<MonoCache>(10001);

    private void OnEnable() { allUpdate.Add(this); allFixedUpdate.Add(this); }

    private void OnDisable() { allUpdate.Remove(this); allFixedUpdate.Remove(this); }

    private void OnDestroy() { allUpdate.Remove(this); allFixedUpdate.Remove(this); }


    public void Tick() { OnUpdate(); }

    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
}
