using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private float rotX = 0.0f;

    void Update()
    {
        rotX -= 2f * Time.deltaTime;
        transform.eulerAngles = new Vector3(rotX, 0.0f, 0.0f);
    }
}
