using System;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;

    private ParticleManager ParticleManager => _particleManager ??= _core.GetCoreComponent<ParticleManager>();
    private ParticleManager _particleManager;

    private Stats Stats => _stats ??= _core.GetCoreComponent<Stats>();
    private Stats _stats;
    
    public void Die()
    {
        for (int i = 0; i < deathParticles.Length; i++)
        {
            ParticleManager.StartParticles(deathParticles[i]);
        }
        
        _core.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
