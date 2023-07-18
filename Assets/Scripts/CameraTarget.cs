using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float lerp = 0.1f;
    private Transform _player;

    [Inject]
    private void Construct(PlayerComposer playerComposer)
    {
        _player = playerComposer.transform;
    }

    public void FixedUpdate()
    {
        transform.position = Vector2.Lerp(_player.position, PlayerInputs.MousePosition(), lerp);
    }
}
