
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;

namespace Source.EasyECS
{
    public abstract class EasyMonoBehaviour : SerializedMonoBehaviour
    {
        private DataPack _dataPack;
        private bool _isInitialized = false;
        private GameShare _gameShare;
        public GameShare GameShare => _gameShare;
        
        public void PreInit(GameShare gameShare, DataPack dataPack)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _dataPack = dataPack;
            _isInitialized = true;
        }

        public virtual void Initialize(){}

        public T GetSharedMonoBehaviour<T>() where T : EasyMonoBehaviour
        {
            return _gameShare.GetSharedMonoBehaviour<T>();
        }
        
        public T GetSharedEcsSystem<T>() where T : IEcsSharingSystem
        {
            return _gameShare.GetSharedEcsSystem<T>();
        }

        public void Inject()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            foreach (var field in fields)
            {
                var injectAttribute = (EasyInjectAttribute)field.GetCustomAttributes(typeof(EasyInjectAttribute), true).FirstOrDefault();

                if (injectAttribute != null)
                {
                    var fieldType = field.FieldType;

                    if (typeof(EasyMonoBehaviour).IsAssignableFrom(fieldType))
                    {
                        var sharedMonoBehaviourMethod = typeof(EasyMonoBehaviour).GetMethod("GetSharedMonoBehaviour").MakeGenericMethod(fieldType);
                        var sharedMonoBehaviour = sharedMonoBehaviourMethod.Invoke(this, null);

                        field.SetValue(this, sharedMonoBehaviour);
                    }
                    else if (typeof(EasySystem).IsAssignableFrom(fieldType) && typeof(IEcsSharingSystem).IsAssignableFrom(fieldType))
                    {
                        var sharedEasySystemMethod = typeof(EasyMonoBehaviour).GetMethod("GetSharedEcsSystem").MakeGenericMethod(fieldType);
                        var sharedSystem = sharedEasySystemMethod.Invoke(this, null);

                        field.SetValue(this, sharedSystem);
                    }
                    else if (typeof(Configuration).IsAssignableFrom(fieldType))
                    {
                        var configurationHub = GetSharedMonoBehaviour<ConfigurationHub>();

                        var getConfigMethod = typeof(ConfigurationHub).GetMethod("GetConfigByType").MakeGenericMethod(fieldType);
                        var sharedObject = getConfigMethod.Invoke(configurationHub, null);

                        field.SetValue(this, sharedObject);
                    }
                }
            }
        }
    }
}