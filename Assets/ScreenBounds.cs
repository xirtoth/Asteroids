using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public float leftBound, rightBound, topBound, bottomBound, buffer;
    public Camera cam;

    private void Awake()
    {
    }

    private void Start()
    {
        topBound = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, cam.transform.position.z)).y;
        bottomBound = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z)).y;
        rightBound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0 - cam.transform.position.z)).x;
        leftBound = cam.ScreenToWorldPoint(new Vector3(0, 0, 0 - cam.transform.position.z)).x;
        buffer = 1f;
    }

    public Vector3 CheckBounds(Transform tra)
    {
        if (tra.position.y > topBound + buffer)
        {
            return new Vector3(tra.position.x, bottomBound - buffer, 0);
        }

        if (tra.position.y < bottomBound - buffer)
        {
            return new Vector3(tra.position.x, topBound + buffer, 0);
        }

        if (tra.position.x < leftBound - buffer)
        {
            return new Vector3(rightBound + buffer, tra.position.y, 0);
        }

        if (tra.position.x > rightBound + buffer)
        {
            return new Vector3(leftBound - buffer, tra.position.y, 0);
        }
        else
        {
            return tra.position;
        }
    }
}