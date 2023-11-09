using UnityEngine;


namespace SuperMageShield
{
    public class EntitySO : ScriptableObject
    {
        public string entityName;
        public Color entityColor; //TODO color[] pallete
        public int entityNumber;
        public GameObject entityPrefab;
        public Vector2 entitySpawnPos;
        public float entityHealth;
        public float entityScore;
    }
}
