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

        public static UnityAction<GameState> StateChanged;

        private GameState _lastState;

        private void Awake()
        {
            Instance = this;
        }
        public void UpdateState(GameState newState)
        {
            _lastState = _currentState;
            StateChanged(newState);
            _currentState = newState;
        }
    }
}
