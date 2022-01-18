using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R6_TrapMove : MonoBehaviour
{
    Vector3 pos; //현재위치

    [SerializeField]
    private float delta = 2.0f; // 이동가능한 최대값
    [SerializeField]
    private float speed = 3.0f; // 이동속도
    [SerializeField]
    private bool isXmove = false;

    [SerializeField]
    private bool Rightmove = false;

    public bool turnon = false;

    private float runningTime = 0f;
    private float yPos = 0f;
    private float xPos = 0f;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (!turnon)
            return;
        if (isXmove) 
        {
            if(Rightmove)
                moveX();
            if (!Rightmove)
                RmoveX();

        }
        else moveY();
    }

    private void moveX() {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.x += Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;
    }
    private void RmoveX()
    {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.x -= Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;

    }
    private void moveY() {
        Vector3 vectorPos = pos;
        runningTime += Time.deltaTime * speed;
        vectorPos.y += Mathf.Sin(runningTime) * delta;
        transform.position = vectorPos;
    }
}
