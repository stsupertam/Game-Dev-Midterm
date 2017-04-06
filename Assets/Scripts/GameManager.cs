using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Checkpoint;
    public GameObject Player;
    [HideInInspector]
    public bool[] checkpoints;
    [HideInInspector]
	public bool isGameover = false;


    void Checkpoint_init()
    {
        checkpoints = new bool[Checkpoint.gameObject.transform.childCount];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i] = false;
        }
    }

	void Respawn()
	{
		Transform _checkpoint;
		for(int i=0; i< checkpoints.Length; i++){
			if(checkpoints[i]){
				_checkpoint = Checkpoint.gameObject.transform.GetChild(i);
				Player.transform.position = _checkpoint.transform.position; 
			}
		}
	}

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        Checkpoint_init();

    }

    void Update()
    {
		if(isGameover){
			Respawn();
			isGameover = false;
		}

    }

}
