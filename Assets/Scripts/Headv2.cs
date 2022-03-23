using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headv2 : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset;

    private Vector3 _headBodyOffset;
    private void Start()
    {
        _headBodyOffset = rootObject.position - followObject.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        rootObject.position = transform.position + _headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}