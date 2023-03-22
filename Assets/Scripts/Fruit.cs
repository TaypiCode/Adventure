using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int _scoreGive;
    [SerializeField] private GameObject _pickUpParticle;
    [SerializeField] private AudioSource _audio;
    private bool _available = true;

    public int ScoreGive { get => _scoreGive;  }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && _available)
        {
            FindObjectOfType<LevelUI>().AddScore(_scoreGive);
            Instantiate(_pickUpParticle, transform.position, Quaternion.identity);
            _audio.Play();
            _available = false;
            Destroy(gameObject,0.15f);
        }
    }
}
