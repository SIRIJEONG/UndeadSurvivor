using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    public ObjectPoolManager pool;
    public PlayerMove player;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
