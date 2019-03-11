﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 120f;
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float sensitivity = 150f;
    [SerializeField] private float followSpeed = 0.5f;
    [SerializeField] private bool doLerpTarget;
    [SerializeField] private GameObject followTarget;
    [SerializeField] private Vector3 offset;

    float rotY;
    float rotX;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Mouse X"); // Get input axises
        float inputY = Input.GetAxis("Mouse Y");

        rotY += inputX * sensitivity * Time.deltaTime;
        rotX += -inputY * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // Clamp up and down rotation.

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        if (doLerpTarget)
        {
            float step = followSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position + offset, step);
        }
        else
        {
            float step = cameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, followTarget.transform.position + offset, step);
        }
    }
}
