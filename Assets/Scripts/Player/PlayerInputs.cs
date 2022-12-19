using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    // Public Variables
    public Vector2 moveVec;
    public Vector2 lookVec;
    public bool jump;
    public bool sprint;
    public bool fire;

    public bool cursorLocked = true;
    

    // Receive Input Functions
    public void OnMove(InputValue value){
        HandleMove(value.Get<Vector2>());
    }

    public void OnLook(InputValue value){
        HandleLook(value.Get<Vector2>());
    }

    public void OnJump(InputValue value){
        HandleJump(value.isPressed);
    }

    public void OnSprint(InputValue value){
        HandleSprint(value.isPressed);
    }

    public void OnFire(InputValue value){
        HandleFire(value.isPressed);
    }

    // Handle Input Functions

    public void HandleMove(Vector2 moveInput){
        moveVec = moveInput;
    }
    
    public void HandleLook(Vector2 lookInput){
        lookVec = lookInput;
    }

    public void HandleJump(bool jumpInput){
        jump = jumpInput;
    }

    public void HandleSprint(bool sprintInput){
        sprint = sprintInput;
    }

    public void HandleFire(bool fireInput){
        fire = fireInput;
    }


    // Cursor Settings
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

}
