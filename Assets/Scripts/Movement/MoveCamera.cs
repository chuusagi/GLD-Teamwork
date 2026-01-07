using UnityEngine;

public class MoveCamera : MonoBehaviour 
{
    
    public Transform cameraPosition;

    private void Update()
    {
        // ensures camera always moves with player
        transform.position = cameraPosition.position;
    }
}
