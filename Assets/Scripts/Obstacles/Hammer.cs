using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 90f;

    private bool isGoingTo180 = true;

    void Update()
    {
        if (isGoingTo180)
        {
            transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            if (transform.eulerAngles.z >= 180f)
            {
                isGoingTo180 = false;
            }
        }
        else
        {
            transform.RotateAround(target.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
            if (transform.eulerAngles.z <= 90f)
            {
                isGoingTo180 = true;
            }
        }
    }
}
