using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;
    public Vector3 startPoint;

    public int maxHealth;
    public int maxBomb;
    public int health;
    public int bullet;
    public int bomb;
    [HideInInspector]
    public bool isGameover = false;
    [HideInInspector]
    public Vector3 checkPoint;
    [HideInInspector]
    public Dictionary<string, int> maxItem = new Dictionary<string, int>();
    [HideInInspector]
    public Dictionary<string, int> item_quantity = new Dictionary<string, int>();

    void Respawn()
    {
        Player.transform.position = checkPoint;
    }

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        checkPoint = startPoint;
        Player.transform.position = startPoint;
        maxItem.Add("Bomb", maxBomb);
        maxItem.Add("Health", maxHealth);
        item_quantity.Add("Bomb", bomb);
        item_quantity.Add("Health", health);
        item_quantity.Add("Bullet", bullet);
    }

    void Update()
    {
        if (isGameover)
        {
            Respawn();
            isGameover = false;
        }
        health = item_quantity["Health"];
        bomb = item_quantity["Bomb"];
        bullet = item_quantity["Bullet"];
    }

}
