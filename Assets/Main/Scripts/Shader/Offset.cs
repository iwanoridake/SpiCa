using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        material.SetTextureOffset("_MainTex", new Vector2(-15000, -15000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
