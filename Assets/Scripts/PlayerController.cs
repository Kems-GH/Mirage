using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public GameObject resourcesPrefab;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip deathSound;
    

    private float speed = 10f;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = visual.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        // Move the player left and right
        transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);

        // Get the vertical input
        float vertical = Input.GetAxis("Vertical");

        // Move the player up and down
        transform.Translate(Vector3.forward * vertical * Time.deltaTime * speed);


        // the player looks at the movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement != Vector3.zero)
        {
            visual.transform.rotation = Quaternion.LookRotation(movement);
            animator.SetFloat("Speed_f", 1);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resources"))
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
            Instantiate(pickUpParticle, other.transform.position, pickUpParticle.transform.rotation);
            Destroy(other.gameObject);
            gameManager.AddScore(1);
            StartCoroutine(spawnManager.Spawn());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            audioSource.clip = deathSound;
            audioSource.Play();
            animator.SetBool("Death_b", true);
            gameManager.GameOver();
        }
    }
}
