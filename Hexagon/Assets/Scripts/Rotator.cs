using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.isPlay)
            return;

        transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);
    }
}
