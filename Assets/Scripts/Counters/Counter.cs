using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseCounter
{     


      [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
     public override void InteractionCounter(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject
           if(player.HasKitchenObject()) {
            //  Player has kitchen object and dops it on the counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
        }
     }
}



//   [SerializeField] private Counter secondCounter;
    //   [SerializeField] private bool testing;

    //   private void Update() {
    //     if (testing && Input.GetKeyDown(KeyCode.T)) {
    //         if(kitchenObject!=null) {
    //             kitchenObject.SetKitchenObjectParent(secondCounter);
    //         }
    //   }
    //   }


    // //overrides the base function 
    //  public override void InteractionCounter(Player player) {
    //     if (kitchenObject == null) { 
    //     Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTop);
    //     kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
    //     } else {
    //         //The player gets the object
    //         kitchenObject.SetKitchenObjectParent(player);
    //     }

    // }