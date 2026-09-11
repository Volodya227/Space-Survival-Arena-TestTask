using UnityEngine;
namespace Systems.Player.Inputs {
    public class PlayerInputCommandsOld : PlayerInputCommands
    {
        public override bool GetEscape => Input.GetKeyDown(KeyCode.Escape);
        public override float GetMoveX => Input.GetAxis("Horizontal");
        public override float GetMoveZ => Input.GetAxis("Vertical");
        public override float GetMoveMouseX => Input.GetAxis("Mouse X");
        public override float GetMoveMouseY => Input.GetAxis("Mouse Y");
        public override bool GetMouseUp => Input.GetKeyUp(KeyCode.Mouse0);
        public override bool GetMouseDown => Input.GetKeyDown(KeyCode.Mouse0);
        public override bool GetReloadPressed => Input.GetKeyDown(KeyCode.R);
        public override bool GetChangeViewKeeping => Input.GetKeyDown(KeyCode.V);//TODO delete "Down"
        public override bool GetChangeActiveUI => Input.GetKeyDown(KeyCode.U);
        public override bool GetChangeCharacter => Input.GetKeyDown(KeyCode.L);
    }
}