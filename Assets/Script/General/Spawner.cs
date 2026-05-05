using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    public enum MonsterType

    {
        Goblin =3,
        Bat=5,
        Spiker=6
    }


    [Header("Couter")]
    public int spikerCount; //Calculates enemy count, stops spawning when reaching the limit
    [Header("")]
    private Transform maxSpawnPoint;
    private Transform minSpawnPoint;
    [Header("Timer")]
    public float levelTimer;
    public float spawnTimer1;
    public float spawnTimer2;

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
        spawnTimer1 += Time.deltaTime;
        spawnTimer2 += Time.deltaTime;

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


                if (spawnTimer1 > 2)
                {
                    spawnTimer1 = 0;
                    GameObject goblin = GameManager.instance.poolManager.Get((int)MonsterType.Goblin);
                    goblin.GetComponent<Goblin>().Init(10, 3, 1); //Parameter:  maxHealth, Speed, Damage
                    goblin.transform.position = SpawnPonit();
                }
                break;


            case 2:
                if (spawnTimer2 > 15) 
                {
                    spawnTimer2 = 0;
                    SpawnCluster((int)MonsterType.Bat, 10, 6, 1);//Parameter: Prefab Index, maxHealth, Speed, Damage
                }



                if (spawnTimer1 > 1)
                {
                    
                    spawnTimer1 = 0;
                    if (spikerCount <= 10)
                    {
                        GameObject spiker = GameManager.instance.poolManager.Get((int)MonsterType.Spiker);
                        spiker.GetComponent<Goblin>().Init(30, 0, 1);
                        spiker.transform.position = SpawnPonit();
                        spikerCount++;
                    }

                    GameObject goblin = GameManager.instance.poolManager.Get((int)MonsterType.Goblin);
                    goblin.GetComponent<Goblin>().Init(15, 3, 1);
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

    private void SpawnCluster(int mosterPrefab, float maxHealth, float speed, float damage) //Batch spawn clustered Monster
    {
        int waveCount = 25;
        Vector3 spawnPosition = SpawnPonit();
        for (int i = 0; i < waveCount; i++)
        {
            GameObject moster = GameManager.instance.poolManager.Get(mosterPrefab);

            moster.GetComponent<Goblin>().Init(maxHealth, speed, damage);


            spawnPosition.x += Random.Range(-0.3f, 0.3f);
            spawnPosition.y += Random.Range(-0.3f, 0.3f);

            moster.transform.position = spawnPosition;
            moster.GetComponent<Goblin>().moveDirection= (GameManager.instance.player.transform.position - moster.transform.position).normalized;
        }

    }
}


