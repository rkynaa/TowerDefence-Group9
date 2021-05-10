using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, ENDED }

    [System.Serializable]
    public class Wave
    {
        public EnemyEntity enemy;
        public int count;
        public float delay;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private SpawnState state = SpawnState.ENDED;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if(!IsEnemyAlive())
            {
                // Finish round
                state = SpawnState.ENDED;
            } 
            else
            {
                return; // Wait for player to kill enemies
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.delay);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    bool IsEnemyAlive()
    {
        return GameMaster.EnemiesAlive > 0;
    }

    void SpawnEnemy(EnemyEntity _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _spawn.position, _spawn.rotation);
    }
}
