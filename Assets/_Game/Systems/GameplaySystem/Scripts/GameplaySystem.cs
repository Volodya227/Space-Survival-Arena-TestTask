namespace Systems.Gameplay
{
    public class GameplaySystem
    {
        private readonly Enemy.EnemySystem _enemySystem;
        private readonly Player.PlayerSystem _playerSystem;
        private readonly Character.CharacterSystem _characterSystem;
        private readonly UI.Gameplay.UISystem _UISystem;
        private readonly Share.TargetForEnemy _targetForEnemy;
        public GameplaySystem(Enemy.EnemySystem enemySystem, Player.PlayerSystem playerSystem, UI.Gameplay.UISystem UISystem, Character.CharacterSystem characterSystem, Share.TargetForEnemy targetForEnemy)
        {
            _enemySystem = enemySystem;
            _playerSystem = playerSystem;
            _UISystem = UISystem;
            _characterSystem = characterSystem;
            _targetForEnemy = targetForEnemy;
            _targetForEnemy.HealthState.EventChangeHealth += CheckEndGame;
            _UISystem.EventRestartGame += Restart;
            //Enent from UISystem bind on Restart
        }
        public void Dispose()
        {
            _targetForEnemy.HealthState.EventChangeHealth -= CheckEndGame;
            _UISystem.EventRestartGame -= Restart;
        }
        public void CheckEndGame()
        {
            if (_targetForEnemy.HealthState.Health == 0)
                EndGame();
        }
        private void EndGame()
        {
            float value = 0;
            for (int i = 0; i < _characterSystem.GetCharacterCount; i++)
            {
                value += _characterSystem.GetCharacterByIndex(i).State.Item.Count;
            }
            _enemySystem.StopGame();
            //TODO new panel for ending
            _UISystem.SetActiveEndMenu(value);
        }
        private void Restart()
        {
            _enemySystem.Restart();
            _characterSystem.Restart();
        }
    }
}