using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct BonusAttackSpeedPerkData : IEcsData<float>
    {
        public float BonusAttackSpeed;

        public void InitializeValues(float value)
        {
            BonusAttackSpeed = value;
        }
    }
}