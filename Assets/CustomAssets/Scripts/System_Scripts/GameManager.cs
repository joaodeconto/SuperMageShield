using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperMageShield
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gmData;

        [Header("UI Elements")]
        [SerializeField] private string gameName;
        [SerializeField] private List<Slider> sliderStats;
        [SerializeField] private TMP_Text textScore;
        [SerializeField] private TMP_Text textVillages;
        [SerializeField] private TMP_Text textLevel;

        private int _currentVillages;
        void Start()
        {
            UpdateScore(0);
            foreach (var slider in sliderStats)
            {
                slider.value = 0;
            }

            _currentVillages = gmData.initialVillages;
            ShieldController.OnReflectedProjectile += UpdateShieldPower;
            EntityController.OnEntityDefeated += HandleEntityDefeated;
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

        // Update is called once per frame
        void UpdateShieldPower(float value)
        {
            Debug.Log("shield power +" + value);
            gmData.heroStats[0] += value;
            sliderStats[0].value = value;
        }
        void UpdateScore(float value)
        {
            Debug.Log("Score +" + value);
            gmData.currentScore += (int)value;
            textScore.text = gmData.currentScore.ToString();
        }

        void UpdateVillage()
        {
            Debug.Log("VillagesLeft + " + _currentVillages);
            _currentVillages--;
            textVillages.text = _currentVillages.ToString();
            if (_currentVillages == 0)
            {
                GameOver();
            }
        }

        void GameOver()
        {

        }
        private void Update()
        {

        }
    }
}