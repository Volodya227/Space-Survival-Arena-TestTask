namespace Systems.Enemy.Inputs
{
    public abstract class EnemyInput
    {
        //rotation object will get from vector moving
        public bool Active { get; protected set; }
        public float MoveX { get; protected set; }
        public float MoveZ { get; protected set; }
        public EnemyInput() {
            SetActive(false);
        }
        public void SetActive(bool active) {
            Active = active;
            if (!Active)
                ResetInput();
        }
        private void ResetInput()
        {
            MoveX = 0;
            MoveZ = 0;
        }
    }
}