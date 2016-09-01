using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ArmStrong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class ArmStrong : Game
    {


        //a comment
        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private KeyboardState oldState;

        int score = 10;

        bool flex = false;
       

        // Rotation angles
        float shoulder_R_rotation = 0f;
        float shoulder_L_rotation = 0f;
        float Pi = 3.14159f;
        float rotation_speed = 0.1f;

        ///////////////////////////////////////////////////////////////////mark added
        float elbow_L_rotation = 0f;
        float elbow_R_rotation = 0f;


        ulong point_total = 0; //a fuckload of points

        bool calc_points = true; //should switch and reset on card load or some other event
        ///////////////////////////////////////////////////////////////////end mark added


        //Declare Textures
        private Texture2D relax_body;
        private Texture2D relax_arm_L_upper;
        private Texture2D relax_arm_L_lower;
        private Texture2D relax_arm_R_upper;
        private Texture2D relax_arm_R_lower;
        private Texture2D flex_body;
        private Texture2D flex_arm_L_upper;
        private Texture2D flex_arm_L_lower;
        private Texture2D flex_arm_R_upper;
        private Texture2D flex_arm_R_lower;

        private Texture2D card3;


        //Declare Sprite Vectors
        private Vector2 main_body_position = new Vector2(150,10);
        private Vector2 shoulder_L_body_pos = new Vector2(174,194);
        private Vector2 shoulder_L_upper_arm_pos = new Vector2(64,52);
        private Vector2 shoulder_R_body_pos = new Vector2(226, 191);
        private Vector2 shoulder_R_upper_arm_pos = new Vector2(28, 53);
        private Vector2 score_position = new Vector2(700, 10);
        private Vector2 elbow_L_upper_arm_pos = new Vector2(25, 36);
        private Vector2 elbow_L_lower_arm_pos = new Vector2(40,86);

        private Vector2 elbow_R_lower_arm_pos = new Vector2(17, 72);

        //private Vector2 elbow_adjustment = new Vector2();
        private Vector2 elbow_L_adjustment = new Vector2();
        private Vector2 elbow_R_adjustment = new Vector2();


        private SpriteFont scoreFont;


        public ArmStrong()
        {

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

            scoreFont = Content.Load<SpriteFont>("scoreFont");

            // RELAXED BODY
            relax_body = Content.Load<Texture2D>("Wrestler Paperdoll/relax_body");
            relax_arm_L_upper = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_L_upper");
            relax_arm_R_upper = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_R_upper");
            relax_arm_L_lower = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_L_lower");
            relax_arm_R_lower = Content.Load<Texture2D>("Wrestler Paperdoll/relax_arm_R_lower");

            //FLEX BODY
            flex_body = Content.Load<Texture2D>("Wrestler Paperdoll/flex_body");
            flex_arm_L_upper = Content.Load<Texture2D>("Wrestler Paperdoll/flex_arm_L_upper");
            flex_arm_R_upper = Content.Load<Texture2D>("Wrestler Paperdoll/flex_arm_R_upper");
            flex_arm_L_lower = Content.Load<Texture2D>("Wrestler Paperdoll/flex_arm_L_lower");
            flex_arm_R_lower = Content.Load<Texture2D>("Wrestler Paperdoll/flex_arm_R_lower");

            //CARDS
            card3 = Content.Load<Texture2D>("Cards/card3");
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
            relax_arm_L_lower.Dispose();
            relax_arm_R_lower.Dispose();
            flex_body.Dispose();
            flex_arm_L_upper.Dispose();
            flex_arm_R_upper.Dispose();
            flex_arm_L_lower.Dispose();
            flex_arm_R_lower.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            Find_L_Elbow();
            Find_R_Elbow();

            KeyboardState newState = Keyboard.GetState();  // get the newest state
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


            //////////////////////////////////////////////////////////////////////////////////mark added
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                elbow_L_rotation = elbow_L_rotation + rotation_speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                elbow_L_rotation = elbow_L_rotation - rotation_speed;

            }





            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                elbow_R_rotation = elbow_R_rotation + rotation_speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                elbow_R_rotation = elbow_R_rotation - rotation_speed;

            }
            /////////////////////////////////////////////////////////////////////////////////////end mark added



            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                shoulder_R_rotation = shoulder_R_rotation + rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                shoulder_R_rotation = shoulder_R_rotation - rotation_speed;
            }
            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
            {
                flex = true;


            }
            if (oldState.IsKeyUp(Keys.R) && newState.IsKeyDown(Keys.R))
            {
                flex = false;

                //////////////////////////////////////////////////////////////////////////////////mark added

                //this is just for the demo
                point_total = 0;
                calc_points = true;

                //////////////////////////////////////////////////////////////////////////////////end mark added


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

            //spriteBatch.DrawString(scoreFont,score.ToString(),score_position,Color.Yellow);
            spriteBatch.DrawString(scoreFont, shoulder_L_rotation.ToString(), score_position, Color.Red); //view angle as we rotate

            //get the cos to give a "distance from" the angle.  1.0 means you're there.  Use this for a points multiplier
            float angle_multiplier = (float)Math.Cos(shoulder_L_rotation);

            //no need for negative points.
            if ( angle_multiplier < 0 ) 
            {
                angle_multiplier = 0;
            }


            //////////////////////////////////////////////////////////////////////////////////mark added

            else if (angle_multiplier > .9)
            {
                angle_multiplier = angle_multiplier * 2;
            }

            //////////////////////////////////////////////////////////////////////////////////mark added



            //display the multiplier for testing
            spriteBatch.DrawString(scoreFont, angle_multiplier.ToString(), score_position + new Vector2(0, 40), Color.Red); 

            

            spriteBatch.Draw(card3, main_body_position, Color.White);

            if (flex == false)
            {
                spriteBatch.Draw(relax_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                //spriteBatch.Draw(relax_arm_R_lower, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(relax_body, main_body_position, Color.White);
                spriteBatch.Draw(relax_arm_L_upper, main_body_position+shoulder_L_body_pos,null,Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos,1, SpriteEffects.None,0);
                //spriteBatch.Draw(relax_arm_L_lower, main_body_position + shoulder_L_body_pos - (shoulder_L_upper_arm_pos - elbow_L_upper_arm_pos), null, Color.White, 0, elbow_L_lower_arm_pos + elbow_adjustment - (shoulder_L_upper_arm_pos - elbow_L_upper_arm_pos), 1, SpriteEffects.None, 0);


                //////////////////////////////////////////////////////////////////////////////////mark added

                spriteBatch.Draw(relax_arm_L_lower, main_body_position + shoulder_L_body_pos - elbow_L_adjustment, null, Color.White, shoulder_L_rotation + elbow_L_rotation, elbow_L_lower_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(relax_arm_R_lower, main_body_position + shoulder_R_body_pos + elbow_R_adjustment, null, Color.White, shoulder_R_rotation - elbow_R_rotation, elbow_R_lower_arm_pos, 1, SpriteEffects.None, 0);

                //////////////////////////////////////////////////////////////////////////////////end mark added


            }
            else
            {
                spriteBatch.Draw(flex_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                //spriteBatch.Draw(flex_arm_R_lower, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(flex_body, main_body_position, Color.White);
                spriteBatch.Draw(flex_arm_L_upper, main_body_position + shoulder_L_body_pos, null, Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos, 1, SpriteEffects.None, 0);


                //////////////////////////////////////////////////////////////////////////////////mark added

                spriteBatch.Draw(flex_arm_L_lower, main_body_position + shoulder_L_body_pos - elbow_L_adjustment, null, Color.White, shoulder_L_rotation + elbow_L_rotation, elbow_L_lower_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(flex_arm_R_lower, main_body_position + shoulder_R_body_pos - elbow_R_adjustment, null, Color.White, shoulder_R_rotation - elbow_R_rotation, elbow_R_lower_arm_pos, 1, SpriteEffects.None, 0);


                //calc points
                if (calc_points)
                {
                    point_total = point_total + (ulong)(100 * angle_multiplier);
                    calc_points = false;
                }

                //display the points
                spriteBatch.DrawString(scoreFont, point_total.ToString(), score_position + new Vector2(0, 60), Color.Red);

                //////////////////////////////////////////////////////////////////////////////////mark added


            }




            spriteBatch.End();

            base.Draw(gameTime);
        }


        /*  
         *
         */
        public void Find_L_Elbow()
        {
            elbow_L_adjustment = new Vector2((float)(42.15 * (Math.Cos(0.389 + shoulder_L_rotation))), (float)(42.15 * (Math.Sin(0.389 + shoulder_L_rotation))));
            
        }


        public void Find_R_Elbow()
        {

            elbow_R_adjustment = new Vector2((float)(45.79 * (Math.Cos(0.5517 + shoulder_R_rotation))), (float)(45.79 * (Math.Sin(0.5517 + shoulder_R_rotation))));

            elbow_R_adjustment = elbow_R_adjustment * new Vector2(1, -1);
        }



        /*Class for a posecard
         */
        protected class Pose_Card
        {
            //correct posses for full points from the card
            float angle_L_upper;
            float angle_R_upper;
            float angle_L_lower;
            float angle_R_lower;

            //sprite
            Texture2D card_art;



            /*4 floats, each an angle in radians that indicates the correct arm position
             *1 texture for the sprite image 
             */

            Pose_Card(float upperL, float upperR, float lowerL, float lowerR, Texture2D art)
            {
                


            }

        }

    }
}
