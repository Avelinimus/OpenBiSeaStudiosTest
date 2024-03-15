using Core;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game.Huds
{
    public sealed class TimerHudModel : Observable
    {
        public string Time;
    }

    public sealed class TimerHudView : BaseHudWithModel<TimerHudModel>
    {
        [SerializeField] private TMP_Text _timeTxt;

        protected override void OnEnable()
        {
            
        }

        protected override void OnDisable()
        {
           
        }

        protected override void OnModelChanged(TimerHudModel model)
        {
            _timeTxt.text = model.Time;
        }
    }
}