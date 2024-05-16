using System.Collections.Generic;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.Scripts.UI;
using Source.SignalSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.MonoBehaviours
{
    public class PerkSignalSender : MonoSignalListener<OnRoomCleanedSignal, OnPerkChosenSignal, OnHeroKilledSignal , OnLevelCompletedSignal>
    {
        [SerializeField, HideInInspector] private Canvas _canvas;
        [SerializeField] private GameObject prefab;
        [SerializeField] private StagePerkWindow content;

        private List<PerkKeys> _usedPerkIDs = new List<PerkKeys>();
        private List<int> _numberList = new List<int>();
        private List<PerksPack> _perksLibrary = new List<PerksPack>();


        private void Start()
        {
            _perksLibrary = Libraries.Instance.PerksLibrary.GetAllPacks();
            _usedPerkIDs = new List<PerkKeys>();
        }

        protected override void OnSignal(OnRoomCleanedSignal data)
        {
            if (_usedPerkIDs.Count >= 8)
            {
                signal.RegistryRaise(new OnPerkChosenSignal());
                return;
            }
            if (content == null) content = Instantiate(prefab, transform).GetComponent<StagePerkWindow>();
            content.gameObject.SetActive(true);

            var perks = GetRandomPerks();

            content.SetContent(perks.Item1, perks.Item2, perks.Item3);
        }

        protected override void OnSignal(OnPerkChosenSignal data)
        {
            _usedPerkIDs.Add(data.ChosenPerkID);
        }

        protected override void OnSignal(OnHeroKilledSignal data)
        {
            ClearPerks();
        }

        protected override void OnSignal(OnLevelCompletedSignal data)
        {
            ClearPerks();
        }

        private void ClearPerks()
        {
            _usedPerkIDs.Clear();
            _perksLibrary.Clear();
            _numberList.Clear();
           Start();
           
        }

        private (OnPerkChosenSignal, OnPerkChosenSignal, OnPerkChosenSignal) GetRandomPerks()
        {
            List<int> availablePerksIndexes = new List<int>();
            for (int i = 0; i < _perksLibrary.Count; i++)
            {
                if (!_usedPerkIDs.Contains(_perksLibrary[i].ID))
                {
                    availablePerksIndexes.Add(i);
                }
            }

            Debug.Log(availablePerksIndexes.Count);
            _numberList.Clear();
            while (_numberList.Count < 3 && availablePerksIndexes.Count > 0)
            {
                int index = Random.Range(0, availablePerksIndexes.Count);
                _numberList.Add(availablePerksIndexes[index]);
                availablePerksIndexes.RemoveAt(index);
            }

            return (
                new OnPerkChosenSignal
                {
                    ChosenPerkID = _perksLibrary[_numberList[0]].ID
                },
                new OnPerkChosenSignal
                {
                    ChosenPerkID = _perksLibrary[_numberList[1]].ID
                },
                new OnPerkChosenSignal
                {
                    ChosenPerkID = _perksLibrary[_numberList[2]].ID
                });
        }


        private void OnValidate()
        {
            if (_canvas == null) _canvas = FindObjectOfType<Canvas>();
        }
    }
}