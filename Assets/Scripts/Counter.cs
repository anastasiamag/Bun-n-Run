using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
      [SerializeField] private KitchenObjectSO kitchenObjectSO;
      [SerializeField] private Transform counterTop;

     public void InteractionCounter() {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTop);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
