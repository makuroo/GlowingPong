using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerOnScreen : MonoBehaviour
{
    [SerializeField] private float boundY = 5f;
    public void HoldPlayer()
    {
        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;
    }
}
