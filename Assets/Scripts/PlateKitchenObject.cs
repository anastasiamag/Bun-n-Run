using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;
    public class OnIngredientAddEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

   private List<KitchenObjectSO> kitchenObjectSOList; 
   [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

   private void Awake() {
    kitchenObjectSOList = new List<KitchenObjectSO>();
   }

   public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // It is not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
                // Has already this type
            return false;
        } else {
            // Adds ingredient
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdd?.Invoke(this, new OnIngredientAddEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() { 
        return kitchenObjectSOList;
    }
}
