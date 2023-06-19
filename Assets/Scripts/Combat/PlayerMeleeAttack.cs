public class PlayerMeleeAttack : MeleeAttack
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StatManager.OnStatsChanged += CalculateAttack;
        StatManager.OnStatsChanged += CalculateKnockback;
    }

    protected void OnDisable()
    {
        StatManager.OnStatsChanged -= CalculateAttack;
        StatManager.OnStatsChanged -= CalculateKnockback;
    }
}