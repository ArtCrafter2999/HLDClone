using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Environment;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip available;
        [SerializeField] private AudioClip nothing;
        private Transform _iconTransform;
        private List<Interactable> _interactables = new List<Interactable>();
        private Interactable _selected;

        private void Start()
        {
            _iconTransform = icon.transform;
            PlayerInputs.Instance.Game.Interact.performed += _ => Interact();
        }
        
        private void Update()
        {
            if (_interactables.Count == 0)
            {
                _selected = null;
                icon.enabled = false;
                return;
            }
            Sort();
            if (_selected != _interactables.First())
            {
                _selected = _interactables.First();
                source.PlayOneShot(available);
            }
            _iconTransform.position = _interactables.First().pos.position;
            icon.enabled = true;
        }
        
        public void AddInteractable(Interactable interactable)
        {
            _interactables.Add(interactable);
        }

        public void RemoveInteractable(Interactable interactable)
        {
            _interactables.Remove(interactable);
        }
        
        private void Sort()
        {
            _interactables.Sort((e1, e2) => (int)(
                    Vector2.Distance(transform.position, e1.transform.position) -
                    Vector2.Distance(transform.position, e2.transform.position)
                )
            );
        }

        private void Interact()
        {
            if (_selected == null)
            {
                source.PlayOneShot(nothing);
                return;
            }
            _selected.Interact();
        }
    }
}