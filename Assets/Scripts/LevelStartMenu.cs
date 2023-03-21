using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStartMenu : MonoBehaviour
{
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
            int reachedScore = scoreReached != null?scoreReached.Length < i? scoreReached[i]:0:0;
            if(startedLevelId == i)
            {
                if(newReachedScoreInLevel > reachedScore)
                {
                    reachedScore = newReachedScoreInLevel;
                }
            }
            texts[1].text = reachedScore + "/"+_maxScoreInLevel[i];
            _reachedScoreInLevel[i] = reachedScore;
        }
        startedLevelId = -1;
        newReachedScoreInLevel = -1;
    }
    private void PlayLevel(int sceneId, int idInArr)
    {
        startedLevelId = idInArr;
        SceneManager.LoadScene(sceneId);
    }
    public int[] GetReachedScore()
    {
        return _reachedScoreInLevel;
    }
}
