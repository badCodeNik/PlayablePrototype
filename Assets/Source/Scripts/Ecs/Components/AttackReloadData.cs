using Source.EasyECS.Interfaces;

namespace Source.Scripts.Ecs.Components
{
    public struct AttackReloadData : IEcsData<float>
    {
        public float RemainingTime;
        public void InitializeValues(float value)
        {
            RemainingTime = value;
        }
    }
}