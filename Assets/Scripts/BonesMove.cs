using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesMove : MonoBehaviour
{
    private Rigidbody boneRb;
    private Vector3 startPos;

    private void Start()
    {
        boneRb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 0 || Mathf.Abs(transform.position.y - startPos.y) > 0)
        {
            boneRb.AddForce((startPos - transform.position) * 2, ForceMode.Impulse);
        }
        if (Mathf.Abs(transform.position.x - startPos.x) > 0.01 || Mathf.Abs(transform.position.y - startPos.y) > 0)
        {
            boneRb.AddForce((startPos - transform.position) * 2, ForceMode.Impulse);
        }
    }
}
