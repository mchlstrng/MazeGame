using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class InputManager
    {
        private KeyboardState keyboardState;
        private GamePadState gamePadState;

        public bool IsUpPressed()
        {
            UpdateInputStates();
            return keyboardState.IsKeyDown(Keys.Up);
        }

        public bool IsDownPressed()
        {
            UpdateInputStates();
            return keyboardState.IsKeyDown(Keys.Down);
        }

        public bool IsLeftPressed()
        {
            UpdateInputStates();
            return keyboardState.IsKeyDown(Keys.Left);
        }

        public bool IsRightPressed()
        {
            UpdateInputStates();
            return keyboardState.IsKeyDown(Keys.Right);
        }

        public bool IsExitRequested()
        {
            UpdateInputStates();
            return gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape);
        }

        private void UpdateInputStates()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);
        }
    }

}
