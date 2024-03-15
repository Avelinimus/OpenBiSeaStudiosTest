using Game.Core.Managers;
using Game.States;
using Game.Utils;
using System;

namespace Game.Managers
{
    public sealed class GameManager : Manager
    {
        public const float kTime = 60;

        public event Action<GameEndDecision> END_GAME;
        public event Action<int> SCORE_CHANGES;

        public int Score;
        public DateTime Time;

        public override void Initialize()
        {
            Time = DateTime.Now.AddSeconds(kTime);
            Score = 0;
        }

        public override void Dispose()
        {
            
        }

        public void FireEndGame(GameEndDecision decision)
        {
            END_GAME.SafeInvoke(decision);
        }

        public void AddScores(int scores)
        {
            Score += scores;
            SCORE_CHANGES.SafeInvoke(Score);
        }
    }
}