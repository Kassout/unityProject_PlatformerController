using System;
using UnityEngine;

public class Stats : CoreComponent
{
    #region Fields

    public event Action OnHealthZero;
    
    [SerializeField] private float maxHealth;

    private float _currentHealth;

    #endregion

    #region CoreComponent

    protected override void Awake()
    {
        base.Awake();

        _currentHealth = maxHealth;
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

    #endregion
}
