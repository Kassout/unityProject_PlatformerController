using UnityEngine;

public class Death : CoreComponent
{
    #region Fields

    [SerializeField] private GameObject[] deathParticles;

    private Stats _stats;
    private ParticleManager _particleManager;
    
    #endregion

    #region Properties

    private Stats Stats => _stats ? _stats : _core.GetCoreComponent(out _stats);
    private ParticleManager ParticleManager => _particleManager ? _particleManager : _core.GetCoreComponent(out _particleManager);

    #endregion

    #region Public

    public void Die()
    {
        for (int i = 0; i < deathParticles.Length; i++)
        {
            ParticleManager.StartParticles(deathParticles[i]);
        }
        
        _core.transform.parent.gameObject.SetActive(false);
    }

    #endregion
    
    #region Private

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }

    #endregion
}
