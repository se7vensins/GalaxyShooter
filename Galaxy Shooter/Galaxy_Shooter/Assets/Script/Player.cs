using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _speedMultiplier;

    [SerializeField]
    private GameObject _laserPref;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _canFire;

    [SerializeField]
    private float _xUpperLim;

    [SerializeField]
    private float _xLowerLim;

    [SerializeField]
    private float _yUpperLim;

    [SerializeField]
    private float _yLowerLim;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private bool _tripleEnabled = false;

    [SerializeField]
    private bool _speedEnabled = false;

    [SerializeField]
    private bool _shieldEnabled = false;

    [SerializeField]
    private GameObject _tripleShotPref;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _ShieldVisuals;

    [SerializeField]
    private GameObject _rightFail, _leftFail;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    [SerializeField]
    private AudioClip _laserSound;

    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager = null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UI_Manager = null");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource = null");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }


    void Update()
    {
        Movement();
        MovementLimits();
        if ((Input.GetKey(KeyCode.Space) && Time.time > _canFire))
        {
            Shoot();
        }

    }


    public void Damage()
    {
        if(_shieldEnabled == true)
        {
            _shieldEnabled = false;
            _ShieldVisuals.SetActive(false);
            return; 
        }

        _lives--;

        if(_lives == 2)
        {
            _rightFail.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftFail.gameObject.SetActive(true);
        }


        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.playerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {

        _canFire = Time.time + _fireRate;

        if (_tripleEnabled == true)
        {
            Instantiate(_tripleShotPref, transform.position + new Vector3(-1.6f, -0.11f, 0), Quaternion.identity);
 
        }
        else
        {
            Instantiate(_laserPref, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    private void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 control = new Vector3(HorizontalInput, VerticalInput, 0);

        transform.Translate(control * _speed * Time.deltaTime);
    }
    private void MovementLimits()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yLowerLim, _yUpperLim), 0);
        if (transform.position.x >= _xUpperLim)
        {
            transform.position = new Vector3(_xLowerLim, transform.position.y, 0);
        }
        else if (transform.position.x <= _xLowerLim)
        {
            transform.position = new Vector3(_xUpperLim, transform.position.y, 0);
        }
    }

    public void TripleShotEnabled()
    {
        _tripleEnabled = true;
        StartCoroutine(TripleShotCD());
    }
    IEnumerator TripleShotCD()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleEnabled = false;
    }

    public void SpeedUPEnabled()
    {
        _speedEnabled = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedUpCD());
    }
    IEnumerator SpeedUpCD()
    {
        yield return new WaitForSeconds(5f);
        _speedEnabled = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldUpEnabled()
    {
        _shieldEnabled = true;
        _ShieldVisuals.gameObject.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

