using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

      // overrides the base function   
     // Spawn object and give it to the player   
      public override void InteractionCounter(Player player) {
        if(!player.HasKitchenObject()) {
        // Player has no kitchen object
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        
        }
    }
}
