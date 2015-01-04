using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;

public class apply_form:Form
{
    private Label account, passwd, confirm, email,account_back, passwd_back, confirm_back;
    private TextBox account_text,passwd_text,confirm_text,email_text;
    private Button OK,Back,check;
    private Process Back_exe,OK_exe,client_exe,check_exe,refresh;
    private Boolean NoUse, OkLen, OkPwd;
    private String currentDir,user_name,user_pwd;

    public apply_form()
    {
        this.Size = new Size(450, 600);
        this.BackColor = Color.LightSteelBlue;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;//不能手動調整視窗邊界
        this.MaximizeBox = false;//不能放大化

        NoUse = true;
        OkLen = false;
        OkPwd = false;

        currentDir = AppDomain.CurrentDomain.BaseDirectory;//獲取目前執行檔資料夾
        
        String file = currentDir + "user_name.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\user_name.txt");
        user_name = ret.ReadLine();
        user_pwd = ret.ReadLine();
        ret.Close();

        this.OK = new Button();
        this.OK.Text = "確定";
        this.OK.Location = new Point(200,300);
        this.OK.Click += new EventHandler(OK_Click);
        this.OK.Size = new Size(80, 30);

        this.Back = new Button();
        this.Back.Text = "回上一頁";
        this.Back.Location = new Point(100,300);
        this.Back.Click += new EventHandler(Back_Click);
        this.Back.Size = new Size(80, 30);

        this.check = new Button();
        this.check.Text = "檢查帳號";
        this.check.Location = new Point(320,100);
        this.check.Click += new EventHandler(check_Click);
        this.check.Size = new Size(80,30);

        this.account = new Label();
        this.account.Text = "帳號";
        this.account.Location = new Point(110, 105);

        this.account_back = new Label();
        this.account_back.Text = "未檢查";
        this.account_back.Location = new Point(260,105);
        this.account_back.ForeColor = Color.Gray;

        this.passwd = new Label();
        this.passwd.Text = "密碼";
        this.passwd.Location = new Point(110, 155);

        this.passwd_back = new Label();
        this.passwd_back.Text = "密碼長度不足";
        this.passwd_back.Location = new Point(260,155);
        this.passwd_back.ForeColor = Color.Gray;

        this.confirm = new Label();
        this.confirm.Text = "再次確認";
        this.confirm.Location = new Point(85, 205);

        this.confirm_back = new Label();
        this.confirm_back.Text = "密碼不符合";
        this.confirm_back.Location = new Point(260,205);
        this.confirm_back.ForeColor = Color.Gray;

        this.email = new Label();
        this.email.Text = "電子信箱";
        this.email.Location = new Point(85, 255);

        this.account_text = new TextBox();
        this.account_text.Location = new Point(150,100);

        this.passwd_text = new TextBox();
        this.passwd_text.Location = new Point(150, 150);
        this.passwd_text.PasswordChar = '*';
        this.passwd_text.TextChanged += new EventHandler(passwd_text_TextChanged);
        this.passwd_text.TextChanged+=new EventHandler(confirm_text_TextChanged);

        this.confirm_text = new TextBox();
        this.confirm_text.Location = new Point(150, 200);
        this.confirm_text.PasswordChar = '*';
        this.confirm_text.TextChanged += new EventHandler(confirm_text_TextChanged);

        this.email_text = new TextBox();
        this.email_text.Location = new Point(150, 250);

        this.Controls.Add(OK);
        this.Controls.Add(Back);
        //this.Controls.Add(check);
        this.Controls.Add(account_text);
        //this.Controls.Add(account_back);
        this.Controls.Add(passwd_text);
        this.Controls.Add(passwd_back);
        this.Controls.Add(confirm_text);
        this.Controls.Add(confirm_back);
        this.Controls.Add(email_text);
        this.Controls.Add(account);
        this.Controls.Add(passwd);
        this.Controls.Add(confirm);
        this.Controls.Add(email);
        
    }

    void confirm_text_TextChanged(object sender, EventArgs e)
    {
        if (passwd_text.Text == confirm_text.Text)
        {
            confirm_back.Text = "密碼符合";
            confirm_back.ForeColor = Color.Green;
            OkPwd = true;
        }
        else
        {
            confirm_back.Text = "密碼不符合";
            confirm_back.ForeColor = Color.Gray;
            OkPwd = false;
        }
    }

    //檢查密碼長度可不可行
    void passwd_text_TextChanged(object sender, EventArgs e)
    {
        if (passwd_text.TextLength < 6)
        {
            passwd_back.Text = "密碼長度不足";
            passwd_back.ForeColor = Color.Gray;
            OkLen = false;
        }
        else if (6 <= passwd_text.TextLength && passwd_text.TextLength < 10)
        {
            passwd_back.Text = "密碼長度尚可";
            passwd_back.ForeColor = Color.Green;
            OkLen = true;
        }
        else
        {
            passwd_back.Text = "密碼長度安全";
            passwd_back.ForeColor = Color.Green;
            OkLen = true;
        }
    }

    //檢查此帳號有沒有被使用過
    void check_Click(object sender, EventArgs e)
    {
        check_exe = new Process();
        check_exe.StartInfo.FileName = "";
        check_exe.StartInfo.Arguments=account_text.Text;
        check_exe.Start();
        
        //回傳判斷此帳號有無使用過
        StreamReader ret = new StreamReader("");
        string str = ret.ToString();
        ret.Close();

        if (str == "1")
        {

        }
        else {
 
        }
    }

    void Back_Click(object sender, EventArgs e)
    {
        refresh = new Process();
        refresh.StartInfo.FileName = currentDir + "VM\\sign.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\sign.exe";
        refresh.StartInfo.Arguments = user_name + " " + user_pwd;
        refresh.StartInfo.CreateNoWindow = true;
        refresh.StartInfo.UseShellExecute = false;
        refresh.Start();
        refresh.WaitForExit();


        Back_exe = new Process();
        Back_exe.StartInfo.FileName = currentDir + "client";// "D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\client";
        Back_exe.Start();
        this.Close();
    }

    void OK_Click(object sender, EventArgs e)
    {
        StreamReader ret;
        string str="0";
        OK_exe = new Process();
        OK_exe.StartInfo.FileName = currentDir + "VM\\create.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\create.exe";//執行檔

        if (NoUse && OkLen && OkPwd && email_text.TextLength != 0)
        {
            OK_exe.StartInfo.Arguments = account_text.Text + " " + passwd_text.Text + " " + email_text.Text;//傳入的參數
            
            OK_exe.StartInfo.UseShellExecute = false;
            OK_exe.StartInfo.CreateNoWindow = true;
            OK_exe.Start();
            OK_exe.WaitForExit();

            ret= new StreamReader("create_c.txt");
            str = ret.ReadLine();
            ret.Close();
        }
        else
        {
            if (!NoUse)
                MessageBox.Show("帳號已經存在，請選擇新的帳號名稱！");
            if (!OkLen)
                MessageBox.Show("密碼長度不足，請選擇新的密碼！");
            if (!OkPwd)
                MessageBox.Show("兩次密碼輸入不一樣，請重新填入一次！");
            if (email_text.TextLength == 0)
                MessageBox.Show("沒有輸入email將會無法進行認證！");
        }

        //回傳得到創建成功or失敗
        if (str == "1")//成功回client
        {
            MessageBox.Show("創建成功");
            client_exe = new Process();
            client_exe.StartInfo.FileName =currentDir+"client";//D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\client";
            client_exe.Start();
            this.Close(); 
        }
        else//失敗停留此頁
        {
            MessageBox.Show("創建失敗");
        }
    }
    
    static void Main()
    {
        Application.Run(new apply_form());
    }
}
