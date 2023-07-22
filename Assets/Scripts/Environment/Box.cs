using System;
using UnityEngine;

namespace Environment
{
    public class Box : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private AudioClip clip;
        
        private Collider2D collider;
        private SpriteRenderer render;
        private AudioSource source;

        private void Start()
        {
            collider = GetComponent<Collider2D>();
            render = GetComponent<SpriteRenderer>();
            source = GetComponent<AudioSource>();
        }

        public void TakeDamage(int amount)
        {
            if (sprite == null) render.enabled = false;
            else render.sprite = sprite;
            collider.enabled = false;
            if(source != null && clip != null) source.PlayOneShot(clip);
        }
    }
}