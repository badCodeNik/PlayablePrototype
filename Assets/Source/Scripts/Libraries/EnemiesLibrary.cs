using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.Libraries
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
        [SerializeField] private GameObject prefab;

        public EnemyInfo EnemyInfo => enemyInfo;
        public GameObject Prefab => prefab;
        public EnemyKeys ID => heroID;
    }
}