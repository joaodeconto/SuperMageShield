
using System.Collections.Generic;
using UnityEngine;


namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "GMSO", menuName = "System/GMSO")]
    public class GameManagerSO : ScriptableObject
    {
        public int currentScore = 0;
        public int initialVillages = 3;
        public int maxLevels = 5;
        public List<int> levelPontuation = new List<int>();
        public List<float> heroStats = new List<float>();
        public List<int> mainScoreList = new List<int>();

        public int CheckScoreStage(int score)
        {
            int level = 0;
            foreach (int levelScoreNeeded in levelPontuation)
            {
                if (score < levelScoreNeeded)
                    return level;

                level++;
            }
            return -1;
        }
        public void AddToMainScore()
        {
            mainScoreList.Add(currentScore);
        }

    }
}