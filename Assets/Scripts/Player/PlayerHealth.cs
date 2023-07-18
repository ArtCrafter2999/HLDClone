using System;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [Header("Sounds")]
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip damagedClip;
        [SerializeField] private AudioClip deathClip;
        [SerializeField] private AudioClip healedClip;
        private PlayerVariables _variables;
        [Inject]
        private void Construct(PlayerVariables variables)
        {
            _variables = variables;
        }

        private void Start()
        {
            
        }

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            source.PlayOneShot(damagedClip);
            impulseSource.GenerateImpulse();
        }

        private void Update()
        {
            _variables.MaxHealth = health;
            _variables.Health = health;
        }
    }
}