using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

   private IKitchenObjectParent kitchenObjectParent;

   public KitchenObjectSO GetKitchenObjectSO() {
    return kitchenObjectSO;
   }

   //Setting the parent and changing the parent of the kitchen object

   public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
      if (this.kitchenObjectParent !=null){ 
      this.kitchenObjectParent.ClearKitchenObject();
      }

      this.kitchenObjectParent = kitchenObjectParent;

      if (kitchenObjectParent.HasKitchenObject()) {
         Debug.LogError("IKitchenObjectParent has already an object");
      }

      kitchenObjectParent.SetKitchenObject(this);

      transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
      transform.localPosition = Vector3.zero;
   }

   public IKitchenObjectParent GetKitchenObjectParent() {
      return kitchenObjectParent;
   }

   public void DestroyKitchenObject(){
      kitchenObjectParent.ClearKitchenObject();

      Destroy(gameObject);   
      }

      public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

            return kitchenObject;
      }
}
