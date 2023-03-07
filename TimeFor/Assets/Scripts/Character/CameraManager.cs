using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public Camera _camera;
    [SerializeField] public CinemachineFreeLook _freeCamera;

    [SerializeField] private GameObject CenterPlayer;
    [SerializeField] private GameObject SpinePlayer;

    [SerializeField] private CharacterStatus status;
    [SerializeField] private CharacterAbilities abilities;

    private void Start()
    {
        #region Cinemachine search 

        status = GetComponent<CharacterStatus>();
        abilities = GetComponent<CharacterAbilities>();

        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _freeCamera = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();


        abilities._camera = this._camera;
        abilities.status = this.status;

        #endregion

        //#region Cinemachine follow

        //CenterPlayer = status.GetComponent<Transform>().GetChild(0).gameObject;
        //SpinePlayer = status.GetComponent<Transform>().GetChild(1).gameObject;

        //_freeCamera.Follow = CenterPlayer.transform;
        //_freeCamera.LookAt = CenterPlayer.transform;

        //_virtualCamera.Follow = SpinePlayer.transform;
        //_virtualCamera.LookAt = SpinePlayer.transform;

        //#endregion
    }
}
