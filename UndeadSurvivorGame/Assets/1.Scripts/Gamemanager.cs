using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public ObjectPoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public GameObject uiResult;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
        isLive = true;
    }

    public void GameOver()
    {

        StartCoroutine(GameOverRoutine());

    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);
        
        uiResult.SetActive(true);
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(!isLive)
        {
            return;
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[Mathf.Min(level , nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;  //timeScale : 유니티의 시간 속도(배율)
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;  
    }
}
