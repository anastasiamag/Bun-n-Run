using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public static GameInput Instance {get; private set;}

    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }

    private PlayerInputActions playerInputActions;

    private void Awake(){
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;

        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

        playerInputActions.Player.Pause.performed += Pause_performed;

        Debug.Log(GetBindingText(Binding.Move_Up));
    }   

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_performed;

        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;

        playerInputActions.Player.Pause.performed -= Pause_performed;

        // Disposing this object : playerInputActions = new PlayerInputActions();
        // Cleans up that object and fress any memory
        playerInputActions.Dispose();

    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){// OnInteractAction?.Invoke(this, EventArgs.Empty);
        if(OnInteractAction!=null){
        OnInteractAction(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVectorNormalized() {
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
   
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding) {

        switch(binding) {
            default:

            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }
}


//      if (Input.GetKey(KeyCode.W)) {
//         inputVector.y = +1;
// } 
        
// if (Input.GetKey(KeyCode.S)) {
//         inputVector.y = -1;
// } 
        
// if (Input.GetKey(KeyCode.A)) {
//         inputVector.x = -1;
// } 
        
// if (Input.GetKey(KeyCode.D)) {
//         inputVector.x = +1;
// } 