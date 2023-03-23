using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _mobileCanvas;
    [SerializeField] private GameObject _levelCanvas;
    [SerializeField] private GameObject _finishCanvas;
    [SerializeField] private TextMeshProUGUI _playerCurrentScore;
    [SerializeField] private TextMeshProUGUI _finishResultTitle;
    [SerializeField] private TextMeshProUGUI _playerFinishResult;
    private static bool _isMobile;
    private Player _player;
    private float _moveForce;
    private int _gotScore;
    private int _maxScore;
    private AdsScript _ads;
    private static int _completedLevelsPerSession;

    public static bool IsMobile { get => _isMobile; set => _isMobile = value; }
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _ads = FindObjectOfType<AdsScript>();
    }
    private void Start()
    {
        _mobileCanvas.SetActive(_isMobile);
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        for(int i =0; i < fruits.Length; i++)
        {
            _maxScore += fruits[i].ScoreGive;
        }
        UpdateCurrentScore();
    }
    private void FixedUpdate()
    {
        if (_isMobile)
        {
           _player.Move(_moveForce);
        }
    }
    private void UpdateCurrentScore()
    {
        string score = _gotScore + "/" + _maxScore;
        switch (Languages.CurrentLanguage)
        {
            case Languages.AllLanguages.Rus:
                _playerCurrentScore.text = "Очки: " + score;
                break;
            case Languages.AllLanguages.Eng:
                _playerCurrentScore.text = "Score: " + score;
                break;
        }
        
    }
    public void PlayerMoveLeft()
    {
        _moveForce = -1;
    }
    public void PlayerMoveRight()
    {
        _moveForce = 1;
    }
    public void PlayerJump()
    {
        _player.Jump();
    }
    public void PlayerStopMoving()
    {
        _moveForce = 0;
    }
    public void AddScore(int score)
    {
        _gotScore += score;
        UpdateCurrentScore();
    }
    public void ShowFinishUI(bool isWin)
    {
        _player.IsFinished = true;
        _levelCanvas.SetActive(false);
        _finishCanvas.SetActive(true);
        string score = _gotScore + "/" + _maxScore;
        switch (Languages.CurrentLanguage)
        {
            case Languages.AllLanguages.Rus:
                _playerFinishResult.text = "Собрано очков: " + score;
                break;
            case Languages.AllLanguages.Eng:
                _playerFinishResult.text = "Score reached: " + score;
                break;
        }
        if (isWin)
        {
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    _finishResultTitle.text = "Победа";
                    break;
                case Languages.AllLanguages.Eng:
                    _finishResultTitle.text = "Win";
                    break;
            }
            
            LevelStartMenu.newReachedScoreInLevel = LevelStartMenu.newReachedScoreInLevel < _gotScore ? _gotScore : LevelStartMenu.newReachedScoreInLevel;
            _completedLevelsPerSession++;
            if (_completedLevelsPerSession % 2 == 0)
            {
                RateUsScript.ShowRateUs();
                _completedLevelsPerSession = 0;
            }
        }
        else
        {
            switch (Languages.CurrentLanguage)
            {
                case Languages.AllLanguages.Rus:
                    _finishResultTitle.text = "Поражение";
                    break;
                case Languages.AllLanguages.Eng:
                    _finishResultTitle.text = "Loose";
                    break;
            }
        }
        
        _ads.ShowNonRewardAd();
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {

    }
}
