using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public GameObject resourcesPrefab;
    [SerializeField] private GameObject visual;

    private float speed = 10f;
    private float boundHorizontal = 24f;
    private float boundVertical = 9f;
    private GameManager gameManager;
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameOver) return;

        MovePlayer();
    }

    void MovePlayer()
    {
        // Get the horizontal input
        float horizontal = Input.GetAxis("Horizontal");
        // Keep the player in Horizontal bounds
        if (transform.position.x < -boundHorizontal)
        {
            transform.position = new Vector3(-boundHorizontal, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > boundHorizontal)
        {
            transform.position = new Vector3(boundHorizontal, transform.position.y, transform.position.z);
        }
        else
        { 
            // Move the player left and right
            transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
        }

        // Get the vertical input
        float vertical = Input.GetAxis("Vertical");

        // Keep the player in Vertical bounds
        if (transform.position.z < -boundVertical)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundVertical);
        }
        else if (transform.position.z > boundVertical)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundVertical);
        }
        else
        {
            // Move the player up and down
            transform.Translate(Vector3.forward * vertical * Time.deltaTime * speed);
        }

        // the player looks at the movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement != Vector3.zero)
        {
            visual.transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resources"))
        {
            Destroy(other.gameObject);
            gameManager.AddScore(1);
            spawnManager.Spawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            gameManager.GameOver();
        }
    }
}
