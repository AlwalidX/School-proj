using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySystem: MonoBehaviour
{
    private static InventorySystem _instance;

    public static InventorySystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventorySystem>();
                if (_instance == null)
                {
                    Debug.LogError("An InventorySystem instance wasn't found in the scene");
                }
            }

            return _instance;
        }
    }
  public Dictionary<InventoryItemData, InventoryItem> m_itemDictionary; 
  public List<InventoryItem> inventory { get; private set; }
  
   public event Action  onInventoryChangedEvent;  // Event for inventory change updates

  private void Awake()
 {
   inventory = new List<InventoryItem>();
   m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
 }
  public void Add (InventoryItemData referenceData)
  {
    if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
    {
        value.AddToStack();
    }
    else
    {
     InventoryItem newItem = new InventoryItem(referenceData); 
     inventory.Add(newItem);
     m_itemDictionary.Add(referenceData, newItem);
    }
    onInventoryChangedEvent.Invoke();
  }
  public void Remove (InventoryItemData referenceData)
  {
     if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
     {
         value.RemoveFromStack();

         if (value.stackSize == 0)
         {
           inventory.Remove(value);
           m_itemDictionary.Remove(referenceData);
         }
     }
      onInventoryChangedEvent.Invoke();
  }
}