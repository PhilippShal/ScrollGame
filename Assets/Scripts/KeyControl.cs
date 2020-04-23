using System;
using Assets.Scripts;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    private Player player;
    private Settings settings;

    private void ControlThroughKeys()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            settings.SettingsClick();
        }

        KeyMoves();
    }

    private void ControlThroughTouch()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        var delta = Input.GetTouch(0).deltaPosition * player.Speed;
        player.MoveVector(delta);
    }

    private void KeyMoves()
    {
        var movementDetected = false;
        var movementVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movementVector += new Vector2(-player.Speed, 0);
            movementDetected = true;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementVector += new Vector2(player.Speed, 0);
            movementDetected = true;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movementVector += new Vector2(0, player.Speed);
            movementDetected = true;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movementVector += new Vector2(0, -player.Speed);
            movementDetected = true;
        }

        if (movementDetected)
        {
            player.MoveVector(movementVector);
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        settings = GameObject.Find("SettingsButton").GetComponent<Settings>();
    }

    private void Update()
    {
        if (settings.InSettings)
        {
            return;
        }
#if UNITY_STANDALONE
        ControlThroughKeys();
#endif
#if UNITY_ANDROID
        ControlThroughTouch();
#endif
    }
}
