using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 initialPosition;
    private float movement, verticalLimit;
    private string upKey;
    private string downKey;

    //Initialize movement values, position and Up-Down keys for Player1 and Player2.
    private void Awake()
    {
        initialPosition = transform.position;
        movement = 0.1f;
        verticalLimit = 3.4f;
        if (transform.position.x < 0)    //Player1 (left)
        {
            upKey = "w";
            downKey = "s";
        }

        if (transform.position.x > 0)    //Player2 (right)
        {
            upKey = "up";
            downKey = "down";
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private bool positionIsValid(float y)
    {
        if(y < verticalLimit && y > (-1.0f)*verticalLimit)
            return true;
        return false;
    }

    //Move player with keyboard
    void Update()
    {
        if (Input.GetKey(upKey) && positionIsValid(transform.position.y + movement) )
        {
            Vector2 moveUp = new Vector2(transform.position.x, transform.position.y + movement);
            transform.position = moveUp;
        }
        if (Input.GetKey(downKey) && positionIsValid(transform.position.y - movement) )
        {
            Vector2 moveUp = new Vector2(transform.position.x, transform.position.y - movement);
            transform.position = moveUp;
        }
    }
}
