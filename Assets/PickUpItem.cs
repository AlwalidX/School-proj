using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private LayerMask itemLayer; // Items must be on this layer
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            CheckForItemPickup();
        }
    }

    private void CheckForItemPickup()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, itemLayer))
        {
            // Item found!
            if (hitInfo.transform.TryGetComponent<ItemObject>(out ItemObject item))
            {
                item.OnHandlePickupItem();
            }
        }
    }
}