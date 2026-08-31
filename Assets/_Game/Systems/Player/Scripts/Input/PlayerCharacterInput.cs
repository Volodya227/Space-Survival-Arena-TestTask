namespace Systems.Player.Inputs
{
    public class PlayerCharacterInput : Character.Inputs.CharacterInput
    {
        public void SetMoving(float x, float z)
        {
            MoveX = x;
            MoveZ = z;
        }
        public void SetView(float x, float y)
        {
            Yrotation = y;
        }
    }
}