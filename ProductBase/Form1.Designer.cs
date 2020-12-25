namespace ProductBase
{
    partial class Form1
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
            this.data_grid_view = new System.Windows.Forms.DataGridView();
            this.label_base_name = new System.Windows.Forms.Label();
            this.textbox_input = new System.Windows.Forms.TextBox();
            this.button_action = new System.Windows.Forms.Button();
            this.label_result = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view)).BeginInit();
            this.SuspendLayout();
            // 
            // data_grid_view
            // 
            this.data_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_grid_view.Location = new System.Drawing.Point(12, 51);
            this.data_grid_view.Name = "data_grid_view";
            this.data_grid_view.RowTemplate.Height = 28;
            this.data_grid_view.Size = new System.Drawing.Size(788, 318);
            this.data_grid_view.TabIndex = 0;
            // 
            // label_base_name
            // 
            this.label_base_name.AutoSize = true;
            this.label_base_name.Location = new System.Drawing.Point(13, 13);
            this.label_base_name.Name = "label_base_name";
            this.label_base_name.Size = new System.Drawing.Size(135, 20);
            this.label_base_name.TabIndex = 1;
            this.label_base_name.Text = "base_name_label";
            // 
            // textbox_input
            // 
            this.textbox_input.Location = new System.Drawing.Point(12, 375);
            this.textbox_input.Name = "textbox_input";
            this.textbox_input.Size = new System.Drawing.Size(788, 26);
            this.textbox_input.TabIndex = 2;
            // 
            // button_action
            // 
            this.button_action.Location = new System.Drawing.Point(17, 424);
            this.button_action.Name = "button_action";
            this.button_action.Size = new System.Drawing.Size(118, 45);
            this.button_action.TabIndex = 3;
            this.button_action.Text = "button_action";
            this.button_action.UseVisualStyleBackColor = true;
            this.button_action.Click += new System.EventHandler(this.button_action_Click);
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Location = new System.Drawing.Point(216, 424);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(90, 20);
            this.label_result.TabIndex = 4;
            this.label_result.Text = "label_result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 498);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.button_action);
            this.Controls.Add(this.textbox_input);
            this.Controls.Add(this.label_base_name);
            this.Controls.Add(this.data_grid_view);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView data_grid_view;
        private System.Windows.Forms.Label label_base_name;
        private System.Windows.Forms.TextBox textbox_input;
        private System.Windows.Forms.Button button_action;
        private System.Windows.Forms.Label label_result;
    }
}

