using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Player : MonoBehaviour

{

  public static Player Instance { get; private set; }

  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs :  EventArgs{
      public Counter selectedCounter;
    }

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private Counter selectedCounter;

    private void Awake(){
      if (Instance != null){ 
          Debug.LogError("There is more than one instance");
      }
      Instance = this;
    }

    private void Start(){
      gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
      if (selectedCounter!=null) {
        selectedCounter.InteractionCounter();
      }
    }

    private void Update(){
      Movement();
      Interactions();
    }


    public bool IsWalking() {
        return isWalking;
    }

    private void Interactions() {
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();

      Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

      if (moveDirection != Vector3.zero) {
        lastInteractDirection = moveDirection;
      }
      float interactDistance = 2f;
      if( Physics.Raycast(transform.position, lastInteractDirection,out RaycastHit raycastHit,  interactDistance, countersLayerMask)){
        if(raycastHit.transform.TryGetComponent(out Counter counter)){
          // Has counter
          if(counter != selectedCounter) {
          SetSelectedCounter(counter);
        
          }
        } else {
            SetSelectedCounter(null);
           
        }
      } else {
        SetSelectedCounter(null);
      }
    }

    private void Movement(){
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();
    Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

    float moveDistance =  moveSpeed * Time.deltaTime;
    float playerSize =.7f;
    float playerHeight = 2f;
    //Για να μην περνάει ο παίκτης μέσα από τα αντικέιμενα
    bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerSize , moveDirection, moveDistance);

    if(!canMove) {
      // cannot move towards movedirection
      //attempt only x movemement
      Vector3 moveDirectionX = new Vector3(moveDirection.x, 0 , 0).normalized;
      canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerSize , moveDirectionX, moveDistance);

      if (canMove) {
        //can move only on the X
        moveDirection= moveDirectionX;
      }else {
        //cannot move only on the X
        // attempt only Z movement
        Vector3 moveDirectionZ = new Vector3(0, 0 , moveDirection.z).normalized;
      canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerSize , moveDirectionZ, moveDistance);
      
      if (canMove){
        //can only move on the Z 
        moveDirection= moveDirectionZ;
      }else {
        //cannot move in any direction
      }
    }
    }

    if(canMove){
      transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    isWalking = moveDirection != Vector3.zero; 
    float rotateSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    
    }

    private void SetSelectedCounter(Counter selectedCounter) {
      this.selectedCounter = selectedCounter;
       OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
          });
    }

}
    

//   private void Update(){
//     Vector2 inputVector = new Vector2(0, 0);
//     if (Input.GetKey(KeyCode.W)) {
//             inputVector.y = +1;
//     } 
    
//     if (Input.GetKey(KeyCode.S)) {
//             inputVector.y = -1;
//     } 
    
//     if (Input.GetKey(KeyCode.A)) {
//             inputVector.x = -1;
//     } 
    
//     if (Input.GetKey(KeyCode.D)) {
//             inputVector.x = +1;
//     } 
//     inputVector = inputVector.normalized;
