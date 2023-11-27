using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private new Camera camera;
    public float rotationSpeed = 10f; // Đổi thành góc mỗi giây bạn muốn xoay

    private Vector3 offset, defaultOffset;

    private float fieldOfView;

    private bool isRotate = false;

    private void Awake()
    {
        offset = transform.position - target.transform.position;
        defaultOffset = transform.position - target.transform.position;
        fieldOfView = camera.fieldOfView;
    }

    private void Start()
    {
        EventManager.instance.OnEventEmitted += OnEventEmitted;
    }

    private void OnEventEmitted(EventType type)
    {
        switch (type)
        {
            case EventType.NextLevel:
                return;
            case EventType.LoadLevel:
                OnInit();
                return;
            case EventType.CompleteLevel:
                Win();
                return;
        }
    }

    public void OnInit()
    {
        offset = defaultOffset;
        camera.fieldOfView = fieldOfView;
    }


    void Update()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform.position);

        if (isRotate) RotateAroundTarget();
    }

    public void Win()
    {
        StartRotate();

        StartCoroutine(EndRotate());
    }

    public void StartRotate()
    {
        isRotate = true;
    }

    public IEnumerator EndRotate()
    {
        yield return new WaitForSeconds(2f);
        isRotate = false;
    }

    // Hàm để xoay camera quanh target
    public void RotateAroundTarget()
    {
        // Tính toán góc xoay trong mỗi bước cập nhật
        float step = rotationSpeed * Time.deltaTime;

        camera.fieldOfView -= step/5;

        // Rotate the offset around the target
        offset = Quaternion.Euler(0, step, 0) * offset;


        // Make the camera look at the target
        transform.LookAt(target.transform.position);
    }
}
