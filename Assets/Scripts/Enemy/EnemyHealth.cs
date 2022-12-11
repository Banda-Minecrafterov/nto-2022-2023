using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyHealth : CharacterHealth
{
    public override bool TakeDamage(float damage)
    {
        if (base.TakeDamage(damage))
        {
            gameObject.SetActive(false);

            UpgradeManager.AddSunEnergy(((Enemy)character).sunEnergy);
            BeastsManager.AddEnemy((Enemy)character);
            return true;
        }
        return false;
    }
}
