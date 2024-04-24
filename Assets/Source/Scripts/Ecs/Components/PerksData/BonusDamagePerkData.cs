using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct BonusDamagePerkData : IEcsData<float>
    {
        public float BonusDamage;

        public void InitializeValues(float value)
        {
            BonusDamage = value;
        }
    }
}