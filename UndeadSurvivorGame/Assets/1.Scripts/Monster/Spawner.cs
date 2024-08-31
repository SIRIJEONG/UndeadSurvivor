using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnerPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;

    float timer;

    private void Awake()
    {
        spawnerPoint = GetComponentsInChildren<Transform>();
        levelTime = Gamemanager.instance.maxGameTime / spawnData.Length;
    }

    private void Update()
    {
        if (!Gamemanager.instance.isLive)       
            return;
        
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(Gamemanager.instance.gameTime / levelTime) , spawnData.Length - 1);


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
