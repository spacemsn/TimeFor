using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    public EntryPoint entry;

    private void Start()
    {
        entry = FindObjectOfType<EntryPoint>();
    }

    public void CloseEducation()
    {
        this.gameObject.SetActive(false);
        if (entry != null)
        {
            entry.player.globallEntry.globall.notVisible();
        }
    }
}
