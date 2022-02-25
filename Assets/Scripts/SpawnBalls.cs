using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    private float posX;
    private float posY;
    private readonly float posZ = -1;

    private readonly float maxPosX = 1.0f;

    private readonly float maxPosY = 3.0f;
    private readonly float minPosY = 0.0f;

    public void SpawnBall()
    {
        Instantiate(ball, BallPosition(), ball.transform.rotation);
    }
    Vector3 BallPosition()
    {
        posX = Random.Range(-maxPosX, maxPosX);
        posY = Random.Range(minPosY, maxPosY);

        return new Vector3(posX, posY, posZ);
    }
}
