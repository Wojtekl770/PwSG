namespace Forms1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            messageBubble1 = new MessageBubble();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(282, 526);
            button1.Name = "button1";
            button1.Size = new Size(90, 23);
            button1.TabIndex = 0;
            button1.Text = "send";
            button1.UseVisualStyleBackColor = true;
            button1.KeyDown += Enter;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(10, 526);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(266, 23);
            textBox1.TabIndex = 1;
            // 
            // messageBubble1
            // 
            messageBubble1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            messageBubble1.BackColor = Color.DarkOrange;
            messageBubble1.Location = new Point(196, 25);
            messageBubble1.Margin = new Padding(4, 3, 4, 3);
            messageBubble1.Name = "messageBubble1";
            messageBubble1.Size = new Size(175, 115);
            messageBubble1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 561);
            Controls.Add(messageBubble1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            MinimumSize = new Size(320, 480);
            Name = "Form1";
            Text = "Group Chat";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private MessageBubble messageBubble1;
    }
}
