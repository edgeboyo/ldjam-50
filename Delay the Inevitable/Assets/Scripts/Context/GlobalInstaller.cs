using Config;
using RuntimeSets;
using UnityEngine;
using Zenject;

namespace Context
{
    [CreateAssetMenu(fileName = "GlobalInstaller", menuName = "ScriptableObjects/Context/GlobalInstaller")]
    public class GlobalInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GlobalEvents globalEvents;
        
        [SerializeField] private PlayerConfig playerConfig;


        public override void InstallBindings()
        {
            QueueInstanceAsSingle(globalEvents);
            
            QueueInstanceAsSingle(playerConfig);

            Container.Bind(typeof(PlayerRuntimeSet)).FromNew().AsSingle();
            Container.Bind(typeof(EnemyRuntimeSet)).FromNew().AsSingle();
        }
        
        private void QueueInstanceAsSingle<TInstance>(TInstance instance)
        {
            Container.BindInstance(instance).AsSingle();
            Container.QueueForInject(instance);
        }
    }
}