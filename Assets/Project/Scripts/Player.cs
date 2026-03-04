using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum DIRECTION
    {
        LEFT,
        DOWN,
        RIGHT,
        UP,
        FORWARD
    }
    [SerializeField] private HandMove hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            hand.direction = DIRECTION.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            hand.direction = DIRECTION.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            hand.direction = DIRECTION.UP;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            hand.direction = DIRECTION.DOWN;
        }
        hand.RotateHand(hand.direction);
    }

    
    
}
