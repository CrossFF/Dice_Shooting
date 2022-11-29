public interface IShootable
{
    void Shoot(int dice);
    void Special(int dice);
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
    void SetDifficulty(Difficulty difficulty);
    void Activate();
    void Pause();
    void DespawnEnemy();
    void DeathEnemy();
    void Win();
    void Lose();
}


