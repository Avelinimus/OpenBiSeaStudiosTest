using Game.Utils;
using System;
using UnityEngine;

namespace Game.Modules
{
    public sealed class BallView : MonoBehaviour
    {
        private const float kTimeToReleaseBall = 3f;
        private const float kDefaultScale = .05f;

        public event Action<BallView> RELEASE;
        public event Action<Collider> TRIGGER_ENTER; 

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        private TimerDelayer _timerDelayer;

        private void Awake()
        {
            _timerDelayer = new TimerDelayer();
        }
        private void OnDisable()
        {
            _timerDelayer.Reset();
        }

        private void Update()
        {
            _timerDelayer.Tick();
        }

        private void OnTriggerEnter(Collider collider)
        {
            TRIGGER_ENTER.SafeInvoke(collider);
        }

        public void SetActive(bool active)
        {
            _rigidbody.isKinematic = !active;
            _collider.enabled = active;
        }

        public void SetForce(Vector3 force)
        {
            SetActive(true);
            _rigidbody.AddForce(force * 5 + Vector3.up, ForceMode.Impulse);
            _timerDelayer.DelayAction(kTimeToReleaseBall, FireReleaseBall);
        }

        public void ResetState()
        {
            transform.localScale = Vector3.one * kDefaultScale;
            transform.localPosition = Vector3.forward * .33f + Vector3.down * .18f;
            transform.rotation = Quaternion.identity;
        }

        private void FireReleaseBall()
        {
            RELEASE.SafeInvoke(this);
        }
    }
}