/*
using UnityEngine;
namespace Systems.Player.Inputs
{
    public class PlayerInputCommandsNew : PlayerInputCommands
    {
        private readonly NewInputConfig _actions;
        public PlayerInputCommandsNew()
        {
            _actions = new NewInputConfig();
        }
        public override bool GetEscape => Input.GetKeyDown(KeyCode.Escape);
        public override float GetMoveX => _actions.Character.Move.ReadValue<Vector2>().x;
        public override float GetMoveZ => _actions.Character.Move.ReadValue<Vector2>().y;
        public override float GetMoveMouseX => Input.GetAxis("Mouse X");
        public override float GetMoveMouseY => Input.GetAxis("Mouse Y");
        public override bool GetMouseUp => Input.GetKeyUp(KeyCode.Mouse0);
        public override bool GetMouseDown => Input.GetKeyDown(KeyCode.Mouse0);
        public override bool GetReloadPressed => Input.GetKeyDown(KeyCode.R);
        public override bool GetChangeViewKeeping => Input.GetKeyDown(KeyCode.V);//TODO delete "Down"
        public override bool GetChangeActiveUI => Input.GetKeyDown(KeyCode.U);
        public override bool GetChangeCharacter => _actions.Player.ChangeCharacter.triggered;
        public override void OnEnable() {
            _actions.Enable();
        }
        public override void OnDisable() {
            _actions.Disable();
        }
    }
}//*/
// референс для майбутнього розширення версія 0