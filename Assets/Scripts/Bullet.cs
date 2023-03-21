using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private Rigidbody2D _rb;
    private bool _isAvailable;

    public bool IsAvailable { get => _isAvailable;  }

    public void Shot(Vector2 startPos, Vector2 velocity)
    {
        transform.position = startPos;
        _rb.velocity = velocity;
        _isAvailable = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAvailable == false)
        {
            Player player;
            if(player = collision.gameObject.GetComponent<Player>())
            {
                player.GetHit();
            }
            _rb.velocity = Vector2.zero;
            Instantiate(_hitParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            _isAvailable = true;
        }
    }
}
