using System;
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.LibrariesSystem
{
    
    [CreateAssetMenu(menuName = "Library/ProjectileLibrary", fileName = "ProjectileLibrary")]
    public class ProjectileLibrary : Library<ProjectilePack, ProjectileKeys>
    {
        
    }
    
    [Serializable]
    public class ProjectilePack : ILibraryItem<ProjectileKeys>
    {
        [SerializeField] private ProjectileKeys projectileID; 
        [SerializeField] private float speed; 
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite previewSprite;
        

        public float Speed => speed;

        public GameObject Prefab => prefab;

        public Sprite PreviewSprite => previewSprite;


        public ProjectileKeys ID => projectileID;
    }
}