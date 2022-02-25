using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private GameManager gameManagerScript;
    private Rigidbody ballRb;
    private AudioSource ballAudio;
    [SerializeField] private AudioClip bounce;
    [SerializeField] private AudioClip rim;
    private bool isRim;
    [SerializeField] private AudioClip net;
    [SerializeField] private AudioClip board;

    void Start()
    {
        transform.localScale *= GameManager.mod;
        gameManagerScript = GameObject.Find("GameScreen").GetComponent<GameManager>();
        ballRb = GetComponent<Rigidbody>();
        ballAudio = GetComponent<AudioSource>();
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
        if (collision.gameObject.CompareTag("Rim") && !isRim)
        {
            isRim = true;
            ballAudio.PlayOneShot(rim);
        }
        if (collision.gameObject.CompareTag("Board"))
        {
            ballAudio.PlayOneShot(board);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CountSensor"))
        {
            gameManagerScript.ScoreGame();
            Invoke(nameof(NetClip), 2.0f);
        }
    }
    void NetClip()
    {
        ballAudio.PlayOneShot(net);
    }
}
//все не для нас