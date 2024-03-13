using System;

namespace Source.EasyECS
{
    public static  class EcsFilterExtensions
    {
        public static void ForeachEntities(this EcsFilter filter, Action<int> action)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, bool> action, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, boolValue);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, string, bool> action, string stringValue, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, stringValue, boolValue);
            }
        }
        public static int GetFirstEntity(this EcsFilter filter)
        {
            foreach (var entity in filter)
            {
                return entity;
            }

            throw new Exception("Пустой филтр");
        }
    }
}
