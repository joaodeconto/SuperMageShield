using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperMageShield
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gmData;

        [Header("SpawnWaves")]
        [SerializeField] private List<GameObject> waveList;

        [Header("UI Elements")]
        [SerializeField] private Transform leftPanel;
        [SerializeField] private Transform startingPanel;
        [SerializeField] private string gameName;
        [SerializeField] private List<Slider> sliderStats;
        [SerializeField] private TMP_Text textScore;
        [SerializeField] private TMP_Text textVillages;
        [SerializeField] private TMP_Text textLevel;
        [SerializeField] private TMP_Text textResult;

        [SerializeField] private List<GameObject> _ingameObjects;

        private int _currentVillages;
        private int _currentLevel;
        private bool _gameStarted;
        private bool _victory;


        private void Update()
        {
           if(GameStateManager.Instance._currentState == GameState.Start && Input.anyKey)
           {
                GameStateManager.Instance.UpdateState(GameState.Playing);
           }
        }

        private void GameStart()
        {
            foreach (var l in waveList)
            {
                l.SetActive(false);
            }
            _gameStarted = true;
            ClearStats();   
            _currentVillages = gmData.initialVillages;
            _currentLevel = -1;
            NextStage();
        }

        private void Awake()
        {
            GameStateManager.OnStateChanged += HandleState;
            ShieldController.OnReflectedProjectile += UpdateShieldPower;
            EntityController.OnEntityDefeated += HandleEntityDefeated;
        }
        private void OnDisable()
        {
            GameStateManager.OnStateChanged -= HandleState;
            ShieldController.OnReflectedProjectile -= UpdateShieldPower;
            EntityController.OnEntityDefeated -= HandleEntityDefeated;
        }

        private void ToggleIngameObjects(bool activate)
        {
            foreach(var obj in _ingameObjects)
            {
                obj.SetActive(activate);
            }
        }
        private void HandleState(GameState gameState)
        {
            switch (gameState)
            {
                case (GameState)0:
                    ShowStaticText("");
                    ToggleIngameObjects(false);
                    startingPanel.gameObject.SetActive(true);
                    leftPanel.gameObject.SetActive(false);
                    break;
                case (GameState)1:
                    ToggleIngameObjects(true);
                    startingPanel.gameObject.SetActive(false);
                    leftPanel.gameObject.SetActive(true);
                    if (!_gameStarted)
                        GameStart();
                    break;
                case (GameState)2:
                    break;
                case (GameState)3:
                case (GameState)4:
                    _gameStarted = false;
                    ToggleIngameObjects(false);
                    PoolManager.Instance.ResetPool();
                    break;
            }
        }

        void HandleEntityDefeated(EntitySO entity)
        {
            if (entity is EnemySO)
            {
                UpdateScore(entity.entityScore);
            }
            else if (entity is VillageSO)
            {
                UpdateVillage();
            }
        }

        void ClearStats()
        {
            for (int i = 0; i < gmData.heroStats.Count; i++)
            {
                gmData.heroStats[i] = 0;
            }
            foreach (var slider in sliderStats)
            {
                slider.value = 0;
            }
            gmData.currentScore = 0;
            UpdateScore(0);
        }

        // Update is called once per frame
        void UpdateShieldPower(float value)
        {
            Debug.Log("shield power +" + value);
            gmData.heroStats[0] += value;
            sliderStats[0].value = gmData.heroStats[0];
        }
        void UpdateScore(float value)
        {
            Debug.Log("Score +" + value);
            gmData.currentScore += (int)value;
            textScore.text = gmData.currentScore.ToString();
            if (gmData.CheckScoreStage(gmData.currentScore) > _currentLevel)
                NextStage();
        }

        void UpdateVillage()
        {
            Debug.Log("VillagesLeft + " + _currentVillages);
            _currentVillages--;
            textVillages.text = _currentVillages.ToString();
            if (_currentVillages == 0)
            {
                _victory = false;
                StartCoroutine(GameOver());
            }
        }

        private IEnumerator ShowDelayText(string text, float delay)
        {
            textResult.gameObject.SetActive(true);
            textResult.text = text;
            yield return new WaitForSeconds(delay);

            textResult.gameObject.SetActive(false);
        }

        private void ShowStaticText(string text)
        {
            textResult.gameObject.SetActive(true);
            textResult.text = text;
        }

        private void NextStage()
        {
            if (!_gameStarted) return;

            _currentLevel++;

            if (_currentLevel > gmData.maxLevels)
            {
                _victory = true;
                StartCoroutine(GameOver());
            }
            else
            {
                waveList[_currentLevel].SetActive(true);
                StartCoroutine(ShowDelayText("Level " + _currentLevel, 2f));
            }
        }

        private IEnumerator GameOver()
        {            
            if (_victory)
            {
                ShowStaticText("WINNER, A Glorious Victory");
                GameStateManager.Instance.UpdateState(GameState.Victory);
            }
            else
            {
                ShowStaticText("DEFEAT, Shame and Despair");
                GameStateManager.Instance.UpdateState(GameState.Defeated);
            }

            yield return new WaitForSeconds(5);

            GameStateManager.Instance.UpdateState(GameState.Start);
        }
    }
}