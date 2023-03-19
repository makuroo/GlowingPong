using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float boundY = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0,1) * moveSpeed;
        BoundCheck();
    }

    private void FixedUpdate()
    {


    }
    private void BoundCheck()
    {
        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
            moveSpeed *= -1;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
            moveSpeed *= -1;
        }
        transform.position = pos;
    }
}
