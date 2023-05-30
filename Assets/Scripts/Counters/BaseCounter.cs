using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTop;

    private KitchenObject kitchenObject;
    // Accesible to this class and any class that extends it: **protected**
    public virtual void InteractionCounter(Player player) {
        Debug.LogError("BaseCounter.InteractionCounter();");
    }

    public virtual void InteractAlternate(Player player) {
        Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform() { 
        return counterTop;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject !=null;
    }
}

