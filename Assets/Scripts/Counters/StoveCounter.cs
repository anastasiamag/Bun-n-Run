using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }


    public enum State{
        Idle, 
        Frying,
        Fried,
        Burned,
    }

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if(HasKitchenObject()) {
        switch(state) {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                if(fryingTimer> fryingRecipeSO.fryingTimerMax) {
                // THe patty is fried
                    GetKitchenObject().DestroyKitchenObject();

                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                    state = State.Fried;
                    burningTimer= 0f;
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                }
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                if(burningTimer> burningRecipeSO.burningTimerMax) {
                // THe patty is fried
                    GetKitchenObject().DestroyKitchenObject();

                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                    state = State.Burned;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                }    
                break;
            case State.Burned:
                break;
        }
      }
    }
    


    public override void InteractionCounter(Player player) {
         if (!HasKitchenObject()) {
            // There is no KitchenObject
           if(player.HasKitchenObject()) {
            //  Player has kitchen object and dops it on the counter
            if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                // Player has Kitchen object that can be cut and drops it
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                 
                state = State.Frying;
                fryingTimer = 0f;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
            }
           } else {
            // Player has no KitchenObject
           }
        } else {
            // There is a KitchenObject
            if (player.HasKitchenObject()) {
                // Player has Kitchen object 
            } else {
                // Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);

                state =State.Idle;  

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
        }
    
   

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if( fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if( burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
