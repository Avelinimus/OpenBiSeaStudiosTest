using Core;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game.Huds
{
    public sealed class DecisionGameHudModel : Observable
    {
        public string DecisionText;
    }

    public sealed class DecisionGameHudView : BaseHudWithModel<DecisionGameHudModel>
    {
        [SerializeField] private TMP_Text _decisionTxt;

        protected override void OnEnable()
        {
            
        }

        protected override void OnDisable()
        {
        
        }

        protected override void OnModelChanged(DecisionGameHudModel model)
        {
            _decisionTxt.text = model.DecisionText;
        }
    }
}