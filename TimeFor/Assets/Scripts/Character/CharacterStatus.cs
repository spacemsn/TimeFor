using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class CharacterStatus : MonoBehaviour
{
    [Header("CharacterStatus")]
    public Vector3 _position;

    [Header("CharacterPrefab")]
    public GameObject _characterPrefab; 

    [Header("CharacterLevel")]
    public int _levelId;

    [Header("CharacterIndicators")]
    public CharacterIndicators _indicators;
    public int _health;
    public int _mana;
    public float _stamina;

    [Header("CharacterMove")]
    public CharacterMove _move;
    public float _aimModeSpeed;
    public float _walkingSpeed;
    public float _runningSpeed;
    public float _normallSpeed;
    public float _jumpValue;
    public float _gravity;
    public float _smoothTime;
    public float _debuff;
    public float _smoothVelocity;
    public bool _charMenegment;

    private void Start()
    {
        #region GameObject

        _characterPrefab = this.gameObject;

        #endregion

        #region CharacterLevel

        _levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        _position = this.transform.position;

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        _health = _indicators.health;
        _mana = _indicators.mana;
        _stamina = _indicators.stamina;

        #endregion

        #region CharacterMove

        _move = this.GetComponent<CharacterMove>();

        _aimModeSpeed = _move.aimModeSpeed;
        _walkingSpeed = _move.walkingSpeed;
        _runningSpeed = _move.runningSpeed;
        _normallSpeed = _move.normallSpeed;
        _jumpValue = _move.jumpValue;
        _gravity = _move.gravity;
        _smoothTime = _move.smoothTime;
        _debuff = _move.debuff;
        _smoothVelocity = _move.smoothVelocity;
        _charMenegment = _move.charMenegment;

        #endregion

    }

    private void UpdateStatus()
    {
        #region CharacterLevel

        _levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        _position = this.transform.position;

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        _health = _indicators.health;
        _mana = _indicators.mana;
        _stamina = _indicators.stamina;

        #endregion

        #region CharacterMove

        _move = this.GetComponent<CharacterMove>();

        _aimModeSpeed = _move.aimModeSpeed;
        _walkingSpeed = _move.walkingSpeed;
        _runningSpeed = _move.runningSpeed;
        _normallSpeed = _move.normallSpeed;
        _jumpValue = _move.jumpValue;
        _gravity = _move.gravity;
        _smoothTime = _move.smoothTime;
        _debuff = _move.debuff;
        _smoothVelocity = _move.smoothVelocity;
        _charMenegment = _move.charMenegment;

        #endregion
    }

    //private void Update()
    //{
    //    _position = this.transform.position;
    //    _health = _indicators.health;
    //    _mana = _indicators.mana;
    //    _stamina = _indicators.stamina;
    //}

    public void SavePlayer()
    {
        UpdateStatus();
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        _levelId = data.level; 
        _health = data.health; _indicators.health = _health;
        _mana = data.mana; _indicators.mana = _mana;
        _stamina = data.stamina; _indicators.stamina = _stamina;
        _position = data.position; _move.transform.position = _position;
        _characterPrefab.transform.position = _position;
    }
}
