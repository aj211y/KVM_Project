using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;//Process
using System.IO;

public class setupTime_Form : Form
{
    private Label h,m,d,M,w,show_vmname;
    private ComboBox month, day, hour, min,week;
    private Button OK, clear, back;
    private Process order_exe,cancel_exe,back_exe,update_exe,refresh;
    private String user_name, user_pwd, vm_name, timeset_ret, show_time;
    private ListBox display_box;
    private String currentDir;

    public setupTime_Form()
    {
        this.Size = new Size(500, 400);
        this.BackColor = Color.LightSteelBlue;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;//不能手動調整視窗邊界
        this.MaximizeBox = false;//不能放大化

        currentDir = AppDomain.CurrentDomain.BaseDirectory;//獲取目前執行檔資料夾
        String file = currentDir + "user_name.txt";

        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\user_name.txt");
        user_name = ret.ReadLine();
        user_pwd = ret.ReadLine();
        ret.Close();
        file = currentDir + "vm_name.txt";
        ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\vm_name.txt");
        vm_name = ret.ReadLine();
        ret.Close();

        this.display_box = new ListBox();
        this.display_box.Location = new Point(30, 85);
        this.display_box.Size = new Size(200, 200);
        display_show();

        this.show_vmname = new Label();
        this.show_vmname.Text = vm_name;
        this.show_vmname.Location = new Point(150,10);
        this.show_vmname.Size = new Size(220,70);
        this.show_vmname.Font = new Font("細明體",40,FontStyle.Regular);

        this.week = new ComboBox();
        this.week.Location = new Point(280,85);
        //this.week.Size = new Size(50,80);
        this.week.SelectedText = "all";
        this.week.Items.Add("all");
        this.week.Items.Add("1");
        this.week.Items.Add("2");
        this.week.Items.Add("3");
        this.week.Items.Add("4");
        this.week.Items.Add("5");
        this.week.Items.Add("6");
        this.week.Items.Add("7");
        
        this.w=new Label();
        this.w.Text = "星期";
        this.w.Location = new Point(410,90);

        this.month = new ComboBox();
        this.month.Location = new Point(280, 130);
        this.month.SelectedText = "all";
        this.month.Items.Add("all");
        this.month.Items.Add("01");
        this.month.Items.Add("02");
        this.month.Items.Add("03");
        this.month.Items.Add("04");
        this.month.Items.Add("05");
        this.month.Items.Add("06");
        this.month.Items.Add("07");
        this.month.Items.Add("08");
        this.month.Items.Add("09");
        this.month.Items.Add("10");
        this.month.Items.Add("11");
        this.month.Items.Add("12");

        this.M = new Label();
        this.M.Text = "月";
        this.M.Size = new Size(40,40);
        this.M.Location = new Point(410,135);

        this.day = new ComboBox();
        this.day.Location = new Point(280, 170);

        this.d = new Label();
        this.d.Text = "日";
        this.d.Location = new Point(410,175);

        this.hour = new ComboBox();
        this.hour.Location = new Point(280, 210);
        this.hour.Items.Add("00");
        this.hour.Items.Add("01");
        this.hour.Items.Add("02");
        this.hour.Items.Add("03");
        this.hour.Items.Add("04");
        this.hour.Items.Add("05");
        this.hour.Items.Add("06");
        this.hour.Items.Add("07");
        this.hour.Items.Add("08");
        this.hour.Items.Add("09");
        this.hour.Items.Add("10");
        this.hour.Items.Add("11");
        this.hour.Items.Add("12");
        this.hour.Items.Add("13");
        this.hour.Items.Add("14");
        this.hour.Items.Add("15");
        this.hour.Items.Add("16");
        this.hour.Items.Add("17");
        this.hour.Items.Add("18");
        this.hour.Items.Add("19");
        this.hour.Items.Add("20");
        this.hour.Items.Add("21");
        this.hour.Items.Add("22");
        this.hour.Items.Add("23");

        this.h = new Label();
        this.h.Location = new Point(410,215);
        this.h.Text = "時";

        this.min = new ComboBox();
        this.min.Location = new Point(280, 250);

        this.m = new Label();
        this.m.Location=new Point(410,255);
        this.m.Text = "分";

        this.OK = new Button();
        this.OK.Location = new Point(340,315);
        this.OK.Size = new Size(80, 38);
        this.OK.Text = "確定";
        this.OK.Click += new EventHandler(OK_Click);

        this.clear = new Button();
        this.clear.Location = new Point(200, 315);
        this.clear.Size = new Size(80, 38);
        this.clear.Text = "清除";
        this.clear.Click += new EventHandler(clear_Click);
        
        this.back = new Button();
        this.back.Location = new Point(60, 315);
        this.back.Size = new Size(80, 38);
        this.back.Text = "回上一頁";
        this.back.Click += new EventHandler(back_Click);

        this.Controls.Add(week);
        this.Controls.Add(w);
        this.Controls.Add(month);
        this.Controls.Add(M);
        this.Controls.Add(day);
        this.Controls.Add(d);
        this.Controls.Add(hour);
        this.Controls.Add(min);
        this.Controls.Add(h);
        this.Controls.Add(m);
        this.Controls.Add(OK);
        this.Controls.Add(clear);
        this.Controls.Add(back);
        this.Controls.Add(show_vmname);
        this.Controls.Add(display_box);
    }

    void timeUpdate()
    {
        update_exe = new Process();
        update_exe.StartInfo.FileName = currentDir+"VM\\timeupdate.exe";//"D:\\Google 雲端硬碟\\專題\\client\\VM\\timeupdate";
        update_exe.StartInfo.Arguments = user_name + " " + vm_name;
        update_exe.StartInfo.CreateNoWindow = true;
        update_exe.StartInfo.UseShellExecute = false;
        update_exe.Start();
        update_exe.WaitForExit();
    }

    String split(string st, char move)
    {
        int num=0,i;
        string ret="",w="";
        string [] words=new string[10];
        //MessageBox.Show(st);
        for (i = 0; i < st.Length; i++)
        {
            if (st[i] == ' ')
            {
                if(w!="")
                   words[num++]=w;
                w = "";
                continue;
            }
            else
            {
                w += st[i];
            }
        }
        words[num++] = w;
        //MessageBox.Show(words[num - 1]);
        String cmp = user_name + "_" + vm_name;
        
        if (move == 'a')
        {
            if (cmp == words[num - 1])//check if the setup time is for this vm
            {
                for (i = num - 5; i >= 0; i--)
                {
                    if (words[i] == "*")
                        ret += "all" + " ";
                    else
                        ret += words[i] + " ";
                }
            }
            else
                return "notYours";
        }
        else//move='d'
        {
            for (i = num - 2; i >= 0; i--)
            {
                if (words[i] == "all")
                    ret += "a" + " ";
                else
                    ret += words[i] + " ";
            }
            if (words[0] == "all")
                ret += "del";
            else
                ret += "rdel";
        }
        return ret;
    }

    void display_show()
    {
        display_box.Items.Clear();
        this.display_box.Items.Add("星期     月   日   時   分");
        timeUpdate();
        String file = currentDir + "timeupdate_c.txt";
        String item;
        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\timeupdate_c.txt");
        while (!ret.EndOfStream)
        {
            show_time = ret.ReadLine();
            show_time = show_time == null ? "" : show_time;
            if (show_time != "")
            {
                item = split(show_time, 'a');
                if(item!="notYours")
                    display_box.Items.Add(item);
            }
        }
        ret.Close();
    }
    void OK_Click(object sender, EventArgs e)
    {
        String arg="";
        order_exe = new Process();
        order_exe.StartInfo.FileName = currentDir + "VM\\timeset.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\timeset.exe";
        arg = user_name + " " + vm_name + " " + min.Text + " " + hour.Text + " " + day.Text + " ";
        if (month.Text == "all")
            arg += "a" + " ";// + week.Text + " ";
        else
            arg += month.Text + " ";// +week.Text + " ";
        if (this.week.Text == "all")
            arg += "a" + " " + "add";
        else
            arg += week.Text + " " + "radd";
        order_exe.StartInfo.Arguments = arg;
        order_exe.StartInfo.CreateNoWindow = true;
        order_exe.StartInfo.UseShellExecute = false;
        order_exe.Start();
        order_exe.WaitForExit();

        String file = currentDir + "timeset_c.txt";

        StreamReader ret = new StreamReader(file);// ("D:\\Google 雲端硬碟\\專題\\client\\client\\client\\bin\\Debug\\timeset_c.txt");
        timeset_ret = ret.ReadLine();
        ret.Close();
        if (timeset_ret == "1")//sucess
        {
            week.SelectedItem = "all";
            month.SelectedItem = "all";
            day.Text="";
            hour.Text="";
            min.Text = "";
            display_show();
        }
        else
        {
            MessageBox.Show("設定時間有錯，請重新輸入！");
        }
        //this.Close();
        display_show();
    }

    void clear_Click(object sender, EventArgs e)
    {
        String arg = "";
        order_exe = new Process();
        order_exe.StartInfo.FileName = currentDir + "VM\\timeset.exe";// "D:\\Google 雲端硬碟\\專題\\client\\VM\\timeset.exe";
        if (display_box.SelectedIndex > 0)
        {
            arg = user_name + " " + vm_name + " " + split(display_box.SelectedItem.ToString(), 'd');
            MessageBox.Show(arg);
            order_exe.StartInfo.Arguments = arg;
            order_exe.StartInfo.CreateNoWindow = true;
            order_exe.StartInfo.UseShellExecute = false;
            order_exe.Start();
            order_exe.WaitForExit();
        }
        display_show();
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
        Application.Run(new setupTime_Form());
    }
}
