using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStartMenu : MonoBehaviour
{
    [SerializeField] private SaveGame _save;
    [SerializeField] private GameObject _levelBtnPrefub;
    [SerializeField] private Transform _levelListContent;
    [SerializeField] private int[] _levelSceneId;
    [SerializeField] private string[] _levelShowNameRus;
    [SerializeField] private string[] _levelShowNameEng;
    [SerializeField] private int[] _maxScoreInLevel;
    private int[] _reachedScoreInLevel;
    public static int startedLevelId;
    public static int newReachedScoreInLevel;
    public void CreateLevelList(int[] scoreReached) 
    {
        _reachedScoreInLevel = new int[_levelSceneId.Length];
        _levelListContent.GetComponentsInChildren<Transform>();
        int totalReachedScore = 0;
        foreach(Transform elem in _levelListContent)
        {
            Destroy(elem.gameObject);
        }
        for (int i = 0; i < _levelSceneId.Length; i++)
        {
            int levelSceneId  = _levelSceneId[i];
            int arrId = i;
            Button btn = Instantiate(_levelBtnPrefub, _levelListContent).GetComponent<Button>();
            btn.onClick.AddListener(delegate { PlayLevel(levelSceneId, arrId); });
            TextMeshProUGUI[] texts = btn.GetComponentsInChildren<TextMeshProUGUI>();

            int reachedScore = 0;
            if(scoreReached != null)
            {
                if(i < scoreReached.Length)
                {
                    reachedScore = scoreReached[i];
                }
            }
            if (startedLevelId == i)
            {
                if(newReachedScoreInLevel > reachedScore)
                {
                    reachedScore = newReachedScoreInLevel;
                }
            }
            string scoreReachedText = reachedScore + "/" + _maxScoreInLevel[i];
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    texts[0].text = _levelShowNameRus[i];
                    texts[1].text = "Собрано очков: " + scoreReachedText;
                    break;
                case Languages.AllLanguages.Eng:
                    texts[0].text = _levelShowNameEng[i];
                    texts[1].text = "Score reached: " + scoreReachedText;
                    break;
            }

            _reachedScoreInLevel[i] = reachedScore;
            totalReachedScore += reachedScore;
        }
        startedLevelId = -1;
        newReachedScoreInLevel = -1;
        LeaderboardScript.SetLeaderboardValue(LeaderboardScript.Names.TotalScore, totalReachedScore);
    }
    private void PlayLevel(int sceneId, int idInArr)
    {
        _save.SaveProgress();
        startedLevelId = idInArr;
        SceneManager.LoadScene(sceneId);
    }
    public int[] GetReachedScore()
    {
        return _reachedScoreInLevel;
    }
}
