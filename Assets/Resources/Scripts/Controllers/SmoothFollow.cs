using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 5;
    // Start is called before the first frame update
    void Start() {
        Vector3 desiredPosition = target.position + offset;
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 intermediaryPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);

        transform.position = new Vector3(intermediaryPosition.x, intermediaryPosition.y, transform.position.z);
    }

    public void AssignTarget(Transform newTarget){
        target = newTarget;
    }
}
