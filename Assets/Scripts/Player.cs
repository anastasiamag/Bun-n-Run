using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Player : MonoBehaviour, IKitchenObjectParent

{

  public static Player Instance { get; private set; }

  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs :  EventArgs{
      public BaseCounter selectedCounter;
    }

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;
  [SerializeField] private Transform counterTop;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject; 

    private void Awake(){
      if (Instance != null){ 
          Debug.LogError("There is more than one instance");
      }
      Instance = this;
    }

    private void Start(){
      gameInput.OnInteractAction += GameInput_OnInteractAction;
      gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter!=null) {
        selectedCounter.InteractAlternate(this);
      }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
      if (!GameManager.Instance.IsGamePlaying()) return;

      if (selectedCounter!=null) {
        selectedCounter.InteractionCounter(this);
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
        if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
          // Has counter
          if(baseCounter != selectedCounter) {
          SetSelectedCounter(baseCounter);
        
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
      canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerSize , moveDirectionX, moveDistance);

      if (canMove) {
        //can move only on the X
        moveDirection= moveDirectionX;
      }else {
        //cannot move only on the X
        // attempt only Z movement
        Vector3 moveDirectionZ = new Vector3(0, 0 , moveDirection.z).normalized;
      canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerSize , moveDirectionZ, moveDistance);
      
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

    private void SetSelectedCounter(BaseCounter selectedCounter) {
      this.selectedCounter = selectedCounter;
       OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
          });
    }

    public Transform GetKitchenObjectFollowTransform() { 
        return counterTop;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject !=null;
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
