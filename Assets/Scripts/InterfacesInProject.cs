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
    float HP { get; set; }
    void GetDamage(float damage);
}

public interface IEnemy
{
    Line Line { get; set; }
    void Despawn();
}

public interface ITurret
{
    Line Line { get; set; }
    float HP { get; set; }
    void Install(int dice);
    void Dismantle();
}

public interface IRewardPanel
{
    void Activate();
    void Desactivate();
    void Use(Dice d);
}

public interface IGameMode
{
    void Activate();
    void Pause();
    void DespawnEnemy();
    void DeathEnemy();
    void Win();
    void Lose();
}


