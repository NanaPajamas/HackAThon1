
using UnityEngine;

public class MouseLook : MonoBehaviour {
    private float mouseX;
    private float mouseY;
    public float mouseSens = 90f;
    private float xRotation = 0f; //this is the rotation for the x-axis that will move the mouse up and down
    public Transform playerBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets rid of the cursor and locks in the middle of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //if you look up, mouseY will be 1 meaning we are subtracting xRotation, which is 0, by one each time and setting it equal.
        xRotation -= mouseY;
        //this will lock our xRotation so we can't look behind us
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //only the x-axis for rotation will change, and keep the y and z-axis at 0.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //camera only

        //move the PLAYER BODY moves left and right. Do this to match the camera wth the player
        playerBody.Rotate(Vector3.up * mouseX); //that's why we make a reference.
    }
}
