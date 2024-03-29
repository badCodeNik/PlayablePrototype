using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components
{
    public struct DestroyingData : IEcsData<float>
    {
        public float TimeRemaining;
        public void InitializeValues(float value)
        {
            TimeRemaining = value;
        }
    }
}