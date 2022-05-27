
namespace WindowsFormsApplication317
{

    /*----------------------------23.06.2021---------------------------------------------------
     - добавлены сервисные ползунки для перемещения модели по осям
    */

    partial class ViewModel
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ZoomMinus = new System.Windows.Forms.Button();
            this.ZoomPlus = new System.Windows.Forms.Button();
            this.speed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SpeedMinus = new System.Windows.Forms.Button();
            this.SpeedPlus = new System.Windows.Forms.Button();
            this.RotBut = new System.Windows.Forms.Button();
            this.DOWN = new System.Windows.Forms.Button();
            this.UP = new System.Windows.Forms.Button();
            this.LEFT = new System.Windows.Forms.Button();
            this.RIGHT = new System.Windows.Forms.Button();
            this.xA = new System.Windows.Forms.TextBox();
            this.yA = new System.Windows.Forms.TextBox();
            this.zA = new System.Windows.Forms.TextBox();
            this.RIGHT_Z = new System.Windows.Forms.Button();
            this.LEFT_Z = new System.Windows.Forms.Button();
            this.aSize = new System.Windows.Forms.TextBox();
            this.zLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.translateZ = new System.Windows.Forms.TrackBar();
            this.translateY = new System.Windows.Forms.TrackBar();
            this.translateX = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateX)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.FrameRate = 24;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(800, 509);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.Visible = false;
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.Draw_Model);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "OBJ-файлы|*.obj";
            // 
            // ZoomMinus
            // 
            this.ZoomMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZoomMinus.Location = new System.Drawing.Point(558, 473);
            this.ZoomMinus.Name = "ZoomMinus";
            this.ZoomMinus.Size = new System.Drawing.Size(68, 24);
            this.ZoomMinus.TabIndex = 6;
            this.ZoomMinus.Text = "-";
            this.ZoomMinus.UseVisualStyleBackColor = true;
            this.ZoomMinus.Click += new System.EventHandler(this.Size_Minus);
            // 
            // ZoomPlus
            // 
            this.ZoomPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ZoomPlus.Location = new System.Drawing.Point(558, 420);
            this.ZoomPlus.Name = "ZoomPlus";
            this.ZoomPlus.Size = new System.Drawing.Size(68, 24);
            this.ZoomPlus.TabIndex = 5;
            this.ZoomPlus.Text = "+";
            this.ZoomPlus.UseVisualStyleBackColor = true;
            this.ZoomPlus.Click += new System.EventHandler(this.Size_Plus);
            // 
            // speed
            // 
            this.speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.speed.Enabled = false;
            this.speed.Location = new System.Drawing.Point(484, 450);
            this.speed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(68, 20);
            this.speed.TabIndex = 11;
            this.speed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Скорость";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(573, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Масштаб";
            // 
            // SpeedMinus
            // 
            this.SpeedMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpeedMinus.Location = new System.Drawing.Point(484, 473);
            this.SpeedMinus.Name = "SpeedMinus";
            this.SpeedMinus.Size = new System.Drawing.Size(68, 24);
            this.SpeedMinus.TabIndex = 17;
            this.SpeedMinus.Text = "-";
            this.SpeedMinus.UseVisualStyleBackColor = true;
            this.SpeedMinus.Click += new System.EventHandler(this.Speed_Minus);
            // 
            // SpeedPlus
            // 
            this.SpeedPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SpeedPlus.Location = new System.Drawing.Point(484, 420);
            this.SpeedPlus.Name = "SpeedPlus";
            this.SpeedPlus.Size = new System.Drawing.Size(68, 24);
            this.SpeedPlus.TabIndex = 16;
            this.SpeedPlus.Text = "+";
            this.SpeedPlus.UseVisualStyleBackColor = true;
            this.SpeedPlus.Click += new System.EventHandler(this.Speed_Plus);
            // 
            // RotBut
            // 
            this.RotBut.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RotBut.Location = new System.Drawing.Point(692, 257);
            this.RotBut.Name = "RotBut";
            this.RotBut.Size = new System.Drawing.Size(100, 46);
            this.RotBut.TabIndex = 19;
            this.RotBut.Text = "Отчет";
            this.RotBut.UseVisualStyleBackColor = true;
            this.RotBut.Click += new System.EventHandler(this.Generate_Button);
            // 
            // DOWN
            // 
            this.DOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DOWN.BackgroundImage = global::_3D_View_Form.Properties.Resources.right;
            this.DOWN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DOWN.Location = new System.Drawing.Point(757, 386);
            this.DOWN.Name = "DOWN";
            this.DOWN.Size = new System.Drawing.Size(31, 32);
            this.DOWN.TabIndex = 4;
            this.DOWN.UseVisualStyleBackColor = true;
            this.DOWN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.X_Plus_MouseDown);
            this.DOWN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // UP
            // 
            this.UP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UP.BackgroundImage = global::_3D_View_Form.Properties.Resources.left;
            this.UP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UP.Location = new System.Drawing.Point(646, 386);
            this.UP.Name = "UP";
            this.UP.Size = new System.Drawing.Size(31, 32);
            this.UP.TabIndex = 3;
            this.UP.UseVisualStyleBackColor = true;
            this.UP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.X_Minus_MouseDown);
            this.UP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // LEFT
            // 
            this.LEFT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LEFT.BackgroundImage = global::_3D_View_Form.Properties.Resources.left;
            this.LEFT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LEFT.Location = new System.Drawing.Point(646, 425);
            this.LEFT.Name = "LEFT";
            this.LEFT.Size = new System.Drawing.Size(31, 32);
            this.LEFT.TabIndex = 2;
            this.LEFT.UseVisualStyleBackColor = true;
            this.LEFT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Y_Minus_MouseDown);
            this.LEFT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // RIGHT
            // 
            this.RIGHT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RIGHT.BackgroundImage = global::_3D_View_Form.Properties.Resources.right;
            this.RIGHT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RIGHT.Location = new System.Drawing.Point(757, 425);
            this.RIGHT.Name = "RIGHT";
            this.RIGHT.Size = new System.Drawing.Size(31, 32);
            this.RIGHT.TabIndex = 1;
            this.RIGHT.UseVisualStyleBackColor = true;
            this.RIGHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Y_Plus_MouseDown);
            this.RIGHT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // xA
            // 
            this.xA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xA.Location = new System.Drawing.Point(679, 399);
            this.xA.Name = "xA";
            this.xA.ReadOnly = true;
            this.xA.Size = new System.Drawing.Size(72, 20);
            this.xA.TabIndex = 20;
            this.xA.Text = "0";
            // 
            // yA
            // 
            this.yA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yA.Location = new System.Drawing.Point(679, 438);
            this.yA.Name = "yA";
            this.yA.ReadOnly = true;
            this.yA.Size = new System.Drawing.Size(72, 20);
            this.yA.TabIndex = 21;
            this.yA.Text = "0";
            // 
            // zA
            // 
            this.zA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zA.Location = new System.Drawing.Point(679, 476);
            this.zA.Name = "zA";
            this.zA.ReadOnly = true;
            this.zA.Size = new System.Drawing.Size(72, 20);
            this.zA.TabIndex = 22;
            this.zA.Text = "0";
            // 
            // RIGHT_Z
            // 
            this.RIGHT_Z.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RIGHT_Z.BackgroundImage = global::_3D_View_Form.Properties.Resources.right;
            this.RIGHT_Z.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RIGHT_Z.Location = new System.Drawing.Point(757, 465);
            this.RIGHT_Z.Name = "RIGHT_Z";
            this.RIGHT_Z.Size = new System.Drawing.Size(31, 32);
            this.RIGHT_Z.TabIndex = 25;
            this.RIGHT_Z.UseVisualStyleBackColor = true;
            this.RIGHT_Z.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Z_Plus_MouseDown);
            this.RIGHT_Z.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // LEFT_Z
            // 
            this.LEFT_Z.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LEFT_Z.BackgroundImage = global::_3D_View_Form.Properties.Resources.left;
            this.LEFT_Z.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LEFT_Z.Location = new System.Drawing.Point(646, 465);
            this.LEFT_Z.Name = "LEFT_Z";
            this.LEFT_Z.Size = new System.Drawing.Size(31, 32);
            this.LEFT_Z.TabIndex = 24;
            this.LEFT_Z.UseVisualStyleBackColor = true;
            this.LEFT_Z.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Z_Minus_MouseDown);
            this.LEFT_Z.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp);
            // 
            // aSize
            // 
            this.aSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aSize.Location = new System.Drawing.Point(558, 447);
            this.aSize.Name = "aSize";
            this.aSize.ReadOnly = true;
            this.aSize.Size = new System.Drawing.Size(68, 20);
            this.aSize.TabIndex = 26;
            this.aSize.Text = "0";
            // 
            // zLabel
            // 
            this.zLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(704, 461);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(14, 13);
            this.zLabel.TabIndex = 27;
            this.zLabel.Text = "Z";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(704, 422);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(14, 13);
            this.yLabel.TabIndex = 28;
            this.yLabel.Text = "Y";
            // 
            // xLabel
            // 
            this.xLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(704, 383);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(14, 13);
            this.xLabel.TabIndex = 29;
            this.xLabel.Text = "X";
            // 
            // translateZ
            // 
            this.translateZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.translateZ.Location = new System.Drawing.Point(12, 452);
            this.translateZ.Maximum = 100;
            this.translateZ.Name = "translateZ";
            this.translateZ.Size = new System.Drawing.Size(155, 45);
            this.translateZ.TabIndex = 30;
            this.translateZ.Visible = false;
            // 
            // translateY
            // 
            this.translateY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.translateY.Location = new System.Drawing.Point(12, 399);
            this.translateY.Maximum = 100;
            this.translateY.Name = "translateY";
            this.translateY.Size = new System.Drawing.Size(155, 45);
            this.translateY.TabIndex = 31;
            this.translateY.Visible = false;
            // 
            // translateX
            // 
            this.translateX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.translateX.Location = new System.Drawing.Point(12, 348);
            this.translateX.Maximum = 100;
            this.translateX.Name = "translateX";
            this.translateX.Size = new System.Drawing.Size(155, 45);
            this.translateX.TabIndex = 31;
            this.translateX.Visible = false;
            // 
            // ViewModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 509);
            this.Controls.Add(this.translateX);
            this.Controls.Add(this.translateY);
            this.Controls.Add(this.translateZ);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.zLabel);
            this.Controls.Add(this.aSize);
            this.Controls.Add(this.RIGHT_Z);
            this.Controls.Add(this.LEFT_Z);
            this.Controls.Add(this.zA);
            this.Controls.Add(this.yA);
            this.Controls.Add(this.xA);
            this.Controls.Add(this.RotBut);
            this.Controls.Add(this.SpeedMinus);
            this.Controls.Add(this.SpeedPlus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.ZoomMinus);
            this.Controls.Add(this.ZoomPlus);
            this.Controls.Add(this.DOWN);
            this.Controls.Add(this.UP);
            this.Controls.Add(this.LEFT);
            this.Controls.Add(this.RIGHT);
            this.Controls.Add(this.openGLControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewModel";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.translateX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button RIGHT;
        private System.Windows.Forms.Button LEFT;
        private System.Windows.Forms.Button UP;
        private System.Windows.Forms.Button DOWN;
        private System.Windows.Forms.Button ZoomMinus;
        private System.Windows.Forms.Button ZoomPlus;
        private System.Windows.Forms.NumericUpDown speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SpeedMinus;
        private System.Windows.Forms.Button SpeedPlus;
        private System.Windows.Forms.Button RotBut;
        private System.Windows.Forms.TextBox xA;
        private System.Windows.Forms.TextBox yA;
        private System.Windows.Forms.TextBox zA;
        private System.Windows.Forms.Button RIGHT_Z;
        private System.Windows.Forms.Button LEFT_Z;
        private System.Windows.Forms.TextBox aSize;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.TrackBar translateZ;
        private System.Windows.Forms.TrackBar translateY;
        private System.Windows.Forms.TrackBar translateX;
    }
}

