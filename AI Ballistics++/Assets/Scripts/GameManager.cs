using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent gameOver;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawn;
    public int wave = 0;
    [SerializeField] int hp = 3;
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
        InvokeRepeating("SpawnEnemy", 0f, 10f);
    }

    private void SpawnEnemy()
    {
        enemySpawn.position = new Vector3(Random.Range(-10f, 10f), enemySpawn.position.y, enemySpawn.position.z);
        Monster newMonster = Instantiate(enemyPrefab, enemySpawn.position, enemySpawn.rotation).GetComponent<Monster>();
        newMonster.SetTarget(enemyTarget);
        wave++;
    }
}
