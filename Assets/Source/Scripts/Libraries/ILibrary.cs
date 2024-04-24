using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.KeysHolder;
using UnityEngine; 

namespace Source.Scripts.LibrariesSystem
{
    public class Library<T, TE> : ScriptableObject, ILibrary 
        where TE : Enum
        where T : ILibraryItem<TE> 
    {
        [SerializeField] private T[] items;
        private Dictionary<TE, T> _itemByID;

        /// <summary>
        /// Use it runtime only.
        /// </summary>
        public T GetByID(TE id)
        {
            return _itemByID[id];
        }

        public List<T> GetAllPacks()
        {
            List<T> packs = new List<T>();
            packs = items.ToList();
            return packs;
        }

        
        /// <summary>
        /// Use it on validation only.
        /// </summary>

        public T GetByIDIterations(TE id)
        {
            Initialize();
            if (_itemByID.ContainsKey(id))
            {
                return _itemByID[id];
            }
            throw new Exception($"Cannot find library item in library : <<{GetType().Name}>> by index : <<{id}>>.");
        }

        public void Initialize()
        {
            _itemByID = new Dictionary<TE, T>();
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    _itemByID[item.ID] = item;
                }
            }
        }
    }

    public interface ILibrary
    {
        public void Initialize();
    }
}