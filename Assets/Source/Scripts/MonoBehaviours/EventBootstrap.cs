using System;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.UI
{
    public class EventBootstrap : MonoBehaviour
    {
        public UnityEvent OnAwake;
        public UnityEvent OnStart;

        private void Awake()
        {
            OnAwake?.Invoke();
        }

        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}