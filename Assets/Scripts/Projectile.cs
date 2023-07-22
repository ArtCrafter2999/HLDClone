using System;
using UnityEngine;

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        [SerializeField] private float destroyTime = 10;
        [SerializeField] private LayerMask obstacles;
        private Rigidbody2D _rigidbody;
        private bool _isInited = false;
        private Vector2 _direction;
        private bool _isPlayerOwner;
        private float _timeLeft;
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _timeLeft = destroyTime;
        }

        public void Init(Vector2 direction, bool isPlayerOwner)
        {
            _isInited = true;
            _direction = direction;
            _isPlayerOwner = isPlayerOwner;
            
        }

        public void Update()
        {
            if(!_isInited) return;
            _rigidbody.velocity = _direction * speed;
            transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, _direction), Vector3.forward);
            _timeLeft -= Time.deltaTime;
            if(_timeLeft<0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(((1<<col.gameObject.layer) & obstacles) == 0) return;
            if ((_isPlayerOwner && col.CompareTag("Player")) || (!_isPlayerOwner && col.CompareTag("Enemy")))  return;
            
            if (col.TryGetComponent(out ITakeDamage target))
            {
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
