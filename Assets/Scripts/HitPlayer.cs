using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.gameObject);
    }
    private void CheckCollision(GameObject collisionObject)
    {
        Player player;
        if (player = collisionObject.GetComponent<Player>())
        {
            player.GetHit();
        }
    }
}
