using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public bool gameFinished;

    [SerializeField] private float minRotateSpeed, maxRotateSpeed;
    private float currentRotateSpeed;

    [SerializeField] private float minRotateTime, maxRotateTime;
    private float currentRotateTime;
    private float rorateTime;

    private void Awake()
    {
        gameFinished = false;
        currentRotateTime = 0f;
        currentRotateSpeed = minRotateSpeed + (maxRotateSpeed - minRotateSpeed) * Random.Range(0, 11) * 0.1f;
        rorateTime = minRotateTime + (maxRotateTime - minRotateTime) * Random.Range(0, 11) * 0.1f;
        currentRotateSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
    }

    private void Update()
    {
        currentRotateTime += Time.deltaTime;
        if (currentRotateTime > rorateTime)
        {
            currentRotateTime = 0f;
            currentRotateSpeed = minRotateSpeed + (maxRotateSpeed - minRotateSpeed) * Random.Range(0, 11) * 0.1f;
            rorateTime = minRotateTime + (maxRotateTime - minRotateTime) * Random.Range(0, 11) * 0.1f;
            currentRotateSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
        }
    }

    private void FixedUpdate()
    {
        if (gameFinished) return;
        transform.Rotate(0, 0, currentRotateSpeed * Time.deltaTime);
    }
}
