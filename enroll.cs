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
    public partial class enroll : Form
    {
        public int pattern_L, pattern_2_L;
        public user[] user1 = new user[10000]; //user배열생성
        public line_x[] x = new line_x[2];
        public line_y[] y = new line_y[7];
        public string[] users = new string[40];
        
        public List<string> list = new List<string>();
        public string fileName;
        public string fileId;
        public string pattern_1, pattern_2; //등록한 첫번째 서명의 패턴과 두번째서명패턴
        public int enroll_index = 21;
       

        public enroll()
        {
            InitializeComponent();
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
        public void ini_Matrix()
        {
            x[0].c = 'A'; x[1].c = 'B';
            y[0].c = 'C'; y[1].c = 'D'; y[2].c = 'E'; y[3].c = 'F'; y[4].c = 'G'; y[5].c = 'H'; y[6].c = 'I';
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
            rate = (ltab[n, m] / average) * 100;
            return rate;
        }
        public string makeTestData(string filePath)
        {

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
            y[2].y = (y_max - y_min) / 6;
            y[3].y = (y_max - y_min) / 6 * 2;
            y[4].y = (y_max - y_min) / 6 * 3;
            y[5].y = (y_max - y_min) / 6 * 4;
            y[6].y = (y_max - y_min) / 6 * 5;


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
                for (int j = 0; (j < 7) && (check == false); j++)
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


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "텍스트파일(*.txt)|*.txt";
           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pattern_1 = makeTestData(openFileDialog1.FileName);
                string strFullPathName = openFileDialog1.FileName;//전체경로
                string strFileName = System.IO.Path.GetFileName(strFullPathName);//확장자 포함 파일명
                fileName = strFileName.Substring(0, strFileName.LastIndexOf('.'));//순수 파일명
                MessageBox.Show("서명이 등록되었습니다"+pattern_1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "C:\\";
            openFileDialog2.Filter = "텍스트파일(*.txt)|*.txt";

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pattern_2 = makeTestData(openFileDialog2.FileName);
                string strFullPathName = openFileDialog2.FileName;//전체경로
                string strFileName = System.IO.Path.GetFileName(strFullPathName);//확장자 포함 파일명
                fileName = strFileName.Substring(0, strFileName.LastIndexOf('.'));//순수 파일명
                MessageBox.Show("서명이 등록되었습니다"+pattern_2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
       
            string temp = null;
            int index;
            
            if (lcs(pattern_1, pattern_2) < 60)
            {
                MessageBox.Show("입력하신 두개의 서명이 너무 다릅니다. 다시 등록해주세요!!");
            }
            else
            {
                string path1 = @openFileDialog1.FileName;
                string path2 = @openFileDialog2.FileName;
                string strFullPathName1 = openFileDialog2.FileName;//전체경로
                string strFileName1 = System.IO.Path.GetFileName(path1);//확장자 포함 파일명
                string fileName1 = strFileName1.Substring(0, strFileName1.LastIndexOf('.'));//순수 파일명
                string[] s = fileName1.Split('-');
                FileInfo file1 = new FileInfo(path1);
                FileInfo file2 = new FileInfo(path2);
               
                register re = new register();
                Form1 f = new Form1();
           
                StreamReader r = new StreamReader("index.txt", System.Text.Encoding.Default);
                FileInfo file = new FileInfo("dates.txt");
                StreamWriter fr = file.AppendText();

                temp = r.ReadLine();
                enroll_index++;
                r.Close();
                index = Convert.ToInt32(temp);
                index++;
                StreamWriter r_2 = new StreamWriter("index.txt", false, Encoding.UTF8);
                r_2.WriteLine(index);
                r_2.Close();
                
                String textValue_1 = "g-" + index + " " + pattern_1+"\r\n";
                String textValue_2 = "g-" + index + " " + pattern_2+"\r\n";
                file1.CopyTo("g-" + index + "-1.txt", true);
                file2.CopyTo("g-" + index + "-2.txt", true);
                re.list.Add("g-" + index + "-1");
                re.list.Add("g-" + index + "-2");
             
                fr.Close();
                System.IO.File.AppendAllText("dates.txt", textValue_1, Encoding.Default);
                System.IO.File.AppendAllText("dates.txt", textValue_2+"\r\n", Encoding.Default);
          
                MessageBox.Show("서명이 등록되었습니다.");
                this.Close();
                
             
                f.label1.Text = s[0]+s[1] +"님이 입장하셨습니다";
                

            }
        }

    }
}
