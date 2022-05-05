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

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    public static Vector3 FindMousePosition(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 temp = FindMousePosition(Input.mousePosition, Camera.main);
        temp.z = 0f;
        return temp;
    }

}

