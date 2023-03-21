using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CharacterScriptable", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterScriptable : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _preview;
    [SerializeField] private RuntimeAnimatorController _animator;

    public string GetName { get => _name; set => _name = value; }
    public Sprite Preview { get => _preview; set => _preview = value; }
    public RuntimeAnimatorController Animator { get => _animator; set => _animator = value; }
}
