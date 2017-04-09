using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public int speed;
    public int distance;
    public string direction;
    private float check_direction = 0;
	private bool isReverse;
    void FixedUpdate()
    {
        if (direction == "Down")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.up * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Up")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.down * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Right")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.left * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Left")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.right * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }

    }
}
