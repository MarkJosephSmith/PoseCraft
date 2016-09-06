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
        
        //Random Variables
        Random rnd_L = new Random();
        Random rnd_R = new Random();
        Random rnd_tired = new Random();

        float score_total = 0;
        float score_round = 0;
        float score_rate = 1f;

        bool flex = false;
        int level = 1;
        int card_state = 1;

        float tiredness;
        float tiredness_rate = 0.01f;

        // Rotation angles
        float shoulder_R_rotation = 0f;
        float shoulder_L_rotation = 0f;
        float Pi = 3.14159f;
        float rotation_speed = 0.1f;

        


        //Timer Variables
        private int timer_length = 30;
        private float remaining_time;

        //Declare Body Textures
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

        private Texture2D sweat1;
        private Texture2D sweat2;
        private Texture2D sweat3;

        //Declare card Textures
        private Texture2D card1;
        private Texture2D card2;
        private Texture2D card3;

        //Background
        private Texture2D background;
        private Texture2D ring;
        private Texture2D podium;

        //Declare Sprite Vectors
        private Vector2 main_body_position = new Vector2(6,10);
        private Vector2 shoulder_L_body_pos = new Vector2(174,194);
        private Vector2 shoulder_L_upper_arm_pos = new Vector2(64,52);
        private Vector2 shoulder_R_body_pos = new Vector2(226, 191);
        private Vector2 shoulder_R_upper_arm_pos = new Vector2(28, 53);
        private Vector2 score_position = new Vector2(650, 10);
        private Vector2 elbow_L_upper_arm_pos = new Vector2(25, 36);
        private Vector2 elbow_L_lower_arm_pos = new Vector2(40,86);
        private Vector2 elbow_R_upper_arm_pos = new Vector2(67, 29);
        private Vector2 elbow_R_lower_arm_pos = new Vector2(17, 72);

        private Vector2 elbow_adjustment_L = new Vector2();
        private Vector2 elbow_adjustment_R = new Vector2();

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
            shoulder_L_rotation = (float) rnd_L.NextDouble() * Pi;
            shoulder_R_rotation = (float) rnd_R.NextDouble() * Pi;
            remaining_time = timer_length;
            tiredness = 0.4f;
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
            card1 = Content.Load<Texture2D>("Cards/card1");
            card2 = Content.Load<Texture2D>("Cards/card2");
            card3 = Content.Load<Texture2D>("Cards/card3");

            //BACKGROUND
            background = Content.Load<Texture2D>("bg");
            ring = Content.Load<Texture2D>("ring");
            podium = Content.Load<Texture2D>("podium");

            sweat1 = Content.Load<Texture2D>("Sweat/sweat1");
            sweat2 = Content.Load<Texture2D>("Sweat/sweat2");
            sweat3 = Content.Load<Texture2D>("Sweat/sweat3");
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
            card3.Dispose();
            background.Dispose();
            podium.Dispose();
            ring.Dispose();
            sweat1.Dispose();
            sweat2.Dispose();
            sweat3.Dispose();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            Find_Elbows();

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
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                shoulder_R_rotation = shoulder_R_rotation + rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                shoulder_R_rotation = shoulder_R_rotation - rotation_speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                flex = true;


            }
            

            //get the cos to give a "distance from" the angle.  1.0 means you're there.  Use this for a points multiplier
            float angle_multiplier_R = (float)Math.Cos(shoulder_R_rotation);
            float angle_multiplier_L = (float)Math.Cos(shoulder_L_rotation);
            //no need for negative points.
            if (angle_multiplier_R < 0)
            {
                angle_multiplier_R = 0;
            }
            if (angle_multiplier_L < 0)
            {
                angle_multiplier_L = 0;
            }

            remaining_time -= (float) gameTime.ElapsedGameTime.TotalSeconds; //Timer

            //Timer

            if (flex == true)
            {
                tiredness = tiredness + 0.0001f;
                score_total = score_total + (score_rate * angle_multiplier_R) + (score_rate * angle_multiplier_L); //Score
                if(rnd_tired.NextDouble()>0.5)
                {
                    shoulder_L_rotation = shoulder_L_rotation - rotation_speed*tiredness;
                }
                else
                {
                    shoulder_L_rotation = shoulder_L_rotation + rotation_speed*tiredness;
                }
            }

            if(remaining_time < 0)
            {
                Reset_Card();
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

            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            

            //spriteBatch.DrawString(scoreFont,score.ToString(),score_position,Color.Yellow);
            spriteBatch.DrawString(scoreFont, "left: "+shoulder_L_rotation+ " right: " + shoulder_R_rotation, score_position+new Vector2(-60,0), Color.Red); //view angle as we rotate


            //display the points
            spriteBatch.DrawString(scoreFont, "Score: "+Convert.ToInt32(score_total).ToString(), score_position + new Vector2(0, 40), Color.Red);

            //display the Timer
            spriteBatch.DrawString(scoreFont, "Time: " +Convert.ToInt32(remaining_time).ToString(), score_position + new Vector2(0, 80), Color.Red);

            //display durability
            spriteBatch.DrawString(scoreFont, "Durability: " + Convert.ToInt32(tiredness*10000).ToString(), score_position + new Vector2(0, 120), Color.Red);

            if(card_state == 1)
            {
                spriteBatch.Draw(card1, main_body_position, Color.White);
            }
            else if(card_state == 2)
            {
                spriteBatch.Draw(card2, main_body_position, Color.White);
            }
            else if (card_state == 3)
            {
                spriteBatch.Draw(card3, main_body_position, Color.White);
            }

            if (flex == false)
            {
                spriteBatch.Draw(relax_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(relax_arm_R_lower, main_body_position + shoulder_R_body_pos - (shoulder_R_upper_arm_pos - elbow_R_upper_arm_pos), null, Color.White, 0, elbow_R_lower_arm_pos + elbow_adjustment_R - (shoulder_R_upper_arm_pos - elbow_R_upper_arm_pos), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(relax_body, main_body_position, Color.White);  
            }
            else
            {
                spriteBatch.Draw(flex_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(flex_body, main_body_position, Color.White);
            }

            if(tiredness>0.3 && tiredness < 0.6)
                   {
                       spriteBatch.Draw(sweat1, main_body_position, Color.White);
                   }
                   else if(tiredness > 0.6 && tiredness < 1)
                   {
                       spriteBatch.Draw(sweat2, main_body_position, Color.White);
                   }
                   else if(tiredness > 1)
                   {
                       spriteBatch.Draw(sweat3, main_body_position, Color.White);
                   }
                   else
                   {
            
                   }

            if (flex == false)
            {
                spriteBatch.Draw(relax_arm_L_upper, main_body_position + shoulder_L_body_pos, null, Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos, 1, SpriteEffects.None, 0);
                //spriteBatch.Draw(relax_arm_L_lower, main_body_position + shoulder_L_body_pos - (shoulder_L_upper_arm_pos - elbow_L_upper_arm_pos), null, Color.White, 0, elbow_L_lower_arm_pos + elbow_adjustment_L - (shoulder_L_upper_arm_pos - elbow_L_upper_arm_pos), 1, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(flex_arm_L_upper, main_body_position + shoulder_L_body_pos, null, Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos, 1, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(podium, new Vector2(20,0), Color.White);
            spriteBatch.Draw(ring, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        /*  
         *
         */
        public void Find_Elbows()
        {
            elbow_adjustment_L = new Vector2((float)(42.15 * (Math.Cos(0.389 + shoulder_L_rotation))), (float)(42.15 * (Math.Sin(0.389 + shoulder_L_rotation))));
            elbow_adjustment_R = new Vector2((float)(65.94 * (Math.Cos(5.57 + shoulder_R_rotation-Pi))), (float)(65.94 * (Math.Sin(5.57 + shoulder_R_rotation-Pi))));

        }


        public void Reset_Card()
        {
            level++;
            remaining_time = timer_length;
            shoulder_L_rotation = (float)rnd_L.NextDouble() * Pi;
            shoulder_R_rotation = (float)rnd_R.NextDouble() * Pi;
            flex = false;
           
            if (card_state == 3)
            {
                level = 1;
                card_state = 1;
            }
            else
            {
                card_state++;
            }

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
