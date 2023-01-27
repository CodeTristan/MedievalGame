using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Wave[] waves;

    private void Start()
    {
        StartCoroutine(SpawnWave(waves[0]));
    }
    public IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.circles.Length; i++)
        {
            Instantiate(wave.circles[i].prefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(wave.circles[i].nextCircleSpawnDelay);
        }
    }
}
