using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using Zenject;

namespace Environment
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField] private Transform playerPos;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource source;
        [SerializeField] private float idleClipCooldown;
        [SerializeField] private AudioClip idleClip;
        [SerializeField] private AudioClip takeClip;
        private PlayerComposer _player;
        private float _cooldown;
        private bool _isInteracted;

        [Inject]
        private void Construct(PlayerComposer player)
        {
            _player = player;
        }

        public void Interacted()
        {
            _isInteracted = true;
            StartCoroutine(Perform());
        }

        private void Update()
        {
            if (_isInteracted) return;
            _cooldown -= Time.deltaTime;
            if (!(_cooldown <= 0)) return;
            _cooldown = idleClipCooldown;
            source.PlayOneShot(idleClip);
        }

        private IEnumerator Perform()
        {
            PlayerInputs.Instance.Game.Disable();
            _player.animations.Down();
            _player.transform.DOMove(playerPos.position, 1f);
            yield return new WaitForSeconds(1.5f);
            animator.SetTrigger("Perform");
            _player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }

        private void PlaySound()
        {
            source.PlayOneShot(takeClip);
        }

        private void TurnPlayerOn()
        {
            _player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        private void TurnControlsOn()
        {
            PlayerInputs.Instance.Game.Enable();
        }
    }
}