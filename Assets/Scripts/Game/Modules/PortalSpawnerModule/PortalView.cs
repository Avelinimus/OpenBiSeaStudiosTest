using Game.Utils;
using System;
using UnityEngine;

namespace Game.Modules
{
    public sealed class PortalView : MonoBehaviour
    {
        public event Action<PortalView> TRIGGER_ENTER;

        [SerializeField] private ScoreTriggerView _scoreTriggerView;

        private void OnEnable()
        {
            _scoreTriggerView.TRIGGER_ENTER += OnTriggerEnterInternal;
        }

        private void OnDisable()
        {
            _scoreTriggerView.TRIGGER_ENTER -= OnTriggerEnterInternal;
        }

        private void OnTriggerEnterInternal()
        {
            TRIGGER_ENTER.SafeInvoke(this);
        }
    }
}