namespace ConfigurationSectionDesigner
{
    partial class AttributesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AttributesForm ) );
            this.OKButton = new System.Windows.Forms.Button();
            this.TreeImageList = new System.Windows.Forms.ImageList( this.components );
            this.AttributeTree = new ConfigurationSectionDesigner.LabelEditEnhancedTreeView();
            this.ExpandAllButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.TreeImageList.Images.SetKeyName( 0, "Attribute" );
            this.TreeImageList.Images.SetKeyName( 1, "LeftBrace" );
            this.TreeImageList.Images.SetKeyName( 2, "Comma" );
            this.TreeImageList.Images.SetKeyName( 3, "RightBrace" );
            this.TreeImageList.Images.SetKeyName( 4, "DoubleBrace" );
            // 
            // AttributeTree
            // 
            this.AttributeTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AttributeTree.FullRowSelect = true;
            this.AttributeTree.ImageIndex = 0;
            this.AttributeTree.ImageList = this.TreeImageList;
            this.AttributeTree.LabelEdit = true;
            this.AttributeTree.Location = new System.Drawing.Point( 12, 12 );
            this.AttributeTree.Name = "AttributeTree";
            this.AttributeTree.SelectedImageIndex = 0;
            this.AttributeTree.Size = new System.Drawing.Size( 454, 274 );
            this.AttributeTree.TabIndex = 0;
            this.AttributeTree.ValidateLabelEdit += new ConfigurationSectionDesigner.LabelEditEnhancedTreeView.ValidateLabelEditEventHandler( this.AttributeTree_ValidateLabelEdit );
            this.AttributeTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler( this.AttributeTree_AfterLabelEdit );
            this.AttributeTree.KeyUp += new System.Windows.Forms.KeyEventHandler( this.AttributeTree_KeyUp );
            this.AttributeTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler( this.AttributeTree_BeforeLabelEdit );
            // 
            // ExpandAllButton
            // 
            this.ExpandAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExpandAllButton.Location = new System.Drawing.Point( 12, 292 );
            this.ExpandAllButton.Name = "ExpandAllButton";
            this.ExpandAllButton.Size = new System.Drawing.Size( 75, 23 );
            this.ExpandAllButton.TabIndex = 3;
            this.ExpandAllButton.Text = "Expand All";
            this.ExpandAllButton.UseVisualStyleBackColor = true;
            this.ExpandAllButton.Click += new System.EventHandler( this.ExpandAllButton_Click );
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
            // AttributesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 478, 327 );
            this.ControlBox = false;
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.ExpandAllButton );
            this.Controls.Add( this.OKButton );
            this.Controls.Add( this.AttributeTree );
            this.MinimizeBox = false;
            this.Name = "AttributesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Attributes";
            this.ResumeLayout( false );

        }

        #endregion

        private LabelEditEnhancedTreeView AttributeTree;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ImageList TreeImageList;
        private System.Windows.Forms.Button ExpandAllButton;
        private System.Windows.Forms.Button cancelButton;
    }
}