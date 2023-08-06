using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;

    private bool isSpawning = false;

    private int enemiesKilled = 0;

    public TextMeshProUGUI scoreText;

    public GameObject player;

    public GameObject gameOverScreen;

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Prevent this game object from being destroyed when loading a new scene
        StartSpawning();

        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Hitable>().OnHealthDepleted += PlayerDestroyed;
        Assert.IsNotNull(player);
    }

    private void PlayerDestroyed(GameObject playerObj)
    {

        // Stop the player from moving
        player.GetComponent<PlayerController>().enabled = false;

        StopGame();
    }

    private void StopGame()
    {
        StopSpawning();

        // Show the game over screen
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // Show the game over screen
        gameOverScreen.SetActive(true);

    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            StopCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            // Calculate a random position within the game area
            var spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);

            // Instantiate the enemy prefab at the spawn position
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<Hitable>().OnHealthDepleted += EnemyHealthDepleted;

            // Wait for a certain amount of time
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public void EnemyHealthDepleted(GameObject enemyObj)
    {
        Destroy(enemyObj);
        enemiesKilled++;
        scoreText.text = $"Killed: {enemiesKilled}";
    }

    public void RestartButtonClick()
    {
        StartSpawning();
        // Reload the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
