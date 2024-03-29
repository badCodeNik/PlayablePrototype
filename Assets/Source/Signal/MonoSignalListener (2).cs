using UnityEngine;

namespace Source.SignalSystem
{
    public abstract class MonoSignalListener<T> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T>(OnSignal);
        }

        protected abstract void OnSignal(T data);
    }
    
    public abstract class MonoSignalListener<T1, T2> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T1>(OnSignal);
            signal.Subscribe<T2>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T1>(OnSignal);
            signal.Unsubscribe<T2>(OnSignal);
        }

        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }
    
    public abstract class MonoSignalListener<T1, T2, T3> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T1>(OnSignal);
            signal.Subscribe<T2>(OnSignal);
            signal.Subscribe<T3>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T1>(OnSignal);
            signal.Unsubscribe<T2>(OnSignal);
            signal.Unsubscribe<T3>(OnSignal);
        }

        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }
    
    public abstract class MonoSignalListener<T1, T2, T3, T4> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T1>(OnSignal);
            signal.Subscribe<T2>(OnSignal);
            signal.Subscribe<T3>(OnSignal);
            signal.Subscribe<T4>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T1>(OnSignal);
            signal.Unsubscribe<T2>(OnSignal);
            signal.Unsubscribe<T3>(OnSignal);
            signal.Unsubscribe<T4>(OnSignal);
        }

        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
    }
    
    public abstract class MonoSignalListener<T1, T2, T3, T4, T5> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T1>(OnSignal);
            signal.Subscribe<T2>(OnSignal);
            signal.Subscribe<T3>(OnSignal);
            signal.Subscribe<T4>(OnSignal);
            signal.Subscribe<T5>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T1>(OnSignal);
            signal.Unsubscribe<T2>(OnSignal);
            signal.Unsubscribe<T3>(OnSignal);
            signal.Unsubscribe<T4>(OnSignal);
            signal.Unsubscribe<T5>(OnSignal);
        }

        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
    }
    
    public abstract class MonoSignalListener<T1, T2, T3, T4, T5, T6> : MonoBehaviour
    {
        [HideInInspector] public Signal signal;
        
        protected virtual void OnEnable()
        {
            signal.Subscribe<T1>(OnSignal);
            signal.Subscribe<T2>(OnSignal);
            signal.Subscribe<T3>(OnSignal);
            signal.Subscribe<T4>(OnSignal);
            signal.Subscribe<T5>(OnSignal);
            signal.Subscribe<T6>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            signal.Unsubscribe<T1>(OnSignal);
            signal.Unsubscribe<T2>(OnSignal);
            signal.Unsubscribe<T3>(OnSignal);
            signal.Unsubscribe<T4>(OnSignal);
            signal.Unsubscribe<T5>(OnSignal);
            signal.Unsubscribe<T6>(OnSignal);
        }

        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
    }
}