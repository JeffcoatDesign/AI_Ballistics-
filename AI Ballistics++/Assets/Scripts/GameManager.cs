using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent showTutorial = new();
    public UnityEvent hideTutorial = new();
    public UnityEvent gameOver = new();
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawn;
    public int wave = 0;
    public int hp = 3;
    private bool isDead = false;

    public void HurtPlayer()
    {
        hp--;
        if (hp < 1 && !isDead) Die();
    }

    private void Die()
    {
        isDead = true;
        gameOver.Invoke();
        Time.timeScale = 0f;
    }

    private void Awake()
    {
        Instance = this;
        InvokeRepeating("SpawnEnemy", 3f, 10f);
        showTutorial.Invoke();
        Invoke("HideTutorial", 3f);
    }

    private void SpawnEnemy()
    {
        enemySpawn.position = new Vector3(Random.Range(-10f, 10f), enemySpawn.position.y, enemySpawn.position.z);
        Monster newMonster = Instantiate(enemyPrefab, enemySpawn.position, enemySpawn.rotation).GetComponent<Monster>();
        newMonster.SetTarget(enemyTarget);
        wave++;
    }

    private void HideTutorial()
    {
        hideTutorial.Invoke();
    }
}
