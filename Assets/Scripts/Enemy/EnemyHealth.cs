using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    [SerializeField]
    int sunEnergy;


    public override bool TakeDamage(float damage)
    {
        if (base.TakeDamage(damage))
        {
            gameObject.SetActive(false);

            UpgradeManager.AddSunEnergy(sunEnergy);
            return true;
        }
        return false;
    }
}
