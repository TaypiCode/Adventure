using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private CharacterMenuUI _characterMenu;
    [SerializeField] private LevelStartMenu _startLevelMenu;
    private Save _save;
    private static bool _firstLoad = true;
    

    [DllImport("__Internal")]
    private static extern void FirstLoadInSession(); //call js from plugin UnityScriptToJS.jslib

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            _save = new Save();
            _save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
        }
        if (!_firstLoad)
        {
            LoadData();
        }
        LoadData();
    }
    private void Start()
    {
        if (_firstLoad)
        {
            //FirstLoadInSession();
        }
    }
    public void LoadFromYandex(string data)
    {
        _save = new Save();
        _save = JsonUtility.FromJson<Save>(data);
        FindObjectOfType<SaveGame>().SaveJson(_save);
        LoadData();
    }

    public void LoadData()
    {
        if (_save != null)
        {
            _characterMenu.CreateCharacterList(_save.choosedCharacterId, _save.characterUnlocked);
            _startLevelMenu.CreateLevelList(_save.reachedScoreInLevel);
        }
        else
        {
            //no save
            _characterMenu.CreateStartedCharacterList();
            _startLevelMenu.CreateLevelList();
        }
        _firstLoad = false;
    }
    public void ShowMobileInputs()
    {
        LevelUI.IsMobile = true;
    }
}
