using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    CameraCalculations cameraCalc;

    public Transform target;

    public int playerLayerNumber = 9;

    public float maxDistance = 4.0f;
    public float minDistance = 2.0f;
    public float minHeight = 1.0f;

    public float distanceOffset;
    public float finalDistance;

    public Vector2 cameraSpeed = new Vector2(3.0f, 1.0f);
    public Vector2 cameraYLimits = new Vector2(5.0f, 60.0f);

    public bool resetCamera = true;

    public float x = 0.0f;
    public float y = 0.0f;
    public float yOffset;
    public float newPos;

    private Quaternion rotation;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        cameraCalc = FindObjectOfType<CameraCalculations>();
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceOffset = cameraCalc.CalculateOffset(target, playerLayerNumber, maxDistance, distanceOffset);
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * cameraSpeed.x;
        y -= Input.GetAxis("Mouse Y") * cameraSpeed.y;

        y = cameraCalc.ClampAngle(y, cameraYLimits.x, cameraYLimits.y);

        yOffset = cameraCalc.FindOffsetY(minHeight);
        newPos = cameraCalc.FindPosition(minHeight);

        if (resetCamera == true)
        {
            x = target.transform.eulerAngles.y;
            y = 0;

            rotation = Quaternion.Euler(y, x, 0.0f);
        }
        else
        {
                
        }
    }
}
