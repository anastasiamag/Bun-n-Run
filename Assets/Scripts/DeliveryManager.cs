using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeComplete;

    public static DeliveryManager Instance{get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipeAmount;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i< waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has same number of ingredients
                bool plateContentMatch = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList) {
                    // Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycling through all ingredients in the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredient match
                            ingredientFound = true;
                            break;
                        }
                    }
                    //  If we have not found an ingredient
                    if (!ingredientFound) {
                        // This recipe ingredient was not found on the plate
                        plateContentMatch = false;
                    }
                }

                if (plateContentMatch) {
                    // player delivered the correct recipe 

                    successfulRecipeAmount++;
                    waitingRecipeSOList.RemoveAt(i);


                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No match found!
        // player did not deliver a correct recipe
        Debug.Log("player did not deliver a correct recipe");
    }


    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipeAmount() {
        return successfulRecipeAmount;
    }

}
