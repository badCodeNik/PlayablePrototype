using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.Libraries
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
        [SerializeField] private GameObject prefab;

        public HeroInfo HeroInfo => heroInfo;
        public GameObject Prefab => prefab;
        public HeroKeys ID => heroID;
    }
}