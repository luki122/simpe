namespace SimPe.Plugin.Tool.Dockable.Finder
{
    partial class FindObj
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindObj));
            this.label1 = new System.Windows.Forms.Label();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.content.SuspendLayout();
            this.SuspendLayout();
            // 
            // content
            // 
            resources.ApplyResources(this.content, "content");
            this.content.Controls.Add(this.tbGUID);
            this.content.Controls.Add(this.label1);
            this.content.Controls.SetChildIndex(this.label1, 0);
            this.content.Controls.SetChildIndex(this.tbGUID, 0);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbGUID
            // 
            resources.ApplyResources(this.tbGUID, "tbGUID");
            this.tbGUID.Name = "tbGUID";
            // 
            // FindObj
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FindObj";
            this.content.ResumeLayout(false);
            this.content.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbGUID;
    }
}
