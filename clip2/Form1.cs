using Microsoft.WindowsAPICodePack.Dialogs;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yooja;


namespace clip2
{
    public partial class Form1 : Form
    {
        ClipboardMonitor clipboardMonitor;

       
        static int a = 1;
        static Image temp_img= null;

        static string common_name;
        public Form1()
        {
            InitializeComponent();
            clipboardMonitor = new ClipboardMonitor();
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
            
           

            

            tb_folder.Text = Application.StartupPath;
            tb_filename.Text = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");

            toolTip1.InitialDelay = 0;
            bt_stop.Enabled = false;

            


            



            

        }

        private static void ClipboardMonitor_ClipboardChanged(object sender, EventArgs e)
        {
            
            Image image = Clipboard.GetImage();
            yooja_File_Control fc= new yooja_File_Control();
            
            if (image == null)
                return;

            if (temp_img == null)
            {
                
                image.Save(fc.File_OverLap(common_name + ".png"));
                temp_img = image;
            }
            else
            {
                if (temp_img.Height != image.Height
                    & temp_img.Width != image.Height) //다른 사진이면
                {
                    image.Save(fc.File_OverLap(common_name + ".png"));
                    temp_img = image;
                    
                }
            }

        }
        private void bt_start_Click(object sender, EventArgs e)
        {
            yooja_File_Control fc=new yooja_File_Control();

            if (!fc.Folder_Write_Enable(tb_folder.Text))
            {
                MessageBox.Show("아래 폴더의 쓰기 권한이 없습니다.\n다른 폴더를 선택해주세요.\n\n" +tb_folder.Text,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!fc.Folder_Exists(tb_folder.Text))
            {
                Directory.CreateDirectory(tb_folder.Text);
                int aaaaaa = 1;
            }
           
            common_name = tb_folder.Text + @"\" + tb_filename.Text;

            tb_folder.Enabled = false;
            tb_filename.Enabled = false;
            bt_start.Enabled = false;
            bt_folder_sel.Enabled = false;
            bt_folder_open.Enabled = false;


            bt_stop.Enabled = true;

            

            clipboardMonitor.Start();

        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            tb_folder.Enabled = true;
            tb_filename.Enabled = true;
            bt_start.Enabled = true;
            bt_folder_sel.Enabled = true;
            bt_folder_open.Enabled = true;


            bt_stop.Enabled = false;
            clipboardMonitor.Stop();
        }

        private void tb_filename_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool t =
                e.KeyChar != Convert.ToChar(@"\") &
                e.KeyChar != Convert.ToChar(@"/") &
                e.KeyChar != Convert.ToChar(@":") &
                e.KeyChar != Convert.ToChar(@"*") &
                e.KeyChar != Convert.ToChar(@"?") &
                e.KeyChar != Convert.ToChar("\"") &
                e.KeyChar != Convert.ToChar(@"<") &
                e.KeyChar != Convert.ToChar(@">") &
                e.KeyChar != Convert.ToChar(@"|");
            if (                t                )
            {
               
            }
            else
            {
                
                toolTip1.Show("파일 이름에는 다음 문자를 사용할 수 없습니다.\n\\ / : * ? \" < > | ", tb_filename);
                e.Handled = true;

            }
            
        }

        private void tb_filename_TextChanged(object sender, EventArgs e)
        {
            if (tb_filename.Text.Contains(@"\") |
                tb_filename.Text.Contains(@"/") |
                tb_filename.Text.Contains(@":") |
                tb_filename.Text.Contains(@"*") |
                tb_filename.Text.Contains(@"?") |
                tb_filename.Text.Contains("\"") |
                tb_filename.Text.Contains(@"<") |
                tb_filename.Text.Contains(@">") |
                tb_filename.Text.Contains(@"|")
                )
            {
                tb_filename.Text = tb_filename.Text
                    .Replace(@"\", "")
                    .Replace(@"/", "")
                    .Replace(@":", "")
                    .Replace(@"*", "")
                    .Replace(@"?", "")
                    .Replace("\"", "")
                    .Replace(@"<", "")
                    .Replace(@">", "")
                    .Replace(@"|", "");
                toolTip1.Show("파일 이름에는 다음 문자를 사용할 수 없습니다.\n\\ / : * ? \" < > | ", tb_filename);
            }
        }

        private void bt_folder_sel_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.IsFolderPicker = true;   
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
               tb_folder.Text= dialog.FileName;
            }
        }

        private void bt_folder_open_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(tb_folder.Text);
        }
    }

    public class ClipboardMonitor : Form
    {
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private const int WM_DRAWCLIPBOARD = 0x0308;
        private IntPtr hWndNextViewer;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public event EventHandler ClipboardChanged;

        public void Start()
        {
            hWndNextViewer = SetClipboardViewer(Handle); // 수정된 부분
        }

        public void Stop()
        {
            ChangeClipboardChain(Handle, hWndNextViewer);
        }

        protected override void WndProc(ref Message m)
        {
            int aaa = m.Msg;
            if (aaa != 28)
            {
                if (m.Msg == WM_DRAWCLIPBOARD)
                {
                    ClipboardChanged?.Invoke(this, EventArgs.Empty);
                }
            }
           
                base.WndProc(ref m);
        }


    }
}
