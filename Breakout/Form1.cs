using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPHwk01___Breakout
{
    public partial class Form1 : Form
    {
        int goes = 0, score = 0, totalScore = 0;
        private int x, y, xChange, yChange, batXCo;
        private Graphics paper;
        private Pen ball;
        private Random randNum;
        private SolidBrush bat, brick, topBat;
        string ballMove;
        const int brickLength = 60;
        const int brickHeight = 20;
        int[,] brickLocation = { { 0, 42 }, { 61, 42 }, { 122, 42 }, { 183, 42 }, { 244, 42 }, { 305, 42 }, { 366, 42 }, { 427, 42 }, { 488, 42 }, { 549, 42 }, { 610, 42 }, { 671, 42 }, { 732, 42 }, { 793, 42 }, { 854, 42 }, { 0, 63 }, { 61, 63 }, { 122, 63 }, { 183, 63 }, { 244, 63 }, { 305, 63 }, { 366, 63 }, { 427, 63 }, { 488, 63 }, { 549, 63 }, { 610, 63 }, { 671, 63 }, { 732, 63 }, { 793, 63 }, { 854, 63 }, { 0, 84 }, { 61, 84 }, { 122, 84 }, { 183, 84 }, { 244, 84 }, { 305, 84 }, { 366, 84 }, { 427, 84 }, { 488, 84 }, { 549, 84 }, { 610, 84 }, { 671, 84 }, { 732, 84 }, { 793, 84 }, { 854, 84 }, { 0, 105 }, { 61, 105 }, { 122, 105 }, { 183, 105 }, { 244, 105 }, { 305, 105 }, { 366, 105 }, { 427, 105 }, { 488, 105 }, { 549, 105 }, { 610, 105 }, { 671, 105 }, { 732, 105 }, { 793, 105 }, { 854, 105 }, { 0, 126 }, { 61, 126 }, { 122, 126 }, { 183, 126 }, { 244, 126 }, { 305, 126 }, { 366, 126 }, { 427, 126 }, { 488, 126 }, { 549, 126 }, { 610, 126 }, { 671, 126 }, { 732, 126 }, { 793, 126 }, { 854, 126 } };
        bool[] brickLive = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        public Form1()
        {
            InitializeComponent();
            paper = picGame.CreateGraphics();
            ball = new Pen(Color.Fuchsia);
            ball.Width = 10;
            bat = new SolidBrush(Color.Firebrick);
            topBat = new SolidBrush(Color.Firebrick);
            randNum = new Random();
            picGame.MouseMove += new
                System.Windows.Forms.MouseEventHandler(picGame_MouseMove);
            brick = new SolidBrush(Color.White);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            btnDisplayBricks.Enabled = false;
            btnDown.Enabled = false;
            btnLeft.Enabled = false;
            btnRandom.Enabled = false;
            btnRight.Enabled = false;
            btnUp.Enabled = false;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            goes += 5;
            lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
            timerBall.Interval = 50;
            timerBricks.Interval = 50;
            timerBat.Interval = 50;
            timerBat.Enabled = true;
            btnDisplayBricks.Enabled = true;
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRandom.Enabled = true;
            btnRight.Enabled = true;
            btnUp.Enabled = true;
        }
        private void btnDisplayBricks_Click(object sender, EventArgs e)
        {
            timerBricks.Interval = 50;
            timerBricks.Enabled = true;
            for (int count=0; count< brickLive.Length; count++){
                brickLive[count] = true;
            }
            drawBricks();
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            ballMove = "up";
            timerBall.Enabled = true;
            moveBall();
            drawBall();
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRandom.Enabled = true;
            btnRight.Enabled = true;
            btnUp.Enabled = false;
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            ballMove = "down";
            timerBall.Enabled = true;
            moveBall();
            drawBall();
            btnDown.Enabled = false;
            btnLeft.Enabled = true;
            btnRandom.Enabled = true;
            btnRight.Enabled = true;
            btnUp.Enabled = true;
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            ballMove = "left";
            timerBall.Enabled = true;
            moveBall();
            drawBall();
            btnDown.Enabled = true;
            btnLeft.Enabled = false;
            btnRandom.Enabled = true;
            btnRight.Enabled = true;
            btnUp.Enabled = true;
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            ballMove = "right";
            timerBall.Enabled = true;
            moveBall();
            drawBall();
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRandom.Enabled = true;
            btnRight.Enabled = false;
            btnUp.Enabled = true;
        }  
        private void btnRandom_Click(object sender, EventArgs e)
        {
            goes -= 1;
            lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
            ballMove = "random";
            x = randNum.Next(1, 914);
            y = (236);//randNum.Next(1, picGame.Width);
            xChange = trkBallSpeed.Value;//randNum.Next(1, 10);
            yChange = trkBallSpeed.Value;//randNum.Next(1, 10);
            timerBall.Enabled = true;
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRandom.Enabled = false;
            btnRight.Enabled = true;
            btnUp.Enabled = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            timerBall.Enabled = false;
            lblScore.Visible = false;
            MessageBox.Show("Game Over! \nYour total score is: " + (totalScore + score));
            Environment.Exit(0);
        }
        private void moveBall()
        {
            if (ballMove == "random")
            {
                x = x + xChange;
                y = y + yChange;
                if (x >= picGame.Width)
                    xChange = -xChange;
                if (x <= 0)
                    xChange = -xChange;
            }
            if (ballMove == "up")
            {
                y = y - yChange;
                if (y >= picGame.Height) //bottom
                    yChange = -yChange;
                if (y <= 0)
                    yChange = -yChange;
            }
            if (ballMove == "down")
            {
                y = y + yChange;
                if (y >= picGame.Height) //bottom
                    yChange = -yChange;
                if (y <= 0)
                    yChange = -yChange;
            }
            if (ballMove == "left")
            {
                x = x - xChange;
                if (x >= picGame.Width)
                    xChange = -xChange;
                if (x <= 0)
                    xChange = -xChange;
            }
            if (ballMove == "right")
            {
                x = x + xChange;
                if (x >= picGame.Width)
                    xChange = -xChange;
                if (x <= 0)
                    xChange = -xChange;
            }
        }
        private void drawBall()
        {
            paper.Clear(Color.FromArgb(80,80,80));
            paper.DrawEllipse(ball, x, y, 10, 10);
            
        }
        private void drawBricks()
        {
            for (int count = 0; count < brickLive.Length; count++)
            {
                if (brickLive[count])
                {
                    if ((count == 0) || (count == 5) || (count == 10) || (count == 15) || (count == 20) || (count == 25) || (count == 30) || (count == 35) || (count == 40) || (count == 45) || (count == 50) || (count == 55) || (count == 60) || (count == 65) || (count == 70))
                        brick.Color = Color.Blue;
                    if ((count == 1) || (count == 6) || (count == 11) || (count == 16) || (count == 21) || (count == 26) || (count == 31) || (count == 36) || (count == 41) || (count == 46) || (count == 51) || (count == 56) || (count == 61) || (count == 66) || (count == 71))
                        brick.Color = Color.Green;
                    if ((count == 2) || (count == 7) || (count == 12) || (count == 17) || (count == 22) || (count == 27) || (count == 32) || (count == 37) || (count == 42) || (count == 47) || (count == 52) || (count == 57) || (count == 62) || (count == 67) || (count == 72))
                        brick.Color = Color.Red;
                    if ((count == 3) || (count == 8) || (count == 13) || (count == 18) || (count == 23) || (count == 28) || (count == 33) || (count == 38) || (count == 43) || (count == 48) || (count == 53) || (count == 58) || (count == 63) || (count == 68) || (count == 73))
                        brick.Color = Color.Black;
                    if ((count == 4) || (count == 9) || (count == 14) || (count == 19) || (count == 24) || (count == 29) || (count == 34) || (count == 39) || (count == 44) || (count == 49) || (count == 54) || (count == 59) || (count == 64) || (count == 69) || (count == 74))
                        brick.Color = Color.Yellow;
                    paper.FillRectangle(brick, brickLocation[count, 0], brickLocation[count, 1], brickLength, brickHeight);
                }
            }
        }
        private void drawBat()
        {
            paper.FillRectangle(bat, batXCo, picGame.Height - 20, 50, 10);
            paper.FillRectangle(topBat, batXCo, 10, 50, 10);
        }
        private void checkCollision()
        {
            if ((x >= 0) && (x <= 60) && (y <= 62) && (y >= 42) && (brickLive[0]))
            {
                brickLive[0] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 61) && (x <= 121) && (y <= 62) && (y >= 42) && (brickLive[1]))
            {
                brickLive[1] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 122) && (x <= 182) && (y <= 62) && (y >= 42) && (brickLive[2]))
            {
                brickLive[2] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 183) && (x <= 243) && (y <= 62) && (y >= 42) && (brickLive[3]))
            {
                brickLive[3] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 244) && (x <= 304) && (y <= 62) && (y >= 42) && (brickLive[4]))
            {
                brickLive[4] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 305) && (x <= 365) && (y <= 62) && (y >= 42) && (brickLive[5]))
            {
                brickLive[5] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 366) && (x <= 426) && (y <= 62) && (y >= 42) && (brickLive[6]))
            {
                brickLive[6] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 427) && (x <= 487) && (y <= 62) && (y >= 42) && (brickLive[7]))
            {
                brickLive[7] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 488) && (x <= 548) && (y <= 62) && (y >= 42) && (brickLive[8]))
            {
                brickLive[8] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 549) && (x <= 609) && (y <= 62) && (y >= 42) && (brickLive[9]))
            {
                brickLive[9] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 610) && (x <= 670) && (y <= 62) && (y >= 42) && (brickLive[10]))
            {
                brickLive[10] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 671) && (x <= 731) && (y <= 62) && (y >= 42) && (brickLive[11]))
            {
                brickLive[11] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 732) && (x <= 792) && (y <= 62) && (y >= 42) && (brickLive[12]))
            {
                brickLive[12] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 793) && (x <= 853) && (y <= 62) && (y >= 42) && (brickLive[13]))
            {
                brickLive[13] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 854) && (x <= 914) && (y <= 62) && (y >= 42) && (brickLive[14]))
            {
                brickLive[14] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 0) && (x <= 60) && (y <= 83) && (y >= 63) && (brickLive[15]))
            {
                brickLive[15] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 61) && (x <= 121) && (y <= 83) && (y >= 63) && (brickLive[16]))
            {
                brickLive[16] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 122) && (x <= 182) && (y <= 83) && (y >= 63) && (brickLive[17]))
            {
                brickLive[17] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 183) && (x <= 243) && (y <= 83) && (y >= 63) && (brickLive[18]))
            {
                brickLive[18] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 244) && (x <= 304) && (y <= 83) && (y >= 63) && (brickLive[19]))
            {
                brickLive[19] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 305) && (x <= 365) && (y <= 83) && (y >= 63) && (brickLive[20]))
            {
                brickLive[20] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 366) && (x <= 426) && (y <= 83) && (y >= 63) && (brickLive[21]))
            {
                brickLive[21] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 427) && (x <= 487) && (y <= 83) && (y >= 63) && (brickLive[22]))
            {
                brickLive[22] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 488) && (x <= 548) && (y <= 83) && (y >= 63) && (brickLive[23]))
            {
                brickLive[23] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 549) && (x <= 609) && (y <= 83) && (y >= 63) && (brickLive[24]))
            {
                brickLive[24] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 610) && (x <= 670) && (y <= 83) && (y >= 63) && (brickLive[25]))
            {
                brickLive[25] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 671) && (x <= 731) && (y <= 83) && (y >= 63) && (brickLive[26]))
            {
                brickLive[26] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 732) && (x <= 792) && (y <= 83) && (y >= 63) && (brickLive[27]))
            {
                brickLive[27] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 793) && (x <= 853) && (y <= 83) && (y >= 63) && (brickLive[28]))
            {
                brickLive[28] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 854) && (x <= 914) && (y <= 83) && (y >= 63) && (brickLive[29]))
            {
                brickLive[29] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 0) && (x <= 60) && (y <= 104) && (y >= 84) && (brickLive[30]))
            {
                brickLive[30] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 61) && (x <= 121) && (y <= 104) && (y >= 84) && (brickLive[31]))
            {
                brickLive[31] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 122) && (x <= 182) && (y <= 104) && (y >= 84) && (brickLive[32]))
            {
                brickLive[32] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 183) && (x <= 243) && (y <= 104) && (y >= 84) && (brickLive[33]))
            {
                brickLive[33] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 244) && (x <= 304) && (y <= 104) && (y >= 84) && (brickLive[34]))
            {
                brickLive[34] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 305) && (x <= 365) && (y <= 104) && (y >= 84) && (brickLive[35]))
            {
                brickLive[35] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 366) && (x <= 426) && (y <= 104) && (y >= 84) && (brickLive[36]))
            {
                brickLive[36] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 427) && (x <= 487) && (y <= 104) && (y >= 84) && (brickLive[37]))
            {
                brickLive[37] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 488) && (x <= 548) && (y <= 104) && (y >= 84) && (brickLive[38]))
            {
                brickLive[38] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 549) && (x <= 609) && (y <= 104) && (y >= 84) && (brickLive[39]))
            {
                brickLive[39] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 610) && (x <= 670) && (y <= 104) && (y >= 84) && (brickLive[40]))
            {
                brickLive[40] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 671) && (x <= 731) && (y <= 104) && (y >= 84) && (brickLive[41]))
            {
                brickLive[41] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 732) && (x <= 792) && (y <= 104) && (y >= 84) && (brickLive[42]))
            {
                brickLive[42] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 793) && (x <= 853) && (y <= 104) && (y >= 84) && (brickLive[43]))
            {
                brickLive[43] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 854) && (x <= 914) && (y <= 104) && (y >= 84) && (brickLive[44]))
            {
                brickLive[44] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 0) && (x <= 60) && (y <= 125) && (y >= 105) && (brickLive[45]))
            {
                brickLive[45] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 61) && (x <= 121) && (y <= 125) && (y >= 105) && (brickLive[46]))
            {
                brickLive[46] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 122) && (x <= 182) && (y <= 125) && (y >= 105) && (brickLive[47]))
            {
                brickLive[47] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 183) && (x <= 243) && (y <= 125) && (y >= 105) && (brickLive[48]))
            {
                brickLive[48] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 244) && (x <= 304) && (y <= 125) && (y >= 105) && (brickLive[49]))
            {
                brickLive[49] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 305) && (x <= 365) && (y <= 125) && (y >= 105) && (brickLive[50]))
            {
                brickLive[50] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 366) && (x <= 426) && (y <= 125) && (y >= 105) && (brickLive[51]))
            {
                brickLive[51] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 427) && (x <= 487) && (y <= 125) && (y >= 105) && (brickLive[52]))
            {
                brickLive[52] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 488) && (x <= 548) && (y <= 125) && (y >= 105) && (brickLive[53]))
            {
                brickLive[53] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 549) && (x <= 609) && (y <= 125) && (y >= 105) && (brickLive[54]))
            {
                brickLive[54] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 610) && (x <= 670) && (y <= 125) && (y >= 105) && (brickLive[55]))
            {
                brickLive[55] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 671) && (x <= 731) && (y <= 125) && (y >= 105) && (brickLive[56]))
            {
                brickLive[56] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 732) && (x <= 792) && (y <= 125) && (y >= 105) && (brickLive[57]))
            {
                brickLive[57] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 793) && (x <= 853) && (y <= 125) && (y >= 105) && (brickLive[58]))
            {
                brickLive[58] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 854) && (x <= 914) && (y <= 125) && (y >= 105) && (brickLive[59]))
            {
                brickLive[59] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 0) && (x <= 60) && (y <= 146) && (y >= 126) && (brickLive[60]))
            {
                brickLive[60] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 61) && (x <= 121) && (y <= 146) && (y >= 126) && (brickLive[61]))
            {
                brickLive[61] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 122) && (x <= 182) && (y <= 146) && (y >= 126) && (brickLive[62]))
            {
                brickLive[62] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 183) && (x <= 243) && (y <= 146) && (y >= 126) && (brickLive[63]))
            {
                brickLive[63] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 244) && (x <= 304) && (y <= 146) && (y >= 126) && (brickLive[64]))
            {
                brickLive[64] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 305) && (x <= 365) && (y <= 146) && (y >= 126) && (brickLive[65]))
            {
                brickLive[65] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 366) && (x <= 426) && (y <= 146) && (y >= 126) && (brickLive[66]))
            {
                brickLive[66] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 427) && (x <= 487) && (y <= 146) && (y >= 126) && (brickLive[67]))
            {
                brickLive[67] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 488) && (x <= 548) && (y <= 146) && (y >= 126) && (brickLive[68]))
            {
                brickLive[68] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 549) && (x <= 609) && (y <= 146) && (y >= 126) && (brickLive[69]))
            {
                brickLive[69] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 610) && (x <= 670) && (y <= 146) && (y >= 126) && (brickLive[70]))
            {
                brickLive[70] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 671) && (x <= 731) && (y <= 146) && (y >= 126) && (brickLive[71]))
            {
                brickLive[71] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 732) && (x <= 792) && (y <= 146) && (y >= 126) && (brickLive[72]))
            {
                brickLive[72] = false;
                score += 20;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 793) && (x <= 853) && (y <= 146) && (y >= 126) && (brickLive[73]))
            {
                brickLive[73] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= 854) && (x <= 914) && (y <= 146) && (y >= 126) && (brickLive[74]))
            {
                brickLive[74] = false;
                score += 10;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
                yChange = -yChange;
                xChange = -xChange;
            }
            if ((x >= batXCo) && (x <= batXCo + 50) && (y >= picGame.Height - 30))
                yChange = -yChange;
            if ((x >= batXCo) && (x <= batXCo + 50) && (y <= 10))
                yChange = -yChange;
        }
        private void timerBall_Tick(object sender, EventArgs e)
        {
          checkCollision();
          moveBall();
          drawBall();
          if (goes !=-1 && (y > picGame.Height || y < 0))
          {
              goes -= 1;
              timerBall.Enabled = false;
              lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
              btnRandom.Enabled = true;
          }
          if (goes == -1)
          {
              timerBall.Enabled = false;
              timerBricks.Enabled = false;
              lblScore.Visible = false;
              MessageBox.Show("Game Over! \nYour total score is: " + (totalScore + score));
              Environment.Exit(0);
          }
        }
        private void timerBricks_Tick(object sender, EventArgs e)
        {
            drawBricks();
            if (score >= 50)
            {
                goes += 1;
                score -= 50;
                totalScore += 50;
                lblScore.Text = "Remaining: " + goes + "\n\nScore: " + score;
            }
            if (brickLive[0] == false && brickLive[1] == false && brickLive[2] == false && brickLive[3] == false && brickLive[4] == false)
            {
                timerBall.Enabled = false;
                timerBricks.Enabled = false;
                MessageBox.Show("Game Over! \nYour total score is: " + (totalScore + score));
                Environment.Exit(0);
            }
        }
        private void timerBat_Tick(object sender, EventArgs e)
        {
            drawBat();
        }
        private void picGame_MouseMove(object sender,
                                     System.Windows.Forms.MouseEventArgs e)
        {
            batXCo = e.X;
        }
    }
}