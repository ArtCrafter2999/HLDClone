using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour, ITakeDamage
{
    public int maxHealth;
    public int health;

    public UnityEvent<float> healthChanged;
    public UnityEvent<float> damaged;
    public UnityEvent<float> healed;
    public UnityEvent<GameObject> dead;
    
    public bool IsDead { get; private set; }

    private void Clamp()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health > 0) return;
        dead.Invoke(gameObject);
        IsDead = true;
    }

    public virtual void TakeDamage(int amount)
    {
        if (!enabled) return;
        health -= amount;
        healthChanged.Invoke(-amount);
        damaged.Invoke(amount);
        Clamp();
    }

    public virtual void TakeHeal(int amount)
    {
        if (!enabled) return;
        health += amount;
        healthChanged.Invoke(amount);
        healed.Invoke(amount);
        Clamp();
    }
}