using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Transform spawnPoint;
        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerMovement>(
                player, spawnPoint.position, Quaternion.identity, null);
            Container
                .Bind<PlayerMovement>()
                .FromInstance(playerInstance)
                .AsSingle();
        }
    }
}