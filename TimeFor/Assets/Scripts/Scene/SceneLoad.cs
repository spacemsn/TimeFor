using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public TextMeshProUGUI loadingT;
    public Image loadingI;

    static SceneLoad load;
    static bool isPlayAnim = false;

    Animator animator;
    AsyncOperation operation;

    public static void SwitchScene(string name)
    {
        load.animator.SetTrigger("Start");
    
        load.operation = SceneManager.LoadSceneAsync(name);
        load.operation.allowSceneActivation = false;
    }

    private void Start()
    {
        load = this;

        animator = GetComponent<Animator>();

        if (isPlayAnim) animator.SetTrigger("End");
    }

    private void Update()
    {
        if (operation != null)
        {
            loadingT.text = Mathf.RoundToInt(operation.progress * 100) + "%";
            loadingI.fillAmount = operation.progress;
        }
    }

    public void OnAnimationOver()
    {
        isPlayAnim = true;
        load.operation.allowSceneActivation = true;
    }
}
