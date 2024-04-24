using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct CriticalDamagePerkData : IEcsData<float>
    {
        public float CriticalDamageChance;

        public void InitializeValues(float value)
        {
            CriticalDamageChance = value;
        }
    }
}