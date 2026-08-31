namespace Systems.Character.Inputs
{
    public abstract class CharacterInput
    {
        public bool Active { get; private set; } = false;
        public float MoveX { get; protected set; }
        public float MoveZ { get; protected set; }
        public float Yrotation { get; protected set; } = 0;
        public float Xrotation { get; protected set; }
        public CharacterInput()
        {
            SetActive(false);
        }
        public void SetActive(bool value)
        {
            //How closed for clild?
            Active = value;
        }
    }
}