using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _rotatespeed;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager = null");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player = null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotatespeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            ExplosionInitiate();
            Destroy(other.gameObject);
        }
        else
        {
            _player.Damage();
            ExplosionInitiate();
        }
    }
    private void ExplosionInitiate()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _spawnManager.StartSpawn();
        Destroy(this.gameObject, 0.25f);
    }
}
