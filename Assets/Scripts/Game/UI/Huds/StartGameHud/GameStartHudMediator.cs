using Game.States;
using Game.UI.Hud;
using Injection;

namespace Game.Huds
{
    public sealed class GameStartHudMediator : Mediator<GameStartHudView>
    {
        [Inject] private GameStateManager _gameStateManager;

        protected override void Show()
        {
            View.PLAY_CLICK += OnPlayClick;
        }

        protected override void Hide()
        {
            View.PLAY_CLICK -= OnPlayClick;
        }

        private void OnPlayClick()
        {
            _gameStateManager.SwitchToState<GamePlayState>();
        }
    }
}