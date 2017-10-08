using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMissile : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        var renderer = this.gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}