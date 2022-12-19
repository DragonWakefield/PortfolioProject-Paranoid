using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    // Movement Setting Variables (Public)
    [Header("Player Settings")]
    public float moveSpeed = 3f;
    public float sprintModifier = 1.75f;
    public float speedChangeModifier = 10f;

    public float jumpHeight = 1.2f;
    public float gravity = -15f; // If deciding not to use rigidbody default gravity (-9.81)

    public float jumpCD = 0.1f;
    public float fallTimeOut = 0.15f; // Used to customize time before entering fall starterAssetsInputs

    public bool isGrounded = true;
    public float groundedOffset = -0.14f;
    public float groundCheckRadius = 0.5f;

    // Camera Movement Variables
    [SerializeField]
    private GameObject camera;
    public float camRotationSpeed = 1.0f;
    private float upperClamp = 90.0f; 
    private float lowerClamp = 90.0f;
    private float cameraPitch;


    private PlayerInput playerInput;
    private CharacterController controller;
    private PlayerInputs inputs;
    [SerializeField]
    private GameObject mainCamera;

    // global variables
    private float speed;
    private float rotVelocity;
    private float verticalVelocity;
    
    private float jumpTimeOutDelta;
    private float fallTimeOutDelta;


    
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        inputs = GetComponent<PlayerInputs>();
        jumpTimeOutDelta = jumpCD;
        fallTimeOutDelta = fallTimeOut;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        Jump();
        GroundCheck();
        MoveCharacter();
    }


    private void GroundCheck(){
        Vector3 rayCastDistance = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
 
        //isGrounded = Physics.Raycast(rayCastDistance, );
        int layermask = 1 << 6;
        isGrounded = Physics.CheckSphere(rayCastDistance, groundCheckRadius, layermask);
        if (isGrounded)
            Debug.DrawRay(rayCastDistance, Vector3.down, Color.green);
    }
    private void RotateCamera(){

    }
    private void MoveCharacter(){

    }
    private void Jump(){
        if (isGrounded){
            
        }
    }
    private void ClampCameraAngle(){
        
    }
}
