using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;

    public float directionChangeInterval;
    public bool followPlayer;

    Coroutine moveCoroutine;

    CircleCollider2D circleCollider;
    Rigidbody2D rb2d;
    //Animator animator;

    Transform targetTransform = null;
    public Transform player;

    Vector3 endPosition;
    float currentAngle = 0;

    void Start()
    {
        //animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;

        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = this.GetComponent<Rigidbody2D>();

        StartCoroutine(WanderRoutine());
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            //wait directionChangeInterval seconds and change direction
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360);

        //if currentAngle is > 360, loop starts at 0 again
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            //when pursuing targetTransform wont be null.
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidBodyToMove != null)
            {
                //animator.SetBool("isWalking", true);

                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }

        //enemy reached endPosition and is waiting for new direction
        //animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;

            //needed so that coruotine can follow player.
            targetTransform = collision.gameObject.transform;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            //will now move towards the player
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            //no target to follow
            targetTransform = null;
        }
    }

    //////////////////////////// debugging

    void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 direction = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb2d.rotation = angle;
        }
        else
        {
            
            Vector3 direction = transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb2d.rotation = angle;
        }
        
        //print("PLAYER" + targetTransform.position);
        //print("ENEMIE" + transform.position);
        //target line
        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }
}
