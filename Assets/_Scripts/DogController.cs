 using System;
 using System.Collections;
 using UnityEngine;
 using UnityEngine.Serialization;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


    public class DogController : MonoBehaviour
    {

        public Vector3 Velocity => horizontalVelocity;
        public Vector3 InputDirection3D => inputDirection3D;
        public bool Grounded => _grounded;
        
        [SerializeField] private float MaxSpeed;
        [SerializeField] private float Acceleration;
        [SerializeField] private float JumpForce;
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
        
        // Grounding Parameters
        public float GroundedClearance = 0.65f;
        
        public LayerMask GroundLayers;
        public float RoughTerrainOffset = -0.14f;
        private bool _grounded;
        public bool pooCollision;
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
            if (pooCollision)
            {
                float newY =  10f *(Mathf.Sin(5 * Time.time) -0.5f);
                transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);
                return;
            }
            GroundedCheck();
            Jump();
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

        private void Jump()
        {
            
            if(Grounded && inputSystem.jump)
            {
                Debug.Log("Jump");
                forceDirection += Vector3.up * JumpForce;
            }
        }

        private void GroundedCheck()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, GroundedClearance))
                _grounded = true;
            else
                _grounded = false;
        }

        public void OnCollisionWithPoo()
        {
            StartCoroutine(PooCollision());
        }

        IEnumerator PooCollision()
        {
            pooCollision = true;
            yield return new WaitForSeconds(5f);
            pooCollision = false;
        }
    

        private Vector3 RemoveYComponent(Vector3 dir)
        {
            dir.y = 0;
            return dir;
        }

        private void OnDrawGizmos()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;
            Vector3 direction = transform.TransformDirection(Vector3.down) * GroundedClearance;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
