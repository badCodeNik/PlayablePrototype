using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct BleedingPerkData : IEcsData<float>
    {
        public float BleedingDamage;
        public void InitializeValues(float value)
        {
            BleedingDamage = value;
        }
    }
}