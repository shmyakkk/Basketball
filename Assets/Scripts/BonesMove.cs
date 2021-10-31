using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesMove : MonoBehaviour
{
    private Rigidbody boneRb;
    Vector3 startPos;

    private void Start()
    {
        boneRb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 0.1f || Mathf.Abs(transform.position.y - startPos.y) > 0.1f)
        {
            boneRb.AddForce((startPos - transform.position), ForceMode.Impulse);
        }
        transform.position += 2f * Time.deltaTime * (startPos - transform.position);
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 0 && Mathf.Abs(transform.position.y - startPos.y) > 0)
        {

            boneRb.AddForce((startPos - transform.position), ForceMode.Impulse);
        }
    }
}
