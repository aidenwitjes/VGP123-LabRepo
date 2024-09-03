using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    //Private Lives variable (_ to indicate an internal variable)
    private int _lives = 3;

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

            Debug.Log($"Lives value on {gameObject.name} has changed to {lives}");
        }
    }

    //Max lives possible
    [SerializeField] private int maxLives = 10;
    [SerializeField] private PlayerController playerPrefab;

    [HideInInspector] public PlayerController PlayerInstance => playerInstance;
    private PlayerController playerInstance;
    private Transform currentCheckpoint;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level" || SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("Title");
                Debug.Log("Title Screen, press Esc to play again");
            }
            else
                SceneManager.LoadScene("Level");
        }
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Game Over, press Esc to go back to the title screen");
        }
    }

    void Respawn()
    {
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
}
