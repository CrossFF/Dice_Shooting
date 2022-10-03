using System.Collections;
using System.Collections.Generic;

public interface IWeapon
{
    void Shoot(Dice dice);
    void Special1(Dice dice);
    void Special2(Dice dice);
    void ClearEffects();
}

public interface IDamageable 
{
    void GetDamage(float damage);
}


