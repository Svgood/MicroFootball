using _Assets.Scripts.Gameplay.Bot;
using MicroFootball.Configs;
using MicroFootball.Gameplay.View;
using UniRx;
using Zenject;

namespace MicroFootball.Gameplay.Model
{
    public sealed class GameplayModel
    {
        private readonly ReactiveProperty<int> _leftScore = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _rightScore = new ReactiveProperty<int>(0);

        public BotModel LeftBot { get; }
        public BotModel RightBot { get; }
        public BallModel Ball { get; }

        public IReadOnlyReactiveProperty<int> LeftScore => _leftScore;
        public IReadOnlyReactiveProperty<int> RightScore => _rightScore;

        public GameplayModel(
            IGameplayPositionsProvider gameplayPositionsProvider,
            BallModel ball, IBotFacadeFactory botFacadeFactory)
        {
            LeftBot = botFacadeFactory.Create(gameplayPositionsProvider.Bot1StartingPosition, gameplayPositionsProvider.Bot2StartingPosition).Model;
            RightBot = botFacadeFactory.Create(gameplayPositionsProvider.Bot2StartingPosition, gameplayPositionsProvider.Bot1StartingPosition).Model;;
            Ball = ball;
        }

        public void Tick(float dt)
        {
            LeftBot.Tick(dt, Ball.Position.Value);
            RightBot.Tick(dt, Ball.Position.Value);
            Ball.Tick(dt);
            TryHandleGoal();
        }

        private void TryHandleGoal()
        {
            var goalSide = Ball.GetGoalSide();
            if (goalSide == GoalSide.Left)
            {
                _rightScore.Value++;
                ResetRound();
                return;
            }

            if (goalSide == GoalSide.Right)
            {
                _leftScore.Value++;
                ResetRound();
            }
        }

        private void ResetRound()
        {
            LeftBot.Reset();
            RightBot.Reset();
            Ball.Reset();
        }
    }
}
