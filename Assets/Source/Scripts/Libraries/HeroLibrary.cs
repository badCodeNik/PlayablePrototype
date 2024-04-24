using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.LibrariesSystem
{
    [CreateAssetMenu(menuName = "Library/HeroLibrary", fileName = "HeroLibrary")]
    public class HeroLibrary : Library<HeroPack, HeroKeys>
    {
        
    }
    
    [Serializable]
    public class HeroPack : ILibraryItem<HeroKeys>
    {
        [SerializeField] private HeroKeys heroID;
        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private string prefab;
        [SerializeField] private Sprite icon;

        public HeroInfo HeroInfo => heroInfo;
        public string Prefab => prefab;
        public Sprite Icon => icon;
        public HeroKeys ID => heroID;
    }
}