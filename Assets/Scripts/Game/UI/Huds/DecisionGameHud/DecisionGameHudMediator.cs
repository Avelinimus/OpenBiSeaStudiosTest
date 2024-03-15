using Game.States;
using Game.UI.Hud;

namespace Game.Huds
{
    public sealed class DecisionGameHudMediator : Mediator<DecisionGameHudView>
    {
        private readonly GameEndDecision _decision;

        public DecisionGameHudMediator(GameEndDecision decision)
        {
            _decision = decision;
        }

        protected override void Show()
        {
            View.Model = CreateModel();
        }

        protected override void Hide()
        {
            View.Model = null;
        }

        private DecisionGameHudModel CreateModel()
        {
            var result = new DecisionGameHudModel
            {
                DecisionText = _decision.ToString()
            };

            return result;
        }
    }
}