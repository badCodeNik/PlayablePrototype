using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEditor.Animations;
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
        [SerializeField] private AnimatorController animatorController;
        [SerializeField] private bool isBought;


        [SerializeField] private HeroInfo heroInfo;
        [SerializeField] private string prefab;
        [SerializeField] private Sprite icon;
        public bool IsBought => isBought;
        public AnimatorController AnimatorController => animatorController;

        public HeroInfo HeroInfo => heroInfo;
        public string Prefab => prefab;
        public Sprite Icon => icon;
        public HeroKeys ID => heroID;
    }
}