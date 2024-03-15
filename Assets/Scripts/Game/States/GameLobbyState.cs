using Game.Core.Managers;
using Game.Huds;
using Injection;

namespace Game.States
{
    public class GameLobbyState : GameState
    {
        [Inject] private HudManager _hudManager;

        public override void Initialize()
        {
            _hudManager.ShowAdditional<GameStartHudMediator>();
        }

        public override void Dispose()
        {
            _hudManager.HideAdditional<GameStartHudMediator>();
        }
    }
}