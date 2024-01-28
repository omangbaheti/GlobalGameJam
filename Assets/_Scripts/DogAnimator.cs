using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private DogController _dogController;
    //animation Paramers
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    // 1 is indie, 2 is running, 3 is jumping
    private int state;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _dogController = GetComponentInParent<DogController>();
        AssignAnimationIDs();
        state = 1;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        //_animator.SetBool(_animIDGrounded, _dogController.Grounded);
        //_animator.SetFloat(_animIDMotionSpeed, _dogController.InputDirection3D.magnitude);
        ChangeState();
    }

    void ChangeState()
    {
        if(!_dogController.Grounded)
        {
            if (state != 3)
            {
                _animator.Play("HuskyJumping");
                state = 3;
            }
        }
        else
        {
            if (Mathf.Abs(_dogController.horizontalVelocity.x) > 1f || Mathf.Abs(_dogController.horizontalVelocity.z) > 1)
            {
                if (state != 2)
                {
                    _animator.Play("HuskyRunning");
                    state = 2;
                }
            }
            else
            {
                if (state != 1)
                {
                    _animator.Play("HuskyIdle");
                    state = 1;
                }
            }
        }
    }
    
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
}
