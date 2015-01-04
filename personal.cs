using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;//Process
using System.IO;

public class personal_Form : Form
{
    private Pen pen;
    private Graphics pic;
    private Button user, modify, logout,addvm,closevm, rmvm,recover_vm,order,openvm,clonevm,test_w7,test_u;
    private Label time;
    private Timer timer;
    private ListBox display_box;
    private TextBox choose_box,clone_box;
    private Process addvm_exe, openvm_exe, open_windows,clone_exe, clone_check_exe,order_exe,remove_exe,test_exe,sign_exe;
    private Boolean loss;
    private string user_name, clone_name, isFinish,pwd;
    private String currentDir;
    public personal_Form()
    {
        this.Size = new Size(450, 600);
        this.BackColor = Color.LightSteelBlue;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;//不能手動調整視窗邊界
        this.MaximizeBox = false;//不能放大化

        currentDir = AppDomain.CurrentDomain.BaseDirectory;//獲取目前執行檔資料夾

        String file = currentDir + "user_name.txt";
        StreamReader ret = new StreamReader(file);//("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\user_name.txt");
        user_name = ret.ReadLine();
        pwd = ret.ReadLine();
        ret.Close();

        this.user = new Button();
        this.user.Location=new Point(0,0);
        this.user.Text = "使用者";
        this.user.Size = new Size(80, 38);
        this.user.FlatAppearance.BorderSize = 0;
        this.user.FlatStyle = FlatStyle.Flat;

        this.modify = new Button();
        this.modify.Location = new Point(300, 0);
        this.modify.Text = "修改資料";
        this.modify.Size = new Size(80, 38);
        this.modify.FlatAppearance.BorderSize = 0;
        this.modify.FlatStyle = FlatStyle.Flat;

        this.logout = new Button();
        this.logout.Location = new Point(380, 0);
        this.logout.Text = "登出";
        this.logout.Size = new Size(70, 38);
        this.logout.FlatAppearance.BorderSize = 0;
        this.logout.FlatStyle = FlatStyle.Flat;
        this.logout.Click += new EventHandler(logout_Click);

        this.addvm = new Button();
        this.addvm.Location = new Point(250,100);
        this.addvm.Text = "新增虛擬機器";
        this.addvm.Size = new Size(170,40);
        this.addvm.Click += new EventHandler(addvm_Click);

        this.clone_box = new TextBox();
        this.clone_box.Location = new Point(250,160);
        this.clone_box.Text = "1";
        this.clone_box.TextChanged += new EventHandler(clone_box_TextChanged);
        this.clone_box.TextAlign=HorizontalAlignment.Right;
        this.clone_box.Size = new Size(30,40);

        this.clonevm = new Button();
        this.clonevm.Location = new Point(290,150);
        this.clonevm.Text = "複製虛擬機器";
        this.clonevm.Size = new Size(130, 40);
        this.clonevm.Click += new EventHandler(clonevm_Click);

        this.openvm = new Button();
        this.openvm.Location = new Point(250,200);
        this.openvm.Text = "開啟虛擬機器";
        this.openvm.Size = new Size(170, 40);
        this.openvm.Click += new EventHandler(openvm_Click);

        this.closevm = new Button();
        this.closevm.Location = new Point(250, 250);
        this.closevm.Text = "關閉虛擬機器";
        this.closevm.Size = new Size(170, 40);
        this.closevm.Click += new EventHandler(closevm_Click);

        this.rmvm = new Button();
        this.rmvm.Location = new Point(250,300);
        this.rmvm.Text = "移除虛擬機器";
        this.rmvm.Size = new Size(170, 40);
        this.rmvm.Click += new EventHandler(rmvm_Click);

        this.recover_vm = new Button();
        this.recover_vm.Location = new Point(250, 350);
        this.recover_vm.Text = "復原虛擬機器";
        this.recover_vm.Size = new Size(170, 40);

        this.order = new Button();
        this.order.Location = new Point(250, 350);
        this.order.Text = "定時虛擬機器";
        this.order.Size = new Size(170, 40);
        this.order.Click += new EventHandler(order_Click);

        this.test_w7 = new Button();
        this.test_w7.Location = new Point(250,400);
        this.test_w7.Text = "Win7 測試機";
        this.test_w7.Size = new Size(170,40);
        this.test_w7.Click += new EventHandler(test_w7_Click);

        this.test_u = new Button();
        this.test_u.Location = new Point(250, 450);
        this.test_u.Text = "Ubuntu 測試機";
        this.test_u.Size = new Size(170, 40);
        this.test_u.Click += new EventHandler(test_u_Click);


        /*計時器*/
        this.time = new Label();
        this.time.Text = DateTime.Now.ToString("HH:mm:ss tt");
        this.time.Location = new Point(160,15);

        this.timer = new Timer();
        this.timer.Interval = 1000;
        this.timer.Enabled = true;
        this.timer.Tick+=new EventHandler(timer_Tick);
        this.timer.Start();

        /*vm列表*/
        this.display_box = new ListBox();
        this.display_box.Location = new Point(30,60);
        this.display_box.Size = new Size(200,450);
        //讀取個資檔列出所有vm //看lecture9 p.53
        //MessageBox.Show("display");
        display_show();
        this.display_box.SelectedIndexChanged+=new EventHandler(display_box_SelectedIndexChanged);
        //直接double click選項也可以開啟vm
        this.display_box.DoubleClick += new EventHandler(openvm_Click);
        //遺失focus的時候
        this.display_box.LostFocus += new EventHandler(display_box_LostFocus);
        
        /*選取到的vm*/
        this.choose_box = new TextBox();
        this.choose_box.Enabled = false;
        this.choose_box.Size = new Size(170,50);
        this.choose_box.Location = new Point(250,60);

        this.Controls.Add(user);
        this.Controls.Add(modify);
        this.Controls.Add(logout);
        this.Controls.Add(time);
        this.Controls.Add(addvm);
        this.Controls.Add(clone_box);
        this.Controls.Add(clonevm);
        this.Controls.Add(openvm);
        this.Controls.Add(closevm);
        this.Controls.Add(rmvm);
        //this.Controls.Add(recover_vm);
        this.Controls.Add(order);
        this.Controls.Add(display_box);
        this.Controls.Add(choose_box);
        this.Controls.Add(test_w7);
        this.Controls.Add(test_u);
    }

    void clone_box_TextChanged(object sender, EventArgs e)
    {
        int clone_num;
        if (clone_box.Text == "")
            clone_num = 1;
        else
            clone_num = Convert.ToInt32(clone_box.Text);
        if (clone_num <= 0)
        {
            clone_box.Text = "1";
        }
        if (clone_num > 100)
        {
            clone_box.Text = "100";
        }
    }
    
    void clonevm_Click(object sender, EventArgs e)
    {
        if (choose_box.Text == "")
            MessageBox.Show("請選擇一台虛擬機器");
        else
        {
            clone_exe = new Process();
            clone_exe.StartInfo.FileName = currentDir + "VM\\clone.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\clone";
            clone_exe.StartInfo.Arguments = user_name + " " + choose_box.Text;
            clone_exe.StartInfo.CreateNoWindow = true;
            clone_exe.StartInfo.UseShellExecute = false;

            clone_check_exe = new Process();
            clone_check_exe.StartInfo.FileName = currentDir + "VM\\clonecheck.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\clonecheck";
            clone_check_exe.StartInfo.CreateNoWindow = true;
            clone_check_exe.StartInfo.UseShellExecute = false;

            int num = 500000;
            int clone_num =Convert.ToInt32(clone_box.Text);
            this.Enabled = false;
            String file=currentDir+"clone_c.txt";
            for (int i = 0; i < clone_num; i++)
            {
                clone_exe.Start();
                clone_exe.WaitForExit();
                StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\clone_c.txt");
                clone_name = ret.ReadLine();
                ret.Close();
                clone_check_exe.StartInfo.Arguments = clone_name;
                isFinish = "0";
                file = currentDir + "clonecheck_c.txt";
                while (isFinish == "0")
                {
                    num = 500000;
                    clone_check_exe.Start();
                    clone_check_exe.WaitForExit();
                    ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\clonecheck_c.txt");
                    isFinish = ret.ReadLine();
                    ret.Close();
                    while (num > 0)
                        num = num - 1;
                }
                MessageBox.Show(i + 1 + " machine cloned");
            }
            this.Enabled = true;
        }
        sign_exe = new Process();
        sign_exe.StartInfo.FileName = currentDir + "VM\\sign.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\sign.exe";
        sign_exe.StartInfo.Arguments = user_name + " " + pwd;
        sign_exe.StartInfo.CreateNoWindow = true;
        sign_exe.StartInfo.UseShellExecute = false;
        sign_exe.Start();
        sign_exe.WaitForExit();

        display_show();
    }

    void display_show()
    {
        string vmInfo;
        this.display_box.Items.Clear();
        String file = currentDir + "sign_c.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\sign_c.txt");
        vmInfo = ret.ReadLine();
        while (!ret.EndOfStream)
        {
            vmInfo = ret.ReadLine();
            this.display_box.Items.Add(vmInfo);
        }
        ret.Close();
    }
    //定時vm
    void order_Click(object sender, EventArgs e)
    {
        if (choose_box.Text == "")
            MessageBox.Show("請選擇一台虛擬機器");
        else
        {
            StreamWriter sw = new StreamWriter("vm_name.txt");
            sw.WriteLine(choose_box.Text);            // 寫入文字
            sw.Close();
            order_exe = new Process();
            order_exe.StartInfo.FileName = currentDir + "setupTime";// "D:\\Google 雲端硬碟\\專題\\client\\setupTime\\setupTime\\bin\\Debug\\setupTime";
            order_exe.Start();
            this.Close();
        }
    }

    void display_box_LostFocus(object sender, EventArgs e)
    {
        //記錄display_box是否遺失focus
        loss = true;
    }

    void test_w7_Click(object sender, EventArgs e)
    {
        string port,argv;
        test_exe = new Process();
        test_exe.StartInfo.FileName = currentDir + "VM\\test.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\test";
        test_exe.StartInfo.Arguments = "win7t";
        test_exe.StartInfo.CreateNoWindow = true;
        test_exe.StartInfo.UseShellExecute = false;
        test_exe.Start();
        test_exe.WaitForExit();

        String file = currentDir + "test_c.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\test_c.txt");
        port = ret.ReadLine();
        ret.Close();
        argv = "140.116.82.180:";
        if (port == "")
            MessageBox.Show("開啟失敗！");
        else
        {
            argv += port;
            MessageBox.Show("請選vnc執行檔，填入 " + argv + " 並連線");
            open_windows = new Process();
            open_windows.StartInfo.FileName = currentDir + "VNC";// "D:\\Google 雲端硬碟\\專題\\client\\VM";
            open_windows.Start();
        }
    }

    void test_u_Click(object sender, EventArgs e)
    {
        string port, argv;
        test_exe = new Process();
        test_exe.StartInfo.FileName = currentDir + "VM\\test.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\test";
        test_exe.StartInfo.Arguments = "ut";
        test_exe.StartInfo.CreateNoWindow = true;
        test_exe.StartInfo.UseShellExecute = false;
        test_exe.Start();
        test_exe.WaitForExit();

        String file = currentDir + "test_c.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\test_c.txt");
        port = ret.ReadLine();
        ret.Close();
        argv = "140.116.82.180:";
        if (port == "")
            MessageBox.Show("開啟失敗！");
        else
        {
            argv += port;
            MessageBox.Show("請選vnc執行檔，填入 " + argv + " 並連線");
            open_windows = new Process();
            open_windows.StartInfo.FileName = currentDir + "VNC";// "D:\\Google 雲端硬碟\\專題\\client\\VM";
            open_windows.Start();
        }
    }
    void rmvm_Click(object sender, EventArgs e)
    {
        if (choose_box.Text == "")
            MessageBox.Show("請選擇一台虛擬機器");
        else
        {
            remove_exe = new Process();
            remove_exe.StartInfo.FileName = currentDir + "VM\\removeVM.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\removeVM";
            remove_exe.StartInfo.Arguments = user_name +" "+choose_box.Text;
            //MessageBox.Show(remove_exe.StartInfo.Arguments);
            remove_exe.StartInfo.CreateNoWindow = true;
            remove_exe.StartInfo.UseShellExecute = false;
            remove_exe.Start();
            display_box.Items.Remove(choose_box.Text);
        }
    }
    
    //當選取的vm換的時候，讓choose_box中顯示所選取的vm名稱
    void display_box_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (loss)
        {
            choose_box.Text="";
            loss = false;
        }
        else
            choose_box.Text = display_box.SelectedItem.ToString();
    }

    void openvm_Click(object sender, EventArgs e)
    {
        openvm_exe = new Process();
        openvm_exe.StartInfo.FileName = currentDir + "VM\\openVM.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\openVM";
        openvm_exe.StartInfo.Arguments = user_name+" "+choose_box.Text+" open";
        //MessageBox.Show(openvm_exe.StartInfo.Arguments);
        
        openvm_exe.StartInfo.CreateNoWindow = true;
        openvm_exe.StartInfo.UseShellExecute = false;
        openvm_exe.Start();
        openvm_exe.WaitForExit();

        String file = currentDir + "openVM_c.txt";
        string port, argv;
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\openVM_c.txt");
        port = ret.ReadLine();
        ret.Close();
        argv = "140.116.82.180:";
        if (port == "")
            MessageBox.Show("開啟失敗！");
        else
        {
            //MessageBox.Show(port);
            argv += port;
            MessageBox.Show("請選vnc執行檔，填入 " + argv + " 並連線");
            open_windows = new Process();
            open_windows.StartInfo.FileName = currentDir + "VNC";// "D:\\Google 雲端硬碟\\專題\\client\\VM";
            open_windows.Start();


        }
    }

    void closevm_Click(object sender, EventArgs e)
    {
        openvm_exe = new Process();
        openvm_exe.StartInfo.FileName = currentDir + "VM\\openVM.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\openVM";
        openvm_exe.StartInfo.Arguments = user_name + " " + choose_box.Text + " close";
        openvm_exe.StartInfo.CreateNoWindow = true;
        openvm_exe.StartInfo.UseShellExecute = false;
        openvm_exe.Start();
        openvm_exe.WaitForExit();
    }

    void addvm_Click(object sender, EventArgs e)
    {
        String file = currentDir + "addvm";
        addvm_exe = new Process();
        addvm_exe.StartInfo.FileName = (file);// ("D:\\Google 雲端硬碟\\專題\\client\\addvm\\addvm\\bin\\Debug\\addvm");
        addvm_exe.Start();
        this.Close();
    }

    void logout_Click(object sender, EventArgs e)
    {
        DialogResult dia_ret= MessageBox.Show("確定登出嗎？","登出確認中...",MessageBoxButtons.YesNo);
        switch (dia_ret)
        {
            case DialogResult.Yes:
                this.Close();
                break;
            case DialogResult.No:
                break;
            default:
                MessageBox.Show("誰叫你要亂按");//有不知名錯誤，請盡速連絡總公司");
                this.Close();
                break;
        }
    }
    private void timer_Tick(object sender, EventArgs e)
    {
        DateTime DT_now = DateTime.Now;
        this.time.Text = DateTime.Now.ToString("HH:mm:ss tt");
    }
    //畫線
    protected override void OnPaint(PaintEventArgs paintEvent)
    {
        pen = new Pen(Color.DarkBlue, 3);
        pic = paintEvent.Graphics;
        pic.DrawLine(pen, 0, 40, 450, 40);
    }

    static void Main()
    {
        Application.Run(new personal_Form());
    }
}
