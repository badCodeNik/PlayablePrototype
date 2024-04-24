using System;
using Source.Scripts.KeysHolder;
using UnityEngine;

namespace Source.Scripts.LibrariesSystem
{
    
        [CreateAssetMenu(menuName = "Library/LocationsLibrary", fileName = "LocationsLibrary")]
        
        public class LocationsLibrary : Library<LocationPack, LocationKeys>
        {
            
        }
    
        [Serializable]
        public class LocationPack : ILibraryItem<LocationKeys>
        {
            [SerializeField] private LocationKeys locationID;
            [SerializeField] private string prefabPath;
            
            public string PrefabPath => prefabPath;
            public LocationKeys ID => locationID;
        }
    }
