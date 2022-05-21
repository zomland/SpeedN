namespace Runtime
{
    using Base.Pattern;
    using Cysharp.Threading.Tasks;
    using Global;
    using UnityEngine.SceneManagement;
    
    public class GameInitializeState : GameState
    {
        public override void EnterStateBehaviour(float dt, GameState fromState)
        {
            GameStateParam.LoginState = true;
        }

        public override void UpdateBehaviour(float dt)
        {
            return;
        }

        public override void CheckExitTransition()
        {
            if (GameStateParam.LoginState) GameStateController.EnqueueTransition<GameLoginState>();
        }

        public override void ExitStateBehaviour(float dt, GameState toState)
        {
            GameStateParam.LoginState = false;
        }
    }
}

