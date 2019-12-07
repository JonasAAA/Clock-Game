using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClockGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        PlayState playState;
        MenuState menuState;
        WinState winState;
        LoseState loseState;
        ExplainState explainState;

        enum GameState
        {
            menu,
            play,
            win,
            lose,
            explain
        }

        GameState gameState;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = C.screenWidth;
            graphics.PreferredBackBufferHeight = C.screenHeight;
            graphics.IsFullScreen = true;
        }

        protected override void LoadContent()
        {
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;
            GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
            graphics.ApplyChanges();

            C.spriteBatch = new SpriteBatch(GraphicsDevice);
            C.Content = Content;

            gameState = GameState.menu;
            menuState = new MenuState();
            //playState = new PlayState();
            //winState = new WinState();
            //loseState = new LoseState();
            //explainState = new ExplainState();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (gameState)
            {
                case GameState.menu:
                    IsMouseVisible = true;
                    menuState.Update();
                    if (menuState.newGame.isAtcivated)
                    {
                        gameState = GameState.play;
                        playState = new PlayState();
                    }
                    if (menuState.howToPlay.isAtcivated)
                    {
                        gameState = GameState.explain;
                        explainState = new ExplainState();
                    }
                    if (menuState.exit.isAtcivated)
                        Exit();
                    break;
                case GameState.play:
                    IsMouseVisible = false;
                    playState.Update(elapsed);
                    if (!playState.player.isAlive)
                    {
                        gameState = GameState.lose;
                        loseState = new LoseState();
                    }
                    if (playState.ifWin)
                    {
                        gameState = GameState.win;
                        winState = new WinState();
                    }
                    break;
                case GameState.win:
                    IsMouseVisible = true;
                    winState.Update();
                    if (winState.back.isAtcivated)
                    {
                        gameState = GameState.menu;
                        menuState = new MenuState();
                    }
                    break;
                case GameState.lose:
                    IsMouseVisible = true;
                    loseState.Update();
                    if (loseState.back.isAtcivated)
                    {
                        gameState = GameState.menu;
                        menuState = new MenuState();
                    }
                    break;
                case GameState.explain:
                    IsMouseVisible = true;
                    explainState.Update();
                    if (explainState.back.isAtcivated)
                    {
                        gameState = GameState.menu;
                        menuState = new MenuState();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            switch (gameState)
            {
                case GameState.menu:
                    menuState.Draw();
                    break;
                case GameState.play:
                    playState.Draw();
                    break;
                case GameState.win:
                    playState.Draw();
                    winState.Draw();
                    break;
                case GameState.lose:
                    playState.Draw();
                    loseState.Draw();
                    break;
                case GameState.explain:
                    explainState.Draw();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
