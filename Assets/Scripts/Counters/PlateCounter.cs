using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

   [SerializeField] private KitchenObjectSO plateKitchenObjectSO; 
   private float spawnPlateTimer;
    private float spawnPlateTimerMax;

   private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax) {
            kitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
        }
   }
}
