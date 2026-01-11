using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private Transform playerCam; // Reference to the player's camera for raycasting
    [SerializeField] private LayerMask pickUpLayerMask; // Layer mask to filter pickable objects
    [SerializeField] private LayerMask coinLayerMask; // Layer mask to filter coin objects

    public CoinManager cm; // Reference to the CoinManager script (should be assigned in the Inspector)
    int value; // Value to increase coins by (should be set appropriately elsewhere)

    private void Update()
    {
        // Check for player input and handle object collection each frame
        Collection();
    }

    /// <summary>
    /// Handles the logic for picking up objects when the player presses the E key.
    /// Uses a raycast from the center of the player's camera to detect pickable objects.
    /// </summary>
    private void Collection()
    {
        // Check if the E key was pressed this frame
        if (Input.GetKeyDown(KeyCode.E))
        {
            float collectionDist = 2f; // Maximum distance to pick up objects
            // Perform a raycast from the camera's position forward
            if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit, collectionDist, pickUpLayerMask))
            {
                // Check if the hit object has a GrabbableObjects component
                if (raycastHit.transform.TryGetComponent(out GrabbableObjects grabbableObject))
                {
                    // Log the object picked up (actual pickup logic can be added here)
                    Debug.Log("Picked up: " + grabbableObject);
                }
            }
        }
    }

    /// Handles collision with coin objects using Unity's trigger system.
    /// Increases the coin count and updates the UI when a coin is collected.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is on the coin layer
        if (((1 << other.gameObject.layer) & coinLayerMask) != 0)
        {
            cm.coinCount++; // Increment the coin count in the CoinManager
            Debug.Log("Coin collected! Total coins: " + cm.coinCount);
            Destroy(other.gameObject); // Remove the coin from the scene
            CoinManager.instance.IncreaseCoins(value); // Update the coin count in the UI (value should be set appropriately)
        }
    }
}
