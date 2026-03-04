using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    private Vector3 vector;
    public Player.DIRECTION direction;
    private Transform initTf;
    public float step = 1.0f;
    // public Transform handTf;
    // private bool isRotated;

    // Start is called before the first frame update
    void Start()
    {
        initTf = transform;
        direction = Player.DIRECTION.FORWARD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateHand(Player.DIRECTION point)
    {
        if (point == Player.DIRECTION.RIGHT)
        {
            vector = Vector3.right;
        }
        if (point == Player.DIRECTION.LEFT)
        {
            vector = Vector3.left;
        }
        if (point == Player.DIRECTION.UP)
        {
            vector = Vector3.up;
        }
        if (point == Player.DIRECTION.DOWN)
        {
            vector = Vector3.down;
        }
        if (point == Player.DIRECTION.FORWARD)
        {
            vector = Vector3.forward;
        }
        Quaternion targetRot = Quaternion.LookRotation(vector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, step);
    }
    
}




