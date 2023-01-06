using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapdata : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float end_point;
    public static Mapdata s_instance;

    private void Awake()
    {
        if(s_instance==null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
