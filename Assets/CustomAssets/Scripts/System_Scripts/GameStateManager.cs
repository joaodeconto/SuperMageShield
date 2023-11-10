using UnityEngine;
using UnityEngine.Events;

namespace SuperMageShield
{
    public enum GameState
    {
        Start = 0,
        Playing = 1,
        Pause = 2,
        Defeated = 3,
        Victory = 4,
    }

    public class GameStateManager : MonoBehaviour
    {

        public GameState _currentState;
        public static GameStateManager Instance;

        public static UnityAction<GameState> OnStateChanged;

        private GameState _lastState;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            UpdateState(GameState.Start);
        }
        public void UpdateState(GameState newState)
        {
            _lastState = _currentState;
            OnStateChanged(newState);
            _currentState = newState;
        }
    }
}
