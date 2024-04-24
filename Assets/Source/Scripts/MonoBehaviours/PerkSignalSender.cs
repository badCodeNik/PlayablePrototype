using System.Collections.Generic;
using Source.Scripts.LibrariesSystem;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class PerkSignalSender : MonoSignalListener<OnRoomCleanedSignal>
    {
        private List<PerksPack> _perksPacks = new List<PerksPack>();
        private List<int> _selectedIndexes = new List<int>();
        protected override void OnSignal(OnRoomCleanedSignal data)
        {
            GetRandomPerks();
            
            signal.RegistryRaise(new OnPerksGenerated()
            {
                PerksPacks = GetRandomPerks()  
            });
        }

        private List<PerksPack> GetRandomPerks()
        {
            
            var perksLibrary = Libraries.Instance.PerksLibrary.GetAllPacks();

           
            while (_perksPacks.Count < 3)
            {
                int randomIndex = Random.Range(0, perksLibrary.Count);
                if (!_selectedIndexes.Contains(randomIndex))
                {
                    _selectedIndexes.Add(randomIndex);
                    _perksPacks.Add(perksLibrary[randomIndex]);
                }
            }

            return _perksPacks;
        }
    }
}