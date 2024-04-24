using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.LibrariesSystem
{
    [CreateAssetMenu(menuName = "Library/EnemiesLibrary", fileName = "EnemiesLibrary")]
    public class EnemiesLibrary : Library<EnemyPack, EnemyKeys>
    {
        
    }
    
    [Serializable]
    public class EnemyPack : ILibraryItem<EnemyKeys>
    {
        [SerializeField] private EnemyKeys heroID;
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private string prefab;
        [SerializeField] private Sprite icon;

        public EnemyInfo EnemyInfo => enemyInfo;
        public string Prefab => prefab;
        public Sprite Icon => icon;
        public EnemyKeys ID => heroID;
    }
}