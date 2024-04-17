using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagers : MonoBehaviour
{
    public static InputManagers inputManagerInstance;
    private Inputs playerInputs;

    private Vector2 leftAxisValue = Vector2.zero;
    private float timeSicneJumpPressed = 0;
    private float timeSicneShootPressed = 0;

    private void Awake()
    {
        if (inputManagerInstance != null && inputManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInputs = new Inputs();
            playerInputs.Character.Enable();
            playerInputs.Character.Movement.performed += LeftAxisUpdate;
            playerInputs.Character.Jump.performed += JumpPress;
            playerInputs.Character.Shoot.performed += ShootPress;


            inputManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSicneJumpPressed += Time.deltaTime;
        timeSicneShootPressed += Time.deltaTime;
        InputSystem.Update();
    }
    private void JumpPress(InputAction.CallbackContext context)
    {
        timeSicneJumpPressed = 0;
    }

    private void ShootPress(InputAction.CallbackContext context)
    {
        timeSicneShootPressed = 0;
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }

    public Vector2 GetLeftAxisUpdate()
    {
        return leftAxisValue;
    }

    public bool GetLeftAxisPressed()
    {
        return leftAxisValue.x != 0;
    }

    public float GetJump()
    {
        return timeSicneJumpPressed;
    }

    public float GetShoot()
    {
        return timeSicneShootPressed;
    }
}
