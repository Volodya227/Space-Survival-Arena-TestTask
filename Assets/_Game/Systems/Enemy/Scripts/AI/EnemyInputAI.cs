namespace Systems.Enemy.AI
{
    public class EnemyInputAI : Inputs.EnemyInput
    {
        public void SetMove(float x, float z)
        {
            MoveX = x;
            MoveZ = z;
        }
        public void SetRotation(float y)
        {
            RotationY = y;
        }
        public void Attack()
        {
            AttackBase();
        }
    }
}