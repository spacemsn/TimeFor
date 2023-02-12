using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _freeCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private GameObject CenterPlayer;
    [SerializeField] private GameObject SpinePlayer;

    [SerializeField] private CharacterMove character;
    [SerializeField] private UsingAbilities abilities;

    private void Start()
    {
        #region Cinemachine search 

        character = GetComponent<CharacterMove>();
        abilities = GetComponent<UsingAbilities>();

        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _freeCamera = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
        _virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();


        character._camera = this._camera.transform;
        abilities._camera = this._camera;
        abilities.character = this.character;
        abilities.virtualCamera = this._virtualCamera;

        #endregion

        #region Cinemachine follow

        CenterPlayer = character.GetComponent<Transform>().GetChild(0).gameObject;
        SpinePlayer = character.GetComponent<Transform>().GetChild(1).gameObject;

        _freeCamera.Follow = CenterPlayer.transform;
        _freeCamera.LookAt = CenterPlayer.transform;

        _virtualCamera.Follow = SpinePlayer.transform;
        _virtualCamera.LookAt = SpinePlayer.transform;

        #endregion
    }
}
