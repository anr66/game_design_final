using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float initialWaitTime = 15f;
    public float waveWaitTime = 5f;

    public GameObject[] enemyPrefabs;
    public Transform firstWaypoint;

    public Text waveText;

    List<Wave> waves = new List<Wave>();
    int currentWave = -1;


    void Awake()
    {
        GetWaves();
        StartCoroutine(WaveLoop());
    }

    void GetWaves()
    {
        int child = 0;

        // loop through each child in spawner
        while (child < transform.childCount)
        {
            // get the wave script
            waves.Add(transform.GetChild(child).GetComponent<Wave>());
            child++;
        }

    }


    IEnumerator WaveLoop()
    {
        // pause before start
        yield return new WaitForSeconds(initialWaitTime);

        // loop through each wave
        while (currentWave < waves.Count)
        {
            // increase wave counter
            currentWave++;
            waveText.text = "Wave " + (currentWave + 1) + "/" + waves.Count;

            // start wave
            yield return StartCoroutine(StartNextWave());        
        }
    }


    IEnumerator StartNextWave()
    {
        // start spawning
        foreach (WaveSegment segment in waves[currentWave].PatternToWaveSegments())
        {
            // spawn enemies
            yield return StartCoroutine(SpawnEnemies(segment.spawns));

            // wait at the end
            yield return new WaitForSeconds(segment.wait);
        }

        // wait between waves
        yield return new WaitForSeconds(waveWaitTime);

    }

    IEnumerator SpawnEnemies(List<int> enemies)
    {
        // loop through enemies
        foreach (int enemy in enemies)
        {

            // create enemy and allocate waypoint
            GameObject newEnemy = Instantiate(enemyPrefabs[enemy], transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().currentWaypoint = firstWaypoint;

            // wait for next enemy
            yield return new WaitForSeconds(0.5f);

        }
    }

  
}
