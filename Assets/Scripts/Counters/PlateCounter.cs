using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{   
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax){
            spawnPlateTimer = 0f;

            if (GameManager.Instance.IsGamePlaying() && plateSpawnAmount < plateSpawnAmountMax) {
                plateSpawnAmount ++;

                OnPlateSpawn?.Invoke(this, EventArgs.Empty);

            }
    }
    }

    public override void InteractionCounter(Player player) {
        if (!player.HasKitchenObject()) {
            // Player has no Kitchen Object(or plates)
            if (plateSpawnAmount > 0) {
                // There is at least one plate
                plateSpawnAmount --;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        } 
    }
}
