using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    private GameObject player;
    private float speed = 3.0f;
    private float maxSpeed = 3.0f;
    private float minSpeed = 1.0f;
    private float rotationSpeed = 5.0f;

    private Rigidbody rb;
    void Start()
    {
        player = GameObject.Find("Player");
        speed = Random.Range(minSpeed, maxSpeed);
        TryGetComponent(out rb);
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rb.velocity = lookDirection * speed;

        Vector3 rotationDirection = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(rotationDirection);
        visual.transform.rotation = Quaternion.Slerp(visual.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
