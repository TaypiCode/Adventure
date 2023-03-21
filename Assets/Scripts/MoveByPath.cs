using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPath : MonoBehaviour
{
    [SerializeField] private Transform _owner;
    [SerializeField] private float _force;
    [SerializeField] private Transform[] _points;
    private int _currentPointId = 0;
    private void Update()
    {
        if (_points.Length > 0)
        {
            _owner.position = Vector2.MoveTowards(_owner.position, _points[_currentPointId].position, _force * Time.deltaTime);
            if (Vector2.Distance(_owner.position, _points[_currentPointId].position) < 0.5f)
            {
                _currentPointId++;
                if (_currentPointId >= _points.Length)
                {
                    _currentPointId = 0;
                }
            }
        }
    }
}
