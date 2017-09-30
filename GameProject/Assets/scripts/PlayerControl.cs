﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 3;
    private Rigidbody2D playerBody;

    private float horizontalMovement, verticalMovement;

    //everything here is used for character rotation
    private Vector3 mousePos;
    private Vector3 objectPos;
    private Transform target;
    private float angle;

    // Use this for initialization
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        target = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //grab user input
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        //Set character rotation to follow on mouse click
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(1))
        {
            mousePos = Input.mousePosition;
            mousePos.z = -(Camera.main.transform.position.z);
            objectPos = Camera.main.WorldToScreenPoint(target.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void FixedUpdate()
    {
        //move using physics
        Vector2 move = new Vector2(horizontalMovement, verticalMovement);
        playerBody.velocity = move * speed;
    }
}
