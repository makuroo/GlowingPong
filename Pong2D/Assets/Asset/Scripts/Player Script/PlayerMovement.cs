using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D rb;
    public void RacketMove()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Input.GetKey(upKey))
        {
            rb.velocity = new Vector2(0, moveSpeed);
        }
        else if (Input.GetKey(downKey))
        {
            rb.velocity = new Vector2(0, -moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
