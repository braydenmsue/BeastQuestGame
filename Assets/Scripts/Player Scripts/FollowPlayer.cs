using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //allow main camera movement along x and y axis between min and max values
        float xAxis = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float yAxis = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        //follows the player
        gameObject.transform.position = new Vector3(xAxis, yAxis, gameObject.transform.position.z);
    }
}
