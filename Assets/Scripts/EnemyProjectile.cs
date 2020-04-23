using System;
using Assets.Scripts;
using UnityEngine;

public class EnemyProjectile : MovingObject
{
    public int Damage;
    public float xSpeed;
    public float ySpeed;
    private Rigidbody2D body;

    private void MoveVector(Vector2 vector)
    {
        if (IsPositionOutOfScreen())
        {
            Destroy(gameObject);
            return;
        }

        body.MovePosition(body.position + vector);
    }

    private void MoveWithSpeed()
    {
        MoveVector(new Vector2(xSpeed, ySpeed));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private new void Start()
    {
        base.Start();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    private new void Update()
    {
        base.Update();
        MoveWithSpeed();
    }
}
