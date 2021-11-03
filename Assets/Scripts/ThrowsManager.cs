using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ThrowsManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private BallController ballControllerScript;
    private Vector3 position;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 touchedPos;

    private void Update()
    {
#if UNITY_EDITOR
        ForConsole();
#else
        ForAndroid();
#endif
    }
    private void ForAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);
            switch (theTouch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 5));
                    ballControllerScript = Instantiate(ball, touchStartPos, ball.transform.rotation).GetComponent<BallController>();
                    break;

                case TouchPhase.Moved:
                    touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 5));
                    ballControllerScript.FollowTouch(touchedPos);
                    break;

                case TouchPhase.Ended:
                    touchEndPos = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 5));
                    Vector3 swipeVector = touchEndPos - touchStartPos;
                    //swipeVector = theTouch.deltaPosition;
                    float v = swipeVector.magnitude / theTouch.deltaTime / 3.0f;
                    ballControllerScript.MoveBall((swipeVector.normalized + Vector3.forward) * v);
                    break;
            }
        }
    }
    private void ForConsole()
    {
        if (Input.GetMouseButtonDown(0))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,5));
            Debug.Log(position);
            Instantiate(ball, position, ball.transform.rotation);
        }
    }
}
