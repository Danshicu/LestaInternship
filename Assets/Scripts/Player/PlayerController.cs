using System;
using System.Collections;
using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour, IStunable
{
    [Header("Moving")]
    [SerializeField] private float maxMovingSpeed;
    [SerializeField] private float speedIncreasingRate;
    [SerializeField] private float speedDecreasingRate;
    [SerializeField] private Transform cameraOrientation;
    private Vector3 moveDirection;
     private float currentSpeed;

    [Header("Jumping")] 
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump = true;
    private bool inputJump;
    
    [Header("Physical stats")] 
    [SerializeField] private float forceMultiplayer;
    [SerializeField] private float airVelocity;

    [Header("Rotating")]
    [SerializeField] private float rotateSpeed;
    
    [Header("States")] 
    [SerializeField] private GroundCheckCollider groundCheckCollider;
    [SerializeField] private bool onGround = true;
    private bool canMove = true;
    
    private InputHandler _inputHandler = new InputHandler();
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }
    
    bool IsGrounded ()
    {
        return groundCheckCollider.OnGround;
    }

    private void Update()
    {
            float horizontal = _inputHandler.MoveInput.x;
            float vertical = _inputHandler.MoveInput.y;

            Vector3 v2 = vertical * cameraOrientation.forward; 
            Vector3 h2 = horizontal * cameraOrientation.right; 
            moveDirection = (v2 + h2).normalized;
            moveDirection.y = 0;

            onGround = IsGrounded();
            inputJump = _inputHandler.IsJumping;
    }
    
    

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                Vector3 targetDirection = moveDirection; //Direction of the character
                Quaternion tr = Quaternion.LookRotation(targetDirection); //Rotation of the character to where it moves
                Quaternion targetRotation =
                    Quaternion.Slerp(transform.rotation, tr,
                        Time.fixedDeltaTime * rotateSpeed); //Rotate the character little by little
                transform.rotation = targetRotation;
            }

            Vector3 velocity = _rigidbody.velocity;
            
            MovePlayer();
            ControlSpeed();
            
            if (onGround)
            {
                if (canJump && inputJump)
                {
                    _rigidbody.velocity = new Vector3(velocity.x, jumpForce, velocity.z);
                    canJump = false;
                    WaitAndDo(jumpCooldown, () => canJump = true);
                }
            }
        }

    }

    private void ControlSpeed()
    {
        if (moveDirection.magnitude !=0)
        {
            currentSpeed += speedIncreasingRate;
        }
        else
        {
            currentSpeed -= speedDecreasingRate;
        }
        
        currentSpeed = Math.Clamp(currentSpeed, 0, maxMovingSpeed);
        
    }

    private void MovePlayer()
    {
        if (onGround)
        {
            _rigidbody.AddForce(moveDirection * currentSpeed *forceMultiplayer, ForceMode.Force);
        }
        else
        {
            _rigidbody.AddForce(moveDirection*currentSpeed*airVelocity * forceMultiplayer, ForceMode.Force);
        }
    }

    public void Stun(float stunDuration)
    {
        if (canMove)
        {
            canMove = false;
            WaitAndDo(stunDuration, () => canMove = true);
        }
    }
    
    
    public Coroutine WaitAndDo(float timeInSeconds, Action action)
    {
        return StartCoroutine(Execute(timeInSeconds, action));
    }
 
    private IEnumerator Execute(float timeInSeconds, Action action)
    {
        yield return new WaitForSeconds(timeInSeconds);
        if (Application.isPlaying) action();
    }
    
}
