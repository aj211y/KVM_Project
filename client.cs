using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;//Process
using System.IO;
using System.Web;

public class back_form : Form
{
    private Button login, apply_acc, forget_pwd;
    private TextBox account_text, passwd_text;
    private Label account, passwd;
    private string login_ret,currentDir;
    private Process apply_exe, login_exe, sign;
    public back_form()
    {
        this.Size = new Size(450, 600);
        this.BackColor = Color.LightSteelBlue;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;//不能手動調整視窗邊界
        this.MaximizeBox = false;//不能放大化

        currentDir = AppDomain.CurrentDomain.BaseDirectory;//獲取目前執行檔資料夾
            
        this.login = new Button();
        this.login.Location = new Point(250, 190);
        this.login.Text = "登入";
        this.login.Size = new Size(80, 25);
        this.login.Click += new EventHandler(login_Click);


        this.apply_acc = new Button();
        this.apply_acc.Location = new Point(160, 190);
        this.apply_acc.Text = "申請帳號";
        this.apply_acc.Size = new Size(80, 25);
        this.apply_acc.FlatStyle = FlatStyle.Flat;
        this.apply_acc.FlatAppearance.BorderSize = 0;
        this.apply_acc.Click += new EventHandler(apply_acc_Click);

        this.forget_pwd = new Button();
        this.forget_pwd.Location = new Point(160, 220);
        this.forget_pwd.Text = "忘記密碼";
        this.forget_pwd.Size = new Size(80, 25);
        this.forget_pwd.FlatStyle = FlatStyle.Flat;
        this.forget_pwd.FlatAppearance.BorderSize = 0;
        this.forget_pwd.Click += new EventHandler(forget_pwd_Click);

        this.account_text = new TextBox();
        this.account_text.Location = new Point(150, 100);

        this.passwd_text = new TextBox();
        this.passwd_text.Location = new Point(150, 150);
        this.passwd_text.PasswordChar = '*';

        this.account = new Label();
        this.account.Text = "帳號：";
        this.account.Location = new Point(110, 105);

        this.passwd = new Label();
        this.passwd.Text = "密碼：";
        this.passwd.Location = new Point(110, 155);

        this.Controls.Add(account_text);
        this.Controls.Add(passwd_text);
        this.Controls.Add(login);
        this.Controls.Add(apply_acc);
        //this.Controls.Add(forget_pwd);
        this.Controls.Add(account);
        this.Controls.Add(passwd);
    }

    private void login_Click(object sender, EventArgs e)
    {
        sign = new Process();
        sign.StartInfo.FileName = currentDir + "VM\\sign.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\sign.exe";
        sign.StartInfo.Arguments = account_text.Text + " " + passwd_text.Text;
        sign.StartInfo.CreateNoWindow = true;
        sign.StartInfo.UseShellExecute = false;
        sign.Start();
        sign.WaitForExit();

        String file=currentDir+"sign_c.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\sign_c.txt");
        login_ret = ret.ReadLine();
        ret.Close();
        
        if (login_ret == "1")
        {
            StreamWriter sw = new StreamWriter("user_name.txt");
            sw.WriteLine(account_text.Text);            // 寫入文字
            sw.WriteLine(passwd_text.Text);
            sw.Close();
            login_exe = new Process();
            login_exe.StartInfo.FileName = currentDir + "personal";// "D:\\Google 雲端硬碟\\專題\\client\\personal\\personal\\bin\\Debug\\personal";
            login_exe.Start();
            this.Close();
        }
        else if (login_ret == "2")
        {
            MessageBox.Show("尚未認證，請先至您的信箱點選認證信，謝謝！");
        }
        else
        {
            MessageBox.Show("帳密輸入錯誤!");
        }
    }
    private void apply_acc_Click(object sender, EventArgs e)
    {
        apply_exe = new Process();
        apply_exe.StartInfo.FileName = currentDir + "apply";// "D:\\Google 雲端硬碟\\專題\\client\\apply\\apply\\bin\\Debug\\apply";
        apply_exe.Start();
        this.Close();
    }
    void forget_pwd_Click(object sender, EventArgs e)
    {
    }
    static void Main()
    {
        Application.Run(new back_form());
    }
}
