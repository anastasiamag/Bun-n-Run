using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void InteractionCounter(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroyKitchenObject();
        }
    }
}
