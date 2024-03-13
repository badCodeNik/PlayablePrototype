using Source.Scripts.Data;

namespace Source.EasyECS
{
    public class EasyNode : EasyMonoBehaviour
    {
        private Componenter _componenter;
        [EasyInject] private GameConfiguration _gameConfiguration;
        [EasyInject] private EventHub _eventHub;
        public static Componenter EcsComponenter { get => Instance._componenter; private set => Instance._componenter = value; }
        public static GameConfiguration GameConfiguration { get => Instance._gameConfiguration; private set => Instance._gameConfiguration = value; }
        public static EventHub EventHub { get => Instance._eventHub; private set => Instance._eventHub = value; }
        public static EasyNode Instance { get; set; }
        public override void Initialize()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
            EcsComponenter = GetSharedEcsSystem<Componenter>();
        }
    }
}