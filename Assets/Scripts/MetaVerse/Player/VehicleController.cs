using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void LetsRide()
    {
        gameObject.SetActive(true);
    }

    public void FinishRide()
    {
        gameObject.SetActive(false);
    }
}
