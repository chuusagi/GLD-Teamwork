using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	// setting mouse axis sensitivities
	public float sensX;
	public float sensY;


	public Transform orientation; // used to store position, rotation and scale

	float cameraXRotation;
	float cameraYRotation;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked; // lock cursor in middle of screen
		Cursor.visible = false; // invisible cursor
	}

	private void Update()
	{
		// get mouse input 
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		// rotation
		cameraYRotation += mouseX;
		cameraXRotation -= mouseY;

		// ensures player cant look up/down more than 90deg
		cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);

		// rotates camera and orientation
		transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0); // rotates camera along both axis
		orientation.rotation = Quaternion.Euler(0, cameraYRotation, 0); // rotates player along y axis


	}


}
