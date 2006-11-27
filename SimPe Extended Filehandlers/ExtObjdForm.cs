/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Zusammenfassung für ExtObjdForm.
	/// </summary>
	internal class ExtObjdForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Button btnUpdateMMAT;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.PropertyGrid pg;
		internal System.Windows.Forms.TabControl tc;
		internal System.Windows.Forms.TabPage tpcatalogsort;
		private System.Windows.Forms.TabPage tpraw;
		internal System.Windows.Forms.CheckBox cbhobby;
		internal System.Windows.Forms.CheckBox cbaspiration;
		internal System.Windows.Forms.CheckBox cbcareer;
		internal System.Windows.Forms.CheckBox cbkids;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbbin;
		private System.Windows.Forms.RadioButton rbdec;
		private System.Windows.Forms.RadioButton rbhex;
		private System.Windows.Forms.CheckBox cball;
		internal System.Windows.Forms.Label lbIsOk;
		private System.Windows.Forms.Label label1;
		internal Ambertation.Windows.Forms.EnumComboBox cbsort;
		private System.Windows.Forms.Label label63;
		internal System.Windows.Forms.TextBox tbproxguid;
		private System.Windows.Forms.Label label97;
		internal System.Windows.Forms.TextBox tborgguid;
		private System.Windows.Forms.LinkLabel llgetGUID;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label lbObjType;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label12;
		internal System.Windows.Forms.TextBox tbflname;
		internal System.Windows.Forms.TextBox tbguid;
		internal System.Windows.Forms.ComboBox cbtype;
		internal System.Windows.Forms.TextBox tbtype;
		internal System.Windows.Forms.Panel pnobjd;
		internal System.Windows.Forms.CheckBox cbbathroom;
		internal System.Windows.Forms.CheckBox cbbedroom;
		internal System.Windows.Forms.CheckBox cbdinigroom;
		internal System.Windows.Forms.CheckBox cbkitchen;
		internal System.Windows.Forms.CheckBox cbstudy;
		internal System.Windows.Forms.CheckBox cblivingroom;
		internal System.Windows.Forms.CheckBox cboutside;
		internal System.Windows.Forms.CheckBox cbmisc;
		internal System.Windows.Forms.CheckBox cbgeneral;
		internal System.Windows.Forms.CheckBox cbelectronics;
		internal System.Windows.Forms.CheckBox cbdecorative;
		internal System.Windows.Forms.CheckBox cbappliances;
		internal System.Windows.Forms.CheckBox cbsurfaces;
		internal System.Windows.Forms.CheckBox cbseating;
		internal System.Windows.Forms.CheckBox cbplumbing;
		internal System.Windows.Forms.CheckBox cblightning;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
        internal TextBox tbdiag;
        private Label label3;
        internal TextBox tbgrid;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel3;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public ExtObjdForm()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
#if DEBUG
#else
			//cbsort.Visible = false;
			//label1.Visible = false;
#endif

			this.cbtype.Items.Add(Data.ObjectTypes.Unknown);
			this.cbtype.Items.Add(Data.ObjectTypes.ArchitecturalSupport);
			this.cbtype.Items.Add(Data.ObjectTypes.Door);
			this.cbtype.Items.Add(Data.ObjectTypes.Memory);
			this.cbtype.Items.Add(Data.ObjectTypes.ModularStairs);
			this.cbtype.Items.Add(Data.ObjectTypes.ModularStairsPortal);
			this.cbtype.Items.Add(Data.ObjectTypes.Normal);
			this.cbtype.Items.Add(Data.ObjectTypes.Outfit);
			this.cbtype.Items.Add(Data.ObjectTypes.Person);
			this.cbtype.Items.Add(Data.ObjectTypes.SimType);
			this.cbtype.Items.Add(Data.ObjectTypes.Stairs);
			this.cbtype.Items.Add(Data.ObjectTypes.Template);
			this.cbtype.Items.Add(Data.ObjectTypes.Vehicle);
			this.cbtype.Items.Add(Data.ObjectTypes.Window);
			this.cbtype.Items.Add(Data.ObjectTypes.Tiles);
			
			this.cbsort.Enum = typeof(Data.ObjFunctionSubSort);	
			this.cbsort.ResourceManager = SimPe.Localization.Manager;
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

			wrapper = null;
		}


		#region ExtObjdForm
		internal ExtObjd wrapper = null;
		internal uint initialguid;
		Ambertation.PropertyObjectBuilderExt pob;
		ArrayList names;
		bool propchanged;
		string GetName(int i)
		{
			string name = "0x"+Helper.HexString((ushort)i) + ": ";
			name += (string)names[i];

			return name;
		}

		void ShowData()
		{
			propchanged = false;
			this.pg.SelectedObject = null;
			
			names = new ArrayList();
			names = wrapper.Opcodes.OBJDDescription((ushort)wrapper.Type);

			Hashtable ht = new Hashtable();
			for (int i=0; i<Math.Min(names.Count, wrapper.Data.Length); i++)
			{
				Ambertation.PropertyDescription pf = ExtObjd.PropertyParser.GetDescriptor((ushort)wrapper.Type, (ushort)i);
				if (pf==null) 
					pf = new Ambertation.PropertyDescription("Unknown", null, wrapper.Data[i]);
				else 					
					pf.Property = wrapper.Data[i];				

				ht[GetName(i)] = pf;				
			}

			pob = new Ambertation.PropertyObjectBuilderExt(ht);
			this.pg.SelectedObject = pob.Instance;
		}

		void UpdateData()
		{
			if (!propchanged) return;
			propchanged = false;

			try 
			{
				Hashtable ht = pob.Properties;

				for (int i=0; i<Math.Min(names.Count, wrapper.Data.Length); i++)
				{
					string name = GetName(i);	
					try 
					{
						if (ht.Contains(name)) 
						{
							object o = ht[name];
							if (o is SimPe.FlagBase) 
								wrapper.Data[i] = ((SimPe.FlagBase)ht[name]);
							else
								wrapper.Data[i] = Convert.ToInt16(ht[name]);
						} 
					}				
					catch (Exception ex)
					{
						if (Helper.DebugMode) Helper.ExceptionMessage("Error converting "+name, ex);
					}
				}

				wrapper.Changed = true;
				wrapper.UpdateFlags();
				wrapper.RefreshUI();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}

		}

		internal void SetFunctionCb(Wrapper.ExtObjd objd)
		{			
			this.cbappliances.Checked = objd.FunctionSort.InAppliances;
			this.cbdecorative.Checked = objd.FunctionSort.InDecorative;
			this.cbelectronics.Checked = objd.FunctionSort.InElectronics;
			this.cbgeneral.Checked = objd.FunctionSort.InGeneral;
			this.cblightning.Checked = objd.FunctionSort.InLighting;
			this.cbplumbing.Checked = objd.FunctionSort.InPlumbing;
			this.cbseating.Checked = objd.FunctionSort.InSeating;
			this.cbsurfaces.Checked = objd.FunctionSort.InSurfaces;
			this.cbhobby.Checked = objd.FunctionSort.InHobbies;
			this.cbaspiration.Checked = objd.FunctionSort.InAspirationRewards;
			this.cbcareer.Checked = objd.FunctionSort.InCareerRewards;

			this.groupBox2.Refresh();
		}

        static string subKey = "ExtObdjForm";
        private int InitialTab
        {
            get
            {
                XmlRegistryKey rkf = Helper.WindowsRegistry.RegistryKey.CreateSubKey(subKey);
                object o = rkf.GetValue("initialTab", 0);
                return Convert.ToInt16(o);
            }

            set
            {
                XmlRegistryKey rkf = Helper.WindowsRegistry.RegistryKey.CreateSubKey(subKey);
                rkf.SetValue("initialTab", value);
            }

        }

		#endregion

		#region IPackedFileUI Member

		public Control GUIHandle
		{
			get 
			{
				return this.pnobjd;
			}
		}

		public void UpdateGUI(SimPe.Interfaces.Plugin.IFileWrapper wrapper)
		{
			Wrapper.ExtObjd objd = (Wrapper.ExtObjd)wrapper;
			this.wrapper = objd;
			this.initialguid = objd.Guid;
			this.Tag = true;

			try 
			{
				this.lbIsOk.Visible = objd.Ok!=Wrapper.ObjdHealth.Ok;
				if (Helper.WindowsRegistry.HiddenMode) 
					this.lbIsOk.Text = "Please commit! ("+objd.Ok.ToString()+")";				
				this.pg.SelectedObject = null;
				//this.tc.SelectedTab = this.tpcatalogsort;
                this.tc.SelectedIndex = InitialTab;

				this.cbtype.SelectedIndex = 0;
				for (int i=0; i<this.cbtype.Items.Count; i++)
				{
					Data.ObjectTypes ot = (Data.ObjectTypes)this.cbtype.Items[i];
					if (ot==objd.Type) 
					{
						this.cbtype.SelectedIndex = i;
						break;
					}
				}

				this.tbtype.Text = "0x"+Helper.HexString((ushort)(objd.Type));

				this.tbguid.Text = "0x"+Helper.HexString(objd.Guid);
				this.tbproxguid.Text = "0x"+Helper.HexString(objd.ProxyGuid);
				this.tborgguid.Text = "0x"+Helper.HexString(objd.OriginalGuid);
                this.tbdiag.Text = "0x" + Helper.HexString(objd.DiagonalGuid);
                this.tbgrid.Text = "0x" + Helper.HexString(objd.GridAlignedGuid);

				this.tbflname.Text = objd.FileName;

				this.cbbathroom.Checked = (objd.RoomSort.InBathroom);
				this.cbbedroom.Checked = (objd.RoomSort.InBedroom);
				this.cbdinigroom.Checked = (objd.RoomSort.InDiningRoom);
				this.cbkitchen.Checked = (objd.RoomSort.InKitchen);
				this.cblivingroom.Checked = (objd.RoomSort.InLivingRoom);
				this.cbmisc.Checked = (objd.RoomSort.InMisc);
				this.cboutside.Checked = (objd.RoomSort.InOutside);
				this.cbstudy.Checked = (objd.RoomSort.InStudy);
				this.cbkids.Checked = (objd.RoomSort.InKids);

				this.SetFunctionCb(objd);
				this.cbsort.SelectedValue = objd.FunctionSubSort;
			} 
			finally 
			{
				this.Tag = null;
			}
		}

		
		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnobjd = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tborgguid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbgrid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbproxguid = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.tbdiag = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUpdateMMAT = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbguid = new System.Windows.Forms.TextBox();
            this.llgetGUID = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cball = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.lbIsOk = new System.Windows.Forms.Label();
            this.tc = new System.Windows.Forms.TabControl();
            this.tpcatalogsort = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbappliances = new System.Windows.Forms.CheckBox();
            this.cbsort = new Ambertation.Windows.Forms.EnumComboBox();
            this.cbsurfaces = new System.Windows.Forms.CheckBox();
            this.cbseating = new System.Windows.Forms.CheckBox();
            this.cbgeneral = new System.Windows.Forms.CheckBox();
            this.cbelectronics = new System.Windows.Forms.CheckBox();
            this.cbaspiration = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cblightning = new System.Windows.Forms.CheckBox();
            this.cbdecorative = new System.Windows.Forms.CheckBox();
            this.cbhobby = new System.Windows.Forms.CheckBox();
            this.cbplumbing = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbbathroom = new System.Windows.Forms.CheckBox();
            this.cblivingroom = new System.Windows.Forms.CheckBox();
            this.cbkids = new System.Windows.Forms.CheckBox();
            this.cbmisc = new System.Windows.Forms.CheckBox();
            this.cbkitchen = new System.Windows.Forms.CheckBox();
            this.cbdinigroom = new System.Windows.Forms.CheckBox();
            this.cbbedroom = new System.Windows.Forms.CheckBox();
            this.cbstudy = new System.Windows.Forms.CheckBox();
            this.cboutside = new System.Windows.Forms.CheckBox();
            this.tpraw = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbhex = new System.Windows.Forms.RadioButton();
            this.rbdec = new System.Windows.Forms.RadioButton();
            this.rbbin = new System.Windows.Forms.RadioButton();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.tbtype = new System.Windows.Forms.TextBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.lbObjType = new System.Windows.Forms.Label();
            this.tbflname = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cbcareer = new System.Windows.Forms.CheckBox();
            this.pnobjd.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tc.SuspendLayout();
            this.tpcatalogsort.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tpraw.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnobjd
            // 
            this.pnobjd.AutoScroll = true;
            this.pnobjd.Controls.Add(this.tableLayoutPanel1);
            this.pnobjd.Controls.Add(this.btnCommit);
            this.pnobjd.Controls.Add(this.lbIsOk);
            this.pnobjd.Controls.Add(this.tc);
            this.pnobjd.Controls.Add(this.tbtype);
            this.pnobjd.Controls.Add(this.cbtype);
            this.pnobjd.Controls.Add(this.lbObjType);
            this.pnobjd.Controls.Add(this.tbflname);
            this.pnobjd.Controls.Add(this.lbFilename);
            this.pnobjd.Controls.Add(this.panel6);
            this.pnobjd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnobjd.Location = new System.Drawing.Point(0, 0);
            this.pnobjd.Name = "pnobjd";
            this.pnobjd.Size = new System.Drawing.Size(959, 456);
            this.pnobjd.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tborgguid, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbgrid, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbproxguid, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label63, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbdiag, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label97, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdateMMAT, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 96);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(311, 197);
            this.tableLayoutPanel1.TabIndex = 37;
            // 
            // tborgguid
            // 
            this.tborgguid.Location = new System.Drawing.Point(117, 88);
            this.tborgguid.Name = "tborgguid";
            this.tborgguid.Size = new System.Drawing.Size(114, 22);
            this.tborgguid.TabIndex = 19;
            this.tborgguid.Text = "0xDDDDDDDD";
            this.tborgguid.TextChanged += new System.EventHandler(this.SetGuid);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "Grid Align GUID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbgrid
            // 
            this.tbgrid.Location = new System.Drawing.Point(117, 172);
            this.tbgrid.Name = "tbgrid";
            this.tbgrid.Size = new System.Drawing.Size(114, 22);
            this.tbgrid.TabIndex = 36;
            this.tbgrid.Text = "0xDDDDDDDD";
            this.tbgrid.TextChanged += new System.EventHandler(this.SetGuid);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(9, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "Diagonal GUID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbproxguid
            // 
            this.tbproxguid.Location = new System.Drawing.Point(117, 116);
            this.tbproxguid.Name = "tbproxguid";
            this.tbproxguid.Size = new System.Drawing.Size(114, 22);
            this.tbproxguid.TabIndex = 21;
            this.tbproxguid.Text = "0xDDDDDDDD";
            this.tbproxguid.TextChanged += new System.EventHandler(this.SetGuid);
            // 
            // label63
            // 
            this.label63.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label63.AutoSize = true;
            this.label63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label63.Location = new System.Drawing.Point(34, 90);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(77, 17);
            this.label63.TabIndex = 22;
            this.label63.Text = "Orig. GUID";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbdiag
            // 
            this.tbdiag.Location = new System.Drawing.Point(117, 144);
            this.tbdiag.Name = "tbdiag";
            this.tbdiag.Size = new System.Drawing.Size(114, 22);
            this.tbdiag.TabIndex = 34;
            this.tbdiag.Text = "0xDDDDDDDD";
            this.tbdiag.TextChanged += new System.EventHandler(this.SetGuid);
            // 
            // label97
            // 
            this.label97.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label97.AutoSize = true;
            this.label97.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label97.Location = new System.Drawing.Point(13, 118);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(98, 17);
            this.label97.TabIndex = 20;
            this.label97.Text = "Fallback GUID";
            this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(69, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "GUID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // btnUpdateMMAT
            // 
            this.btnUpdateMMAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateMMAT.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdateMMAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateMMAT.Location = new System.Drawing.Point(45, 37);
            this.btnUpdateMMAT.Name = "btnUpdateMMAT";
            this.btnUpdateMMAT.Size = new System.Drawing.Size(66, 27);
            this.btnUpdateMMAT.TabIndex = 32;
            this.btnUpdateMMAT.Text = "Update";
            this.btnUpdateMMAT.Click += new System.EventHandler(this.btnUpdateMMAT_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.tbguid);
            this.panel2.Controls.Add(this.llgetGUID);
            this.panel2.Location = new System.Drawing.Point(117, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(191, 28);
            this.panel2.TabIndex = 37;
            // 
            // tbguid
            // 
            this.tbguid.Location = new System.Drawing.Point(3, 3);
            this.tbguid.Name = "tbguid";
            this.tbguid.Size = new System.Drawing.Size(114, 22);
            this.tbguid.TabIndex = 9;
            this.tbguid.Text = "0xDDDDDDDD";
            this.tbguid.TextChanged += new System.EventHandler(this.SetGuid);
            // 
            // llgetGUID
            // 
            this.llgetGUID.AutoSize = true;
            this.llgetGUID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llgetGUID.LinkArea = new System.Windows.Forms.LinkArea(0, 8);
            this.llgetGUID.Location = new System.Drawing.Point(122, 6);
            this.llgetGUID.Name = "llgetGUID";
            this.llgetGUID.Size = new System.Drawing.Size(66, 17);
            this.llgetGUID.TabIndex = 16;
            this.llgetGUID.TabStop = true;
            this.llgetGUID.Text = "get GUID";
            this.llgetGUID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetGuid);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.cball);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(117, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(149, 45);
            this.panel3.TabIndex = 38;
            // 
            // cball
            // 
            this.cball.AutoSize = true;
            this.cball.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cball.Location = new System.Drawing.Point(0, 20);
            this.cball.Name = "cball";
            this.cball.Size = new System.Drawing.Size(146, 22);
            this.cball.TabIndex = 28;
            this.cball.Text = "update all MMATs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 31;
            this.label2.Text = "MMATs and commit";
            // 
            // btnCommit
            // 
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCommit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommit.Location = new System.Drawing.Point(36, 64);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(89, 26);
            this.btnCommit.TabIndex = 30;
            this.btnCommit.Text = "Commit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // lbIsOk
            // 
            this.lbIsOk.AutoSize = true;
            this.lbIsOk.Location = new System.Drawing.Point(131, 69);
            this.lbIsOk.Name = "lbIsOk";
            this.lbIsOk.Size = new System.Drawing.Size(102, 17);
            this.lbIsOk.TabIndex = 29;
            this.lbIsOk.Text = "Please commit!";
            this.lbIsOk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbIsOk.Visible = false;
            // 
            // tc
            // 
            this.tc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tc.Controls.Add(this.tpcatalogsort);
            this.tc.Controls.Add(this.tpraw);
            this.tc.Location = new System.Drawing.Point(328, 83);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(622, 361);
            this.tc.TabIndex = 26;
            this.tc.SelectedIndexChanged += new System.EventHandler(this.CangedTab);
            // 
            // tpcatalogsort
            // 
            this.tpcatalogsort.Controls.Add(this.groupBox2);
            this.tpcatalogsort.Controls.Add(this.groupBox1);
            this.tpcatalogsort.Location = new System.Drawing.Point(4, 25);
            this.tpcatalogsort.Name = "tpcatalogsort";
            this.tpcatalogsort.Size = new System.Drawing.Size(614, 332);
            this.tpcatalogsort.TabIndex = 0;
            this.tpcatalogsort.Text = "Catalog Sort";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(219, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 186);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function Sort";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.cbappliances, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbsort, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.cbsurfaces, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbseating, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbgeneral, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbelectronics, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbaspiration, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.cblightning, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbdecorative, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbhobby, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbplumbing, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 22);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(361, 143);
            this.tableLayoutPanel3.TabIndex = 20;
            // 
            // cbappliances
            // 
            this.cbappliances.AutoSize = true;
            this.cbappliances.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbappliances.Location = new System.Drawing.Point(3, 3);
            this.cbappliances.Name = "cbappliances";
            this.cbappliances.Size = new System.Drawing.Size(102, 22);
            this.cbappliances.TabIndex = 8;
            this.cbappliances.Text = "Appliances";
            this.cbappliances.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbsort
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.cbsort, 2);
            this.cbsort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbsort.Enum = null;
            this.cbsort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbsort.Location = new System.Drawing.Point(111, 115);
            this.cbsort.Name = "cbsort";
            this.cbsort.ResourceManager = null;
            this.cbsort.Size = new System.Drawing.Size(247, 25);
            this.cbsort.TabIndex = 19;
            this.cbsort.SelectedIndexChanged += new System.EventHandler(this.cbsort_SelectedIndexChanged);
            // 
            // cbsurfaces
            // 
            this.cbsurfaces.AutoSize = true;
            this.cbsurfaces.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbsurfaces.Location = new System.Drawing.Point(111, 87);
            this.cbsurfaces.Name = "cbsurfaces";
            this.cbsurfaces.Size = new System.Drawing.Size(89, 22);
            this.cbsurfaces.TabIndex = 15;
            this.cbsurfaces.Text = "Surfaces";
            this.cbsurfaces.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbseating
            // 
            this.cbseating.AutoSize = true;
            this.cbseating.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbseating.Location = new System.Drawing.Point(111, 59);
            this.cbseating.Name = "cbseating";
            this.cbseating.Size = new System.Drawing.Size(81, 22);
            this.cbseating.TabIndex = 14;
            this.cbseating.Text = "Seating";
            this.cbseating.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbgeneral
            // 
            this.cbgeneral.AutoSize = true;
            this.cbgeneral.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbgeneral.Location = new System.Drawing.Point(3, 87);
            this.cbgeneral.Name = "cbgeneral";
            this.cbgeneral.Size = new System.Drawing.Size(84, 22);
            this.cbgeneral.TabIndex = 11;
            this.cbgeneral.Text = "General";
            this.cbgeneral.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbelectronics
            // 
            this.cbelectronics.AutoSize = true;
            this.cbelectronics.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbelectronics.Location = new System.Drawing.Point(3, 59);
            this.cbelectronics.Name = "cbelectronics";
            this.cbelectronics.Size = new System.Drawing.Size(102, 22);
            this.cbelectronics.TabIndex = 10;
            this.cbelectronics.Text = "Electronics";
            this.cbelectronics.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbaspiration
            // 
            this.cbaspiration.AutoSize = true;
            this.cbaspiration.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbaspiration.Location = new System.Drawing.Point(208, 31);
            this.cbaspiration.Name = "cbaspiration";
            this.cbaspiration.Size = new System.Drawing.Size(96, 22);
            this.cbaspiration.TabIndex = 17;
            this.cbaspiration.Text = "Aspiration";
            this.cbaspiration.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Overall Sort:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // cblightning
            // 
            this.cblightning.AutoSize = true;
            this.cblightning.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cblightning.Location = new System.Drawing.Point(111, 3);
            this.cblightning.Name = "cblightning";
            this.cblightning.Size = new System.Drawing.Size(71, 22);
            this.cblightning.TabIndex = 12;
            this.cblightning.Text = "Lights";
            this.cblightning.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbdecorative
            // 
            this.cbdecorative.AutoSize = true;
            this.cbdecorative.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbdecorative.Location = new System.Drawing.Point(3, 31);
            this.cbdecorative.Name = "cbdecorative";
            this.cbdecorative.Size = new System.Drawing.Size(101, 22);
            this.cbdecorative.TabIndex = 9;
            this.cbdecorative.Text = "Decorative";
            this.cbdecorative.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbhobby
            // 
            this.cbhobby.AutoSize = true;
            this.cbhobby.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbhobby.Location = new System.Drawing.Point(208, 3);
            this.cbhobby.Name = "cbhobby";
            this.cbhobby.Size = new System.Drawing.Size(85, 22);
            this.cbhobby.TabIndex = 16;
            this.cbhobby.Text = "Hobbies";
            this.cbhobby.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // cbplumbing
            // 
            this.cbplumbing.AutoSize = true;
            this.cbplumbing.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbplumbing.Location = new System.Drawing.Point(111, 31);
            this.cbplumbing.Name = "cbplumbing";
            this.cbplumbing.Size = new System.Drawing.Size(91, 22);
            this.cbplumbing.TabIndex = 13;
            this.cbplumbing.Text = "Plumbing";
            this.cbplumbing.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(3, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 183);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Room Sort";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cbbathroom, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cblivingroom, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbkids, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbmisc, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbkitchen, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbdinigroom, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbbedroom, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbstudy, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboutside, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(199, 140);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // cbbathroom
            // 
            this.cbbathroom.AutoSize = true;
            this.cbbathroom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbbathroom.Location = new System.Drawing.Point(3, 3);
            this.cbbathroom.Name = "cbbathroom";
            this.cbbathroom.Size = new System.Drawing.Size(94, 22);
            this.cbbathroom.TabIndex = 0;
            this.cbbathroom.Text = "Bathroom";
            this.cbbathroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cblivingroom
            // 
            this.cblivingroom.AutoSize = true;
            this.cblivingroom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cblivingroom.Location = new System.Drawing.Point(3, 115);
            this.cblivingroom.Name = "cblivingroom";
            this.cblivingroom.Size = new System.Drawing.Size(102, 22);
            this.cblivingroom.TabIndex = 6;
            this.cblivingroom.Text = "Livingroom";
            this.cblivingroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbkids
            // 
            this.cbkids.AutoSize = true;
            this.cbkids.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbkids.Location = new System.Drawing.Point(114, 87);
            this.cbkids.Name = "cbkids";
            this.cbkids.Size = new System.Drawing.Size(60, 22);
            this.cbkids.TabIndex = 8;
            this.cbkids.Text = "Kids";
            this.cbkids.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbmisc
            // 
            this.cbmisc.AutoSize = true;
            this.cbmisc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbmisc.Location = new System.Drawing.Point(114, 3);
            this.cbmisc.Name = "cbmisc";
            this.cbmisc.Size = new System.Drawing.Size(65, 22);
            this.cbmisc.TabIndex = 4;
            this.cbmisc.Text = "Misc.";
            this.cbmisc.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbkitchen
            // 
            this.cbkitchen.AutoSize = true;
            this.cbkitchen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbkitchen.Location = new System.Drawing.Point(3, 87);
            this.cbkitchen.Name = "cbkitchen";
            this.cbkitchen.Size = new System.Drawing.Size(80, 22);
            this.cbkitchen.TabIndex = 3;
            this.cbkitchen.Text = "Kitchen";
            this.cbkitchen.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbdinigroom
            // 
            this.cbdinigroom.AutoSize = true;
            this.cbdinigroom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbdinigroom.Location = new System.Drawing.Point(3, 59);
            this.cbdinigroom.Name = "cbdinigroom";
            this.cbdinigroom.Size = new System.Drawing.Size(105, 22);
            this.cbdinigroom.TabIndex = 2;
            this.cbdinigroom.Text = "Diningroom";
            this.cbdinigroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbbedroom
            // 
            this.cbbedroom.AutoSize = true;
            this.cbbedroom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbbedroom.Location = new System.Drawing.Point(3, 31);
            this.cbbedroom.Name = "cbbedroom";
            this.cbbedroom.Size = new System.Drawing.Size(90, 22);
            this.cbbedroom.TabIndex = 1;
            this.cbbedroom.Text = "Bedroom";
            this.cbbedroom.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cbstudy
            // 
            this.cbstudy.AutoSize = true;
            this.cbstudy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbstudy.Location = new System.Drawing.Point(114, 59);
            this.cbstudy.Name = "cbstudy";
            this.cbstudy.Size = new System.Drawing.Size(69, 22);
            this.cbstudy.TabIndex = 7;
            this.cbstudy.Text = "Study";
            this.cbstudy.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // cboutside
            // 
            this.cboutside.AutoSize = true;
            this.cboutside.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboutside.Location = new System.Drawing.Point(114, 31);
            this.cboutside.Name = "cboutside";
            this.cboutside.Size = new System.Drawing.Size(82, 22);
            this.cboutside.TabIndex = 5;
            this.cboutside.Text = "Outside";
            this.cboutside.CheckedChanged += new System.EventHandler(this.SetRoomFlags);
            // 
            // tpraw
            // 
            this.tpraw.Controls.Add(this.panel1);
            this.tpraw.Controls.Add(this.pg);
            this.tpraw.Location = new System.Drawing.Point(4, 25);
            this.tpraw.Name = "tpraw";
            this.tpraw.Size = new System.Drawing.Size(614, 332);
            this.tpraw.TabIndex = 1;
            this.tpraw.Text = "RAW Data";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rbhex);
            this.panel1.Controls.Add(this.rbdec);
            this.panel1.Controls.Add(this.rbbin);
            this.panel1.Location = new System.Drawing.Point(281, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 19);
            this.panel1.TabIndex = 4;
            // 
            // rbhex
            // 
            this.rbhex.Location = new System.Drawing.Point(188, 1);
            this.rbhex.Name = "rbhex";
            this.rbhex.Size = new System.Drawing.Size(114, 19);
            this.rbhex.TabIndex = 6;
            this.rbhex.Text = "Hexadecimal";
            this.rbhex.CheckedChanged += new System.EventHandler(this.DigitChanged);
            // 
            // rbdec
            // 
            this.rbdec.Location = new System.Drawing.Point(92, 1);
            this.rbdec.Name = "rbdec";
            this.rbdec.Size = new System.Drawing.Size(85, 19);
            this.rbdec.TabIndex = 5;
            this.rbdec.Text = "Decimal";
            this.rbdec.CheckedChanged += new System.EventHandler(this.DigitChanged);
            // 
            // rbbin
            // 
            this.rbbin.Location = new System.Drawing.Point(7, 1);
            this.rbbin.Name = "rbbin";
            this.rbbin.Size = new System.Drawing.Size(76, 19);
            this.rbbin.TabIndex = 4;
            this.rbbin.Text = "Binary";
            this.rbbin.CheckedChanged += new System.EventHandler(this.DigitChanged);
            // 
            // pg
            // 
            this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg.HelpVisible = false;
            this.pg.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pg.Location = new System.Drawing.Point(0, 0);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(614, 332);
            this.pg.TabIndex = 0;
            this.pg.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropChanged);
            // 
            // tbtype
            // 
            this.tbtype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbtype.Location = new System.Drawing.Point(883, 35);
            this.tbtype.Name = "tbtype";
            this.tbtype.ReadOnly = true;
            this.tbtype.Size = new System.Drawing.Size(67, 22);
            this.tbtype.TabIndex = 25;
            this.tbtype.Text = "0xDDDD";
            // 
            // cbtype
            // 
            this.cbtype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbtype.Location = new System.Drawing.Point(684, 35);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(200, 24);
            this.cbtype.TabIndex = 24;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.ChangeType);
            // 
            // lbObjType
            // 
            this.lbObjType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbObjType.AutoSize = true;
            this.lbObjType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbObjType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbObjType.Location = new System.Drawing.Point(599, 38);
            this.lbObjType.Name = "lbObjType";
            this.lbObjType.Size = new System.Drawing.Size(79, 17);
            this.lbObjType.TabIndex = 12;
            this.lbObjType.Text = "Obj. Type";
            // 
            // tbflname
            // 
            this.tbflname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbflname.Location = new System.Drawing.Point(128, 35);
            this.tbflname.Name = "tbflname";
            this.tbflname.Size = new System.Drawing.Size(457, 22);
            this.tbflname.TabIndex = 11;
            this.tbflname.TextChanged += new System.EventHandler(this.SetFlName);
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(49, 38);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(73, 17);
            this.lbFilename.TabIndex = 10;
            this.lbFilename.Text = "Filename";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel6.Controls.Add(this.label12);
            this.panel6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(959, 27);
            this.panel6.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(0, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(166, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Object Data Editor";
            // 
            // cbcareer
            // 
            this.cbcareer.Location = new System.Drawing.Point(0, 0);
            this.cbcareer.Name = "cbcareer";
            this.cbcareer.Size = new System.Drawing.Size(104, 24);
            this.cbcareer.TabIndex = 0;
            this.cbcareer.CheckedChanged += new System.EventHandler(this.SetFunctionFlags);
            // 
            // ExtObjdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 456);
            this.Controls.Add(this.pnobjd);
            this.Name = "ExtObjdForm";
            this.Text = "ExtObjdForm";
            this.pnobjd.ResumeLayout(false);
            this.pnobjd.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tc.ResumeLayout(false);
            this.tpcatalogsort.ResumeLayout(false);
            this.tpcatalogsort.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tpraw.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void GetGuid(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Sims.GUID.GUIDGetterForm form = new Sims.GUID.GUIDGetterForm();
			Registry reg = Helper.WindowsRegistry;

			try 
			{
				uint guid = form.GetNewGUID(reg.Username, reg.Password, this.wrapper.Guid, this.tbflname.Text);

				reg.Username = form.tbusername.Text;
				reg.Password = form.tbpassword.Text;
				this.tbguid.Text = "0x"+Helper.HexString(guid);				
			} 
			catch (Exception ex) {
				if (Helper.DebugMode) Helper.ExceptionMessage("", ex);
			}
		}

		private void ChangeType(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				if (cbtype.SelectedIndex<0) return;
				Data.ObjectTypes ot = (Data.ObjectTypes)cbtype.Items[cbtype.SelectedIndex];
				tbtype.Text = "0x"+Helper.HexString((ushort)ot);

				wrapper.Type = ot;
				wrapper.Changed = true;

				if (this.pg.SelectedObject!=null) 
				{
					UpdateData();
					ShowData();
				}
			} 
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetRoomFlags(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.RoomSort.InBathroom = cbbathroom.Checked;
				wrapper.RoomSort.InBedroom = cbbedroom.Checked;
				wrapper.RoomSort.InDiningRoom = cbdinigroom.Checked;
				wrapper.RoomSort.InKitchen = cbkitchen.Checked;
				wrapper.RoomSort.InLivingRoom = cblivingroom.Checked;
				wrapper.RoomSort.InMisc = cbmisc.Checked;
				wrapper.RoomSort.InOutside = cboutside.Checked;
				wrapper.RoomSort.InStudy = cbstudy.Checked;
				wrapper.RoomSort.InKids = cbkids.Checked;

				wrapper.Changed = true;				
			}
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetFunctionFlags(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.FunctionSort.InAppliances = this.cbappliances.Checked;
				wrapper.FunctionSort.InDecorative = this.cbdecorative.Checked;
				wrapper.FunctionSort.InElectronics = this.cbelectronics.Checked;
				wrapper.FunctionSort.InGeneral = this.cbgeneral.Checked;
				wrapper.FunctionSort.InLighting = this.cblightning.Checked;
				wrapper.FunctionSort.InPlumbing = this.cbplumbing.Checked;
				wrapper.FunctionSort.InSeating = this.cbseating.Checked;
				wrapper.FunctionSort.InSurfaces = this.cbsurfaces.Checked;
				wrapper.FunctionSort.InHobbies = this.cbhobby.Checked;
				wrapper.FunctionSort.InAspirationRewards = this.cbaspiration.Checked;
				wrapper.FunctionSort.InCareerRewards = this.cbcareer.Checked;

				wrapper.Changed = true;
			} 
			finally 
			{
				this.Tag = null;
			}
		}

		private void SetGuid(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			this.Tag = true;

			try 
			{
				wrapper.Guid = Convert.ToUInt32(tbguid.Text, 16);
				wrapper.ProxyGuid = Convert.ToUInt32(this.tbproxguid.Text, 16);
                wrapper.OriginalGuid = Convert.ToUInt32(this.tborgguid.Text, 16);
                wrapper.DiagonalGuid = Convert.ToUInt32(this.tbdiag.Text, 16);
                wrapper.GridAlignedGuid = Convert.ToUInt32(this.tbgrid.Text, 16);
				wrapper.Changed = true;
			} 
			catch (Exception){}
			finally 
			{
				this.Tag = null;
			}
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			if (this.pg.SelectedObject!=null) UpdateData();
			wrapper.SynchronizeUserData();
		}

		private void btnUpdateMMAT_Click(object sender, System.EventArgs e)
		{
			if ((wrapper.Guid!=initialguid) || (cball.Checked))
			{
				SimPe.Plugin.FixGuid fg = new SimPe.Plugin.FixGuid(wrapper.Package);
				if (cball.Checked) 
				{
					fg.FixGuids(wrapper.Guid);
				} 
				else 
				{
					ArrayList al = new ArrayList();
					SimPe.Plugin.GuidSet gs = new SimPe.Plugin.GuidSet();
					gs.oldguid = initialguid;
					gs.guid = wrapper.Guid;
					al.Add(gs);

					fg.FixGuids(al);
				}
				initialguid = wrapper.Guid;
			}

			wrapper.SynchronizeUserData();
		}

		private void CangedTab(object sender, System.EventArgs e)
		{
            InitialTab = tc.SelectedIndex;
            if (tc.SelectedTab == tpraw)
			{
				rbhex.Checked = (Ambertation.BaseChangeableNumber.DigitBase==16);
				rbbin.Checked = (Ambertation.BaseChangeableNumber.DigitBase==2);
				rbdec.Checked = (!rbhex.Checked && !rbbin.Checked);

				//if (this.pg.SelectedObject==null) 
					ShowData();
			} 
			else 
			{
				if (this.pg.SelectedObject!=null) UpdateData();
				this.pg.SelectedObject = null;
			}
		}

		private void PropChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			propchanged = true;
		}

		private void SetFlName(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			wrapper.FileName = tbflname.Text;
			wrapper.Changed = true;
		}

		private void DigitChanged(object sender, System.EventArgs e)
		{
			if (rbhex.Checked) Ambertation.BaseChangeableNumber.DigitBase = 16;
			else if (rbbin.Checked) Ambertation.BaseChangeableNumber.DigitBase = 2;			
			else Ambertation.BaseChangeableNumber.DigitBase = 10;

			this.pg.Refresh();		
		}

		private void label8_Click(object sender, System.EventArgs e)
		{
			SimPe.Data.ObjectTypes[] ts = (SimPe.Data.ObjectTypes[])System.Enum.GetValues(typeof(SimPe.Data.ObjectTypes));
			System.IO.StreamWriter sw = System.IO.File.CreateText(@"h:\objd.xml");
			Hashtable have = new Hashtable();
			try
			{
				sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				sw.WriteLine("<properties xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"propertydefinition.xsd\">");
				foreach (SimPe.Data.ObjectTypes t in ts)
				{
					names = wrapper.Opcodes.OBJDDescription((ushort)t);
					for (int i=0; i<names.Count; i++)
					{
						string k = (string)names[i];
						string cont = (string)have[k.Trim().ToLower()];
						if (cont==null) 
						{					
							cont += "<property type=\"short\">" + Helper.lbr;
							cont += "    <name>"+k.Trim()+"</name>" + Helper.lbr;
							cont += "    <help>"+k.Trim()+"</help>" + Helper.lbr;
							cont += "    <default>0</default>" + Helper.lbr;
							if (k.Trim().ToLower().IndexOf("read_only")!=-1 || k.Trim().ToLower().IndexOf("readonly")!=-1 || k.Trim().ToLower().IndexOf("read only")!=-1) 
								cont += "    <readonly />" + Helper.lbr;
						}
						cont += "    <index type=\""+((ushort)t).ToString()+"\">"+i.ToString()+"</index>" + Helper.lbr;						

						have[k.Trim().ToLower()] = cont;
					}
				}

				foreach (string v in have.Values)
				{
					sw.Write(v);
					sw.WriteLine("</property>");
				}
				sw.WriteLine("</properties>");
			} 
			finally 
			{
				sw.Close();
			}
		}

		private void cbsort_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (Tag!=null) return;
			this.Tag = true;
			wrapper.FunctionSubSort = (Data.ObjFunctionSubSort)cbsort.SelectedValue;
			wrapper.Changed = true;
			this.SetFunctionCb(wrapper);
			this.Tag = null;
		}

	}
}
