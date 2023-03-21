using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int _scoreGive;
    [SerializeField] private GameObject _pickUpParticle;

    public int ScoreGive { get => _scoreGive;  }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FindObjectOfType<LevelUI>().AddScore(_scoreGive);
            Instantiate(_pickUpParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
