using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartingCamera : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }
    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += input =>
        {
            gameObject.SetActive(false);
        };
    }
}
