using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [SerializeField] private float minDis = 2;
    [SerializeField] private float maxDis = 25;
    [SerializeField] private float wheel = 400;
    [SerializeField] private float xSpeed = 400;
    [SerializeField] private float ySpeed = 250;

    private float xMin = 5;
    private float xMax = 80;
    private float x, y;
    private float distance;


    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        distance = Vector3.Distance(transform.position, target.position);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime; // x축 회전시 y값 변화
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime; // y축 회전시 x값 변화

        y = ClampAngle(y, xMin, xMax); // 최솟값 최댓값 지정

        transform.rotation = Quaternion.Euler(y, x, 0);
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheel * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDis, maxDis); // 최소 최대값 지정해줘서 범위 이외의 값 넘지 못하게 함
    }

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    public void SetTarget(Transform trans)
    {
        target = trans;
    }
}
