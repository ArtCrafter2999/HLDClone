using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;
using Zenject;

public class GenericMeleeEnemy : MonoBehaviour
{
    public Animator animator;
    public float invokeDistance;
    public float invokeTime;
    public float speed;
    public float attackDistance;
    public float preparingTime;
    public float attackTime;
    public float attackForce;
    [NonSerialized] public Rigidbody2D Rigidbody;

    public GameObject Player { get; private set; }
    private IState _state;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody = GetComponent<Rigidbody2D>();
        ChangeState(new IdleState(this));
    }

    public void ChangeState(IState newState)
    {
        _state = newState;
        newState.Enter();
    }

    private void Update()
    {
        _state.Stay();
    }
}
