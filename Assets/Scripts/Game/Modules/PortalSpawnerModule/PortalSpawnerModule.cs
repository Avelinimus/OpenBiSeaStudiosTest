using Game.Core;
using Game.Managers;
using Game.UI;
using Injection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Modules
{
    public sealed class PortalSpawnerModule : Module<PortalSpawnerModuleView>
    {
        private const float kRadius = 1.5f;

        [Inject] private GameView _gameView;

        private readonly List<PortalView> _portalViews;

        public PortalSpawnerModule(PortalSpawnerModuleView view) : base(view)
        {
            _portalViews = new List<PortalView>();
        }

        public override void Initialize()
        {
            CreatePortal();
        }

        public override void Dispose()
        {
            for(var i = _portalViews.Count - 1; i >= 0; i--) 
            {
                var portalView = _portalViews[i];
                ReleasePortal(portalView);
            }
        }

        public void CreatePortal()
        {
            var portalView = View.Factory.Get<PortalView>();
            portalView.TRIGGER_ENTER += OnTriggerEnter;
            var angle = Random.Range(0f, Mathf.PI * 2f);

            var offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * kRadius;
            portalView.transform.position = _gameView.Player.transform.position + offset;
            portalView.transform.LookAt(_gameView.Player.position);

            portalView.transform.eulerAngles
                = new Vector3(-90, portalView.transform.eulerAngles.y, portalView.transform.eulerAngles.z);
            _portalViews.Add(portalView);
        }

        public void ReleasePortal(PortalView portalView)
        {
            portalView.TRIGGER_ENTER -= OnTriggerEnter;
            _portalViews.Remove(portalView);
            View.Factory.Release(portalView);
        }

        private void OnTriggerEnter(PortalView portalView)
        {
            ReleasePortal(portalView);
            CreatePortal();
        }
    }
}