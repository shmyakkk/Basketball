using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f) * GameManager.mod;
    }
}
