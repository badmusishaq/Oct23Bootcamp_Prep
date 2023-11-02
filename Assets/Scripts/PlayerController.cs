using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed;
    public float arrowMinX, arrowMaxX;
    public float throwForce;

    [SerializeField] private Transform throwingArrow;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private Rigidbody selectedBall;
    [SerializeField] private Rigidbody[] balls;
    [SerializeField] private Animator arrowAnimator;

    Vector3 ballOffset;
    bool wasBallThrown;
    //bool animValue;

    // Start is called before the first frame update
    void Start()
    {
        ballOffset = ballSpawnPoint.position - throwingArrow.position;
        //StartThrow();
    }

    // Update is called once per frame
    void Update()
    {
        TryMoveArrow();
        TryShootBall();
    }

    public void StartThrow()
    {
        arrowAnimator.SetBool("Aiming", true);
        wasBallThrown = false;

        int ballToSpawnIndex = Random.Range(0, balls.Length);
        selectedBall = Instantiate(balls[ballToSpawnIndex], ballSpawnPoint.position, Quaternion.identity);
        Debug.Log($"Balls to spawn index = {ballToSpawnIndex}");
    }

    void TryMoveArrow()
    {
        if(!wasBallThrown)
        {
            //Move arrow without bounds
            //throwingArrow.position += throwingArrow.right * Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;

            //Move throwing arrow with bounds
            float movePosition = Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;
            throwingArrow.position = new Vector3(Mathf.Clamp(throwingArrow.position.x + movePosition, arrowMinX, arrowMaxX),
                throwingArrow.position.y, throwingArrow.position.z);

            //move the ball with the arrow
            selectedBall.transform.position = throwingArrow.position + ballOffset;
            ballSpawnPoint.position = throwingArrow.position + ballOffset;
        }
    }

    void TryShootBall()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            wasBallThrown = true;
            selectedBall.AddForce(throwingArrow.forward * throwForce, ForceMode.Impulse);
            arrowAnimator.SetBool("Aiming", false);
        }
    }
}
