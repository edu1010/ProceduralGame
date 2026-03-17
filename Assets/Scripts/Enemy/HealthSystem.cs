using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour , IRecieveDamage
{
    public float MaxHealth =100;
    private float _currentHealth;

    public delegate void DamageDelegate(float healthLeftFraction);
    public static DamageDelegate OnPlayerRecieveDamage;

    public delegate void OnDeathDelegate();
    public OnDeathDelegate OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
        OnPlayerRecieveDamage?.Invoke(_currentHealth / MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (transform.tag.Equals("Player")) 
        {
            OnPlayerRecieveDamage?.Invoke(_currentHealth / MaxHealth);
        }
       
        if (_currentHealth <= 0.0f)
            {
                OnDeath?.Invoke();
            }
    }
}
