using System;
using UnityEngine;

public class Stats : CoreComponent
{
    #region Fields

    public event Action OnHealthZero;
    public event Action OnStunResistanceZero;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float stunResistance;
    [SerializeField] private float stunRecoveryTime;

    private float _currentHealth;
    private float _currentStunResistance;

    #endregion

    #region CoreComponent

    protected override void Awake()
    {
        base.Awake();

        _currentHealth = maxHealth;
        _currentStunResistance = stunResistance;
    }

    #endregion

    #region Public

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            OnHealthZero?.Invoke();
        }
    }

    public void IncreaseHealth(float amount)
    {
        _currentHealth += Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
    }

    public void DecreaseStunResistance(float amount)
    {
        _currentStunResistance -= amount;

        if (_currentStunResistance <= 0f)
        {
            ResetStunResistance();
            OnStunResistanceZero?.Invoke();
        }
    }

    private void ResetStunResistance()
    {
        _currentStunResistance = stunResistance;
    }

    #endregion
}
