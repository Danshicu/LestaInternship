using UnityEngine;

public class InputHandler
{
    private Vector2 _moveInput;
    private bool isJumping;
    public Vector2 MoveInput
    {
        get
        {
            HandleMoveInput();
            return _moveInput;
        }
    }

    public bool IsJumping
    {
        get
        {
            HandleKeyInput();
            return isJumping;
        }
    }
    
    public InputHandler()
    {
        _moveInput = new Vector2();
    }
    private void HandleMoveInput()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
        
    }

    private void HandleKeyInput()
    {
        isJumping = Input.GetButton("Jump");
        
    }
    
}
