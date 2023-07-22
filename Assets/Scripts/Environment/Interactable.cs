using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Environment
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent interacted;
        public Transform pos;
        [SerializeField] private float distance = 0.5f;
        [SerializeField] private bool disableAfterInteraction;
        [SerializeField] private ProtectedAudioSource source;
        [SerializeField] private AudioClip activate;
        private PlayerInteractions _playerInteractions;
        private bool _added;


        [Inject]
        private void Construct(PlayerComposer player)
        {
            _playerInteractions = player.interactions;
        }

        public void Interact()
        {
            if(source != null && activate != null) source.Play(activate);
            interacted.Invoke();
            if (!disableAfterInteraction) return;

            _playerInteractions.RemoveInteractable(this);
            enabled = false;
        }

        private void Update()
        {
            if (!_added && Vector2.Distance(transform.position, _playerInteractions.transform.position) < distance)
            {
                _playerInteractions.AddInteractable(this);
                _added = true;
            }
            else if (_added && Vector2.Distance(transform.position, _playerInteractions.transform.position) > distance)
            {
                _playerInteractions.RemoveInteractable(this);
                _added = false;
            }
        }
    }
}
