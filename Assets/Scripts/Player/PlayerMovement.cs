using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var inputVector = PlayerInputs.Instance.Game.Move.ReadValue<Vector2>();
            _rigidbody.velocity = inputVector * speed;
        }
    }
}