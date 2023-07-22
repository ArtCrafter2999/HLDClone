using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class MedkitsPoint : MonoBehaviour
    {
        [SerializeField] private int point;
        private readonly Color _active = new(0.275f, 1, 0.639f);
        private readonly Color _notActive = new(0, 0, 0, 0.5f);
        private readonly Color _notAvailable = new(0, 0, 0, 1);
        private Image _image;
        private PlayerVariables _variables;

        [Inject]
        private void Construct(PlayerVariables playerVariables)
        {
            _variables = playerVariables;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (point > _variables.MaxMedkits)
            {
                _image.color = _notAvailable;
                return;
            }

            _image.color = _variables.Medkits >= point ? _active : _notActive;
        }
    }
}
