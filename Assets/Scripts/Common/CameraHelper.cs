using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraHelper
{
        
        
    /// <summary>
    /// 获取指定距离下相机视口四个角的坐标
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="distance">相对于相机的距离</param>
    /// <returns></returns>
    public static Vector3[] GetCameraFovPositionByDistance(this Camera cam, float distance)
    {
        Vector3[] corners = new Vector3[4];
        float halfFOV = (cam.fieldOfView * 0.5f) * Mathf.Deg2Rad;
        float aspect = cam.aspect;
        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;
        Transform tx = cam.transform;
// 左上角
        corners[0] = tx.position - (tx.right * width);
        corners[0] += tx.up * height;
        corners[0] += tx.forward * distance;
// 右上角
        corners[1] = tx.position + (tx.right * width);
        corners[1] += tx.up * height;
        corners[1] += tx.forward * distance;
// 左下角
        corners[2] = tx.position - (tx.right * width);
        corners[2] -= tx.up * height;
        corners[2] += tx.forward * distance;
// 右下角
        corners[3] = tx.position + (tx.right * width);
        corners[3] -= tx.up * height;
        corners[3] += tx.forward * distance;
        return corners;
    }
    
    
    
    
    
    
}
