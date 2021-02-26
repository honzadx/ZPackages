using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace zhdx
{
    namespace General
    {
        public class Health : MonoBehaviour
        {
            public float maxHealth;
            public float health;
            public bool invulnerable;

            public System.Action<float> healthValueChanged;

            public UnityEvent damage;
            public UnityEvent heal;
            public UnityEvent death;

            public void Kill()
            {
                death?.Invoke();
            }

            public void Damage(float damageAmount)
            {
                if (invulnerable)
                    return;

                health -= damageAmount;
                damage?.Invoke();
                healthValueChanged?.Invoke(health);

                if (health <= 0)
                {
                    Kill();
                }
            }

            public void Heal(float healAmount)
            {
                health = Mathf.Min(health + healAmount, maxHealth);
                heal?.Invoke();
                healthValueChanged?.Invoke(health);
            }
        }
    }
}
