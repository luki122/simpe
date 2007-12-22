/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung f�r Hidden.
	/// </summary>
	public class Hidden : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbComp;
		private System.Windows.Forms.TextBox tbBig;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lbMem;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Hidden()
		{
			//
			// Erforderlich f�r die Windows Form-Designerunterst�tzung
			//
			InitializeComponent();

			//
			// TODO: F�gen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode f�r die Designerunterst�tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Hidden));
			this.label1 = new System.Windows.Forms.Label();
			this.tbComp = new System.Windows.Forms.TextBox();
			this.tbBig = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbMem = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Compression Strength:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// tbComp
			// 
			this.tbComp.Location = new System.Drawing.Point(200, 24);
			this.tbComp.Name = "tbComp";
			this.tbComp.Size = new System.Drawing.Size(104, 21);
			this.tbComp.TabIndex = 1;
			this.tbComp.Text = "";
			// 
			// tbBig
			// 
			this.tbBig.Location = new System.Drawing.Point(200, 48);
			this.tbBig.Name = "tbBig";
			this.tbBig.Size = new System.Drawing.Size(104, 21);
			this.tbBig.TabIndex = 3;
			this.tbBig.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Big Package Resource Count:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(176, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Used Memory:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// lbMem
			// 
			this.lbMem.Location = new System.Drawing.Point(200, 88);
			this.lbMem.Name = "lbMem";
			this.lbMem.Size = new System.Drawing.Size(176, 23);
			this.lbMem.TabIndex = 6;
			this.lbMem.Text = "0";
			this.lbMem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(200, 112);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Collect Garbage";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Hidden
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(458, 144);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lbMem);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbBig);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbComp);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Hidden";
			this.Opacity = 0.8;
			this.Text = "Hidden";
			this.Load += new System.EventHandler(this.Hidden_Load);
			this.Closed += new System.EventHandler(this.Hidden_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private void Hidden_Load(object sender, System.EventArgs e)
		{
			this.tbComp.Text = SimPe.Packages.PackedFile.CompressionStrength.ToString();
			tbBig.Text = Helper.WindowsRegistry.BigPackageResourceCount.ToString();

			this.lbMem.Text = GC.GetTotalMemory(false).ToString("N0") + " Byte";
		}

		private void Hidden_Closed(object sender, System.EventArgs e)
		{
			try 
			{
				SimPe.Packages.PackedFile.CompressionStrength = Convert.ToInt32(this.tbComp.Text);
				Helper.WindowsRegistry.BigPackageResourceCount = Convert.ToInt32(tbBig.Text);
			} 
			catch {}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			this.lbMem.Text = GC.GetTotalMemory(false).ToString("N0") + " Byte";

			this.Cursor = Cursors.Default;
		}
	}
}