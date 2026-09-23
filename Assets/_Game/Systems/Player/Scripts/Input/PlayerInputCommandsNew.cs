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
        public override bool GetEscape => GetInput.GetKeyDown(KeyCode.Escape);
        public override float GetMoveX => _actions.Character.Move.ReadValue<Vector2>().x;
        public override float GetMoveZ => _actions.Character.Move.ReadValue<Vector2>().y;
        public override float GetMoveMouseX => GetInput.GetAxis("Mouse X");
        public override float GetMoveMouseY => GetInput.GetAxis("Mouse Y");
        public override bool GetMouseUp => GetInput.GetKeyUp(KeyCode.Mouse0);
        public override bool GetMouseDown => GetInput.GetKeyDown(KeyCode.Mouse0);
        public override bool GetReloadPressed => GetInput.GetKeyDown(KeyCode.R);
        public override bool GetChangeViewKeeping => GetInput.GetKeyDown(KeyCode.V);//TODO delete "Down"
        public override bool GetChangeActiveUI => GetInput.GetKeyDown(KeyCode.U);
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