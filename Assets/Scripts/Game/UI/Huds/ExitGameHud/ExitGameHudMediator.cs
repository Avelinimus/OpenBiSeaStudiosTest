using Game.States;
using Game.UI.Hud;
using Injection;

namespace Game.Huds
{
    public sealed class ExitGameHudMediator : Mediator<ExitGameHudView>
    {
        [Inject] private GameStateManager _gameStateManager;

        protected override void Show()
        {
            View.EXIT_CLICK += OnClickExit;
        }

        protected override void Hide()
        {
            View.EXIT_CLICK -= OnClickExit;
        }

        private void OnClickExit()
        {
            _gameStateManager.SwitchToState<GameLobbyState>();
        }
    }
}