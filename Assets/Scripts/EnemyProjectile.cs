using System;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

public class EnemyProjectile : MovingObject
{
    public int Damage;
    public float xSpeed;
    public float ySpeed;
    private Rigidbody2D body;

    private void FixedUpdate()
    {
    }

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
        var movingVector = new Vector2(xSpeed, ySpeed);
        MoveVector(movingVector);
        if (movingVector != Vector2.zero)
        {
            transform.rotation = Converters.GetAngleFromDirection(movingVector.x, movingVector.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            Destroy(gameObject);
        }
    }

    private new void Start()
    {
        base.Start();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    private new void Update()
    {
        base.Update();
        MoveWithSpeed();
    }
}
