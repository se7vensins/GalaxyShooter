using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _xUpperLim;

    [SerializeField]
    private float _xLowerLim;

    [SerializeField]
    private float _yUpperLim;

    [SerializeField]
    private float _yLowerLim;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private GameObject _explosionPrefab;
    private Animator _anim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player = null");
        }
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator  = null");
        }
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= _yLowerLim)
        {
            float randomX = Random.Range(_xLowerLim, _xUpperLim);
            transform.position = new Vector3(randomX, _yUpperLim, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                _player.AddScore(5);
                player.Damage();
            }
            // _anim.SetTrigger("OnEnemyDeath");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            //_anim.SetTrigger("OnEnemyDeath");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;
            Destroy(this.gameObject);
        }

    }


}
