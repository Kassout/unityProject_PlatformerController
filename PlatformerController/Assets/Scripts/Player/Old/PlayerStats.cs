using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    private float _currentHealth;

    private GameManager _gameManager;

    private void Start()
    {
        _currentHealth = maxHealth;
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        
        _gameManager.Respawn();
        
        Destroy(gameObject);
    }
}
