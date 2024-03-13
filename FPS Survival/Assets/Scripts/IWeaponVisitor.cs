public interface IWeaponVisitor
{
    public void Visit(EnemyMakingDamage visitor);
    public void Visit(CollisionScanProjectile visitor);

}
