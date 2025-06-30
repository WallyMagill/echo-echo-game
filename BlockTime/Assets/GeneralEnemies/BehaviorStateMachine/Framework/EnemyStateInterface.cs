public interface IEnemyState
{
    void EnterState(GenEnemy enemy);
    void UpdateState(GenEnemy enemy);
    void ExitState(GenEnemy enemy);
}
