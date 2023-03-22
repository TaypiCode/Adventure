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
    [SerializeField] private string[] _levelShowName;
    [SerializeField] private int[] _maxScoreInLevel;
    private int[] _reachedScoreInLevel;
    public static int startedLevelId;
    public static int newReachedScoreInLevel;
    public void CreateLevelList(int[] scoreReached = null) 
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
            texts[0].text = _levelShowName[i];
            int reachedScore = scoreReached != null? i <scoreReached.Length ? scoreReached[i]:0:0;
            if(startedLevelId == i)
            {
                if(newReachedScoreInLevel > reachedScore)
                {
                    reachedScore = newReachedScoreInLevel;
                }
            }
            texts[1].text = "Собрано очков: " + reachedScore + "/"+_maxScoreInLevel[i];
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
