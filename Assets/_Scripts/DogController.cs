 using System;
 using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


    public class DogController : MonoBehaviour
    {
        
        [SerializeField] private float MaxSpeed;
        [SerializeField] private float Acceleration;
        [SerializeField] private float JumpHeight;
        [SerializeField] private float RotationSmoothTime;
       
        //velocity
        private Vector3 horizontalVelocity;
        
        //inputs
        private Vector3 inputDirection3D;
        private Vector3 lookDirection3D;
        
        //Gameobjects
        private Rigidbody rigidBody;
        private Camera playerCamera;
        private InputSystem inputSystem;
        
        //constants
        private float threshold = 0.1f;
        private Vector3 forceDirection;
        private float _rotationVelocity;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            playerCamera = transform.parent.GetComponentInChildren<Camera>();
            inputSystem = GetComponent<InputSystem>();
        }

        private void Start()
        {
            
        }

        
        private void FixedUpdate()
        {
            SanitisedInputs();
            Move();
        }
        

        private void Update()
        {
            SanitisedInputs();
        }

        private void LateUpdate()
        {
            
        }

        private void SanitisedInputs()
        {
            float inputMagnitude = inputSystem.AnalogMovement ? inputSystem.move.magnitude : 1f;
            inputDirection3D = new Vector3(inputSystem.move.x, 0f, inputSystem.move.y).normalized * inputMagnitude;
            inputMagnitude = inputSystem.AnalogMovement ? inputSystem.look.magnitude : 1f;
            Debug.Log(inputMagnitude);
            lookDirection3D = new Vector3(inputSystem.look.x, 0f, inputSystem.look.y).normalized * inputMagnitude;
        }
        
        private void Move()
        {
            Vector3 cameraRight = RemoveYComponent(playerCamera.transform.right).normalized;
            Vector3 cameraForward = RemoveYComponent(playerCamera.transform.forward).normalized;
            
            horizontalVelocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            if (horizontalVelocity.magnitude < MaxSpeed - threshold)
            {
                //provides velocity
                forceDirection += inputDirection3D.x * cameraRight * Acceleration;
                forceDirection += inputDirection3D.z * cameraForward * Acceleration;
                rigidBody.AddForce(forceDirection, ForceMode.Acceleration);
                
                //rotates the body
                float targetAngle = Mathf.Atan2(inputDirection3D.x, inputDirection3D.z) * Mathf.Rad2Deg
                                    + playerCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity,
                    RotationSmoothTime);                

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);                                
                forceDirection = Vector3.zero;
                
                
            }
            if (horizontalVelocity.sqrMagnitude > MaxSpeed * MaxSpeed)
                rigidBody.velocity = horizontalVelocity.normalized * MaxSpeed + Vector3.up * rigidBody.velocity.y;
        }

        

        private Vector3 RemoveYComponent(Vector3 dir)
        {
            dir.y = 0;
            return dir;
        }

        private bool GroundCheck()
        {
            return false;
        }

        private void Jump()
        {
            
        }
    }
