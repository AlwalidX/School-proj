using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    Item item;
    public Item helmetOfProt;
    public Transform container;
    public Transform shopItemTemplate;

    private void Awake() 
{
    container = transform.Find("container");
    shopItemTemplate = container.Find("shopItemTemplate");
    if (container == null || shopItemTemplate == null)
    {
        Debug.LogError("Container or shopItemTemplate not found!");
        return;
    }
    shopItemTemplate.gameObject.SetActive(false);
}
    private void Start() 
    {
      CreateItemButton(helmetOfProt.icon, helmetOfProt.name, helmetOfProt.price, 0);
    }
 private void CreateItemButton (Sprite itemSprite, string itemName, int itemCost, int positionIndex) 
{
    Transform shopItemTransform = Instantiate(shopItemTemplate, container);
    if (shopItemTransform != null)
    {
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    }
    else
    {
        Debug.LogError("shopItemTemplate is null!");
    }
}

}
