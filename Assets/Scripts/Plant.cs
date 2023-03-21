using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bulletPrefub;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private Vector2 _bulletVelocity;
    [SerializeField] private float _cooldownTime;
    private Timer _timer;
    private List<Bullet> _bullets = new List<Bullet>();
    private void Start()
    {
        _timer = gameObject.AddComponent<Timer>();
        _timer.SetTimer(_cooldownTime);
    }
    private void Update()
    {
        if(_timer.GetTime() <= 0)
        {
            Shot();
        }
    }
    private void Shot()
    {
        _timer.SetTimer(_cooldownTime);
        //_animator.SetTrigger("AttackTrigger");
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (_bullets[i].IsAvailable)
            {
                _bullets[i].Shot(_bulletSpawn.position, _bulletVelocity);
                return;
            }
        }
        Bullet newBullet = Instantiate(_bulletPrefub, _bulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
        _bullets.Add(newBullet);
        newBullet.Shot(_bulletSpawn.position, _bulletVelocity);
    }
}
