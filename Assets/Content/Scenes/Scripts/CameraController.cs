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

        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime; // x�� ȸ���� y�� ��ȭ
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime; // y�� ȸ���� x�� ��ȭ

        y = ClampAngle(y, xMin, xMax); // �ּڰ� �ִ� ����

        transform.rotation = Quaternion.Euler(y, x, 0);
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheel * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDis, maxDis); // �ּ� �ִ밪 �������༭ ���� �̿��� �� ���� ���ϰ� ��
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
