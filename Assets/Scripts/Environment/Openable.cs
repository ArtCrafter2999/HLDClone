using System;
using System.Collections;
using UnityEngine;

namespace Environment
{
    public class Openable : MonoBehaviour, IStateChanging
    {
        [SerializeField] private float speedModifier = 1;
        [SerializeField] private bool initallyOpen;
        [SerializeField] private bool closeWithDelay;
        [SerializeField] private float closingDelay = 4;
        [SerializeField] private Interactable interactable;
        private Animator _animator;
        private Collider2D _collider;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            TryGetComponent(out _collider);
            _animator.SetBool("Open" , initallyOpen);
        }

        public void ChangeState()
        {
            _animator.SetBool("Open" , !_animator.GetBool("Open"));
            _animator.SetFloat("Speed" , speedModifier);
            if (closeWithDelay) StartCoroutine(DelayedClosing());
        }
        public void ChangeState(bool state)
        {
            _animator.SetBool("Open" , state);
            _animator.SetFloat("Speed" , speedModifier);
            if (closeWithDelay) StartCoroutine(DelayedClosing());
        }

        private IEnumerator DelayedClosing()
        {
            yield return new WaitForSeconds(closingDelay);
            ChangeState(false);
            interactable.enabled = true;
        }
        
        private void ColliderEnable()
        {
            if(_collider == null) return;
            _collider.enabled = true;
        }
        private void ColliderDisable()
        {
            if(_collider == null) return;
            _collider.enabled = false;
        }
    }
}