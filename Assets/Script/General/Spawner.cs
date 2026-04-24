using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform maxSpawnPoint;
    private Transform minSpawnPoint;
    public float levelTimer;
    public float spawnTimer;
    public int level;
    private void Awake()
    {
        level = 1;
        maxSpawnPoint = transform.Find("MaxPoint");
        minSpawnPoint = transform.Find("MinPoint");

    }
    private void Update()
    {
        Spawn();
    }
    private void Spawn()
    {
        spawnTimer += Time.deltaTime;
        if (level <2)
        {
            levelTimer += Time.deltaTime;
            if (levelTimer > 30)
            {
                levelTimer = 0;
                level++;
            }
        }

        switch (level)
        {
            case 1:
                if(spawnTimer > 2)
                {
                    spawnTimer = 0;
                    GameObject goblin = GameManager.instance.poolManager.Get(3);
                    goblin.GetComponent<Goblin>().Init(10, 3, 1);
                    goblin.transform.position = SpawnPonit();
                }
                break;
            case 2:
                if(spawnTimer > 1)
                {
                    spawnTimer = 0;
                    GameObject goblin = GameManager.instance.poolManager.Get(3);
                    goblin.GetComponent<Goblin>().Init(20, 3, 1);
                    goblin.transform.position = SpawnPonit();
                }
                break;
        }
    }

    private Vector3 SpawnPonit()
    {
    Vector3 spawnPoint= Vector3.zero;
        if(Random.Range(0f,1f)>0.5f)
        {
            spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
            if(Random.Range(0f, 1f) > 0.5f)
                spawnPoint.y = minSpawnPoint.position.y;
            else
                spawnPoint.y= maxSpawnPoint.position.y;
        }
        else
        {
            spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
            if (Random.Range(0f, 1f) > 0.5f)
                spawnPoint.x = minSpawnPoint.position.x;
            else
                spawnPoint.x = maxSpawnPoint.position.x;

        }
        return spawnPoint;
    }
}
