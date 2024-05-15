using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject m_slotPrefab;
    
  public void Start()
  {
     InventorySystem.Instance.onInventoryChangedEvent += OnUpdateInventory;
  }


  private void OnUpdateInventory()
 {  
      foreach(Transform t in transform)
     {
         Destroy(t.gameObject);
     }
   
     DrawInventory();
 }

  public void DrawInventory()
  {
     foreach(InventoryItem item in InventorySystem.Instance .inventory)
     {
         AddInventorySlot(item);
     }
  }
  public void AddInventorySlot (InventoryItem item)
  {
     GameObject obj = Instantiate(m_slotPrefab); 
     obj.transform. SetParent (transform, false);
     SlotScript slot = obj.GetComponent<SlotScript>(); 
     slot.Set (item);
  }
}
