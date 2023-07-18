using System;
using UnityEngine;

namespace Player
{
    public class PlayerComposer : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController animations;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerMeleeAttack meleeAttack;
        [SerializeField] private PlayerDash dash;
        public Health health;
        private void Start()
        {
            meleeAttack.attackPerformed.AddListener(animations.MeleeAttack);
            meleeAttack.attackPerformed.AddListener(CheckDisableMove);
            dash.dashPerformed.AddListener(CheckDisableMove);
        }

        private void Update()
        {
            movement.CanMove = !meleeAttack.IsAttacking && !dash.IsDashing;
            animations.CanWalk = movement.CanMove;
            dash.CanDash = !meleeAttack.IsAttacking;
            meleeAttack.CanAttack = !dash.IsDashing;
        }

        private void CheckDisableMove()
        {
            movement.CanMove = !meleeAttack.IsAttacking && !dash.IsDashing;
            animations.CanWalk = movement.CanMove;
        }
    }
}