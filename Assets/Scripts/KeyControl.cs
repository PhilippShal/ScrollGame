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

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.MoveLeft();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.MoveRight();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            player.MoveUp();
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            player.MoveDown();
        }
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
