using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class StagePerkWindowSwitcher : MonoSignalListener<OnRoomCleanedSignal,OnPerkChosenSignal>
    {
        [SerializeField] private GameObject window;
        [SerializeField] private GameObject perkPanel;
        protected override void OnSignal(OnRoomCleanedSignal data)
        {
            Instantiate(window);
        }

        protected override void OnSignal(OnPerkChosenSignal data)
        {
           perkPanel.SetActive(false);
        }
    }
}