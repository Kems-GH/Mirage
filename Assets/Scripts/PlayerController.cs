using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMouvement : MonoBehaviour
{
    public GameObject resourcesPrefab;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private Slider energySlider;
    

    private float speed = 5f;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody playerRb;
    private Vector3 moveInput;
    private MenuHandler MenuHandler;

    private float dashForce = 5f;
    private float dashEnergyCost = 20f;
    private float energy = 100f;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = visual.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        TryGetComponent(out playerRb);
        MenuHandler = GameObject.Find("Canvas").GetComponent<MenuHandler>();

        StartCoroutine(RefreshEnergy());
    }

    private void Update()
    {
        if (gameManager.gameOver) return;
        energySlider.value = energy;

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveInput = Vector3.ClampMagnitude(moveInput, 1);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuHandler.Pause();
        }

        if(Input.GetKeyDown(KeyCode.Space) && energy > dashEnergyCost)
        {
            float distance = dashForce;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, moveInput, out hit, 5f))
            {
                distance = hit.distance;
            }
            
            transform.position += moveInput * distance;
            audioSource.clip = dashSound;
            audioSource.Play();
            energy -= dashEnergyCost;
        }
    }

    void FixedUpdate()
    {
        if (gameManager.gameOver) return;

        MovePlayer();
    }

    void MovePlayer()
    {
        playerRb.velocity = moveInput * speed;

        if (moveInput.sqrMagnitude > 0)
        {
            visual.transform.rotation = Quaternion.LookRotation(moveInput);
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
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private IEnumerator RefreshEnergy()
    {
        while (!gameManager.gameOver)
        {
            yield return new WaitForSeconds(0.1f);
            energy += 0.1f;
            energy = Mathf.Clamp(energy, 0, 100);
        }
    }
}
