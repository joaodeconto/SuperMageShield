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
        [SerializeField] private string gameName;
        [SerializeField] private List<Slider> sliderStats;
        [SerializeField] private TMP_Text textScore;
        [SerializeField] private TMP_Text textVillages;
        [SerializeField] private TMP_Text textLevel;
        [SerializeField] private TMP_Text textResult;

        private int _currentVillages;
        private int _currentLevel;
        void Start()
        {
            ClearStats();                   

            _currentVillages = gmData.initialVillages;
            _currentLevel = -1;
            NextStage();
        }

        private void OnEnable()
        {

            ShieldController.OnReflectedProjectile += UpdateShieldPower;
            EntityController.OnEntityDefeated += HandleEntityDefeated;
        }

        private void OnDisable()
        {
            ShieldController.OnReflectedProjectile -= UpdateShieldPower;
            EntityController.OnEntityDefeated -= HandleEntityDefeated;
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
                GameOverDefeat();
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
            _currentLevel++;

            if (_currentLevel > gmData.maxLevels)
            {
                GameOverVictory();
            }
            else
            {
                waveList[_currentLevel].SetActive(true);
                StartCoroutine(ShowDelayText("Level " + _currentLevel, 2f));
            }
        }

        private void GameOverVictory()
        {
            ShowStaticText("WINNER, A Glorious Victory");
        }
        private void GameOverDefeat()
        {
            ShowStaticText("DEFEAT, Shame and Despair");
        }
    }
}