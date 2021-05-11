using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, ENDED }

    [System.Serializable]
    public class Round
    {
        public RoundSegment[] enemies;
    }

    [System.Serializable]
    public class RoundSegment
    {
        /// <summary>
        /// The enemy type to spawn
        /// </summary>
        public EnemyEntity enemy;

        /// <summary>
        /// The number of enemies of enemy type to spawn in this segment
        /// </summary>
        public int count;

        /// <summary>
        /// The delay between enemy spawns
        /// </summary>
        public float delay;

        /// <summary>
        /// Delay after spawning this segment in seconds
        /// </summary>
        public float delayAfter;
    }

    public Round[] rounds;
    private int nextRound = 0;

    public Transform[] spawnPoints;

    public float timeBetweenRounds = 5f;
    private float roundCountdown;

    private SpawnState state = SpawnState.ENDED;

    private void Start()
    {
        roundCountdown = timeBetweenRounds;
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

        if (roundCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                Debug.Log("Spawning round number " + nextRound);
                StartCoroutine(SpawnRound(rounds[nextRound]));

                if (nextRound < rounds.Length - 1)
                {
                    nextRound += 1;
                }
            }
        }
        else
        {
            roundCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnRound(Round _round)
    {
        state = SpawnState.SPAWNING;

        // Spawn
        for(int i = 0; i < _round.enemies.Length; i++)
        {
            RoundSegment rs = _round.enemies[i];
            for (int j = 0; j < rs.count; j++)
            {
                SpawnEnemy(rs.enemy);
                if(rs.delay > 0 && j != rs.count - 1)
                {
                    yield return new WaitForSeconds(rs.delay);
                }
            }
            if (rs.delayAfter > 0)
            {
                yield return new WaitForSeconds(rs.delayAfter);
            }
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
        Transform _spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _spawn.position, _spawn.rotation);
    }
}
