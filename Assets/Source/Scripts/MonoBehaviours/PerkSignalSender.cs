using System.Collections.Generic;
using Source.Scripts.Ecs.Systems.PerkSystems;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.Scripts.UI;
using Source.SignalSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.MonoBehaviours
{
    public class PerkSignalSender : MonoSignalListener<OnRoomCleanedSignal>
    {
        [SerializeField, HideInInspector] private Canvas _canvas;
        [SerializeField] private GameObject prefab;
        [SerializeField] private StagePerkWindow content;


        protected override void OnSignal(OnRoomCleanedSignal data)
        {
            if (content == null) content = Instantiate(prefab, transform).GetComponent<StagePerkWindow>();
            content.gameObject.SetActive(true);

            var perks = GetRandomPerks();

            content.SetContent(perks.Item1, perks.Item2, perks.Item3);
        }

        private (OnPerkChosenSignal, OnPerkChosenSignal, OnPerkChosenSignal) GetRandomPerks()
        {
            var perksLibrary = Libraries.Instance.PerksLibrary.GetAllPacks();
            var numberList = new List<int>();

            while (numberList.Count < 2 || numberList.Count < perksLibrary.Count)
            {
                var randomNumber = Random.Range(0, perksLibrary.Count);
                if (!numberList.Contains(randomNumber)) numberList.Add(randomNumber);
            }

            return (
                new OnPerkChosenSignal
                {
                    //ChosenPerkID = perksLibrary[numberList[0]].ID
                    ChosenPerkID = PerkKeys.HealthRestoration,
                    Data = new HealthRestorationData()
                    {
                        TimeRemaining = 8,
                        Interval = 1,
                        RestorationAmount = 2
                    }
                },
                new OnPerkChosenSignal
                {
                    ChosenPerkID = perksLibrary[numberList[1]].ID
                },
                new OnPerkChosenSignal
                {
                    ChosenPerkID = perksLibrary[numberList[2]].ID
                });
        }


        private void OnValidate()
        {
            if (_canvas == null) _canvas = FindObjectOfType<Canvas>();
        }
    }
}