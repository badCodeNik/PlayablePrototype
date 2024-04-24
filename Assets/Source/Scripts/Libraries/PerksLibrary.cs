using System;
using Source.Scripts.KeysHolder;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.LibrariesSystem
{
    [CreateAssetMenu(menuName = "Library/PerksLibrary", fileName = "PerksLibrary")]
    public class PerksLibrary : Library<PerksPack, PerkKeys>
    {
        
    }
    
    [Serializable]
    public class PerksPack : ILibraryItem<PerkKeys>
    {
        [SerializeField] private PerkKeys perkID;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;
        [SerializeField] private int chanceForPerk;

        public int ChanceForPerk => chanceForPerk;

        public string Description => description;
        
        public Sprite Icon => icon;
        public PerkKeys ID => perkID;
    }
}