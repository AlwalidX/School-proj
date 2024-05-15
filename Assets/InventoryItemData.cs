using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu( menuName = "Iventory Item Data")]
public class InventoryItemData : ScriptableObject 
{
   public string id;
   public string displayName;
   public Sprite icon;
   public GameObject prefab;

   public ItemType itemType;
   public int damage;
   public float attackSpeed;




   public enum ItemType
   {
    weapon,
    armor,
    consumable,
    other
   }
}
