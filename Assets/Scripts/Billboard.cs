using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;
    public enum BillboardType {  LookAtCamera, CameraForward };

    private Vector2 originalRotation; 

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles; 
    }
    private void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(transform.position); break;
                case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward; break;
            default:
                break; 
        }
    }
}
