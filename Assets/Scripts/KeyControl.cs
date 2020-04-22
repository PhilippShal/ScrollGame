using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyControl : MonoBehaviour
{
    private const float Speed = 0.1f;
    private float bottomBound;
    private float leftBound;
    private Camera mainCamera;
    private GameObject player;
    private float rightBound;
    private Vector2 screenBounds;
    private float topBound;
    private float screenHeight;

    private bool CheckPosition(float xShift, float yShift)
    {
        if (player.transform.position.x + xShift > rightBound ||
            player.transform.position.x + xShift < leftBound ||
            player.transform.position.y + yShift > topBound)
        {
            return true;
        }

        return false;
    }

    private void MovePlayer(float xShift, float yShift)
    {
        if (CheckPosition(xShift, yShift))
        {
            return;
        }

        player.transform.position = new Vector3(player.transform.position.x + xShift, player.transform.position.y + yShift, player.transform.position.z);
    }

    private void RefreshBounds()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        topBound = screenBounds.y;
        bottomBound = topBound - screenHeight;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        topBound = screenBounds.y;
        bottomBound = -screenBounds.y;
        leftBound = -screenBounds.x;
        rightBound = screenBounds.x;
        screenHeight = topBound * 2;
    }

    private void Update()
    {
        RefreshBounds();
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayer(-Speed, 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayer(Speed, 0);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayer(0, Speed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MovePlayer(0, -Speed);
        }

        if (player.transform.position.y < bottomBound)
        {
            player.transform.position = new Vector3(player.transform.position.x, bottomBound, player.transform.position.z);
        }
    }
}
