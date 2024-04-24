using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct HPRestorationPerkData : IEcsData<float>
    {
        public float RestorationAmount;

        public void InitializeValues(float value)
        {
            RestorationAmount = value;
        }
    }
}