using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public Action<int> OnLifeValueChanged;
    //Public UnityEvent<int> OnLifeValueChanged;

    //Private Lives variable (_ to indicate an internal variable)
    private int _lives = 3;

    //Variable to track the pause state of the game
    private bool isPaused = false;

    //Public variable for getting and setting lives
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            //All lives lost (zero counts as a life due to the check
            if (value < 0)
            {
                GameOver();
                return;
            }

            //Lost a life
            if (value < _lives)
            {
                Respawn();
            }

            //Cannot roll over the maximum amount of lives
            if (value >= maxLives)
            {
                value = maxLives;
            }
            _lives = value;
            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"Lives value on {gameObject.name} has changed to {lives}");
        }
    }

    public AudioMixerGroup SFXGroup;
    public AudioMixerGroup MusicGroup;

    //Max lives possible
    [SerializeField] private int maxLives = 10;
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private AudioClip pauseClip;

    AudioSource audioSource;
    [HideInInspector] public PlayerController PlayerInstance => playerInstance;
    private PlayerController playerInstance;
    private Transform currentCheckpoint;
    private MenuController currentMenuController;
    private void Awake()
    {
        //If we are the first instance of the gamemanager object - ensure that our instance variable is filled and we cannot be destroyed when loading new levels.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return; //Early exit out of the function
        }

        //If we are down here in execution - that means that the above if statement didn't run - which means we are a clone
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = GetComponent<AudioSource>();

        if (!currentMenuController) return;

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Level")
        {
            if (isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
            audioSource.PlayOneShot(pauseClip);
        }
    }

    public void PauseGame()
    {
        currentMenuController.SetActiveState(MenuController.MenuStates.Pause);
        Time.timeScale = 0;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void UnpauseGame()
    {
        currentMenuController.JumpBack();
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("Game Unpaused");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Out of lives. Game Over!");
        }
    }

    void Respawn()
    {
        playerInstance.MarioDeathAudio();
        playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }

    public void SetMenuController(MenuController menuController)
    {
        currentMenuController = menuController;
    }
}
