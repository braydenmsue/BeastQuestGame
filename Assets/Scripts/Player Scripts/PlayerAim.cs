using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAim : MonoBehaviour
{
    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        Vector3 mousePosition = GetMousePosition();
        //mouse point in game minus the gameObject origin yields direction, normalize it for consistency
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    public static Vector3 FindMousePosition(Vector3 onscreenPosition, Camera gameCamera)
    {
        // returns the transformation of a point on the player's screen to a point in the game world
        Vector3 gamePosition = gameCamera.ScreenToWorldPoint(onscreenPosition);
        return gamePosition;
    }

    public static Vector3 GetMousePosition()
    {
        //return x and y values of mouse position
        Vector3 temp = FindMousePosition(Input.mousePosition, Camera.main);
        temp.z = 0f;
        return temp;
    }

}

