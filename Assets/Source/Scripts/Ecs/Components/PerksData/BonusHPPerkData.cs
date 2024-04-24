using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct BonusHPPerkData : IEcsData<int>
    {
        public int BonusHP;

        public void InitializeValues(int value)
        {
            BonusHP = value;
        }
    }
}