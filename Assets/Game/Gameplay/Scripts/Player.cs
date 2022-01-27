using System;
using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Character character;
    [SerializeField] private NavMeshAgent agent;

    private Enemy[] enemies;
    private Enemy targetEnemy;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackTiming;
    [SerializeField] private float totalAttackTime = 1f;
    [SerializeField] private float attackSpeed = 1f;
    
    [SerializeField] private Transform exitPosition;
    [SerializeField] private float exitDistance = 1f;

    [SerializeField] private int _hp = 10;

    private enum PlayerState
    {
        Idle,
        Walk,
        Run,
        RunBack,
        Attack,
        ChangeWeapon,
        Dead,
        Win
    }

    private PlayerState playerState;

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        exitPosition = FindObjectOfType<Finish>().transform;
    }

    private void Update()
    {
        // if win or lose, return
        if (playerState == PlayerState.Win || playerState == PlayerState.Dead) return;
        
        // try move player according joystick
        var joystickDirection = joystick.Direction;
        var playerVelocity = new Vector3(joystickDirection.x, 0, joystickDirection.y) * moveSpeed;
        
        // check win condition
        var distance = Vector3.Distance(transform.position, exitPosition.position);
        if (playerState != PlayerState.Dead && distance < exitDistance)
        {
            ChangePlayerState(PlayerState.Win);
            return;
        }
        
        // check die condition
        if (playerState != PlayerState.Win && _hp <= 0)
        {
            ChangePlayerState(PlayerState.Dead);
            return;
        }

        var canAttack = TryAttack();
        switch (playerState)
        {
            case PlayerState.Idle:
                if (canAttack) return;
                if (playerVelocity.magnitude > walkSpeed)
                {
                    ChangePlayerState(PlayerState.Walk);
                }
                break;
            case PlayerState.Walk:
                if (canAttack) return;
                agent.velocity = playerVelocity;
                if (playerVelocity.magnitude < walkSpeed)
                {
                    ChangePlayerState(PlayerState.Idle);
                }
                if (playerVelocity.magnitude > runSpeed)
                {
                    ChangePlayerState(PlayerState.Run);
                }
                break;
            case PlayerState.Run:
                if (canAttack) return;
                agent.velocity = playerVelocity;
                if (playerVelocity.magnitude < runSpeed)
                {
                    ChangePlayerState(PlayerState.Walk);
                }
                break;
            case PlayerState.RunBack:
                break;
            case PlayerState.ChangeWeapon:
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Win:
                break;
            case PlayerState.Attack:
                attackTiming -= Time.deltaTime;
                if (attackTiming <= 0)
                {
                    ChangePlayerState(PlayerState.Walk);
                }
                else
                {
                    var v = targetEnemy.transform.position - transform.position;
                    transform.forward = new Vector3(v.x, 0, v.z);
                    agent.velocity = v * attackSpeed;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void ChangePlayerState(PlayerState state)
    {
        if (playerState == state) return;
        ExitState();
        playerState = state;
        EnterState();
    }

    private bool TryAttack()
    {
        var minDistance = attackRange;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetEnemy = enemy;
            }
        }
        
        if (minDistance < attackRange)
        {
            ChangePlayerState(PlayerState.Attack);
            return true;
        }
        return false;
    }

    private void ExitState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.RunBack:
                break;
            case PlayerState.ChangeWeapon:
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Win:
                break;
            case PlayerState.Attack:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerState), playerState, null);
        }
    }

    private void EnterState()
    {
        Debug.Log(playerState);
        switch (playerState)
        {
            case PlayerState.Idle:
                character.AnimateIdle();
                break;
            case PlayerState.Walk:
                character.AnimateWalk();
                break;
            case PlayerState.Run:
                character.AnimateRun();
                break;
            case PlayerState.RunBack:
                break;
            case PlayerState.ChangeWeapon:
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Win:
                break;
            case PlayerState.Attack:
                attackTiming = totalAttackTime;
                character.AnimateAttack();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerState), playerState, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            
        }
    }
}
