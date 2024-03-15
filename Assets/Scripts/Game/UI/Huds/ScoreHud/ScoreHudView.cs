using Core;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game.Huds
{
    public sealed class ScoreHudModel : Observable
    {
        public int Scores;
    }

    public sealed class ScoreHudView : BaseHudWithModel<ScoreHudModel>
    {
        [SerializeField] private TMP_Text _scoreTxt;

        protected override void OnEnable()
        {
            
        }

        protected override void OnDisable()
        {

        }

        protected override void OnModelChanged(ScoreHudModel model)
        {
            _scoreTxt.text = model.Scores.ToString();
        }
    }
}