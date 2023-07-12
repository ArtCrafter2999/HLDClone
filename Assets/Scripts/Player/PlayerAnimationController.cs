using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerMeleeAttack meleeAttack;
        [SerializeField] private Animator animator;
        
        [Header("Sound")]
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip meleeAttackClip;
        private int _dir = 2;
        private bool _isAttacking;

        private void Start()
        {
            meleeAttack.attackPerformed.AddListener(MeleeAttack);
            meleeAttack.attackEnded.AddListener(() => _isAttacking = false);
        }

        private void Update()
        {
            if(_isAttacking) return;
            var moveInput = PlayerInputs.Instance.Game.Move.ReadValue<Vector2>();
            animator.SetBool("IsWalking", moveInput != Vector2.zero);
            if (moveInput.y > 0) _dir = 0;
            if (moveInput.y < 0) _dir = 2;
            if (moveInput.x > 0) _dir = 1;
            if (moveInput.x < 0) _dir = 3;
            animator.SetInteger("Direction", _dir);
        }

        private void MeleeAttack()
        {
            var angle = Vector2.SignedAngle(Vector2.up, PlayerInputs.MouseDirection(transform.position));
            switch (angle)
            {
                case > -45 and <= 45:
                    _dir = 0;
                    break;
                case > -135 and <= -45:
                    _dir = 1;
                    break;
                case > 135 or <= -135 :
                    _dir = 2;
                    break;
                case > 45 and <= 135:
                    _dir = 3;
                    break;
                default:
                    _dir = 0;
                    break;
            }
            animator.SetInteger("Direction", _dir);
            //print("mousePos:" + mousepos + "; angle:" + angle + "; dir:" + dir);
            animator.SetTrigger("Attack");
            source.PlayOneShot(meleeAttackClip);
            _isAttacking = true;
        }
    }
}