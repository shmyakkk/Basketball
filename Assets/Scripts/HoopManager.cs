using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    [SerializeField] private GameObject hoop;
    private Transform hoopTransform;
    void InstantiateHoop()
    {

        Instantiate(hoop, hoopTransform);
    }
}
