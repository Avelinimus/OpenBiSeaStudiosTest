using Game.UI;
using Game.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Huds
{
    public sealed class GameStartHudView : BaseHud
    {
        public event Action PLAY_CLICK;

        [SerializeField] private Button _playBtn;

        protected override void OnEnable()
        {
            _playBtn.onClick.AddListener(OnPlayClick);
        }

        protected override void OnDisable()
        {
            _playBtn.onClick.RemoveListener(OnPlayClick);
        }

        private void OnPlayClick()
        {
            PLAY_CLICK.SafeInvoke();
        }
    }
}