using UnityEngine;

public class ParticleManager : CoreComponent
{
    #region Fields

    private Transform _particleContainer;

    #endregion

    #region CoreComponent

    protected override void Awake()
    {
        base.Awake();

        _particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    #endregion

    #region Public

    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, _particleContainer);
    }

    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }

    #endregion
}
