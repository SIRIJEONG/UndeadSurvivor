using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public PlayerMove player;

    private void Awake()
    {
        instance = this;
    }
}
