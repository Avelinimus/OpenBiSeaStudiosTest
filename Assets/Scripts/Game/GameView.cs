using Game.Modules;
using UnityEngine;

namespace Game.UI
{
    public sealed class GameView : MonoBehaviour
    {
        private const string kSimulationCamera = "SimulationCamera";

        public Transform HudsLayer;
        public Transform WindowsLayer;

        public Transform Player;
        public PlayerInputModuleView PlayerInputModuleView;
        public PortalSpawnerModuleView PortalSpawnerModuleView;

#if UNITY_EDITOR
        private void Awake()
        {
            Player = GameObject.Find(kSimulationCamera).transform;
        }
#endif
    }
}