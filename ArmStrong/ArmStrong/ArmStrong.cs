using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

//9/7/16

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

        //pose_card variables
        //the current pose card being displayed and scored.
        Pose_Card current_card = new Pose_Card(0, 0, 0, 0, null);
        Queue<Pose_Card> card_q = new Queue<Pose_Card>();
        


        //Random Variables
        Random rnd_L = new Random();
        Random rnd_R = new Random();
        Random rnd_tired_L_shoulder = new Random();
        Random rnd_tired_L_elbow = new Random();
        Random rnd_tired_R_shoulder = new Random();
        Random rnd_tired_R_elbow = new Random();

        float score_total = 0;
        float score_round = 0;
        float score_rate = 100f;

        bool flex = false;
        int level = 1;
        int game_state;
        int card_state = 3;
        int judge_state;
        int remaining_strikes;
        int strike_buffer;

        float tiredness;
        float tiredness_rate = 0.01f;

        float judgement_L;

        // Rotation angles
        float shoulder_R_rotation = 0f;
        float shoulder_L_rotation = 0f;
        float elbow_L_rotation = 0f;
        float elbow_R_rotation = 0f;
        float Pi = 3.14159f;
        float rotation_speed = 0.1f;

        


        //Timer Variables
        private int timer_length = 30;
        private float remaining_time;
        private float clock_rotation;

        //SOUND EFFECT
        private SoundEffect sound_failure;
        private SoundEffect theme;
        private SoundEffectInstance theme_instance;

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

        //Judge
        private Texture2D judge_bad;
        private Texture2D judge_good;
        private Texture2D judge_great;
        private Texture2D judge_failure;

        //Declare card Textures
        private Texture2D card1;
        private Texture2D card2;
        private Texture2D card3;
        private Texture2D card4;
        private Texture2D card5;
        private Texture2D card_clock;
        private Texture2D clock_hand;
        float card3_shoulder_L_perfect = 5.18f;
        float card3_elbow_L_perfect = 2.80f;

        //Background
        private Texture2D background;
        private Texture2D ring;
        private Texture2D podium;

        //SCREENS
        private Texture2D startscreen;
        private Texture2D gameover;
        private Texture2D pressspace;

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

        private Vector2 elbow_L_adjustment = new Vector2();
        private Vector2 elbow_R_adjustment = new Vector2();


        //font for score
        private SpriteFont ScoreFont;
        //font for text
        private SpriteFont textFont;
        //font for others
        private SpriteFont plainFont;



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
            tiredness = 0.0f;
            remaining_strikes = 3;
            strike_buffer = 15;
            game_state = 1;
            card_state = 1;
            base.Initialize();
            theme_instance.Play();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            plainFont = Content.Load<SpriteFont>("plainFont");

            textFont = Content.Load<SpriteFont>("Font/textFont");
            ScoreFont = Content.Load<SpriteFont>("Font/ScoreFont");


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
            card4 = Content.Load<Texture2D>("Cards/card4");
            card5 = Content.Load<Texture2D>("Cards/card5");

            clock_hand = Content.Load<Texture2D>("Cards/clock_hand");
            card_clock = Content.Load<Texture2D>("Cards/card_clock");
            //JUDGE
            judge_bad = Content.Load<Texture2D>("Judge/judge_bad");
            judge_good = Content.Load<Texture2D>("Judge/judge_good");
            judge_great = Content.Load<Texture2D>("Judge/judge_great");
            judge_failure = Content.Load<Texture2D>("Judge/judge_failure");

            //BACKGROUND
            background = Content.Load<Texture2D>("bg");
            ring = Content.Load<Texture2D>("ring");
            podium = Content.Load<Texture2D>("podium");

            sweat1 = Content.Load<Texture2D>("Sweat/sweat1");
            sweat2 = Content.Load<Texture2D>("Sweat/sweat2");
            sweat3 = Content.Load<Texture2D>("Sweat/sweat3");

            //SCREENS
            startscreen = Content.Load<Texture2D>("startscreen");
            gameover = Content.Load<Texture2D>("gameover");
            pressspace = Content.Load<Texture2D>("pressspace");

            //SOUND FX
            sound_failure = Content.Load<SoundEffect>("SoundFX/failure");
            theme = Content.Load<SoundEffect>("SoundFX/theme");
            theme_instance = theme.CreateInstance();

            //create the card queue
            card_q.Enqueue(new Pose_Card(0, 0, 0, 0, card1));  //Hulkamania
            card_q.Enqueue(new Pose_Card(6.438583f, -0.02f, 6.368582f, -1.349f, card2)); //Beast Mode
            card_q.Enqueue(new Pose_Card(5.120202f, -3.40f, 7.00f, 2.03f, card3)); //Super Justice
            card_q.Enqueue(new Pose_Card(4.592286f, -5.230025f, 3.089114f, 0.029999f, card4)); //The Champ
            card_q.Enqueue(new Pose_Card(5.079138f, -7.570078f, 1.119116f, -0.070000f, card5)); //Ultra Falcon

            current_card = card_q.Dequeue();
            card_q.Enqueue(current_card);

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
            card_q.Clear();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();  // get the newest state
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (game_state == 1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    game_state = 2;
                }
            }
            else if (game_state == 2)
            {
                theme_instance.Stop();

                Find_L_Elbow();
                Find_R_Elbow();




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

                remaining_time -= (float)gameTime.ElapsedGameTime.TotalSeconds; //Timer
                clock_rotation = (float)-(Math.PI * remaining_time) / 15 - (float)Math.PI / 2;
                //Timer

                if (flex == true)
                {
                    tiredness = tiredness + 0.0001f;
                    score_total = score_total + (score_rate * angle_multiplier_R) + (score_rate * angle_multiplier_L); //Score
                    //LEFT ARM RANDOM
                    if (rnd_tired_L_shoulder.NextDouble() > 0.5)
                    {
                        shoulder_L_rotation = shoulder_L_rotation - rotation_speed * tiredness;
                    }
                    else
                    {
                        shoulder_L_rotation = shoulder_L_rotation + rotation_speed * tiredness;
                    }

                    if (rnd_tired_L_elbow.NextDouble() > 0.5)
                    {
                        elbow_L_rotation = elbow_L_rotation - rotation_speed * tiredness;
                    }
                    else
                    {
                        elbow_L_rotation = elbow_L_rotation + rotation_speed * tiredness;
                    }

                    // RIGHT ARM RANDOM
                    if (rnd_tired_R_shoulder.NextDouble() > 0.5)
                    {
                        shoulder_R_rotation = shoulder_R_rotation - rotation_speed * tiredness;
                    }
                    else
                    {
                        shoulder_R_rotation = shoulder_R_rotation + rotation_speed * tiredness;
                    }

                    if (rnd_tired_L_elbow.NextDouble() > 0.5)
                    {
                        elbow_R_rotation = elbow_R_rotation - rotation_speed * tiredness;
                    }
                    else
                    {
                        elbow_R_rotation = elbow_R_rotation + rotation_speed * tiredness;
                    }

                    if (judge_state == 0 && strike_buffer == 0)
                    {
                        remaining_strikes--;
                        strike_buffer = 15;
                        sound_failure.Play();
                        Reset_Card();
                    }
                }

                Judgement();

                if (remaining_time < 0)
                {
                    Reset_Card();
                }
            }
            else if (game_state == 3)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    remaining_strikes = 3;
                    game_state= 1;
                }
            }
            

            if(strike_buffer !=0)
            {
                strike_buffer--;
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

            if (game_state == 1)
            {
                spriteBatch.Draw(startscreen, Vector2.Zero, Color.White);
                spriteBatch.Draw(pressspace, new Vector2(150 ,400), Color.White);
            }
            else if (game_state == 2)
            {

                spriteBatch.Draw(background, Vector2.Zero, Color.White);

                if (judge_state == 3)
                {
                    spriteBatch.Draw(judge_great, Vector2.Zero, Color.White);
                }
                else if (judge_state == 2)
                {
                    spriteBatch.Draw(judge_good, Vector2.Zero, Color.White);
                }
                else if (judge_state == 1)
                {
                    spriteBatch.Draw(judge_bad, Vector2.Zero, Color.White);
                }
                else
                {
                    spriteBatch.Draw(judge_failure, Vector2.Zero, Color.White);
                }


                spriteBatch.DrawString(plainFont, "Lshoulder: " + shoulder_L_rotation + " Lelbow: " + elbow_L_rotation, score_position + new Vector2(-150, 0), Color.Red); //view angle as we rotate
                spriteBatch.DrawString(plainFont, "Rshoulder: " + shoulder_R_rotation + " Relbow: " + elbow_R_rotation, score_position + new Vector2(-150, 20), Color.Red); //view angle as we rotate
                spriteBatch.DrawString(plainFont, judge_state.ToString(), score_position + new Vector2(-150, 40), Color.Red); //view angle as we rotate



                //display the Timer
                spriteBatch.DrawString(plainFont, "Strikes: " + Convert.ToInt32(remaining_strikes).ToString(), score_position + new Vector2(0, 80), Color.Red);

                //display durability
                spriteBatch.DrawString(plainFont, "Durability: " + Convert.ToInt32(tiredness * 10000).ToString(), score_position + new Vector2(0, 120), Color.Red);

                if (card_state == 1)
                {
                    spriteBatch.Draw(card1, main_body_position, Color.White);
                }
                else if (card_state == 2)
                {
                    spriteBatch.Draw(card2, main_body_position, Color.White);
                }
                else if (card_state == 3)
                {
                    spriteBatch.Draw(card3, main_body_position, Color.White);
                }
                else if (card_state == 4)
                {
                    spriteBatch.Draw(card4, main_body_position, Color.White);
                }
                else if (card_state == 5)
                {
                    spriteBatch.Draw(card5, main_body_position, Color.White);
                }
                else
                {

                }

                if (flex == false)
                {
                    spriteBatch.Draw(relax_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(relax_arm_R_lower, main_body_position + shoulder_R_body_pos - (elbow_R_adjustment * new Vector2(-1, 1)), null, Color.White, shoulder_R_rotation + elbow_R_rotation, elbow_R_lower_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(relax_body, main_body_position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(flex_arm_R_upper, main_body_position + shoulder_R_body_pos, null, Color.White, shoulder_R_rotation, shoulder_R_upper_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(flex_arm_R_lower, main_body_position + shoulder_R_body_pos - (elbow_R_adjustment * new Vector2(-1, 1)), null, Color.White, shoulder_R_rotation + elbow_R_rotation, elbow_R_lower_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(flex_body, main_body_position, Color.White);
                }

                if (tiredness > 0.2 && tiredness < 0.4)
                {
                    spriteBatch.Draw(sweat1, main_body_position, Color.White);
                }
                else if (tiredness > 0.4 && tiredness < 0.6)
                {
                    spriteBatch.Draw(sweat2, main_body_position, Color.White);
                }
                else if (tiredness > 0.6)
                {
                    spriteBatch.Draw(sweat3, main_body_position, Color.White);
                }
                else
                {

                }

                if (flex == false)
                {
                    spriteBatch.Draw(relax_arm_L_upper, main_body_position + shoulder_L_body_pos, null, Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(relax_arm_L_lower, main_body_position + shoulder_L_body_pos - elbow_L_adjustment, null, Color.White, shoulder_L_rotation + elbow_L_rotation, elbow_L_lower_arm_pos, 1, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(flex_arm_L_upper, main_body_position + shoulder_L_body_pos, null, Color.White, shoulder_L_rotation, shoulder_L_upper_arm_pos, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(flex_arm_L_lower, main_body_position + shoulder_L_body_pos - elbow_L_adjustment, null, Color.White, shoulder_L_rotation + elbow_L_rotation, elbow_L_lower_arm_pos, 1, SpriteEffects.None, 0);
                }

                spriteBatch.Draw(podium, new Vector2(20, 0), Color.White);
                spriteBatch.Draw(ring, Vector2.Zero, Color.White);

                //display string "MACHO POINTS"
                spriteBatch.DrawString(textFont, "MACHO POINTS", score_position + new Vector2(-100, 295), Color.White);

                //display the points
                spriteBatch.DrawString(ScoreFont, Convert.ToInt32(score_total).ToString(), score_position + new Vector2(-130, 340), Color.White);

                spriteBatch.Draw(card_clock, main_body_position, Color.White);
                spriteBatch.Draw(clock_hand, main_body_position + new Vector2(363, 62), null, Color.White, clock_rotation, new Vector2(5, 5), 1, SpriteEffects.None, 0);

            }
            else if (game_state == 3)
            {
                spriteBatch.Draw(gameover, Vector2.Zero, Color.White);
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
            elbow_R_adjustment = new Vector2((float)(45.79 * (Math.Cos(0.5517 - shoulder_R_rotation))), (float)(45.79 * (Math.Sin(0.5517 - shoulder_R_rotation))));
        }



        public void Reset_Card()
        {
            level++;
            remaining_time = timer_length;
            shoulder_L_rotation = (float)rnd_L.NextDouble() * Pi;
            shoulder_R_rotation = (float)rnd_R.NextDouble() * Pi;
            flex = false;

            //these next 2 lines probably make a ton of other crap unneeded
            current_card = card_q.Dequeue();
            card_q.Enqueue(current_card);


            if (card_state == 5)
            {
                //current_card = card_q.Dequeue();
                //card_q.Enqueue(current_card);

                level = 1;
                card_state = 1;


            }
            else
            {
                card_state++;

                //these next 2 lines probably make a ton of other crap unneeded
                //current_card = card_q.Dequeue();
                //card_q.Enqueue(current_card);

            }

            if(remaining_strikes == 0)
            {
                game_state = 3;
            }
            

        }

        public void Judgement()
        {
            //the state of the judge
            int calc_state = 0;
            
            //get the score, cast it to an int so it cuts off any remainders
            calc_state = (int)current_card.Calc_Score(shoulder_L_rotation, elbow_L_rotation, shoulder_R_rotation, elbow_R_rotation);
            
            //set the judge state
            judge_state = calc_state;
            
            /*  old judge system
            if (Math.Cos(shoulder_L_rotation) < 0 && Math.Cos(elbow_L_rotation) < 0)
            {
                judgement_L = 0;
            }
            else if (Math.Cos(shoulder_L_rotation) < 0 && Math.Cos(elbow_L_rotation) > 0)
            {
                judgement_L = (float) Math.Cos(elbow_L_rotation);
            }
            else if (Math.Cos(shoulder_L_rotation) > 0 && Math.Cos(elbow_L_rotation) < 0)
            {
                judgement_L = (float) Math.Cos(shoulder_L_rotation);
            }
            else if (Math.Cos(shoulder_L_rotation) > 0 && Math.Cos(elbow_L_rotation) > 0)
            {
                judgement_L = (float) Math.Cos(shoulder_L_rotation) + (float) Math.Cos(elbow_L_rotation);
            }
            else
            {

            }

            if (judgement_L > 1.5)
            {
                judge_state = 3;
            }
            else if (judgement_L < 1.5 && judgement_L > 0.5)
            {
                judge_state = 2;
            }
            else if (judgement_L < 0.5 && judgement_L > 0) 
            {
                judge_state = 1;
            }
            else if(judgement_L == 0)
            {
                judge_state = 0;
            }
            else
            {

            }
            */

        }


        /*Class for a posecard
         */
        protected class Pose_Card
        {
            //correct posses for full points from the card
            float LShoulder;
            float LElbow;
            float RShoulder;
            float RElbow;

            //sprite
            Texture2D card_art;



            /*4 floats, each an angle in radians that indicates the correct arm position
             *1 texture for the sprite image ///not yet in 
             */

            public Pose_Card(float ls, float le, float rs, float re, Texture2D art)
            {

                //get the angles in radians as a float
                LShoulder = (float)Math.Cos(ls);
                LElbow = (float)Math.Cos(le);
                RShoulder = (float)Math.Cos(rs);
                RElbow = (float)Math.Cos(re);

                card_art = art;

            }

            /// <summary>
            /// calculate the score using the 4 input angles and the card's default settings.
            /// </summary>
            /// <returns>Returns a value between 0 and 3 to corespond with the judge state.</returns>
            public float Calc_Score(float ls, float le, float rs, float re)
            {
                float result = 0; //return this at the end

                //mod off all 4 input angles
                ls = (float)Math.Cos(ls);
                le = (float)Math.Cos(le);
                rs = (float)Math.Cos(rs);
                re = (float)Math.Cos(re);

                //these 4 values determine how close each angle is
                float lsValue = ls / LShoulder;
                float leValue = le / LElbow;
                float rsValue = rs / RShoulder;
                float reValue = re / RElbow;

                //round off any negative values to 0
                if (lsValue < 0)
                {
                    lsValue = 0;
                }
                if (rsValue < 0)
                {
                    rsValue = 0;
                }
                if (leValue < 0)
                {
                    leValue = 0;
                }
                if (reValue < 0)
                {
                    reValue = 0;
                }

                //at this point each one has a value from 0 to 1 and we have 4 total angles.
                result = lsValue*(.75f) + leValue*(.75f) + rsValue*(.75f) + reValue*(.75f);

                //this is pretty much how it should score but we are going to convert to an int
                //that's going to make a full 3 nearly impossible so give a little padding on it.
                if (result > 2.95f)
                {
                    result = 3.0f;
                }

                return result;
            }

        }

    }
}
