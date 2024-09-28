using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, 10f).setLoopClamp();

    }
}
