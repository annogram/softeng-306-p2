﻿using UnityEngine;
using Managers;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndlessMovement : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject boundary;
    public float maxSpeed = 100.0F;
    public float acceleration = 100.0F;

    public GameObject[] bumpers;
    public GameObject[] coins;
    public GameObject[] platforms;
    public GameObject cameraObject;
    public float maxCamSize = 100;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    private Rigidbody2D rbb;
    private Camera cam;

    private float[] bumperYPos = { 5, 6, 7, 8, 9, 10, 11, 12, 18, 19, 20, 21, 22 };
    private GameController _controller;

    // Use this for initialization
    void Start () {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
        rbb = boundary.GetComponent<Rigidbody2D>();
        cam = cameraObject.GetComponent<Camera>();
        _controller = GameController.Instance;

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (cam.orthographicSize > maxCamSize) {
            _controller.loadScreenSingle(SceneManager.GetActiveScene().name);
        }

        // Update platform positions
        foreach(GameObject platform in platforms) {
            Rigidbody2D platformRb = platform.GetComponent<Rigidbody2D>();
            if ((platformRb.position.x + (platformRb.transform.localScale.x/2)) < rbb.position.x) {
                platformRb.transform.Translate(platformRb.position + new Vector2(platformRb.transform.localScale.x, 0));
            }
        }

        // Update boundary position
        Vector2 nextPos = rbb.position + new Vector2(maxSpeed/2, 0) * Time.fixedDeltaTime;
        Vector2 minPos = new Vector2(cam.transform.position.x - (cam.orthographicSize * Screen.width / Screen.height), 0);
        Vector2 newPos = nextPos.x > minPos.x ? new Vector2(nextPos.x, rbb.position.y) : new Vector2(rbb.position.x + (minPos.x - rbb.position.x) * Time.fixedDeltaTime, rbb.position.y);
        rbb.MovePosition(newPos);

        System.Random rand = new System.Random();
        // Update bumper positions
        foreach (GameObject bumper in bumpers) {
            Rigidbody2D bumperRb = bumper.GetComponent<Rigidbody2D>();
            if (bumperRb.position.x < rbb.position.x) {
                float minX = cam.transform.position.x + (cam.orthographicSize * Screen.width / Screen.height);
                foreach (GameObject otherBumper in bumpers) {
                    // Ensure bumper is spaced at least 30 from every other bumper
                    float otherX = otherBumper.GetComponent<Rigidbody2D>().position.x + 20;
                    minX = otherX > minX ? otherX : minX;
                }
                float posX = minX + rand.Next(0, 80);
                float posY = bumperYPos[rand.Next(0, bumperYPos.Length)];
                bumperRb.position = new Vector2(posX, posY);
                bumper.GetComponent<BumperScript>().OverCharge = rand.Next(3) == 0;
            }
        }
        
        // Update token positions
        foreach (GameObject coin in coins) {
            Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
            if (coinRb.position.x < rbb.position.x) {
                float minX = cam.transform.position.x + (cam.orthographicSize * Screen.width / Screen.height) + 10;
                float posX = minX + rand.Next(0, 250);
                float posY = rand.Next(8, 30);
                coinRb.position = new Vector3(posX, posY, -1);
            }
       }
    }
}
