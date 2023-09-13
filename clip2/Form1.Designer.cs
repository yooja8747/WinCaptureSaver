namespace clip2
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bt_start = new System.Windows.Forms.Button();
            this.bt_stop = new System.Windows.Forms.Button();
            this.lb_folder = new System.Windows.Forms.Label();
            this.tb_folder = new System.Windows.Forms.TextBox();
            this.tb_filename = new System.Windows.Forms.TextBox();
            this.lb_filename = new System.Windows.Forms.Label();
            this.bt_folder_sel = new System.Windows.Forms.Button();
            this.bt_folder_open = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // bt_start
            // 
            this.bt_start.Location = new System.Drawing.Point(361, 60);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(75, 23);
            this.bt_start.TabIndex = 0;
            this.bt_start.Text = "시작";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // bt_stop
            // 
            this.bt_stop.Location = new System.Drawing.Point(442, 60);
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.Size = new System.Drawing.Size(75, 23);
            this.bt_stop.TabIndex = 1;
            this.bt_stop.Text = "멈춰!";
            this.bt_stop.UseVisualStyleBackColor = true;
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            // 
            // lb_folder
            // 
            this.lb_folder.AutoSize = true;
            this.lb_folder.Location = new System.Drawing.Point(12, 9);
            this.lb_folder.Name = "lb_folder";
            this.lb_folder.Size = new System.Drawing.Size(69, 12);
            this.lb_folder.TabIndex = 2;
            this.lb_folder.Text = "저장할 폴더";
            // 
            // tb_folder
            // 
            this.tb_folder.Location = new System.Drawing.Point(87, 6);
            this.tb_folder.Name = "tb_folder";
            this.tb_folder.Size = new System.Drawing.Size(349, 21);
            this.tb_folder.TabIndex = 3;
            // 
            // tb_filename
            // 
            this.tb_filename.Location = new System.Drawing.Point(87, 33);
            this.tb_filename.Name = "tb_filename";
            this.tb_filename.Size = new System.Drawing.Size(349, 21);
            this.tb_filename.TabIndex = 5;
            this.tb_filename.TextChanged += new System.EventHandler(this.tb_filename_TextChanged);
            this.tb_filename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_filename_KeyPress);
            // 
            // lb_filename
            // 
            this.lb_filename.AutoSize = true;
            this.lb_filename.Location = new System.Drawing.Point(12, 36);
            this.lb_filename.Name = "lb_filename";
            this.lb_filename.Size = new System.Drawing.Size(53, 12);
            this.lb_filename.TabIndex = 4;
            this.lb_filename.Text = "파일이름";
            // 
            // bt_folder_sel
            // 
            this.bt_folder_sel.Location = new System.Drawing.Point(442, 4);
            this.bt_folder_sel.Name = "bt_folder_sel";
            this.bt_folder_sel.Size = new System.Drawing.Size(75, 23);
            this.bt_folder_sel.TabIndex = 6;
            this.bt_folder_sel.Text = "폴더 선택";
            this.bt_folder_sel.UseVisualStyleBackColor = true;
            this.bt_folder_sel.Click += new System.EventHandler(this.bt_folder_sel_Click);
            // 
            // bt_folder_open
            // 
            this.bt_folder_open.Location = new System.Drawing.Point(442, 31);
            this.bt_folder_open.Name = "bt_folder_open";
            this.bt_folder_open.Size = new System.Drawing.Size(75, 23);
            this.bt_folder_open.TabIndex = 7;
            this.bt_folder_open.Text = "폴더 열기";
            this.bt_folder_open.UseVisualStyleBackColor = true;
            this.bt_folder_open.Click += new System.EventHandler(this.bt_folder_open_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 89);
            this.Controls.Add(this.bt_start);
            this.Controls.Add(this.bt_stop);
            this.Controls.Add(this.lb_folder);
            this.Controls.Add(this.tb_folder);
            this.Controls.Add(this.tb_filename);
            this.Controls.Add(this.lb_filename);
            this.Controls.Add(this.bt_folder_sel);
            this.Controls.Add(this.bt_folder_open);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "WinCaptureSaver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.Button bt_stop;
        private System.Windows.Forms.Label lb_folder;
        private System.Windows.Forms.Label lb_filename;
        private System.Windows.Forms.Button bt_folder_sel;
        private System.Windows.Forms.Button bt_folder_open;
        public System.Windows.Forms.TextBox tb_folder;
        public System.Windows.Forms.TextBox tb_filename;
        private System.Windows.Forms.ToolTip toolTip1;
        
    }
}

