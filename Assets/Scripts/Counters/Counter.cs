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
                if ( player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject) ) {
                    // Player has a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                    GetKitchenObject().DestroyKitchenObject();
                    }
                } else {
                    // Player has no Plate,but has a kitchen object
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter has a plate
                        if ( plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroyKitchenObject();
                        }
                    }
                }
            } else {
                // Player has no KitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
     }
}