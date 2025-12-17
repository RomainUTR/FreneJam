using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [TitleGroup("Configuration")]
    [SerializeField, Required] private Transform spawnPoint;
    [SerializeField, Required] private Transform[] enemyPrefabs;

    [TitleGroup("Wave Settings")]
    [SerializeField, Range(0f, 10f)] private float timeBetweenWaves = 5f;
    [SerializeField, Range(0f, 5f)] private float countdown = 2f;
    [SerializeField] private int waveIndex = 0;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        Debug.Log("Vague dans : " + Mathf.Round(countdown));
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log("Vague " + waveIndex + " en approche !");

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        int i = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[i], spawnPoint.position, spawnPoint.rotation);
    }
}
