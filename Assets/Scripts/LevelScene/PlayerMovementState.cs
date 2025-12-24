using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Cinemachine;

public class PlayerMovementState : MonoBehaviour
{
    // [SerializeField] private PlayerMovement2 playerMovement;
    // AudioManager audioManager;

    public enum MoveState
    {
        Idle,
        Running,
        Jump,
        Fall,
        Double_Jump,
        Wall_Jump,
    }
    public MoveState CurrentMoveState { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;

    private const string idleAnim = "Idle";
    private const string runAnim = "Running";
    private const string jumpAnim = "Jump";
    private const string fallAnim = "Fall";
    private const string doubleJumpAnim = "Double_Jump";
    private const string wallJumpAnim = "Wall_Jump";

    public static Action<MoveState> OnPlayerMoveStateChanged;

    // void Start()
    // {
    //     audioManager = GameObject
    //         .FindGameObjectWithTag("Audio")
    //         .GetComponent<AudioManager>();
    // }

    void Update()
    {
        
    }

    public void SetMoveState(MoveState moveState)
    {
        if (moveState == CurrentMoveState) return;

        switch (moveState)
        {
            case MoveState.Idle:
                HandleIdle();
                break;

            case MoveState.Running:
                HandleRun();
                break;

            case MoveState.Jump:
                HandleJump();
                break;

            case MoveState.Fall:
                HandleFall();
                break;

            case MoveState.Double_Jump:
                HandleDoubleJump();
                break;

            case MoveState.Wall_Jump:
                HandleWallJump();
                break;

            default:
                Debug.LogError($"Invalid Movement State: {moveState}");
                break;
        }

        OnPlayerMoveStateChanged?.Invoke(moveState);

        CurrentMoveState = moveState;
    }

    private void HandleIdle()
    {
        animator.Play(idleAnim);
    }

    private void HandleRun()
    {
        animator.Play(runAnim);
    }

    private void HandleJump()
    {
        animator.Play(jumpAnim);
    }

    private void HandleFall()
    {
        animator.Play(fallAnim);
    }

    private void HandleDoubleJump()
    {
        animator.Play(doubleJumpAnim);
    }

    private void HandleWallJump()
    {
        animator.Play(wallJumpAnim);
    }
}
