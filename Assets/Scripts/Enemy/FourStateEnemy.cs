using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;
using Zenject;

public class FourStateEnemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Preparing,
        Attack
    }
    
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioSource source;
    public StateBase idleState;
    public StateBase runState;
    public StateBase preparingState;
    public StateBase attackState;
    public Animator animator;
    [NonSerialized] public Rigidbody2D Rigidbody;
    public bool IsFlipped => sprite.flipX;
    public bool CanFlip = true;

    public Transform Player { get; private set; }
    private StateBase _stateBase;

    [Inject]
    private void Construct(PlayerComposer playerComposer)
    {
        Player = playerComposer.transform;
        playerComposer.health.dead.AddListener(_ => ChangeState(idleState));
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        ChangeState(idleState);
    }

    public void ChangeState(StateBase newStateBase)
    {
        if(!enabled) return;
        if(_stateBase != null)_stateBase.Exit();
        _stateBase = newStateBase;
        newStateBase.Enter();
    }
    public void ChangeStateEnum(State newState)
    {
        ChangeState(newState switch
        {
            State.Idle => idleState,
            State.Run => runState,
            State.Preparing => preparingState,
            State.Attack => attackState,
            _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
        });
    }

    public void Play(AudioClip clip, float volume = 1)
    {
        if(source == null || clip == null) return;
        source.PlayOneShot(clip, volume);
    }

    private void Update()
    {
        _stateBase.Stay();
        
        if(!CanFlip) return;
        var dirToPlayer = (Player.position - transform.position).normalized;
        sprite.flipX = dirToPlayer.x < 0;
    }
}
