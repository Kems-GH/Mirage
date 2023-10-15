using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    private GameObject player;
    private float speed = 3.0f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.Translate(lookDirection * Time.deltaTime * speed);

        visual.transform.LookAt(player.transform);

    }
}
