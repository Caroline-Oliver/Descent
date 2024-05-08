using UnityEngine;

public abstract class Weapon
{
    public string name;
    private float baseDamage;

    protected float DamageCalculation() {
        int minRand = 1, maxRand = 20;

        // min damage for all things will be 1
        if (baseDamage <= 1) {
            return 1;
        }
        
        float damage = baseDamage;

        // generate numbers in range (minRand <= num <= maxRand)
        int random = Random.Range(minRand, maxRand+1);

        // critical failure
        if (random == minRand) {
            return 1;
        }

        // critical success
        if (random == maxRand) {
            return damage * 2;
        }

        damage *= 0.025f*(random+10);

        return damage;
    }

    protected abstract void Attack();
}