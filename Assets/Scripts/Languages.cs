using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Languages :MonoBehaviour
{
    [SerializeField] private GameObject _chooseLanguageCanvas;
    [SerializeField] private GameObject _firstBtnsCanvas;
    [SerializeField] private SaveGame _save;
    private static AllLanguages _currentLanguage;

    public static AllLanguages CurrentLanguage { get => _currentLanguage;}

    public enum AllLanguages
    {
        Rus,
        Eng
    }
    public void SetLanguage(AllLanguages language, bool needReload)
    {
        _currentLanguage = language;
        if (needReload) 
        { 
            SceneManager.LoadScene(0);
            _save.SaveProgress();
        }
    }
    public void SetLanguage(string language, bool needReload)
    {
        if (language == AllLanguages.Rus.ToString())
        {
            SetLanguage(AllLanguages.Rus, needReload);
        }
        else if (language == AllLanguages.Eng.ToString())
        {
            SetLanguage(AllLanguages.Eng, needReload);
        }
    }public void SetLanguageFromUI(string language)
    {
        SetLanguage(language, true);
    }
    public void ShowChooseLanguage()
    {
        _chooseLanguageCanvas.SetActive(true);
        _firstBtnsCanvas.SetActive(false);
    }
}
