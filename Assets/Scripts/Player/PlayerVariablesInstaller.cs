using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerVariablesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerVariables variables;
        public override void InstallBindings()
        {
            Container.Bind<PlayerVariables>().FromInstance(variables).AsSingle();
        }
    }
}