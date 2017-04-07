using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;
    public Vector3 startPoint;
    [HideInInspector]
    public bool isGameover = false;
    [HideInInspector]
    public Vector3 checkPoint;

    void Respawn()
    {
        Player.transform.position = checkPoint;
    }

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        checkPoint = startPoint;
        Player.transform.position = startPoint;
    }

    void Update()
    {
        if (isGameover)
        {
            Respawn();
            isGameover = false;
        }

    }

}
