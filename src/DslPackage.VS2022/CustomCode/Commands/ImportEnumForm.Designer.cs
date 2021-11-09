namespace ConfigurationSectionDesigner
{
    partial class ImportEnumForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ImportEnumForm ) );
            this.OKButton = new System.Windows.Forms.Button();
            this.TreeImageList = new System.Windows.Forms.ImageList( this.components );
            this.EnumTree = new System.Windows.Forms.TreeView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Enabled = false;
            this.OKButton.Location = new System.Drawing.Point( 391, 292 );
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size( 75, 23 );
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // TreeImageList
            // 
            this.TreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "TreeImageList.ImageStream" )));
            this.TreeImageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.TreeImageList.Images.SetKeyName( 0, "Enum" );
            this.TreeImageList.Images.SetKeyName( 1, "Value" );
            // 
            // EnumTree
            // 
            this.EnumTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EnumTree.HideSelection = false;
            this.EnumTree.ImageIndex = 0;
            this.EnumTree.ImageList = this.TreeImageList;
            this.EnumTree.Location = new System.Drawing.Point( 12, 12 );
            this.EnumTree.Name = "EnumTree";
            this.EnumTree.SelectedImageIndex = 0;
            this.EnumTree.Size = new System.Drawing.Size( 454, 274 );
            this.EnumTree.TabIndex = 0;
            this.EnumTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.EnumTree_AfterSelect );
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point( 310, 292 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size( 75, 23 );
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ImportEnumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 478, 327 );
            this.ControlBox = false;
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.OKButton );
            this.Controls.Add( this.EnumTree );
            this.MinimizeBox = false;
            this.Name = "ImportEnumForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Enum";
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ImageList TreeImageList;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TreeView EnumTree;
    }
}