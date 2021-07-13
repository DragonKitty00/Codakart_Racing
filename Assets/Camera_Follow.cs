using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    //camera calculation script for functions
    CameraCalculations cameraCalc;

    //target we want to follow
    public Transform target;

    //layer number of player object
    public int playerLayerNumber = 9;

    //limaitations for distance and height
    public float maxDistance = 4.0f;
    public float minDistance = 2.0f;
    public float minHeight = 1.0f;

    //final distance and offset used when calculating camera placement
    public float distanceOffset;
    public float finalDistance;

    //speed variables and limits    
    public Vector2 cameraSpeed = new Vector2(3.0f, 1.0f);
    public Vector2 cameraYLimits = new Vector2(5.0f, 60.0f);

    //whether we reset the camera or not
    public bool resetCamera = true;

    // angle variables for camera
    public float x = 0.0f;
    public float y = 0.0f;
    public float yOffset;
    public float newPos;

    //offset and position that will change based on camera
    private Quaternion rotation;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        //set the angle variables, mouse visibility, and get the camera calc script
        cameraCalc = FindObjectOfType<CameraCalculations>();
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get the distace offset based on collisions
        distanceOffset = cameraCalc.CalculateOffset(target, playerLayerNumber, maxDistance, distanceOffset);
    }

    //late update that is called after regular update
    void LateUpdate()
    {
        //get the x and y variables based on the mouse input
        x += Input.GetAxis("Mouse X") * cameraSpeed.x;
        y -= Input.GetAxis("Mouse Y") * cameraSpeed.y;

        //clamp the y angle to the boundaries we set
        y = cameraCalc.ClampAngle(y, cameraYLimits.x, cameraYLimits.y);

        //get offset and position based on terrain and collisions
        yOffset = cameraCalc.FindOffsetY(minHeight);
        newPos = cameraCalc.FindPosition(minHeight);

        if (resetCamera == true)
        {
            //reset camera to a defalt position
            x = target.transform.eulerAngles.y;
            y = 0;
            
            rotation = Quaternion.Euler(y, x, 0.0f);
        }
        else
        {
            //calculate the pos and rot based on the mouse variables
            finalDistance = Mathf.Min(-minDistance, -maxDistance + distanceOffset);

            rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 changedPos = target.position - new Vector3(0.0f, newPos, 0.0f);
            position = rotation * new Vector3(0.0f, 0.0f, finalDistance) + changedPos;
            position.y = position.y + yOffset;
        }
        //set the rotation of camera to rot pos variables
        transform.rotation = rotation;
        transform.position = position;
        resetCamera = false;
    }
}
