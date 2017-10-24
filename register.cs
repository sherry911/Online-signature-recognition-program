using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 개별연구_다시
{
    public partial class register : Form
    {
        public string fileName;
        public string fileId;
        public string fileName_2 = null;
     
        public int pattern_L, pattern_2_L;
        public user[] user1 = new user[10000]; //user배열생성
        public line_x[] x = new line_x[2];
        public line_y[] y = new line_y[15];
        public string[] users = new string[40];
        public string result=null;
        public List<string> list = new List<string>();
       
       
        public register()
        {
            int i=0;
            InitializeComponent();
            ini_data();
           
       
            foreach (string it in list)
            {
                makeUserData(it);
                i++;
            }

            if (i == users.Length - 2)
            {
             
                System.IO.File.AppendAllText("dates.txt",result, Encoding.Default);
                
            }
        }
        public struct user
        {
            public int x;//x좌표
            public int y; //y좌표
        }
        public struct line_x
        {
            public int x;
            public char c;
        }
        public struct line_y
        {
            public int y;
            public char c;
        }

        public void ini_data()
        {
            list.Add("g-1-1"); list.Add("g-1-4"); list.Add("g-2-6"); list.Add("g-2-9"); list.Add("g-3-4"); list.Add("g-3-9"); list.Add("g-4-7");
            list.Add("g-4-11"); list.Add("g-5-3"); list.Add("g-5-8"); list.Add("g-6-1"); list.Add("g-6-10");
           list.Add("g-7-2"); list.Add("g-7-9"); list.Add("g-8-5"); list.Add("g-8-8"); list.Add("g-10-4"); list.Add("g-10-9");
            list.Add("g-11-2"); list.Add("g-11-9"); list.Add("g-12-7"); list.Add("g-12-8"); list.Add("g-13-5"); list.Add("g-13-8");
            list.Add("g-14-4"); list.Add("g-14-7"); list.Add("g-15-5");
            list.Add("g-15-10"); list.Add("g-16-3"); list.Add("g-16-8"); list.Add("g-17-2"); list.Add("g-17-8"); list.Add("g-18-2");
            list.Add("g-18-3"); list.Add("g-19-1"); list.Add("g-19-9");
            list.Add("g-20-8"); list.Add("g-20-10");
           
        }  
        public int max(int x, int y)
        {
            if (x >= y) return x;
            else return y;
        }
        public int min(int x, int y)
        {
            if (x >= y) return y;
            else return x;
        }
        public double lcs(string p, string a)
        {
            int i, j;
            int m = p.Length;
            int n = a.Length;
            double average = (double)(m + m) / 2;
            double rate;
            int[,] ltab = new int[n + 1, m + 1];
            for (i = 0; i <= n; i++) ltab[i, 0] = 0;
            for (j = 0; j <= m; j++) ltab[0, j] = 0;
            for (i = 1; i <= n; i++)
                for (j = 1; j <= m; j++)
                    if (a[i - 1] == p[j - 1]) ltab[i, j] = ltab[i - 1, j - 1] + 1;
                    else ltab[i, j] = max(ltab[i - 1, j], ltab[i, j - 1]);
            rate = (ltab[n, m] / average)*100;
            return rate;
        }

        public void ini_Matrix()
        {
            x[0].c = 'A'; x[1].c = 'B';
            y[0].c = 'C'; y[1].c = 'D'; y[2].c = 'E'; y[3].c = 'F'; y[4].c = 'G'; y[5].c = 'H'; y[6].c = 'I';
            y[7].c = 'J'; y[8].c = 'K'; y[9].c = 'L'; y[10].c = 'M'; y[11].c = 'N'; y[12].c = 'O'; y[13].c = 'P'; y[14].c = 'Q';
        }
        public string makeTestData(string file)
        {
            string filePath = openFileDialog1.FileName;
            string[] d = file.Split('-');
           
            string pattern = null;
            StreamReader r = new StreamReader(filePath, System.Text.Encoding.Default);


            int i = 0, Matrix_Size = 0;
            int x_min = 0, x_max = 0, y_min = 0, y_max = 0;
            string temp = null; //읽어온줄을 임시로 저장하는 temp string변수
            while ((temp = r.ReadLine()) != null) //읽어온 줄이 null이 아니라면
            {

                string[] s = temp.Split(' ');
                user1[i].x = int.Parse(s[1]);
                user1[i].y = int.Parse(s[2]);

                if (i == 0)
                {
                    x_min = x_max = user1[0].x;
                    y_min = y_max = user1[0].y;
                }
                if (x_min > user1[i].x)
                {
                    x_min = user1[i].x; //가장작은 x값을 찾음
                }
                if (x_max < user1[i].x)
                {
                    x_max = user1[i].x;
                }
                if (y_min > user1[i].y)
                {
                    y_min = user1[i].y; //가장작은 y값을 찾음
                }
                if (y_max < user1[i].y)
                {
                    y_max = user1[i].y;
                }
                i++;
            }

            Matrix_Size = i;


            ini_Matrix(); //line배열 초기화작업
            x[0].x = x_min;
            x[1].x = x_max;

            y[0].y = y_min;
            y[1].y = y_max;
            y[2].y = (y_max - y_min) / 14;
            y[3].y = (y_max - y_min) / 14 * 2;
            y[4].y = (y_max - y_min) / 14 * 3;
            y[5].y = (y_max - y_min) / 14 * 4;
            y[6].y = (y_max - y_min) / 14 * 5;
            y[7].y = (y_max - y_min) / 14 * 6;
            y[8].y = (y_max - y_min) / 14 * 7;
            y[9].y = (y_max - y_min) / 14 * 8;
            y[10].y = (y_max - y_min) / 14 * 9;
            y[11].y = (y_max - y_min) / 14 * 10;
            y[12].y = (y_max - y_min) / 14 * 11;
            y[13].y = (y_max - y_min) / 14 * 12;
            y[14].y = (y_max - y_min) / 14 * 13;

            for (i = 0; i < Matrix_Size; i++)
            {
                bool check = false;
                if (user1[i].x == x[0].x)
                {
                    pattern += x[0].c; //패턴생성
                    check = true;
                }
                if (user1[i].x == x[1].x && check == false)
                {
                    pattern += x[1].c; //패턴생성
                    check = true;
                }
                for (int j = 0; (j < 15) && (check == false); j++)
                {
                    if (user1[i].y == y[j].y)
                    {
                        pattern += y[j].c; //패턴생성
                        check = true;
                    }
                }
                check = false;


            }
           
            return pattern;
        }
        public string makeUserData(string file)
        {
            string filePath = file + ".txt";
            string []d = file.Split('-');
            string ID = d[0]+"-"+d[1];
            string pattern = null;
            StreamReader r = new StreamReader(filePath, System.Text.Encoding.Default);
           
            
            int i = 0, Matrix_Size = 0;
            int x_min = 0, x_max = 0, y_min = 0, y_max = 0;
            string temp = null; //읽어온줄을 임시로 저장하는 temp string변수
            while ((temp = r.ReadLine()) != null) //읽어온 줄이 null이 아니라면
            {

                string[] s = temp.Split(' ');
                user1[i].x = int.Parse(s[1]);
                user1[i].y = int.Parse(s[2]);

                if (i == 0)
                {
                    x_min = x_max = user1[0].x;
                    y_min = y_max = user1[0].y;
                }
                if (x_min > user1[i].x)
                {
                    x_min = user1[i].x; //가장작은 x값을 찾음
                }
                if (x_max < user1[i].x)
                {
                    x_max = user1[i].x;
                }
                if (y_min > user1[i].y)
                {
                    y_min = user1[i].y; //가장작은 y값을 찾음
                }
                if (y_max < user1[i].y)
                {
                    y_max = user1[i].y;
                }
                i++;
            }

            Matrix_Size = i;


            ini_Matrix(); //line배열 초기화작업
            x[0].x = x_min;
            x[1].x = x_max;

            y[0].y = y_min;
            y[1].y = y_max;
            y[2].y = (y_max - y_min) / 14;
            y[3].y = (y_max - y_min) / 14 * 2;
            y[4].y = (y_max - y_min) / 14 * 3;
            y[5].y = (y_max - y_min) / 14 * 4;
            y[6].y = (y_max - y_min) / 14 * 5;
            y[7].y = (y_max - y_min) / 14 * 6;
            y[8].y = (y_max - y_min) / 14 * 7;
            y[9].y = (y_max - y_min) / 14 * 8;
            y[10].y = (y_max - y_min) / 14 * 9;
            y[11].y = (y_max - y_min) / 14 * 10;
            y[12].y = (y_max - y_min) / 14 * 11;
            y[13].y = (y_max - y_min) / 14 * 12;
            y[14].y = (y_max - y_min) / 14 * 13;
            for (i = 0; i < Matrix_Size; i++)
            {
                bool check = false;
                if (user1[i].x == x[0].x)
                {
                    pattern += x[0].c; //패턴생성
                    check = true;
                }
                if (user1[i].x == x[1].x && check == false)
                {
                    pattern += x[1].c; //패턴생성
                    check = true;
                }
                for (int j = 0; (j < 15) && (check == false); j++)
                {
                    if (user1[i].y == y[j].y)
                    {
                        pattern += y[j].c; //패턴생성
                        check = true;
                    }
                }
                check = false;


            }

            result += ID + " " + pattern + "\r\n";
            return pattern;
          
        }
        private void button1_Click(object sender, EventArgs e)//로그인버튼
        {
            Form1 f = new Form1();
            StreamReader r = new StreamReader("dates.txt", System.Text.Encoding.Default);
             StreamReader a = new StreamReader("dates.txt", System.Text.Encoding.Default);
            string temp;
            fileName = openFileDialog1.FileName;
            string userPattern=makeTestData(fileName); //로그인하는 사람의 서명 pattern
            string userID=null;
            double tempRate=0.0;
            double maxRate = 70;
            int index=0,maxIndex=0;
            while ((temp = r.ReadLine()) != null ) //읽어온 줄이 null이 아니라면
            {
                if (temp == "") continue;
                string[] t = temp.Split(' ');
                tempRate=lcs(userPattern,t[1]);
                if (maxRate < tempRate)
                {
                    maxRate = tempRate;
                    maxIndex = index;
                }
              
                index++;
            }
            if (maxRate == 70) {
                MessageBox.Show("해당하는 로그인정보가 없습니다. 다시 로그인하세요!!");
            }
            r.Close();
            if (maxRate > 70)
            {
                for (int i = 0; i <= maxIndex; i++)
                {
                    temp = a.ReadLine();
                    if (i == maxIndex)
                    {
                        string[] s = temp.Split(' ');
                        userID = s[0];
                        MessageBox.Show(userID + "로 로그인 되셨습니다");
                        this.Close();
                        f.label1.Text = userID + "님이 입장하셨습니다!";
                        f.ShowDialog();

                    }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)//등록부분
        {
            enroll en = new enroll();
            en.ShowDialog();
            //this.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
          openFileDialog1.ShowDialog();
          
        }
    }
}
