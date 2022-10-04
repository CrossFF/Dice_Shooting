using System.Collections;
using System.Collections.Generic;

public interface IWeapon
{
    void Shoot(int dice);
    void Special1(int dice);
    void Special2(int dice);
    void ClearEffects();
}

public interface IDamageable 
{
    void GetDamage(float damage);
}


