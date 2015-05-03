using UnityEngine;
using System.Collections;
 
public class CameraFacingBillboard : MonoBehaviour
{
    public Camera Camera;
 
    void LateUpdate()
    {
        transform.LookAt( Camera.transform.position, Camera.transform.up );
    }
}