using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpawner : MonoBehaviour
{
    public enum SpawnState { STARTING, SPAWNING, WAITING, ENDED }

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

    public FreeplayRound freeplayTemplate;

    [System.Serializable]
    public class FreeplayRound : Round
    {
        public int divisor = 1;
        private double[] multipliers;

        public void CalcRatios()
        {
            multipliers = new double[enemies.Length];

            for (int i = 0; i < enemies.Length; i++)
            {
                multipliers[i] = (double) enemies[i].count / divisor;
            }
        }

        public void Populate(int round)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].count = (int) (multipliers[i] * round);
            }
        }
    }

    public Round[] rounds;
    private int nextRound = 0;

    public Transform[] spawnPoints;

    private float timeBetweenRounds = 1f;
    private float roundCountdown;

    private const float minDelay = 0.01f;

    [HideInInspector]
    public SpawnState state = SpawnState.ENDED;

    private void Start()
    {
        roundCountdown = timeBetweenRounds;
        freeplayTemplate.CalcRatios();
    }

    public void StartRound()
    {
        if(state == SpawnState.ENDED)
        {
            state = SpawnState.STARTING;
        }
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
                GameMaster.instance.GainMoney(200);
                // TODO: Push round ended state to start round button here
            } 
            else
            {
                return; // Wait for player to kill enemies
            }
        }

        // TODO: swap this out with a next round button
        if(state == SpawnState.ENDED && GameMaster.autoNextRound)
        {
            if (roundCountdown <= 0)
            {
                state = SpawnState.STARTING;
            }
            else
            {
                roundCountdown -= Time.deltaTime;
            }
        }

        if (state == SpawnState.STARTING)
        {
            Debug.Log("Spawning round number " + nextRound);
            Round round;

            if (nextRound < rounds.Length)
            {
                round = rounds[nextRound];
            }
            else
            {
                freeplayTemplate.Populate(nextRound);
                round = freeplayTemplate;
                GameMaster.instance.enemyDifficulty += 0.4;
            }

            StartCoroutine(SpawnRound(round));

            nextRound += 1;
            // Display Here
        }

    }

    /// <summary>
    /// Spawns all the enemies in a round
    /// Must be put into a StartCoroutine() call
    /// </summary>
    /// <param name="_round">The round ehich contains enemies and timings</param>
    /// <returns></returns>
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
                if(j != rs.count - 1)
                {
                    if(rs.delay > 0)
                    {
                        yield return new WaitForSeconds(rs.delay);
                    }
                    else
                    {
                        yield return new WaitForSeconds(minDelay);
                    }
                }
            }
            if (rs.delayAfter > 0)
            {
                yield return new WaitForSeconds(rs.delayAfter);
            }
            else
            {
                yield return new WaitForSeconds(minDelay);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    /// <summary>
    /// Checks if any enemies are alive on the map
    /// </summary>
    /// <returns>True if enemies > 0</returns>
    bool IsEnemyAlive()
    {
        return GameMaster.instance.enemiesAlive.Count > 0;
    }

    /// <summary>
    /// Spawns an enemy at a random spawn point
    /// </summary>
    /// <param name="_enemy">The enemy prefab to spawn</param>
    void SpawnEnemy(EnemyEntity _enemy)
    {
        Transform _spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _spawn.position, _spawn.rotation);
    }
}
