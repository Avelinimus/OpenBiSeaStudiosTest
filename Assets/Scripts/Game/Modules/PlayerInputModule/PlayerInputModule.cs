using Game.Core;
using Game.Managers;
using Game.UI;
using Game.Utils;
using Injection;
using System.Collections.Generic;
using UnityEngine;
using Timer = Game.Core.Timer;

namespace Game.Modules
{
    public sealed class PlayerInputModule : Module<PlayerInputModuleView>
    {
        private const float kMousePositionZ = 10f;
        private const float kTimeToReload = 1f;
        private const int kAddScored = 10;

        [Inject] private Timer _timer;
        [Inject] private GameView _gameView;
        [Inject] private GameManager _gameManager;

        private readonly TimerDelayer _timerDelayer;
        private readonly List<BallView> _releaseBallViews;

        private BallView _activeBall;
        private bool _isPause;

        public PlayerInputModule(PlayerInputModuleView view) : base(view)
        {
            _timerDelayer = new TimerDelayer();
            _releaseBallViews = new List<BallView>();
        }

        public override void Initialize()
        {
            _isPause = true;
            _timerDelayer.DelayAction(kTimeToReload, CreateNoActiveBall);
            _timer.TICK += OnTICK;
        }

        public override void Dispose()
        {
            _timer.TICK -= OnTICK;
            _activeBall = null;
            foreach (var ball in _releaseBallViews)
            {
                ball.TRIGGER_ENTER -= OnTriggerEnter;
                ball.RELEASE -= OnReleaseBall; 
            }
            _releaseBallViews.Clear();
            View.Factory.ReleaseAllInstances();
            _timerDelayer.Dispose();
        }

        private void OnTICK()
        {
            _timerDelayer.Tick();
            ProcessInput();
        }

        private void ProcessInput()
        {
            if(_isPause)
                return;

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = kMousePositionZ;
#else
            if (Input.touchCount > 0)
            {
                var touchPosition = Input.touches[0].position;
                var mousePosition = new Vector3(touchPosition.x, touchPosition.y, kMousePositionZ);
#endif
                ThrowActiveBall();
                _timerDelayer.DelayAction(kTimeToReload, CreateNoActiveBall);
            }
        }

        private void ThrowActiveBall()
        {
            if(_activeBall == null)
                return;

            _activeBall.transform.SetParent(View.Factory.Content);
            _activeBall.SetForce(_gameView.Player.forward);
            _releaseBallViews.Add(_activeBall);
            _activeBall = null;
            _isPause = true;
        }

        private void OnReleaseBall(BallView ball)
        {
            ball.RELEASE -= OnReleaseBall;
            ball.TRIGGER_ENTER -= OnTriggerEnter;
            _releaseBallViews.Remove(ball);
            View.Factory.Release(ball);
        }

        private void CreateNoActiveBall()
        {
            _isPause = false;
            _activeBall = View.Factory.Get<BallView>();
            _activeBall.RELEASE += OnReleaseBall;
            _activeBall.TRIGGER_ENTER += OnTriggerEnter;
            _activeBall.SetActive(false);
            _activeBall.transform.SetParent(_gameView.Player);
            _activeBall.ResetState();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<ScoreTriggerView>())
            {
                _gameManager.AddScores(kAddScored);
            }
        }
    }
}