using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private Transform playerCam; // serialize field to assign the player camera in inspector
    [SerializeField] private LayerMask pickUpLayerMask; // serialize field to assign the layer mask in inspector
    [SerializeField] private LayerMask coinLayerMask;

    public CoinManager cm; // calls coinmanager script
    private void Update()
    {
        Collection();
    }
    private void Collection()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float collectionDist = 2f; // distance within which player can pick up objects
            // raycast from center of player camera forward to detect pickable objects
            if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit, collectionDist, pickUpLayerMask))
            {
                // check if the hit object has a GrabbableObjects component 
                // IMP: ENSURE INTERACTIBLE ITEMS HAVE GrabbableObjects SCRIPT ATTACHED
                if (raycastHit.transform.TryGetComponent(out GrabbableObjects grabbableObject))
                {
                    Debug.Log("Picked up: " + grabbableObject);
                    Inventory();
                }
            }

        }
    }

    private void Inventory()
    {
        // TO DO: INV SYSTEM
        Debug.Log("Added to Inventory");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is on the coin layer
        if (((1 << other.gameObject.layer) & coinLayerMask) != 0)
        {
            cm.coinCount++;
            Debug.Log("Coin collected! Total coins: " + cm.coinCount);
            // Optional: Destroy the coin after collection
            Destroy(other.gameObject);
        }


    }
}
