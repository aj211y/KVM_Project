using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;//Process
using System.IO;

public class addvm : Form
{
    private Label name, memory, RAM, OS, iso, G, MB,software,Install;
    private TextBox name_box, RAM_box, iso_box;
    private ComboBox memory_box, OS_box,Install_box;
    private Button back, OK, upload;
    private Process back_exe, OK_exe, refresh, openvm_exe, open_windows;
    private string user_name, user_pwd, ram_size;
    private int ram_count;
    private String currentDir;

    public addvm()
    {
        get_user_name();
        this.Size = new Size(466, 600);
        this.BackColor = Color.LightSteelBlue;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;//不能手動調整視窗邊界
        this.MaximizeBox = false;//不能放大化

        currentDir = AppDomain.CurrentDomain.BaseDirectory;//獲取目前執行檔資料夾

        this.name = new Label();
        this.name.Location = new Point(50,60);
        this.name.Size = new Size(80,40);
        this.name.Text = "虛擬機器名稱";

        this.name_box = new TextBox();
        this.name_box.Location = new Point(140, 55);
        this.name_box.Size = new Size(140, 40);

        this.Install = new Label();
        this.Install.Location = new Point(60,100);
        this.Install.Size = new Size(80,40);
        this.Install.Text = "預選軟體";

        this.Install_box = new ComboBox();
        this.Install_box.Location = new Point(140,95);
        this.Install_box.Size = new Size(140,40);
        this.Install_box.Items.Add("不預選");
        //this.Install_box.Items.Add("Win7 + matlab");
        this.Install_box.Items.Add("Win7 + visual studio");
        this.Install_box.SelectedItem = "不預選";
        //讓使用者不能修改combobox的文字
        this.Install_box.DropDownStyle = ComboBoxStyle.DropDownList;
        this.Install_box.SelectedIndexChanged += new EventHandler(Install_box_SelectedIndexChanged);

        this.memory = new Label();
        this.memory.Location = new Point(55, 140);
        this.memory.Size = new Size(80, 40);
        this.memory.Text = "記憶體大小";

        this.memory_box = new ComboBox();
        this.memory_box.Location = new Point(140,135);
        this.memory_box.Size = new Size(140, 40);
        this.memory_box.Items.Add("1");
        this.memory_box.Items.Add("2");
        this.memory_box.Items.Add("3");
        this.memory_box.Items.Add("4");
        this.memory_box.Items.Add("5");

        this.G = new Label();
        this.G.Text = "G";
        this.G.Location = new Point(285,140);

        this.RAM = new Label();
        this.RAM.Location = new Point(60, 180);
        this.RAM.Size = new Size(80, 40);
        this.RAM.Text = "RAM大小";

        this.RAM_box = new TextBox();
        this.RAM_box.Text = "1024";
        this.RAM_box.Location = new Point(140,175);
        this.RAM_box.Size = new Size(140, 40);

        this.MB = new Label();
        this.MB.Text = "MB";
        this.MB.Location = new Point(285,180);

        this.OS = new Label();
        this.OS.Location = new Point(60, 220);
        this.OS.Size = new Size(80, 40);
        this.OS.Text = "作業系統";

        this.OS_box = new ComboBox();
        this.OS_box.Location = new Point(140, 215);
        this.OS_box.Size = new Size(140, 40);
        this.OS_box.MaxDropDownItems = 3;
        this.OS_box.DropDownStyle = ComboBoxStyle.DropDownList;
        this.OS_box.Items.Add("Ubuntu10.04");
        this.OS_box.Items.Add("Windows7");
        this.OS_box.Items.Add("其他");
        this.OS_box.SelectedItem = "Ubuntu10.04";
        this.OS_box.SelectedIndexChanged += new EventHandler(OS_box_SelectedIndexChanged);

        this.iso = new Label();
        this.iso.Location = new Point(60, 260);
        this.iso.Size = new Size(75, 40);
        this.iso.Text = "上傳檔案";

        this.iso_box = new TextBox();
        this.iso_box.Location = new Point(140, 255);
        this.iso_box.Size = new Size(140, 40);
        this.iso_box.Enabled = false;

        this.upload = new Button();
        this.upload.Location = new Point(285,255);
        this.upload.Size=new Size(70,25);
        this.upload.Text = "選擇檔案";
        this.upload.Enabled = false;

        this.back = new Button();
        this.back.Text = "回上一頁";
        this.back.Click+=new EventHandler(back_Click);
        this.back.Location = new Point(100,300);
        this.back.Size = new Size(80,40);

        this.OK = new Button();
        this.OK.Text = "確定";
        this.OK.Location = new Point(200, 300);
        this.OK.Size = new Size(80, 40);
        this.OK.Click += new EventHandler(OK_Click);

        this.Controls.Add(name);
        this.Controls.Add(Install);
        this.Controls.Add(software);
        this.Controls.Add(memory);
        this.Controls.Add(RAM);
        this.Controls.Add(OS);
        this.Controls.Add(iso);
        this.Controls.Add(name_box);
        this.Controls.Add(Install_box);
        this.Controls.Add(memory_box);
        this.Controls.Add(RAM_box);
        this.Controls.Add(MB);
        this.Controls.Add(OS_box);
        this.Controls.Add(iso_box);
        this.Controls.Add(upload);
        this.Controls.Add(G);
        this.Controls.Add(back);
        this.Controls.Add(OK);
    }

    void Install_box_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Install_box.SelectedItem.ToString() != "不預選")
        {
            this.memory_box.Enabled = false;
            this.OS_box.Enabled = false;
            this.RAM_box.Enabled = false;
        }
        else
        {
            this.memory_box.Enabled = true;
            this.OS_box.Enabled = true;
            this.RAM_box.Enabled = true;
        }
    }

    void OS_box_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OS_box.SelectedItem.ToString() == "其他")
            this.upload.Enabled = true ;
        else
            this.upload.Enabled = false;
    }

    void get_user_name()
    {
        String file = currentDir + "user_name.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\user_name.txt");
        user_name = ret.ReadLine();
        user_pwd = ret.ReadLine();
        ret.Close();
    }

    void OK_Click(object sender, EventArgs e)
    {
        OK_exe = new Process();
        if (Install_box.Text == "不預選")
        {
            OK_exe.StartInfo.FileName = currentDir + "VM\\createVM.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\createVM";
            ram_count = Convert.ToInt32(RAM_box.Text) * 1024;
            ram_size = Convert.ToString(ram_count);
            //modify
            OK_exe.StartInfo.Arguments = user_name + " " + name_box.Text + " " + memory_box.Text + " " + ram_size + " ";
            MessageBox.Show(OK_exe.StartInfo.Arguments);
            if (OS_box.SelectedItem.ToString() == "Ubuntu10.04")
                OK_exe.StartInfo.Arguments += "u";
            else if (OS_box.SelectedItem.ToString() == "Windows7")
                OK_exe.StartInfo.Arguments += "w7";
            else if (OS_box.SelectedItem.ToString() == "其他")
            {
                //才開放上傳
            }
        }
        else 
        {
            OK_exe.StartInfo.FileName = currentDir + "VM\\fast.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\fast.exe";
            OK_exe.StartInfo.Arguments = user_name +" "+name_box.Text+" ";
            if (Install_box.Text == "Win7 + visual studio")
                OK_exe.StartInfo.Arguments += "win7_VS";
        }
        OK_exe.StartInfo.CreateNoWindow = true;
        OK_exe.StartInfo.UseShellExecute = false;
        OK_exe.Start();
        OK_exe.WaitForExit();

        MessageBox.Show("done");

        String file = currentDir + "createVM_c.txt";
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\createVM_c.txt");
        string success = ret.ReadLine();
        ret.Close();

        if (success == "1")
        {
            refresh = new Process();
            refresh.StartInfo.FileName = currentDir + "VM\\sign.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\sign.exe";
            refresh.StartInfo.Arguments = user_name+" "+user_pwd;
            refresh.StartInfo.CreateNoWindow = true;
            refresh.StartInfo.UseShellExecute = false;
            refresh.Start();
            refresh.WaitForExit();


            openvm_exe = new Process();
            openvm_exe.StartInfo.FileName = currentDir + "VM\\openVM.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\openVM";
            openvm_exe.StartInfo.Arguments = user_name + " " + name_box.Text + " open";
            MessageBox.Show(openvm_exe.StartInfo.Arguments);

            openvm_exe.StartInfo.CreateNoWindow = true;
            openvm_exe.StartInfo.UseShellExecute = false;
            openvm_exe.Start();
            openvm_exe.WaitForExit();

            MessageBox.Show("注意！請先將虛擬機灌完，並修改密碼，否則會有被他人駭進系統的危險！");

            string port, argv;
            file=currentDir+"openVM_c.txt";
            ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\openVM_c.txt");
            port = ret.ReadLine();
            ret.Close();
            argv = "140.116.82.180:";
            argv += port;
            MessageBox.Show("請選vnc執行檔，填入 " + argv + " 並連線");

            open_windows = new Process();
            open_windows.StartInfo.FileName = currentDir + "VNC";// "D:\\Google 雲端硬碟\\專題\\client\\VM";
            open_windows.Start();
            //open_windows.WaitForExit();

            back_exe = new Process();
            back_exe.StartInfo.FileName = currentDir + "personal";// "D:\\Google 雲端硬碟\\專題\\client\\personal\\personal\\bin\\Debug\\personal";
            back_exe.Start();
            this.Close();
        }
        else
        {
            MessageBox.Show("建立失敗，請洽工程師！");
        }
    }
    void back_Click(object sender, EventArgs e)
    {
        refresh = new Process();
        refresh.StartInfo.FileName = currentDir + "VM\\sign.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\sign.exe";
        refresh.StartInfo.Arguments = user_name + " " + user_pwd;
        refresh.StartInfo.CreateNoWindow = true;
        refresh.StartInfo.UseShellExecute = false;
        refresh.Start();
        refresh.WaitForExit();

        back_exe = new Process();
        back_exe.StartInfo.FileName = currentDir + "personal";// "D:\\Google 雲端硬碟\\專題\\client\\personal\\personal\\bin\\Debug\\personal";
        back_exe.Start();
        this.Close();
    }
    static void Main()
    {
        Application.Run(new addvm());
    }
}
