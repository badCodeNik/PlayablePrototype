using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class StagePerkWindow : MonoBehaviour
    {
        [SerializeField] private Signal signal;
        [SerializeField] private PerkCard firstCard;
        [SerializeField] private PerkCard secondCard;
        [SerializeField] private PerkCard thirdCard;
        private OnPerkChosenSignal _first;
        private OnPerkChosenSignal _second;
        private OnPerkChosenSignal _third;


        public void SetContent(OnPerkChosenSignal first, OnPerkChosenSignal second, OnPerkChosenSignal third)
        {
            _first = first;
            _second = second;
            _third = third;
            firstCard.SetContent(_first.ChosenPerkID);
            secondCard.SetContent(_second.ChosenPerkID);
            thirdCard.SetContent(_third.ChosenPerkID);
        }


        public void InvokeFirst()
        {
            signal.RegistryRaise(_first);
            gameObject.SetActive(false);
        }

        public void InvokeSecond()
        {
            signal.RegistryRaise(_second);
            gameObject.SetActive(false);
        }

        public void InvokeThird()
        {
            signal.RegistryRaise(_third);
            gameObject.SetActive(false);
        }
    }
}