using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject [] _powerUpID;

    [SerializeField]
    private float _xUpperLim;

    [SerializeField]
    private float _xLowerLim;

    [SerializeField]
    private float _yUpperLim;

    [SerializeField]
    private float _yLowerLim;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private bool _stopSpawnEnemy = false;

    [SerializeField]
    private bool _spawnPowerUp = true;

    public void StartSpawn()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawnEnemy == false)
        {
            Vector3 SpawnPoint = new Vector3(Random.Range(_xLowerLim, _xUpperLim), _yUpperLim, 0);
            GameObject enemySpawn  =  Instantiate(_enemyPrefab, SpawnPoint, Quaternion.identity);
            enemySpawn.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);

        }
    }

    IEnumerator PowerUpSpawn()
    {
        yield return new WaitForSeconds(3f);
        while (_spawnPowerUp == true)
        {
            Vector3 Spawnpoint = new Vector3(Random.Range(_xLowerLim, _xUpperLim), _yUpperLim, 0);
            int randomPower = Random.Range(0,2);
            Instantiate(_powerUpID[randomPower], Spawnpoint, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    public void playerDeath()
    {
        _stopSpawnEnemy = true;
    }

}
