using Game.Core;
using Game.Managers;
using Game.States;
using Game.UI.Hud;
using Game.Utilities;
using Injection;
using System;

namespace Game.Huds
{
    public sealed class TimerHudMediator : Mediator<TimerHudView>
    {
        [Inject] private Timer _timer;
        [Inject] private GameManager _gameManager;
        private TimerHudModel _model;

        protected override void Show()
        {
            _model = CreateModel();
            View.Model = _model;
            _timer.TICK += OnTICK;
        }

        private TimerHudModel CreateModel()
        {
            var result = new TimerHudModel
            {
                Time = TimeSpan.FromSeconds(GameManager.kTime).TimeToMS()
            };

            return result;
        }

        protected override void Hide()
        {
            View.Model = null;
            _timer.TICK -= OnTICK;
        }

        private void OnTICK()
        {
            var time = _gameManager.Time.Subtract(DateTime.Now);
            _model.Time = time.TimeToMS();
            _model.SetChanged();

            if (time.TotalSeconds <= 0)
                _gameManager.FireEndGame(GameEndDecision.Lose);
        }
    }
}