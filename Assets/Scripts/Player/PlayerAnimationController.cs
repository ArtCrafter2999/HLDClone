using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private int _dir = 2;
        public bool CanWalk { get; set; } = true;



        private void Update()
        {
            Walk();
        }

        private void Walk()
        {
            if(!CanWalk) return;
            var moveInput = PlayerInputs.Instance.Game.Move.ReadValue<Vector2>();
            animator.SetBool("IsWalking", moveInput != Vector2.zero);
            if(moveInput == Vector2.zero) return;
            var angle = Vector2.SignedAngle(Vector2.up, moveInput);
            _dir = angle switch
            {
                > -45 and < 45 => 0,
                >= -135 and <= -45 => 1,
                > 135 or < -135 => 2,
                >= 45 and <= 135 => 3,
                _ => 2
            };
            animator.SetInteger("Direction", _dir);
        }

        public void MeleeAttack()
        {
            var angle = Vector2.SignedAngle(Vector2.up, PlayerInputs.MouseDirection(transform.position));
            var dir = angle switch
            {
                > -45 and <= 45 => 0,
                > -135 and <= -45 => 1,
                > 135 or <= -135 => 2,
                > 45 and <= 135 => 3,
                _ => 0
            };
            animator.SetInteger("AttackDirection", dir);
            //print("mousePos:" + mousepos + "; angle:" + angle + "; dir:" + dir);
            animator.SetTrigger("Attack");
        }

        public void Healing()
        {
            animator.SetTrigger("Heal");
        }

        public void Death()
        {
            animator.SetTrigger("Dead");
        }

        public void Down()
        {
            _dir = 2;
            animator.SetInteger("Direction", 2);
        }
    }
}