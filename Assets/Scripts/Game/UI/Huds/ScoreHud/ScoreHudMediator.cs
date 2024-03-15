using Game.Managers;
using Game.UI.Hud;
using Injection;

namespace Game.Huds
{
    public sealed class ScoreHudMediator : Mediator<ScoreHudView>
    {
        [Inject] private GameManager _gameManager;
        private ScoreHudModel _model;

        protected override void Show()
        {
            _model = CreateModel();
            View.Model = _model;
            _gameManager.SCORE_CHANGES += OnScoreChanges;
        }

        private ScoreHudModel CreateModel()
        {
            var result = new ScoreHudModel
            {
                Scores = _gameManager.Score
            };

            return result;
        }

        protected override void Hide()
        {
            View.Model = null;
            _gameManager.SCORE_CHANGES -= OnScoreChanges;
        }

        private void OnScoreChanges(int scores)
        {
            _model.Scores = scores;
            _model.SetChanged();
        }
    }
}