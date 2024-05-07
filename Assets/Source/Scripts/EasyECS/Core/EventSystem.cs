using System;
using System.Collections.Generic;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.Data;
using Source.Scripts.Extensions;

namespace Source.Scripts.EasyECS.Core
{
    public class EventSystem : IEcsRunSystem, IGameShareItem
    {
        private List<EcsFilter> _filterList = new();
        private List<Action> _jobQuery = new();
        private List<Type> _types = new();
        private List<EcsEventListener> _listeners = new();
        private Dictionary<Type, List<EcsEventListener>> _listenersByType = new();
        private EcsWorld World { get; set; }
        private Componenter Componenter { get; set; }


        public void AddListener(EcsEventListener eventListener)
        {
            var types = eventListener.GetListenTypes();
            foreach (var type in types)
            {
                _listeners.Add(eventListener);
                if (!_listenersByType.ContainsKey(type)) _listenersByType[type] = new List<EcsEventListener>();
                _listenersByType[type].Add(eventListener);
            }
        }

        private void InvokeEvent<T>(T data) where T : struct, IEcsEvent<T>
        {
            var type = typeof(T);
            if (!_listenersByType.ContainsKey(type)) return;
            foreach (var listener in _listenersByType[type])
            {
                listener.InvokeEvent(data);
            }
        }

        protected void SetEventFilter<T>() where T : struct
        {
            var type = typeof(T);
            var filter = World.Filter<T>().End();
            _types.Add(type);
            _filterList.Add(filter);
        }

        private void AddActionQuery()
        {
            for (int i = _jobQuery.Count - 1; i >= 0; i--) _jobQuery.Pop(i).Invoke();
        }

        private void TryDelEvents()
        {
            foreach (var filter in _filterList)
            {
                foreach (var entity in filter) Componenter.DelEntity(entity);
            }
        }

        public void RegistryEvent<T>(T eventData) where T : struct, IEcsEvent<T>
        {
            if (!_types.Contains(typeof(T)))
                SetEventFilter<T>();

            _jobQuery.Add(() =>
            {
                var entity = Componenter.GetNewEntity();
                ref var data = ref Componenter.AddOrGet<T>(entity);
                data.InitializeValues(eventData);
                InvokeEvent(data);
            });
        }

        public void PreInit(EcsWorld world, GameShare gameShare)
        {
            Componenter = gameShare.GetSharedObject<Componenter>();
            World = world;
        }

        public void Run(IEcsSystems systems)
        {
            TryDelEvents();
            AddActionQuery();
        }
    }

    public interface IEcsEvent<in T> : IEcsComponent where T : struct, IEcsComponent
    {
        public void InitializeValues(T eventData);
    }


    public abstract class EcsEventListener : EasySystem
    {
        protected EcsEventListener()
        {
        }

        public abstract void InvokeEvent<T>(T data) where T : struct, IEcsEvent<T>;
        public abstract Type[] GetListenTypes();
    }

    public abstract class EcsEventListener<T> : EcsEventListener where T : struct, IEcsEvent<T>
    {
        public override void InvokeEvent<TData>(TData data)
        {
            if (data is T eventData) OnEvent(eventData);
        }

        public override Type[] GetListenTypes()
        {
            return new[] { typeof(T) };
        }

        public abstract void OnEvent(T data);
    }

    public abstract class EcsEventListener<T1, T2> : EcsEventListener
        where T1 : struct, IEcsComponent
        where T2 : struct, IEcsComponent
    {
        public override void InvokeEvent<TData>(TData data)
        {
            if (data is T1 eventData1) OnEvent(eventData1);
            else if (data is T2 eventData2) OnEvent(eventData2);
        }

        public override Type[] GetListenTypes()
        {
            return new[] { typeof(T1), typeof(T2) };
        }

        public abstract void OnEvent(T1 data);
        public abstract void OnEvent(T2 data);
    }

    public abstract class EcsEventListener<T1, T2, T3> : EcsEventListener
        where T1 : struct, IEcsComponent
        where T2 : struct, IEcsComponent
        where T3 : struct, IEcsComponent
    {
        public override void InvokeEvent<TData>(TData data)
        {
            if (data is T1 eventData1) OnEvent(eventData1);
            else if (data is T2 eventData2) OnEvent(eventData2);
            else if (data is T3 eventData3) OnEvent(eventData3);
        }

        public override Type[] GetListenTypes()
        {
            return new[] { typeof(T1), typeof(T2), typeof(T3) };
        }

        public abstract void OnEvent(T1 data);
        public abstract void OnEvent(T2 data);
        public abstract void OnEvent(T3 data);
    }

    public abstract class EcsEventListener<T1, T2, T3, T4> : EcsEventListener
        where T1 : struct, IEcsComponent
        where T2 : struct, IEcsComponent
        where T3 : struct, IEcsComponent
        where T4 : struct, IEcsComponent
    {
        public override void InvokeEvent<TData>(TData data)
        {
            if (data is T1 eventData1) OnEvent(eventData1);
            else if (data is T2 eventData2) OnEvent(eventData2);
            else if (data is T3 eventData3) OnEvent(eventData3);
            else if (data is T4 eventData4) OnEvent(eventData4);
        }

        public override Type[] GetListenTypes()
        {
            return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
        }

        public abstract void OnEvent(T1 data);
        public abstract void OnEvent(T2 data);
        public abstract void OnEvent(T3 data);
        public abstract void OnEvent(T4 data);
    }
}