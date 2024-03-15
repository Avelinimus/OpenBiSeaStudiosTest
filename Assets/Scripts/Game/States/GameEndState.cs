using Game.Core;
using Game.Core.Managers;
using Game.Huds;
using Game.Utils;
using Injection;

namespace Game.States
{
    public enum GameEndDecision
    {
        Win,
        Lose
    }

    public sealed class GameEndState : GameState
    {
        private const float kDelayToSwapLobbyState = 3f;

        [Inject] private Timer _timer;
        [Inject] private HudManager _hudManager;
        [Inject] private GameStateManager _gameStateManager;

        private readonly TimerDelayer _timerDelayer;

        private readonly GameEndDecision _decision;

        public GameEndState(GameEndDecision decision)
        {
            _decision = decision;
            _timerDelayer = new TimerDelayer();
        }

        public override void Initialize()
        {
            _timerDelayer.DelayAction(kDelayToSwapLobbyState, OnSwitchLobbyState);
            _timer.TICK += OnTICK;
            _hudManager.ShowAdditional<DecisionGameHudMediator>(_decision);
            _hudManager.ShowAdditional<GameStartHudMediator>();
        }

        public override void Dispose()
        {
            _timerDelayer.Dispose();
            _timer.TICK -= OnTICK;
            _hudManager.HideAdditional<GameStartHudMediator>();
            _hudManager.HideAdditional<DecisionGameHudMediator>();
        }

        private void OnTICK()
        {
            _timerDelayer.Tick();
        }

        private void OnSwitchLobbyState()
        {
            _gameStateManager.SwitchToState<GameLobbyState>();
        }
    }
}