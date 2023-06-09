﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuUI : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private GameObject _characterBtnPrefub;
    [SerializeField] private Transform _characterListContent;
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private Image _characterImg;
    [SerializeField] private TextMeshProUGUI _chooseBtnText;
    [SerializeField] private CharacterScriptable[] _characters;
    private bool[] _characterUnlocked;
    private int _selectedCharacterId;
    private CharacterScriptable _selectedCharacter;
    private static CharacterScriptable _choosedCharacter;
    private string[] texts = new string[3];

    public static CharacterScriptable ChoosedCharacter { get => _choosedCharacter;  }

    public void CreateCharacterList(string selectedCharacterItemId, bool[] unlockedCharacters)
    {
        _characterListContent.GetComponentsInChildren<Transform>();
        foreach (Transform elem in _characterListContent)
        {
            Destroy(elem.gameObject);
        }
        _characterUnlocked = new bool[_characters.Length];
        for(int i = 0; i < _characters.Length; i++)
        {
            Button btn = Instantiate(_characterBtnPrefub, _characterListContent).GetComponent<Button>();
            int id = i;
            btn.onClick.AddListener(delegate { ShowCharacter(_characters[id]); } );
            btn.GetComponentInChildren<TextMeshProUGUI>().text = _characters[i].GetName;
            if(i < unlockedCharacters.Length)
            {
                _characterUnlocked[i] = unlockedCharacters[i];
            }
            else
            {
                _characterUnlocked[i] = false;
            }
            if(i == 0)
            {
                _characterUnlocked[i] = true;
            }
            if(selectedCharacterItemId == _characters[i].itemId)
            {
                _choosedCharacter = _characters[i];
            }
            
        }
        if(_choosedCharacter== null) 
        {
            _choosedCharacter = _characters[0];
        }
        switch (Languages.CurrentLanguage)
        {
            case Languages.AllLanguages.Rus:
                texts[0] = "Выбрано";
                texts[1] = "Выбрать";
                texts[2] = "Разблокировать за рекламу";
                break;
            case Languages.AllLanguages.Eng:
                texts[0] = "Selected";
                texts[1] = "Select";
                texts[2] = "Unlock for ads";
                break;
        }
        ShowCharacter(_choosedCharacter);
    }
    public void CreateStartedCharacterList()
    {
        _characterUnlocked = new bool[_characters.Length];
        for(int i = 0; i < _characterUnlocked.Length; i++)
        {
            if(i == 0)
            {
                _characterUnlocked[i] = true;
            }
            else
            {
                _characterUnlocked[i] = false;
            }
        }
        _choosedCharacter = _characters[0];
        CreateCharacterList(_choosedCharacter.itemId, _characterUnlocked);
    }
    private void ShowCharacter(CharacterScriptable character)
    {
        if(character == null)
        {
            Debug.LogWarning("No character");
            return;
        }
        _selectedCharacter = character;
        for (int i = 0; i < _characters.Length; i++)
        {
            if(_selectedCharacter == _characters[i])
            {
                _selectedCharacterId= i;
            }
        }
        _characterName.text = _selectedCharacter.GetName;
        _characterImg.sprite = _selectedCharacter.Preview;
        
        

        if (_selectedCharacterId < _characterUnlocked.Length)
        {
            if (_characterUnlocked[_selectedCharacterId])
            {
                if(_choosedCharacter == _selectedCharacter)
                {
                    _chooseBtnText.text = texts[0];
                }
                else
                {
                    _chooseBtnText.text = texts[1];
                }
            }
            else
            {
                _chooseBtnText.text = texts[2];
            }
        }
        else
        {
            _chooseBtnText.text = texts[2];
        }
    }
    public void TryChooseCharacter()
    {
        if (_characterUnlocked[_selectedCharacterId])
        {
            _choosedCharacter = _selectedCharacter;
            _chooseBtnText.text = texts[0];
        }
        else
        {
            FindObjectOfType<AdsScript>().ShowRewardAdsForCharacter();
        }
    }
    public void UnlockCharacter()
    {
        _characterUnlocked[_selectedCharacterId] = true;
        ShowCharacter(_selectedCharacter);
    }
    public bool[] GetUnlockedCharacters()
    {
        return _characterUnlocked;
    }
}
