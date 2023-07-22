using System;
using UnityEngine;

namespace Environment
{
    public class SpriteSwitch : MonoBehaviour, IStateChanging
    {
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite otherSprite;
        private SpriteRenderer _renderer;
        private bool _isSwitched;
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (defaultSprite == null) defaultSprite = _renderer.sprite;
        }

        public void ChangeState()
        {
            _renderer.sprite = !_isSwitched ? otherSprite : defaultSprite;
        }
    }
}