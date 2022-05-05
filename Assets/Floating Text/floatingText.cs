using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //destroy text after one second and spawn it slightly above enemy
        Destroy(gameObject, 1f);
        transform.localPosition += new Vector3(0, 0.5f,0);
    }

}
