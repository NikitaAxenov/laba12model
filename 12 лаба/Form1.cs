using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12_лаба
{
    public partial class Form1 : Form
    {
        const double b = 4294967299;
        const double m = 9223372036854775808;
        double xNext = b;
        double xBefore, xNow;
        bool temp = true;
        class TeamInf
        {
            public TeamInf(string team, double k, int num)
            {
                this.team = team;
                this.k = k;
                this.num = num;
            }
            public string team;
            public double k;
            public double bd = 0;
            public int[] plays = new int[7];
            public int num;
            public int play = 0;
            public int goals = 0;
            public int win = 0;
            public int draw = 0;
            public int loss = 0;
            public int ingoal = 0;
            public int outgoal = 0;
            public int inoutgoal = 0;
            public int point = 0;
        }
        private TeamInf[] teams = new TeamInf[8];
        int tours = 0;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                xBefore = xNext;
                xNext = (b * xBefore) % m;
                xNow = xNext / m;
            }
            teams[0] = new TeamInf("Команда 1", 2, 1);
            teams[1] = new TeamInf("Команда 2", 1.6, 2);
            teams[2] = new TeamInf("Команда 3", 1.7, 3);
            teams[3] = new TeamInf("Команда 4", 1.5, 4);
            teams[4] = new TeamInf("Команда 5", 1, 5);
            teams[5] = new TeamInf("Команда 6", 1.3, 6);
            teams[6] = new TeamInf("Команда 7", 2.1, 7);
            teams[7] = new TeamInf("Команда 8", 1.9, 8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tours < 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    int M = 0;
                    double S = 0;
                    xBefore = xNext;
                    xNext = (b * xBefore) % m;
                    xNow = xNext / m;
                    teams[i].bd = xNow;
                    S += Math.Log(teams[i].bd);
                    while (S >= -teams[i].k)
                    {
                        M++;
                        xBefore = xNext;
                        xNext = (b * xBefore) % m;
                        xNow = xNext / m;
                        teams[i].bd = xNow;
                        S += Math.Log(teams[i].bd);
                    }
                    teams[i].goals = M;
                };
                for (int i = 0; i < 8; i++)
                {
                    for (int f = 0; f < 8; f++)
                    {
                        if (teams[i].play == tours && teams[f].play == tours && teams[i].num != teams[f].num)
                        {
                            for (int g = 0; g < teams[i].play; g++)
                            {
                                if (teams[i].plays[g] == teams[f].num)
                                    temp = false;
                            }
                            if (temp == true)
                            {
                                teams[i].plays[teams[i].play] = teams[f].num;
                                teams[i].play++;
                                teams[f].plays[teams[f].play] = teams[i].num;
                                teams[f].play++;
                                if (teams[i].goals > teams[f].goals)
                                {
                                    teams[i].win++;
                                    teams[i].point += 3;
                                    teams[f].loss++;
                                    teams[f].point += 0;
                                }
                                else if (teams[i].goals == teams[f].goals)
                                {
                                    teams[i].draw++;
                                    teams[i].point += 1;
                                    teams[f].draw++;
                                    teams[f].point += 1;
                                }
                                else
                                {
                                    teams[f].win++;
                                    teams[f].point += 3;
                                    teams[i].loss++;
                                    teams[i].point += 0;
                                }
                                teams[i].ingoal += teams[i].goals;
                                teams[i].outgoal += teams[f].goals;
                                teams[i].inoutgoal = teams[i].ingoal - teams[i].outgoal;
                                teams[f].ingoal += teams[f].goals;
                                teams[f].outgoal += teams[f].goals;
                                teams[f].inoutgoal = teams[f].ingoal - teams[f].outgoal;
                                
                            }
                            else
                                temp = true;
                        }
                    }
                }
                TeamInf n;
                TeamInf[] array = new TeamInf[8];
                teams.CopyTo(array, 0);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = i + 1; j < 8; j++)
                    {
                        if ((array[i].point < array[j].point) || (array[i].point == array[j].point && array[i].inoutgoal < array[j].inoutgoal))
                        {
                            n = array[i];
                            array[i] = array[j];
                            array[j] = n;
                        }
                    }
                }
                team1.Text = "Команда " + array[0].num;
                team2.Text = "Команда " + array[1].num;
                team3.Text = "Команда " + array[2].num;
                team4.Text = "Команда " + array[3].num;
                team5.Text = "Команда " + array[4].num;
                team6.Text = "Команда " + array[5].num;
                team7.Text = "Команда " + array[6].num;
                team8.Text = "Команда " + array[7].num;

                win1.Text = array[0].win.ToString();
                win2.Text = array[1].win.ToString();
                win3.Text = array[2].win.ToString();
                win4.Text = array[3].win.ToString();
                win5.Text = array[4].win.ToString();
                win6.Text = array[5].win.ToString();
                win7.Text = array[6].win.ToString();
                win8.Text = array[7].win.ToString();

                play1.Text = array[0].play.ToString();
                play2.Text = array[1].play.ToString();
                play3.Text = array[2].play.ToString();
                play4.Text = array[3].play.ToString();
                play5.Text = array[4].play.ToString();
                play6.Text = array[5].play.ToString();
                play7.Text = array[6].play.ToString();
                play8.Text = array[7].play.ToString();

                draw1.Text = array[0].draw.ToString();
                draw2.Text = array[1].draw.ToString();
                draw3.Text = array[2].draw.ToString();
                draw4.Text = array[3].draw.ToString();
                draw5.Text = array[4].draw.ToString();
                draw6.Text = array[5].draw.ToString();
                draw7.Text = array[6].draw.ToString();
                draw8.Text = array[7].draw.ToString();

                loss1.Text = array[0].loss.ToString();
                loss2.Text = array[1].loss.ToString();
                loss3.Text = array[2].loss.ToString();
                loss4.Text = array[3].loss.ToString();
                loss5.Text = array[4].loss.ToString();
                loss6.Text = array[5].loss.ToString();
                loss7.Text = array[6].loss.ToString();
                loss8.Text = array[7].loss.ToString();

                ingoal1.Text = array[0].ingoal.ToString();
                ingoal2.Text = array[1].ingoal.ToString();
                ingoal3.Text = array[2].ingoal.ToString();
                ingoal4.Text = array[3].ingoal.ToString();
                ingoal5.Text = array[4].ingoal.ToString();
                ingoal6.Text = array[5].ingoal.ToString();
                ingoal7.Text = array[6].ingoal.ToString();
                ingoal8.Text = array[7].ingoal.ToString();

                outgoal1.Text = array[0].outgoal.ToString();
                outgoal2.Text = array[1].outgoal.ToString();
                outgoal3.Text = array[2].outgoal.ToString();
                outgoal4.Text = array[3].outgoal.ToString();
                outgoal5.Text = array[4].outgoal.ToString();
                outgoal6.Text = array[5].outgoal.ToString();
                outgoal7.Text = array[6].outgoal.ToString();
                outgoal8.Text = array[7].outgoal.ToString();

                inoutgoal1.Text = array[0].inoutgoal.ToString();
                if (array[0].inoutgoal >= 0)
                    inoutgoal1.ForeColor = Color.Green;
                else
                    inoutgoal1.ForeColor = Color.Red;
                inoutgoal2.Text = array[1].inoutgoal.ToString();
                if (array[1].inoutgoal >= 0)
                    inoutgoal2.ForeColor = Color.Green;
                else
                    inoutgoal2.ForeColor = Color.Red;
                inoutgoal3.Text = array[2].inoutgoal.ToString();
                if (array[2].inoutgoal >= 0)
                    inoutgoal3.ForeColor = Color.Green;
                else
                    inoutgoal3.ForeColor = Color.Red;
                inoutgoal4.Text = array[3].inoutgoal.ToString();
                if (array[3].inoutgoal >= 0)
                    inoutgoal4.ForeColor = Color.Green;
                else
                    inoutgoal4.ForeColor = Color.Red;
                inoutgoal5.Text = array[4].inoutgoal.ToString();
                if (array[4].inoutgoal >= 0)
                    inoutgoal5.ForeColor = Color.Green;
                else
                    inoutgoal5.ForeColor = Color.Red;
                inoutgoal6.Text = array[5].inoutgoal.ToString();
                if (array[5].inoutgoal >= 0)
                    inoutgoal6.ForeColor = Color.Green;
                else
                    inoutgoal6.ForeColor = Color.Red;
                inoutgoal7.Text = array[6].inoutgoal.ToString();
                if (array[6].inoutgoal >= 0)
                    inoutgoal7.ForeColor = Color.Green;
                else
                    inoutgoal7.ForeColor = Color.Red;
                inoutgoal8.Text = array[7].inoutgoal.ToString();
                if (array[7].inoutgoal >= 0)
                    inoutgoal8.ForeColor = Color.Green;
                else
                    inoutgoal8.ForeColor = Color.Red;

                point1.Text = array[0].point.ToString();
                point2.Text = array[1].point.ToString();
                point3.Text = array[2].point.ToString();
                point4.Text = array[3].point.ToString();
                point5.Text = array[4].point.ToString();
                point6.Text = array[5].point.ToString();
                point7.Text = array[6].point.ToString();
                point8.Text = array[7].point.ToString();
            }
            else
                button1.Enabled = false;

            if (tours < 7)
            {
                tours++;
                tour.Text = tours + " / 7";
                if (tours == 7)
                    button1.Text = "Закончить";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Text = "Играть тур";

            teams[0] = new TeamInf("Команда 1", 2, 1);
            teams[1] = new TeamInf("Команда 2", 1.6, 2);
            teams[2] = new TeamInf("Команда 3", 1.7, 3);
            teams[3] = new TeamInf("Команда 4", 1.5, 4);
            teams[4] = new TeamInf("Команда 5", 1, 5);
            teams[5] = new TeamInf("Команда 6", 1.3, 6);
            teams[6] = new TeamInf("Команда 7", 2.1, 7);
            teams[7] = new TeamInf("Команда 8", 1.9, 8);

            team1.Text = "Команда 1";
            team2.Text = "Команда 2";
            team3.Text = "Команда 3";
            team4.Text = "Команда 4";
            team5.Text = "Команда 5";
            team6.Text = "Команда 6";
            team7.Text = "Команда 7";
            team8.Text = "Команда 8";

            play1.Text = "0";
            play2.Text = "0";
            play3.Text = "0";
            play4.Text = "0";
            play5.Text = "0";
            play6.Text = "0";
            play7.Text = "0";
            play8.Text = "0";

            win1.Text = "0";
            win2.Text = "0";
            win3.Text = "0";
            win4.Text = "0";
            win5.Text = "0";
            win6.Text = "0";
            win7.Text = "0";
            win8.Text = "0";

            draw1.Text = "0";
            draw2.Text = "0";
            draw3.Text = "0";
            draw4.Text = "0";
            draw5.Text = "0";
            draw6.Text = "0";
            draw7.Text = "0";
            draw8.Text = "0";

            loss1.Text = "0";
            loss2.Text = "0";
            loss3.Text = "0";
            loss4.Text = "0";
            loss5.Text = "0";
            loss6.Text = "0";
            loss7.Text = "0";
            loss8.Text = "0";

            ingoal1.Text = "0";
            ingoal2.Text = "0";
            ingoal3.Text = "0";
            ingoal4.Text = "0";
            ingoal5.Text = "0";
            ingoal6.Text = "0";
            ingoal7.Text = "0";
            ingoal8.Text = "0";

            outgoal1.Text = "0";
            outgoal2.Text = "0";
            outgoal3.Text = "0";
            outgoal4.Text = "0";
            outgoal5.Text = "0";
            outgoal6.Text = "0";
            outgoal7.Text = "0";
            outgoal8.Text = "0";

            inoutgoal1.Text = "0";
            inoutgoal2.Text = "0";
            inoutgoal3.Text = "0";
            inoutgoal4.Text = "0";
            inoutgoal5.Text = "0";
            inoutgoal6.Text = "0";
            inoutgoal7.Text = "0";
            inoutgoal8.Text = "0";

            point1.Text = "0";
            point2.Text = "0";
            point3.Text = "0";
            point4.Text = "0";
            point5.Text = "0";
            point6.Text = "0";
            point7.Text = "0";
            point8.Text = "0";

            tour.Text = "0 / 7";
            tours = 0;
        }
    }
}
