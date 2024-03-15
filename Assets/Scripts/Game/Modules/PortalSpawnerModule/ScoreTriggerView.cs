using Game.Utils;
using System;
using UnityEngine;

namespace Game.Modules
{
    public sealed class ScoreTriggerView : MonoBehaviour
    {
        public event Action TRIGGER_ENTER;

        private void OnTriggerEnter(Collider collider)
        {
            TRIGGER_ENTER.SafeInvoke();
        }
    }
}