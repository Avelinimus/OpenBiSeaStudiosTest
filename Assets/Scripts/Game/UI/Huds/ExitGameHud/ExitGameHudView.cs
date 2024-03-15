using Game.UI;
using Game.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Huds
{
    public sealed class ExitGameHudView : BaseHud
    {
        public event Action EXIT_CLICK;

        [SerializeField] private Button _exitBtn;

        protected override void OnEnable()
        {
            _exitBtn.onClick.AddListener(OnExitClick);
        }

        protected override void OnDisable()
        {
            _exitBtn.onClick.RemoveListener(OnExitClick);
        }

        private void OnExitClick()
        {
            EXIT_CLICK.SafeInvoke();
        }
    }
}