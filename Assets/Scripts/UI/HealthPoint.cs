using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class HealthPoint : MonoBehaviour
{
    [SerializeField] private int point;
    private readonly Color _active = new(0.275f, 1, 0.639f);
    private readonly Color _notActive = new(0, 0, 0, 0.5f);
    private Image _image;
    private PlayerVariables _health;
    
    [Inject]
    private void Construct(PlayerVariables playerVariables)
    {
        _health = playerVariables;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.color = _health.Health >= point ? _active : _notActive;
    }
}
