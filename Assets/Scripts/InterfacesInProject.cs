using System.Collections;
using System.Collections.Generic;

public interface IWeapon
{
    void Shoot(int dice);
}

public interface IDamageable 
{
    void GetDamage(float damage);
}


