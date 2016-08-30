﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmStrong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class ArmStrong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float shoulder_R_rotation = 0f;
        float shoulder_L_rotation = 0f;
        float Pi = 3.14159f;
        float rotation_speed = 0.1f;

        //Declare Textures
        private Texture2D relax_body;
        private Texture2D relax_arm_L_upper;
        private Texture2D relax_arm_L_lower;
        private Texture2D relax_arm_R_upper;
        private Texture2D relax_arm_R_lower;

        //Declare Sprite Vectors
        private Vector2 main_body_position = new Vector2(200,100);
        private Vector2 shoulder_L_body_pos = new Vector2(108,91);
        private Vector2 shoulder_L_upper_arm_pos = new Vector2(64,52);
        private Vector2 shoulder_R_body_pos = new Vector2(165, 88);
        private Vector2 shoulder_R_upper_arm_pos = new Vector2(28, 53);

        public ArmStrong()
        {

            //fuck mark's comment
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            relax_body = Content.Load<Texture2D>("Wrestler Paperdoll/relax_body");
            relax_arm_L_upper = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_L_upper");
            relax_arm_R_upper = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_R_upper");
            relax_arm_L_lower = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_L_lower");
            relax_arm_R_lower = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_R_lower");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            relax_body.Dispose();
            relax_arm_L_upper.Dispose();
            relax_arm_R_upper.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                shoulder_L_rotation = shoulder_L_rotation + rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                shoulder_L_rotation = shoulder_L_rotation - rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                shoulder_R_rotation = shoulder_R_rotation + rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                shoulder_R_rotation = shoulder_R_rotation - rotation_speed;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(relax_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(relax_arm_R_lower, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(relax_body, main_body_position, Color.White);
            spriteBatch.Draw(relax_arm_L_upper, main_body_position+shoulder_L_body_pos,null,Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos,1, SpriteEffects.None,0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}