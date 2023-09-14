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
        ClipboardMonitor clipboardMonitor; //클립보트 모니터 객체

       
        static int a = 1;                   //파일 중복방지를 위해 파일 뒤에 붙는 숫자 

        static Image temp_img= null;        //파일이 2개씩 저장 되는걸 방지하기 위해 임시로 이미지를 저장해 놓는 객체

        static string common_name;          //저장할 파일의 공통 이름

        public Form1()
        {
            InitializeComponent();

            clipboardMonitor = new ClipboardMonitor();
            
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
            //클립보드가 바뀔경우 이벤트 발생
            
            tb_folder.Text = Application.StartupPath+@"\WinCaptureSaver";   //기본 저장 폴더
            tb_filename.Text = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");//기본 공통 이름


            toolTip1.InitialDelay = 0;
            bt_stop.Enabled = false;
        }


        /// <summary>
        /// 클립보드가 바뀌었을때 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ClipboardMonitor_ClipboardChanged(object sender, EventArgs e)
        {
            
            Image image = Clipboard.GetImage();         //클립보드에서 이미지 가져옴
            yooja_File_Control fc= new yooja_File_Control();    
            
            if (image == null)      //복사한게 텍스트 같은 이미지가 아닐 경우 리턴
                return;

            if (temp_img == null)   //이전에 캡쳐한 이미지가 없는경우=처음 캡쳐한 경우
            {
                
                image.Save(fc.File_OverLap(common_name + ".png"));  //이미지를 저장
                temp_img = image;   //temp_img에 현재 캡쳐한 이미지 
            }
            else                    //처음 캡쳐한게 아닌겨웅
            {
                if (temp_img.Height != image.Height
                    & temp_img.Width != image.Height) //다른 사진이면 저장
                                                      //단순 크기 비교라 오류가 있을 수 있음
                {
                    image.Save(fc.File_OverLap(common_name + ".png"));
                    temp_img = image;
                    
                }
            }

        }

        /// <summary>
        /// 시작 버튼 눌렀을떄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_start_Click(object sender, EventArgs e)
        {
            yooja_File_Control fc=new yooja_File_Control();

            if (!fc.Folder_Write_Enable(tb_folder.Text))//쓰기 권한 있는지 확인
            {
                MessageBox.Show("아래 폴더의 쓰기 권한이 없습니다.\n다른 폴더를 선택해주세요.\n\n" +tb_folder.Text,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!fc.Folder_Exists(tb_folder.Text)) //존재안하는 폴더라면 생성
            {
                Directory.CreateDirectory(tb_folder.Text);
                
            }
           
            common_name = tb_folder.Text + @"\" + tb_filename.Text;//공동 이름 

            tb_folder.Enabled = false;
            tb_filename.Enabled = false;
            bt_start.Enabled = false;
            bt_folder_sel.Enabled = false;
            bt_folder_open.Enabled = false;


            bt_stop.Enabled = true;

            

            clipboardMonitor.Start();       //클립보드 변경사항 있는지 모니터링 시작

        }


        //멈춰! 버튼 눌렀을때
        private void bt_stop_Click(object sender, EventArgs e)
        {
            tb_folder.Enabled = true;
            tb_filename.Enabled = true;
            bt_start.Enabled = true;
            bt_folder_sel.Enabled = true;
            bt_folder_open.Enabled = true;


            bt_stop.Enabled = false;


            clipboardMonitor.Stop();    //클립보드 변경사항 있는지 모니터링 종료
        }

        /// <summary>
        /// 파일 이름 textbox에 타이밍 할때, 경로로 들어면 안되는거 터링
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// 파일이름 textbox의 text 변경되었을때, 들어가면 안되는거 있는지 필터링
        /// 복붙 등으로 넣을때 동작함
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 폴더 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


    /// <summary>
    /// 다른분이 만든거라 모르겠음
    /// </summary>
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
