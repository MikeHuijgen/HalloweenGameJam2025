using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Header("Beweging instellingen")]
    public Vector3 targetOffset = new Vector3(5f, 0f, 0f); // hoeveel hij naar rechts beweegt
    public float moveSpeed = 2f; // snelheid van bewegen

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool moving = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + targetOffset;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                moving = false;
            }
        }
    }

    // Publieke functie om beweging te starten
    public void MoveCamera()
    {
        moving = true;
    }
}
