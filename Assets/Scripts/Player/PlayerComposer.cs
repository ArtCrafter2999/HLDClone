using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerComposer : MonoBehaviour
    {

        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerMeleeAttack meleeAttack;
        public PlayerAnimationController animations;
        public PlayerDash dash;
        public PlayerHealth health;
        public PlayerInteractions interactions;
        private void Start()
        {
            meleeAttack.attackPerformed.AddListener(animations.MeleeAttack);
            health.HealPerformed.AddListener(animations.Healing);
            meleeAttack.attackPerformed.AddListener(CheckDisableMove);
            dash.dashPerformed.AddListener(CheckDisableMove);
            health.HealPerformed.AddListener(CheckDisableMove);
            StartCoroutine(LateActivate());
        }

        private IEnumerator LateActivate()
        {
            yield return new WaitForSeconds(7);
            PlayerInputs.Instance.Game.Enable();
        }

        private void Update()
        {
            CheckDisableMove();
            dash.CanDash = !meleeAttack.IsAttacking && !health.IsHealing;
            meleeAttack.CanAttack = !dash.IsDashing && !health.IsHealing;
        }

        private void CheckDisableMove()
        {
            movement.CanMove = !meleeAttack.IsAttacking && !dash.IsDashing && !health.IsHealing;
            animations.CanWalk = movement.CanMove;
        }
    }
}