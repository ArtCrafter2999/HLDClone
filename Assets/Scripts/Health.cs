using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour, ITakeDamage
{
    public float maxHealth;
    public float health;

    public UnityEvent<float> healthChanged;
    public UnityEvent<float> damaged;
    public UnityEvent<float> healed;
    public UnityEvent<GameObject> dead;

    private void Clamp()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0) dead.Invoke(gameObject);
    }

    public virtual void TakeDamage(float amount)
    {
        if (!enabled) return;
        health -= amount;
        healthChanged.Invoke(-amount);
        damaged.Invoke(amount);
        Clamp();
    }

    public virtual void TakeHeal(float amount)
    {
        if (!enabled) return;
        health += amount;
        healthChanged.Invoke(amount);
        healed.Invoke(amount);
        Clamp();
    }
}