using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{
    private void Awake()
    {
        direction = Vector3.down;
    }
   
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 30% chance 
        if (Random.value <= 0.3f)
        {

            AnnouncerSystem.Instance.TriggerAnnouncer();
        }
        Destroy(gameObject); //s� fort den krockar med n�got s� ska den f�rsvinna.

    }
   
}
