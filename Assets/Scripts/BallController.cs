using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody ballRb;

    void Start()
    {
        transform.localScale *= GameManager.mod;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        ballRb = GetComponent<Rigidbody>();
    }

    public void FollowTouch(Vector3 touchPos)
    {
        transform.position = Vector3.Lerp(transform.position, touchPos, 5 * Time.deltaTime);
    }
    public void MoveBall(Vector3 force)
    {
        ballRb.useGravity = true;
        ballRb.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroySensor"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CountSensor"))
        {
            gameManager.ScoreGame();
        }
    }
}
