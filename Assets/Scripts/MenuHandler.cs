using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public static float volume { get; private set; } = 1f;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;

    private bool isPaused = false;

    private void Start()
    {
        volumeSlider.value = volume;
        SetVolume();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
         Application.Quit();
    }

    public void SetVolume()
    {
        if(volumeSlider != null)
        {
            volume = volumeSlider.value;
        }

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }

    }

    public void OpenOptionMenu()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void CloseOptionMenu()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void Pause()
    {
        if(isPaused)
        {
            Resume();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);

    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        mainMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

}
