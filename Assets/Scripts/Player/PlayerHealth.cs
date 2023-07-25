using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [Header("Healing")]
        [SerializeField] private float healingCoolDown;
        [SerializeField] private float healingDelay;
        public UnityEvent HealPerformed;
        
        [Header("Sounds")]
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip damagedClip;
        [SerializeField] private AudioClip deathClip;
        [SerializeField] private AudioClip healedClip;
        [SerializeField] private AudioClip cantHealClip;
        [SerializeField] private AudioClip lowHpClip;
        [SerializeField] private float lowHpCooldown;
        [SerializeField] private AudioClip healingClip;
        
        private PlayerVariables _variables;
        private float _lowHpCooldown;
        [NonSerialized] public bool IsHealing;
        
        [Inject]
        private void Construct(PlayerVariables variables)
        {
            _variables = variables;
        }

        private void Start()
        {
            _lowHpCooldown = lowHpCooldown;
            PlayerInputs.Instance.Game.Heal.performed += _ => Heal();
            dead.AddListener(_ => PlayerInputs.ClearInstance());
        }

        public override void TakeDamage(int amount)
        {
            if(!enabled) return;
            IsHealing = false;
            base.TakeDamage(amount);
            _variables.Health = health;
            source.PlayOneShot(damagedClip);
            impulseSource.GenerateImpulse();
        }

        private void Update()
        {
            _variables.MaxHealth = maxHealth;
            _variables.Health = health;
            if(health > 2) return;
            _lowHpCooldown -= Time.deltaTime;
            if (_lowHpCooldown <= 0)
            {
                source.PlayOneShot(lowHpClip);
                _lowHpCooldown = lowHpCooldown;
            }
        }

        private void Heal()
        {
            if (_variables.Medkits <= 0 || health >= maxHealth || IsHealing)
            {
                source.PlayOneShot(cantHealClip);
                return;
            }
            StartCoroutine(HealPerform());
        }

        private IEnumerator HealPerform()
        {
            _variables.Medkits--;
            IsHealing = true;
            source.PlayOneShot(healingClip);
            HealPerformed.Invoke();
            yield return new WaitForSeconds(healingDelay);
            while (IsHealing)
            {
                yield return new WaitForSeconds(healingCoolDown);
                TakeHeal(1);
                if (health >= maxHealth) IsHealing = false;
            }
        }
    }
}