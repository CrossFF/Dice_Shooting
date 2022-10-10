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

public interface IEnemy
{
    Line Line { get; set; }
}

public interface ITurret
{
    Line Line { get; set; }
    float HP { get; set; }
    void Install(int dice);
    void Dismantle();
}


