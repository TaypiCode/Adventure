using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private CharacterMenuUI _characterMenu;
    [SerializeField] private LevelStartMenu _levelMenu;
    private Save save = new Save();

    [DllImport("__Internal")]
    private static extern void SaveData(string data); //call js from plugin UnityScriptToJS.jslib

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(){
        SaveProgress();
    }
#endif
    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        CreateSaveData();
        SavePlayerPrefs();
        SaveInYandex();
    }
    private void CreateSaveData()
    {
        save.characterUnlocked = _characterMenu.GetUnlockedCharacters();
        save.choosedCharacterId = CharacterMenuUI.ChoosedCharacter.itemId;

        save.reachedScoreInLevel = _levelMenu.GetReachedScore();
    }
    private void SavePlayerPrefs()
    {
        SaveJson(save);
    }
    private void SaveInYandex()
    {
        SaveData(JsonUtility.ToJson(save));
    }
    public void SaveJson(Save save)
    {
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }
}
[Serializable]
public class Save
{
    public bool[] characterUnlocked;
    public string choosedCharacterId;

    public int[] reachedScoreInLevel;
}