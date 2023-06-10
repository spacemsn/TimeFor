using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public delegate void SceneLoadDelegate();
    public static event SceneLoadDelegate onSceneLoaded;
    private static bool isSceneLoaded = false;

    static SceneLoad load;

    Animator animator;
    AsyncOperation operation;

    public TextMeshProUGUI loadingText;
    public Image loadingImage;

    public static void SwitchScene(string name) // переход из одной сцены в другую
    {
        load.animator.SetTrigger("Start");
    
        load.operation = SceneManager.LoadSceneAsync(name);
        load.operation.allowSceneActivation = false;
    }

    public static void SwitchIndexScene(int index) // переход из одной сцены в другую
    {
        load.animator.SetTrigger("Start");

        load.operation = SceneManager.LoadSceneAsync(index);
        load.operation.allowSceneActivation = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        load = this;
        if (isSceneLoaded) 
        {
            //onSceneLoaded.Invoke();
            animator.SetTrigger("End");
        }
    }

    private void Update()
    {
        if (operation != null)
        {
            loadingText.text = Mathf.RoundToInt(operation.progress * 100) + "%";
            loadingImage.fillAmount = operation.progress;
        }
    }

    public void OnAnimationOver() // узнаем закончилась ли загрузка уровня 
    {
        isSceneLoaded = true;
        load.operation.allowSceneActivation = true;
    }
}
