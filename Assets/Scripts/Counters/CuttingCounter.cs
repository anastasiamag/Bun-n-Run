using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

   public override void InteractionCounter(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject
           if(player.HasKitchenObject()) {
            //  Player has kitchen object and dops it on the counter
            if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                // Player has Kitchen object that can be cut and drops it
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
        }
   }

   public override void InteractAlternate(Player player) {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a KitchenObject && can be cut
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroyKitchenObject();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
   }

   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
        if (cuttingRecipeSO.input == inputKitchenObjectSO) {
            return true;
        }
    }
    return false;
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
        if (cuttingRecipeSO.input == inputKitchenObjectSO) {
            return cuttingRecipeSO.output;
        }
    }
    return null;
   }
}
