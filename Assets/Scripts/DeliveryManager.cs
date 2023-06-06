using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance{get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
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
                    Debug.Log("player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        // No match found!
        // player did not deliver a correct recipe
        Debug.Log("player did not deliver a correct recipe");
    }


}
