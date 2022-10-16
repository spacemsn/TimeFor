using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    static SceneLoad load;

    Animator animator;
    AsyncOperation operation;

    public static void SwitchScene(string name)
    {
        load.animator.SetTrigger("ScreenLoad End");
    
        //load.operation = SceneLoad
    }

    private void Start()
    {
        load = this;

        animator = GetComponent<Animator>();
    }
}
