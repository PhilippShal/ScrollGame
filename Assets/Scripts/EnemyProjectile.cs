﻿using System;
using Assets.Scripts.Helpers;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public int Damage;

    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            Destroy(gameObject);
        }
    }
}
