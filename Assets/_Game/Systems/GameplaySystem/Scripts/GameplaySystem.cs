using UnityEngine;
namespace Systems.Gameplay
{
    public class GameplaySystem
    {
        private readonly Enemy.EnemySystem _enemySystem;
        private readonly Player.PlayerSystem _playerSystem;
        private readonly UI.Gameplay.UISystem _UISystem;
        private readonly Share.TargetForEnemy _targetForEnemy;
        public GameplaySystem(Enemy.EnemySystem enemySystem, Player.PlayerSystem playerSystem, UI.Gameplay.UISystem UISystem, Share.TargetForEnemy targetForEnemy)
        {
            _enemySystem = enemySystem;
            _playerSystem = playerSystem;
            _UISystem = UISystem;
            _targetForEnemy = targetForEnemy;
            _targetForEnemy.HealthState.EventChangeHealth += CheckEndGame;
            //Enent from UISystem bind on Restart
        }
        public void Dispose()
        {
            _targetForEnemy.HealthState.EventChangeHealth -= CheckEndGame;
        }
        public void CheckEndGame()
        {
            if(_targetForEnemy.HealthState.Health == 0)
                EndGame();
        }
        private void EndGame()
        {
            _playerSystem.SetActiveInput(false);
            //TODO new panel for ending
            //_UISystem.Do openEndingPanel
        }
        private void Restart()
        {
            //_playerSystem.SetActiveInput(true);
            //_enemySystem.Restart();
        }
    }
}