using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;

    public float spawnRadius = 0.5f;

    public GameObject[] slimes;
    public int spawnNumber = 5;
    public int enemyNumber;
    private void Start()
    {
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }


    private void Update()
    {
        if(enemyNumber <= 1)
        {
            SpawnSlimes();
        }
    }

    public void CountEnemy()
    {
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("current enemy number " + enemyNumber);
    }

    private void SpawnSlimes()
    {
        for(int i =0; i < spawnNumber; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(-2f, 2f));
            int randSlimeType = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randSlimeType];

            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        }

        CountEnemy();
    }


}
