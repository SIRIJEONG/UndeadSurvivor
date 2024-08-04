using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnerPoint;
    public SpawnData[] spawnData;

    int level;

    float timer;

    private void Awake()
    {
        spawnerPoint = GetComponentsInChildren<Transform>();    
    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(Gamemanager.instance.gameTime / 10f) , spawnData.Length - 1);


        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = Gamemanager.instance.pool.Get(0);
        enemy.transform.position = spawnerPoint[Random.Range(1, spawnerPoint.Length)].position ;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
