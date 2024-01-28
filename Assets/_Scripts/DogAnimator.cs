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

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _dogController = GetComponentInParent<DogController>();
        AssignAnimationIDs();
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(_animIDGrounded, _dogController.Grounded);
        _animator.SetFloat(_animIDMotionSpeed, _dogController.InputDirection3D.magnitude);
        
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
