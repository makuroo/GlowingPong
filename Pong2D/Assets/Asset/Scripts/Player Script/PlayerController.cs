using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private KeepPlayerOnScreen keepPlayerOnScreen;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        keepPlayerOnScreen = GetComponent<KeepPlayerOnScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.RacketMove();
        keepPlayerOnScreen.HoldPlayer();
    }
}
