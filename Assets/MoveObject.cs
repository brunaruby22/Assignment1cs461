using UnityEngine;
using System.Collections; //needed for the use of IEnumerator
using UnityEngine.InputSystem; //required for input

//script is fully commented to explain code.

public class MoveObject : MonoBehaviour
{
    //variables of the built in type Vector3, that carries 3 numbers (like x,y, and z) for mainly 3D working
    //setting the positions for each in the inspector window in unity is possible, but here are the default positions I manually chose.
    //if inspector value changes are not saved to project, I prefer to force the actual positions here then just initializing it, so
    public Vector3 positionA = new Vector3(47f, -5f, -20f);
    public Vector3 positionB = new Vector3(-80f, -5f, -95f);
    public Vector3 positionC = new Vector3(76f, -5f, -80f);

    public float moveTime = 2f; //sets the journey from one position to the other, exactly 2 seconds as requested in assingment.

    //protected variables that handle the pausing and resuming using coroutine
    private Coroutine myCoroutine; //initializes a coroutine
    private bool isMoving = false;//and sets isMoving property to later check whether the selected object is moving or not

    private Vector3 legStartPosition; //points to first position before moving (as a position, it carries 3 numbers)
    private int currentTargetIndex = 0; //this variable is for the target, it points to which waypoint the object should go to
    private float timer = 0f; //tracks the current leg time in seconds

    void Update() //listens to user input and sets changes
    {
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (!isMoving)
            {
                isMoving = true;
                myCoroutine = StartCoroutine(MoveLoop());
            }
        }

        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (isMoving)
            {
                StopCoroutine(myCoroutine);
                isMoving = false;
            }
        }
    }

    IEnumerator MoveLoop() //actuall logic, IEnumerator is a return type that is a coroutine, and it may pause and resume
    {
        Vector3[] waypoints = { positionA, positionB, positionC }; //a position array that may be iterated (to move through)

        while (true)// makes the loop endless until manually stopped (in this case with the 'C' key)
        {
            legStartPosition = transform.position; //notes starting point

            //while still with time, this relates distance with time for smoothness AND MOVES the object
            while (timer < moveTime)
            {
                timer += Time.deltaTime; //Time.deltaTime as expected to add as time goes by
                transform.position = Vector3.Lerp(legStartPosition, waypoints[currentTargetIndex], timer / moveTime);//linear interpolation btw the two positions, so theres a relation between position distance and its percentage with the amount of time to complete it 
                yield return null; //yield return as expected
            }

            //selects next posiiton and reset timer for next leg to make sure it alwasy falls within 2 seconds as mentioned
            transform.position = waypoints[currentTargetIndex];
            timer = 0f; //resets to 0

            //points to next position by updating the current target
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
        }
    }
}