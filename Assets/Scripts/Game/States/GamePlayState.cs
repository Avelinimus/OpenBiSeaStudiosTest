using Game.Core.Managers;
using Game.Huds;
using Game.Managers;
using Game.Modules;
using Game.UI;
using Injection;

namespace Game.States
{
    public sealed class GamePlayState : GameLoadingState
    {
        private const int kLevelWin = 100;

        [Inject] private Injector _injector;
        [Inject] private Context _context;
        [Inject] private ModuleManager _moduleManager;
        [Inject] private HudManager _hudManager;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private GameView _gameView;

        private GameManager _gameManager;

        public override void Initialize()
        {
            _gameManager = new GameManager();
            _gameManager.Initialize();
            _context.Install(_gameManager);
            _injector.Inject(_gameManager);
            _gameManager.END_GAME += OnEndGame;
            _gameManager.SCORE_CHANGES += OnScoreChanges;
            AddHuds();
            AddModules();
        }

        public override void Dispose()
        {
            _gameManager.END_GAME -= OnEndGame;
            _gameManager.SCORE_CHANGES -= OnScoreChanges;
            _context.Uninstall(_gameManager);
            _gameManager.Dispose();
            RemoveAllModules();
            RemoveHuds();
        }

        private void OnScoreChanges(int score)
        {
            if(score >= kLevelWin)
                _gameManager.FireEndGame(GameEndDecision.Win);
        }

        private void AddModules()
        {
            _moduleManager.AddModule<PlayerInputModule, PlayerInputModuleView>(this, _gameView.PlayerInputModuleView);
            _moduleManager.AddModule<PortalSpawnerModule, PortalSpawnerModuleView>(this, _gameView.PortalSpawnerModuleView);
        }

        private void RemoveAllModules()
        {
            _moduleManager.DisposeModules(this);
        }

        private void AddHuds()
        {
            _hudManager.ShowAdditional<TimerHudMediator>();
            _hudManager.ShowAdditional<ScoreHudMediator>();
            _hudManager.ShowAdditional<ExitGameHudMediator>();
        }

        private void RemoveHuds()
        {
            _hudManager.HideAdditional<TimerHudMediator>();
            _hudManager.HideAdditional<ScoreHudMediator>();
            _hudManager.HideAdditional<ExitGameHudMediator>();
        }

        private void OnEndGame(GameEndDecision decision)
        {
            _gameStateManager.SwitchToState(new GameEndState(decision));
        }
    }
}