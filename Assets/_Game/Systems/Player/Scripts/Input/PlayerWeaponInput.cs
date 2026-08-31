namespace Systems.Player.Inputs
{
    public class PlayerWeaponInput : Weapon.Inputs.WeaponInput
    {
        public void InputAttackPressed() {
            OnAttackPressed();
        }
        public void InputAttackReleased()
        {
            OnAttackReleased();
        }
        public void InputReload()
        {
            OnReload();
        }
    }
}