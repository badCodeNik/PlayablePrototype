using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Libraries
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
            foreach (var item in items)
            {
                _itemByID[item.ID] = item;
            }
        }
    }

    public interface ILibrary
    {
        public void Initialize();
    }
}