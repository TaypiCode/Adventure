using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CharacterScriptable", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterScriptable : ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private string _nameRus;
    [SerializeField] private string _nameEng;
    [SerializeField] private Sprite _preview;
    [SerializeField] private RuntimeAnimatorController _animator;

    public string GetName { get
        {
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    return _nameRus;
                case Languages.AllLanguages.Eng:
                    return _nameEng;
            }
            return _nameEng;
        }
    }
    public Sprite Preview { get => _preview;  }
    public RuntimeAnimatorController Animator { get => _animator;  }
}
