using GLGBRZYLFinal.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Text;


namespace GLGBRZYLFinal.Scenes
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Component> components;
        private State currentState;
        private State nextState;

        public void ChangeScene(State state)
        {
            nextState = state;
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            currentState = new MenuState(this, GraphicsDevice, Content);
            currentState.LoadContent();
            nextState = null;
            
        }
        

        protected override void Update(GameTime gameTime)
        {
            
            
            if(nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime);
            currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, _spriteBatch);
            

            base.Draw(gameTime);
        }
    }
}