using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    private void Start()
    {
        transform.localScale *= GameManager.mod;
    }
}
