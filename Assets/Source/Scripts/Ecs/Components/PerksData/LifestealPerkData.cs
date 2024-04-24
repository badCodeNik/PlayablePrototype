using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components.PerksData
{
    public struct LifestealPerkData : IEcsData<float>
    {
        public float LifestealAmount;
        
        public void InitializeValues(float value)
        {
            LifestealAmount = value;
        }
    }
}