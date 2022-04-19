using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiCa{
public class Stroke : MonoBehaviour
{
    public GameObject LineObject=null;

    public void Addvalue(int value){
        LineObject = this.gameObject;
        
        LineObject.layer = 9 + value;
    }

}}
