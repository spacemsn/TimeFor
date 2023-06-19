using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotemTeleport : NPCBehaviour
{
    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.F) && Player[0] != null)
        {
            Player[0].GetComponent<DialogManager>().EndDialog();
            Destroy(currentButton.gameObject);
            maskPlayer.value = default;
            SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
