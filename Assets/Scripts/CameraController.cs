using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    public Vector3 officeTrans;
    public Vector3 archivesTrans;
    public Vector3 offset;
    public float cameraSpeed;

    private bool isInOffice;
    private Vector3 destination;
    private float destinationSensitivity;

    private bool isMoving;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        this.transform.position = officeTrans + offset;
        isInOffice = true;

        destinationSensitivity = 0.05f;
        isMoving = false;
    }

    // When space is pressed, cameraview changes from one room to the other
    void LateUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            destination = isInOffice ? archivesTrans + offset : officeTrans + offset;
            isInOffice = !isInOffice;
            isMoving = true;
        }

        if (isMoving)
        {
            if (this.transform.position.x < destination.x - destinationSensitivity  || this.transform.position.x > destination.x + destinationSensitivity)
                this.transform.position = Vector3.Lerp(this.transform.position, destination, cameraSpeed * Time.deltaTime);
            else
            {
                this.transform.position = destination;
                isMoving = false;
            }
        }
    }
}
