/***************************************************************************
 *   Original (C) Bidou, assumed to be licenced as part of SimPE           *
 *   Pet updates copyright (C) 2007 by Peter L Jones                       *
 *   pljones@users.sf.net                                                  *
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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using SimPe;
using SimPe.Data;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper;


namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CareerEditor : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code

		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TextBox CareerTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown CareerLvls;
        private System.Windows.Forms.GroupBox gbJLDetails;
		private System.Windows.Forms.ComboBox Vehicle;
		private System.Windows.Forms.ComboBox Outfit;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox gbJLHoursWages;
        private System.Windows.Forms.GroupBox gbJLPromo;
		private System.Windows.Forms.NumericUpDown PromoBody;
		private System.Windows.Forms.NumericUpDown PromoMechanical;
		private System.Windows.Forms.NumericUpDown PromoCooking;
		private System.Windows.Forms.NumericUpDown PromoCharisma;
		private System.Windows.Forms.NumericUpDown PromoFriends;
		private System.Windows.Forms.NumericUpDown PromoCleaning;
		private System.Windows.Forms.NumericUpDown PromoLogic;
		private System.Windows.Forms.NumericUpDown PromoCreativity;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label102;
		private System.Windows.Forms.ComboBox CareerReward;
		private System.Windows.Forms.ListView JobDetailList;
		private System.Windows.Forms.ColumnHeader JdLvl;
		private System.Windows.Forms.ColumnHeader JdJobTitle;
		private System.Windows.Forms.ColumnHeader JdDesc;
		private System.Windows.Forms.ColumnHeader JdOutfit;
		private System.Windows.Forms.ColumnHeader JdVehicle;
		private System.Windows.Forms.ListView HoursWagesList;
		private System.Windows.Forms.ColumnHeader HwLvl;
		private System.Windows.Forms.ColumnHeader HwStart;
		private System.Windows.Forms.ColumnHeader HwHours;
		private System.Windows.Forms.ColumnHeader HwEnd;
		private System.Windows.Forms.ColumnHeader HwWages;
        private ColumnHeader HwCatWages;
        private ColumnHeader HwDogWages;
        private System.Windows.Forms.ColumnHeader HwSun;
		private System.Windows.Forms.ColumnHeader HwMon;
		private System.Windows.Forms.ColumnHeader HwTue;
		private System.Windows.Forms.ColumnHeader HwWed;
		private System.Windows.Forms.ColumnHeader HwThu;
		private System.Windows.Forms.ColumnHeader HwFri;
		private System.Windows.Forms.ColumnHeader HwSat;
		private System.Windows.Forms.ListView PromoList;
		private System.Windows.Forms.ColumnHeader PrLvl;
		private System.Windows.Forms.ColumnHeader PrCooking;
		private System.Windows.Forms.ColumnHeader PrMechanical;
		private System.Windows.Forms.ColumnHeader PrCharisma;
		private System.Windows.Forms.ColumnHeader PrBody;
		private System.Windows.Forms.ColumnHeader PrCreativity;
		private System.Windows.Forms.ColumnHeader PrLogic;
		private System.Windows.Forms.ColumnHeader PrCleaning;
		private System.Windows.Forms.ColumnHeader PrFriends;
        private System.Windows.Forms.GroupBox gbPromo;
        private System.Windows.Forms.GroupBox gbJobDetails;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem miEnglishOnly;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem miInsertLvl;
		private System.Windows.Forms.MenuItem miRemoveLvl;
		private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem5;
        private Label label101;
        private ComboBox cbTrick;
        private ColumnHeader PrTrick;
        private JobDescPanel jdpMale;
        private JobDescPanel jdpFemale;
        private LinkLabel JobDetailsCopy;
        private FlowLayoutPanel flpChanceCards;
        private FlowLayoutPanel flpChanceHeader;
        private LabelledNumericUpDown lnudChanceCurrentLevel;
        private LabelledNumericUpDown lnudChancePercent;
        private ChoicePanel cpChoiceA;
        private ChoicePanel cpChoiceB;
        private FlowLayoutPanel flpChanceText;
        private FlowLayoutPanel flpCTMale;
        private Label label51;
        private TextBox ChanceTextMale;
        private LinkLabel ChanceCopy;
        private FlowLayoutPanel flpCTFemale;
        private Label label52;
        private TextBox ChanceTextFemale;
        private TabControl tcChanceOutcome;
        private TabPage tabPage5;
        private EffectPanel epPassA;
        private TabPage tabPage6;
        private EffectPanel epFailA;
        private TabPage tabPage7;
        private EffectPanel epPassB;
        private TabPage tabPage8;
        private EffectPanel epFailB;
        private GroupBox gbHoursWages;
        private FlowLayoutPanel flpHoursWages;
        private FlowLayoutPanel flpWork;
        private LabelledNumericUpDown lnudWorkStart;
        private LabelledNumericUpDown lnudWorkHours;
        private LabelledNumericUpDown lnudWorkFinish;
        private FlowLayoutPanel flpWages;
        private LabelledNumericUpDown lnudWages;
        private LabelledNumericUpDown lnudWagesDog;
        private LabelledNumericUpDown lnudWagesCat;
        private FlowLayoutPanel flpWorkDays;
        private CheckBox WorkMonday;
        private CheckBox WorkTuesday;
        private CheckBox WorkWednesday;
        private CheckBox WorkThursday;
        private CheckBox WorkFriday;
        private CheckBox WorkSaturday;
        private CheckBox WorkSunday;
        private GroupBox gbHWMotives;
        private Label label27;
        private Label label24;
        private NumericUpDown ComfortHours;
        private NumericUpDown HygieneTotal;
        private NumericUpDown BladderTotal;
        private Label label21;
        private NumericUpDown WorkBladder;
        private Label label23;
        private Label label19;
        private NumericUpDown WorkComfort;
        private NumericUpDown HungerHours;
        private NumericUpDown EnergyHours;
        private Label label25;
        private Label label18;
        private NumericUpDown WorkPublic;
        private NumericUpDown WorkHunger;
        private NumericUpDown EnvironmentTotal;
        private NumericUpDown BladderHours;
        private NumericUpDown ComfortTotal;
        private Label label22;
        private NumericUpDown HungerTotal;
        private NumericUpDown HygieneHours;
        private NumericUpDown ThirstHours;
        private NumericUpDown WorkEnergy;
        private NumericUpDown WorkFun;
        private NumericUpDown WorkThirst;
        private NumericUpDown WorkFamily;
        private NumericUpDown WorkEnvironment;
        private NumericUpDown PublicHours;
        private Label label20;
        private NumericUpDown FamilyTotal;
        private NumericUpDown EnergyTotal;
        private NumericUpDown FunTotal;
        private NumericUpDown PublicTotal;
        private Label label33;
        private Label label32;
        private Label label31;
        private Label label30;
        private Label label29;
        private Label label28;
        private Label label26;
        private NumericUpDown FunHours;
        private NumericUpDown WorkHygiene;
        private NumericUpDown FamilyHours;
        private NumericUpDown EnvironmentHours;
        private NumericUpDown ThirstTotal;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public CareerEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            //this.lnudChanceCurrentLevel.NoLabel = false;
            //this.lnudChancePercent.NoLabel = false;

			XmlRegistryKey rk = Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("CareerEditor");

			oneEnglish = (rk.GetValue("oneEnglish", "True").ToString()) == "True";
			englishOnly = (rk.GetValue("englishOnly", "False").ToString()) == "True";
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flpChanceCards = new System.Windows.Forms.FlowLayoutPanel();
            this.flpChanceHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.lnudChanceCurrentLevel = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudChancePercent = new SimPe.Plugin.LabelledNumericUpDown();
            this.cpChoiceA = new SimPe.Plugin.ChoicePanel();
            this.cpChoiceB = new SimPe.Plugin.ChoicePanel();
            this.flpChanceText = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCTMale = new System.Windows.Forms.FlowLayoutPanel();
            this.label51 = new System.Windows.Forms.Label();
            this.ChanceTextMale = new System.Windows.Forms.TextBox();
            this.ChanceCopy = new System.Windows.Forms.LinkLabel();
            this.flpCTFemale = new System.Windows.Forms.FlowLayoutPanel();
            this.label52 = new System.Windows.Forms.Label();
            this.ChanceTextFemale = new System.Windows.Forms.TextBox();
            this.tcChanceOutcome = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.epPassA = new SimPe.Plugin.EffectPanel();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.epFailA = new SimPe.Plugin.EffectPanel();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.epPassB = new SimPe.Plugin.EffectPanel();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.epFailB = new SimPe.Plugin.EffectPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gbJLPromo = new System.Windows.Forms.GroupBox();
            this.PromoList = new System.Windows.Forms.ListView();
            this.PrLvl = new System.Windows.Forms.ColumnHeader();
            this.PrCooking = new System.Windows.Forms.ColumnHeader();
            this.PrMechanical = new System.Windows.Forms.ColumnHeader();
            this.PrBody = new System.Windows.Forms.ColumnHeader();
            this.PrCharisma = new System.Windows.Forms.ColumnHeader();
            this.PrCreativity = new System.Windows.Forms.ColumnHeader();
            this.PrLogic = new System.Windows.Forms.ColumnHeader();
            this.PrCleaning = new System.Windows.Forms.ColumnHeader();
            this.PrFriends = new System.Windows.Forms.ColumnHeader();
            this.PrTrick = new System.Windows.Forms.ColumnHeader();
            this.gbPromo = new System.Windows.Forms.GroupBox();
            this.cbTrick = new System.Windows.Forms.ComboBox();
            this.label101 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.PromoFriends = new System.Windows.Forms.NumericUpDown();
            this.PromoCleaning = new System.Windows.Forms.NumericUpDown();
            this.PromoLogic = new System.Windows.Forms.NumericUpDown();
            this.PromoCreativity = new System.Windows.Forms.NumericUpDown();
            this.PromoCharisma = new System.Windows.Forms.NumericUpDown();
            this.PromoBody = new System.Windows.Forms.NumericUpDown();
            this.PromoMechanical = new System.Windows.Forms.NumericUpDown();
            this.PromoCooking = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbHoursWages = new System.Windows.Forms.GroupBox();
            this.flpHoursWages = new System.Windows.Forms.FlowLayoutPanel();
            this.flpWork = new System.Windows.Forms.FlowLayoutPanel();
            this.lnudWorkStart = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWorkHours = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWorkFinish = new SimPe.Plugin.LabelledNumericUpDown();
            this.flpWages = new System.Windows.Forms.FlowLayoutPanel();
            this.lnudWages = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWagesDog = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWagesCat = new SimPe.Plugin.LabelledNumericUpDown();
            this.flpWorkDays = new System.Windows.Forms.FlowLayoutPanel();
            this.WorkMonday = new System.Windows.Forms.CheckBox();
            this.WorkTuesday = new System.Windows.Forms.CheckBox();
            this.WorkWednesday = new System.Windows.Forms.CheckBox();
            this.WorkThursday = new System.Windows.Forms.CheckBox();
            this.WorkFriday = new System.Windows.Forms.CheckBox();
            this.WorkSaturday = new System.Windows.Forms.CheckBox();
            this.WorkSunday = new System.Windows.Forms.CheckBox();
            this.gbHWMotives = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.ComfortHours = new System.Windows.Forms.NumericUpDown();
            this.HygieneTotal = new System.Windows.Forms.NumericUpDown();
            this.BladderTotal = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.WorkBladder = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.WorkComfort = new System.Windows.Forms.NumericUpDown();
            this.HungerHours = new System.Windows.Forms.NumericUpDown();
            this.EnergyHours = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.WorkPublic = new System.Windows.Forms.NumericUpDown();
            this.WorkHunger = new System.Windows.Forms.NumericUpDown();
            this.EnvironmentTotal = new System.Windows.Forms.NumericUpDown();
            this.BladderHours = new System.Windows.Forms.NumericUpDown();
            this.ComfortTotal = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.HungerTotal = new System.Windows.Forms.NumericUpDown();
            this.HygieneHours = new System.Windows.Forms.NumericUpDown();
            this.ThirstHours = new System.Windows.Forms.NumericUpDown();
            this.WorkEnergy = new System.Windows.Forms.NumericUpDown();
            this.WorkFun = new System.Windows.Forms.NumericUpDown();
            this.WorkThirst = new System.Windows.Forms.NumericUpDown();
            this.WorkFamily = new System.Windows.Forms.NumericUpDown();
            this.WorkEnvironment = new System.Windows.Forms.NumericUpDown();
            this.PublicHours = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.FamilyTotal = new System.Windows.Forms.NumericUpDown();
            this.EnergyTotal = new System.Windows.Forms.NumericUpDown();
            this.FunTotal = new System.Windows.Forms.NumericUpDown();
            this.PublicTotal = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.FunHours = new System.Windows.Forms.NumericUpDown();
            this.WorkHygiene = new System.Windows.Forms.NumericUpDown();
            this.FamilyHours = new System.Windows.Forms.NumericUpDown();
            this.EnvironmentHours = new System.Windows.Forms.NumericUpDown();
            this.ThirstTotal = new System.Windows.Forms.NumericUpDown();
            this.gbJLHoursWages = new System.Windows.Forms.GroupBox();
            this.HoursWagesList = new System.Windows.Forms.ListView();
            this.HwLvl = new System.Windows.Forms.ColumnHeader();
            this.HwStart = new System.Windows.Forms.ColumnHeader();
            this.HwHours = new System.Windows.Forms.ColumnHeader();
            this.HwEnd = new System.Windows.Forms.ColumnHeader();
            this.HwWages = new System.Windows.Forms.ColumnHeader();
            this.HwDogWages = new System.Windows.Forms.ColumnHeader();
            this.HwCatWages = new System.Windows.Forms.ColumnHeader();
            this.HwMon = new System.Windows.Forms.ColumnHeader();
            this.HwTue = new System.Windows.Forms.ColumnHeader();
            this.HwWed = new System.Windows.Forms.ColumnHeader();
            this.HwThu = new System.Windows.Forms.ColumnHeader();
            this.HwFri = new System.Windows.Forms.ColumnHeader();
            this.HwSat = new System.Windows.Forms.ColumnHeader();
            this.HwSun = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbJobDetails = new System.Windows.Forms.GroupBox();
            this.JobDetailsCopy = new System.Windows.Forms.LinkLabel();
            this.jdpFemale = new SimPe.Plugin.JobDescPanel();
            this.jdpMale = new SimPe.Plugin.JobDescPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Vehicle = new System.Windows.Forms.ComboBox();
            this.Outfit = new System.Windows.Forms.ComboBox();
            this.gbJLDetails = new System.Windows.Forms.GroupBox();
            this.JobDetailList = new System.Windows.Forms.ListView();
            this.JdLvl = new System.Windows.Forms.ColumnHeader();
            this.JdJobTitle = new System.Windows.Forms.ColumnHeader();
            this.JdDesc = new System.Windows.Forms.ColumnHeader();
            this.JdOutfit = new System.Windows.Forms.ColumnHeader();
            this.JdVehicle = new System.Windows.Forms.ColumnHeader();
            this.CareerLvls = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CareerTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Language = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.CareerReward = new System.Windows.Forms.ComboBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.miInsertLvl = new System.Windows.Forms.MenuItem();
            this.miRemoveLvl = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miEnglishOnly = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.tabPage4.SuspendLayout();
            this.flpChanceCards.SuspendLayout();
            this.flpChanceHeader.SuspendLayout();
            this.flpChanceText.SuspendLayout();
            this.flpCTMale.SuspendLayout();
            this.flpCTFemale.SuspendLayout();
            this.tcChanceOutcome.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.gbJLPromo.SuspendLayout();
            this.gbPromo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PromoFriends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCleaning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCreativity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCharisma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoMechanical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCooking)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.gbHoursWages.SuspendLayout();
            this.flpHoursWages.SuspendLayout();
            this.flpWork.SuspendLayout();
            this.flpWages.SuspendLayout();
            this.flpWorkDays.SuspendLayout();
            this.gbHWMotives.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComfortHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HygieneTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BladderTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkBladder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkComfort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HungerHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPublic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkHunger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BladderHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComfortTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HungerTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HygieneHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirstHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkFun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkThirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkFamily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkEnvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublicHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FamilyTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublicTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkHygiene)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FamilyHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirstTotal)).BeginInit();
            this.gbJLHoursWages.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbJobDetails.SuspendLayout();
            this.gbJLDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CareerLvls)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flpChanceCards);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(976, 598);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Chance Cards";
            // 
            // flpChanceCards
            // 
            this.flpChanceCards.Controls.Add(this.flpChanceHeader);
            this.flpChanceCards.Controls.Add(this.cpChoiceA);
            this.flpChanceCards.Controls.Add(this.cpChoiceB);
            this.flpChanceCards.Controls.Add(this.flpChanceText);
            this.flpChanceCards.Controls.Add(this.tcChanceOutcome);
            this.flpChanceCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpChanceCards.Location = new System.Drawing.Point(0, 0);
            this.flpChanceCards.Name = "flpChanceCards";
            this.flpChanceCards.Size = new System.Drawing.Size(980, 605);
            this.flpChanceCards.TabIndex = 1;
            // 
            // flpChanceHeader
            // 
            this.flpChanceHeader.AutoSize = true;
            this.flpChanceHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpChanceHeader.Controls.Add(this.lnudChanceCurrentLevel);
            this.flpChanceHeader.Controls.Add(this.lnudChancePercent);
            this.flpChanceHeader.Location = new System.Drawing.Point(0, 0);
            this.flpChanceHeader.Margin = new System.Windows.Forms.Padding(0);
            this.flpChanceHeader.Name = "flpChanceHeader";
            this.flpChanceHeader.Size = new System.Drawing.Size(392, 28);
            this.flpChanceHeader.TabIndex = 1;
            // 
            // lnudChanceCurrentLevel
            // 
            this.lnudChanceCurrentLevel.AutoSize = true;
            this.lnudChanceCurrentLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudChanceCurrentLevel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudChanceCurrentLevel.Label = "Current Level";
            this.lnudChanceCurrentLevel.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lnudChanceCurrentLevel.Location = new System.Drawing.Point(0, 0);
            this.lnudChanceCurrentLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lnudChanceCurrentLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lnudChanceCurrentLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudChanceCurrentLevel.Name = "lnudChanceCurrentLevel";
            this.lnudChanceCurrentLevel.NoLabel = false;
            this.lnudChanceCurrentLevel.ReadOnly = false;
            this.lnudChanceCurrentLevel.Size = new System.Drawing.Size(162, 28);
            this.lnudChanceCurrentLevel.TabIndex = 1;
            this.lnudChanceCurrentLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudChanceCurrentLevel.ValueSize = new System.Drawing.Size(57, 22);
            this.lnudChanceCurrentLevel.ValueChanged += new System.EventHandler(this.lnudChanceCurrentLevel_ValueChanged);
            // 
            // lnudChancePercent
            // 
            this.lnudChancePercent.AutoSize = true;
            this.lnudChancePercent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudChancePercent.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudChancePercent.Label = "Chance of Happening %";
            this.lnudChancePercent.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lnudChancePercent.Location = new System.Drawing.Point(162, 0);
            this.lnudChancePercent.Margin = new System.Windows.Forms.Padding(0);
            this.lnudChancePercent.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.lnudChancePercent.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudChancePercent.Name = "lnudChancePercent";
            this.lnudChancePercent.NoLabel = false;
            this.lnudChancePercent.ReadOnly = false;
            this.lnudChancePercent.Size = new System.Drawing.Size(230, 28);
            this.lnudChancePercent.TabIndex = 2;
            this.lnudChancePercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudChancePercent.ValueSize = new System.Drawing.Size(57, 22);
            // 
            // cpChoiceA
            // 
            this.cpChoiceA.AutoSize = true;
            this.cpChoiceA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cpChoiceA.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Label = "ChoiceA";
            this.cpChoiceA.Labels = true;
            this.cpChoiceA.Location = new System.Drawing.Point(0, 28);
            this.cpChoiceA.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Margin = new System.Windows.Forms.Padding(0);
            this.cpChoiceA.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceA.Name = "cpChoiceA";
            this.cpChoiceA.Size = new System.Drawing.Size(974, 54);
            this.cpChoiceA.TabIndex = 2;
            this.cpChoiceA.Value = "ChoiceA";
            this.cpChoiceA.ValueWidth = 300;
            // 
            // cpChoiceB
            // 
            this.cpChoiceB.AutoSize = true;
            this.cpChoiceB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cpChoiceB.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Label = "ChoiceB";
            this.cpChoiceB.Labels = false;
            this.cpChoiceB.Location = new System.Drawing.Point(0, 82);
            this.cpChoiceB.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Margin = new System.Windows.Forms.Padding(0);
            this.cpChoiceB.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cpChoiceB.Name = "cpChoiceB";
            this.cpChoiceB.Size = new System.Drawing.Size(974, 28);
            this.cpChoiceB.TabIndex = 3;
            this.cpChoiceB.Value = "ChoiceB";
            this.cpChoiceB.ValueWidth = 300;
            // 
            // flpChanceText
            // 
            this.flpChanceText.AutoSize = true;
            this.flpChanceText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpChanceText.Controls.Add(this.flpCTMale);
            this.flpChanceText.Controls.Add(this.ChanceCopy);
            this.flpChanceText.Controls.Add(this.flpCTFemale);
            this.flpChanceText.Location = new System.Drawing.Point(3, 113);
            this.flpChanceText.Name = "flpChanceText";
            this.flpChanceText.Size = new System.Drawing.Size(971, 159);
            this.flpChanceText.TabIndex = 4;
            // 
            // flpCTMale
            // 
            this.flpCTMale.AutoSize = true;
            this.flpCTMale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCTMale.Controls.Add(this.label51);
            this.flpCTMale.Controls.Add(this.ChanceTextMale);
            this.flpCTMale.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCTMale.Location = new System.Drawing.Point(0, 0);
            this.flpCTMale.Margin = new System.Windows.Forms.Padding(0);
            this.flpCTMale.Name = "flpCTMale";
            this.flpCTMale.Size = new System.Drawing.Size(454, 159);
            this.flpCTMale.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(0, 0);
            this.label51.Margin = new System.Windows.Forms.Padding(0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(69, 17);
            this.label51.TabIndex = 1;
            this.label51.Text = "Text Male";
            // 
            // ChanceTextMale
            // 
            this.ChanceTextMale.Location = new System.Drawing.Point(3, 20);
            this.ChanceTextMale.Multiline = true;
            this.ChanceTextMale.Name = "ChanceTextMale";
            this.ChanceTextMale.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChanceTextMale.Size = new System.Drawing.Size(448, 136);
            this.ChanceTextMale.TabIndex = 2;
            this.ChanceTextMale.Text = "textBox3";
            // 
            // ChanceCopy
            // 
            this.ChanceCopy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ChanceCopy.AutoSize = true;
            this.ChanceCopy.Location = new System.Drawing.Point(457, 71);
            this.ChanceCopy.Name = "ChanceCopy";
            this.ChanceCopy.Size = new System.Drawing.Size(57, 17);
            this.ChanceCopy.TabIndex = 3;
            this.ChanceCopy.TabStop = true;
            this.ChanceCopy.Text = "Copy ->";
            // 
            // flpCTFemale
            // 
            this.flpCTFemale.AutoSize = true;
            this.flpCTFemale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCTFemale.Controls.Add(this.label52);
            this.flpCTFemale.Controls.Add(this.ChanceTextFemale);
            this.flpCTFemale.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCTFemale.Location = new System.Drawing.Point(517, 0);
            this.flpCTFemale.Margin = new System.Windows.Forms.Padding(0);
            this.flpCTFemale.Name = "flpCTFemale";
            this.flpCTFemale.Size = new System.Drawing.Size(454, 159);
            this.flpCTFemale.TabIndex = 2;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Margin = new System.Windows.Forms.Padding(0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(85, 17);
            this.label52.TabIndex = 1;
            this.label52.Text = "Text Female";
            // 
            // ChanceTextFemale
            // 
            this.ChanceTextFemale.Location = new System.Drawing.Point(3, 20);
            this.ChanceTextFemale.Multiline = true;
            this.ChanceTextFemale.Name = "ChanceTextFemale";
            this.ChanceTextFemale.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChanceTextFemale.Size = new System.Drawing.Size(448, 136);
            this.ChanceTextFemale.TabIndex = 2;
            this.ChanceTextFemale.Text = "textBox4";
            // 
            // tcChanceOutcome
            // 
            this.tcChanceOutcome.Controls.Add(this.tabPage5);
            this.tcChanceOutcome.Controls.Add(this.tabPage6);
            this.tcChanceOutcome.Controls.Add(this.tabPage7);
            this.tcChanceOutcome.Controls.Add(this.tabPage8);
            this.tcChanceOutcome.Location = new System.Drawing.Point(3, 278);
            this.tcChanceOutcome.Name = "tcChanceOutcome";
            this.tcChanceOutcome.SelectedIndex = 0;
            this.tcChanceOutcome.Size = new System.Drawing.Size(972, 313);
            this.tcChanceOutcome.TabIndex = 5;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.epPassA);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(964, 284);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Pass A";
            // 
            // epPassA
            // 
            this.epPassA.AutoSize = true;
            this.epPassA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.epPassA.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Female = "textBox2";
            this.epPassA.IsPetCareer = false;
            this.epPassA.JobLevels = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Location = new System.Drawing.Point(0, 0);
            this.epPassA.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Male = "textBox1";
            this.epPassA.Margin = new System.Windows.Forms.Padding(0);
            this.epPassA.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.MinimumSize = new System.Drawing.Size(824, 131);
            this.epPassA.Money = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassA.Name = "epPassA";
            this.epPassA.Size = new System.Drawing.Size(963, 281);
            this.epPassA.TabIndex = 0;
            this.epPassA.TextSize = new System.Drawing.Size(450, 208);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.epFailA);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(964, 284);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Fail A";
            // 
            // epFailA
            // 
            this.epFailA.AutoSize = true;
            this.epFailA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.epFailA.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Female = "textBox2";
            this.epFailA.IsPetCareer = false;
            this.epFailA.JobLevels = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Location = new System.Drawing.Point(0, 0);
            this.epFailA.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Male = "textBox1";
            this.epFailA.Margin = new System.Windows.Forms.Padding(0);
            this.epFailA.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.MinimumSize = new System.Drawing.Size(824, 131);
            this.epFailA.Money = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailA.Name = "epFailA";
            this.epFailA.Size = new System.Drawing.Size(963, 273);
            this.epFailA.TabIndex = 1;
            this.epFailA.TextSize = new System.Drawing.Size(450, 200);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.epPassB);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(964, 284);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Pass B";
            // 
            // epPassB
            // 
            this.epPassB.AutoSize = true;
            this.epPassB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.epPassB.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Female = "textBox2";
            this.epPassB.IsPetCareer = false;
            this.epPassB.JobLevels = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Location = new System.Drawing.Point(0, 0);
            this.epPassB.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Male = "textBox1";
            this.epPassB.Margin = new System.Windows.Forms.Padding(0);
            this.epPassB.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.MinimumSize = new System.Drawing.Size(824, 131);
            this.epPassB.Money = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epPassB.Name = "epPassB";
            this.epPassB.Size = new System.Drawing.Size(963, 273);
            this.epPassB.TabIndex = 1;
            this.epPassB.TextSize = new System.Drawing.Size(450, 200);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.epFailB);
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(964, 284);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "Fail B";
            // 
            // epFailB
            // 
            this.epFailB.AutoSize = true;
            this.epFailB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.epFailB.Body = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Charisma = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Cleaning = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Cooking = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Creativity = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Female = "textBox2";
            this.epFailB.IsPetCareer = false;
            this.epFailB.JobLevels = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Location = new System.Drawing.Point(0, 0);
            this.epFailB.Logic = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Male = "textBox1";
            this.epFailB.Margin = new System.Windows.Forms.Padding(0);
            this.epFailB.Mechanical = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.MinimumSize = new System.Drawing.Size(824, 131);
            this.epFailB.Money = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.epFailB.Name = "epFailB";
            this.epFailB.Size = new System.Drawing.Size(963, 273);
            this.epFailB.TabIndex = 1;
            this.epFailB.TextSize = new System.Drawing.Size(450, 200);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gbJLPromo);
            this.tabPage3.Controls.Add(this.gbPromo);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(976, 598);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Promotion";
            // 
            // gbJLPromo
            // 
            this.gbJLPromo.AutoSize = true;
            this.gbJLPromo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbJLPromo.Controls.Add(this.PromoList);
            this.gbJLPromo.Location = new System.Drawing.Point(10, 8);
            this.gbJLPromo.Name = "gbJLPromo";
            this.gbJLPromo.Size = new System.Drawing.Size(960, 339);
            this.gbJLPromo.TabIndex = 1;
            this.gbJLPromo.TabStop = false;
            this.gbJLPromo.Text = "Job Levels";
            // 
            // PromoList
            // 
            this.PromoList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PrLvl,
            this.PrCooking,
            this.PrMechanical,
            this.PrBody,
            this.PrCharisma,
            this.PrCreativity,
            this.PrLogic,
            this.PrCleaning,
            this.PrFriends,
            this.PrTrick});
            this.PromoList.FullRowSelect = true;
            this.PromoList.GridLines = true;
            this.PromoList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PromoList.HideSelection = false;
            this.PromoList.Location = new System.Drawing.Point(10, 18);
            this.PromoList.MultiSelect = false;
            this.PromoList.Name = "PromoList";
            this.PromoList.Size = new System.Drawing.Size(944, 300);
            this.PromoList.TabIndex = 1;
            this.PromoList.UseCompatibleStateImageBehavior = false;
            this.PromoList.View = System.Windows.Forms.View.Details;
            this.PromoList.SelectedIndexChanged += new System.EventHandler(this.PromoList_SelectedIndexChanged);
            // 
            // PrLvl
            // 
            this.PrLvl.Text = "Lvl";
            this.PrLvl.Width = 32;
            // 
            // PrCooking
            // 
            this.PrCooking.Text = "Cooking";
            this.PrCooking.Width = 64;
            // 
            // PrMechanical
            // 
            this.PrMechanical.Text = "Mechanical";
            this.PrMechanical.Width = 84;
            // 
            // PrBody
            // 
            this.PrBody.Text = "Body";
            this.PrBody.Width = 64;
            // 
            // PrCharisma
            // 
            this.PrCharisma.Text = "Charisma";
            this.PrCharisma.Width = 80;
            // 
            // PrCreativity
            // 
            this.PrCreativity.Text = "Creativity";
            this.PrCreativity.Width = 84;
            // 
            // PrLogic
            // 
            this.PrLogic.Text = "Logic";
            this.PrLogic.Width = 64;
            // 
            // PrCleaning
            // 
            this.PrCleaning.Text = "Cleaning";
            this.PrCleaning.Width = 84;
            // 
            // PrFriends
            // 
            this.PrFriends.Text = "Friends";
            this.PrFriends.Width = 64;
            // 
            // PrTrick
            // 
            this.PrTrick.Text = "Trick";
            this.PrTrick.Width = 113;
            // 
            // gbPromo
            // 
            this.gbPromo.Controls.Add(this.cbTrick);
            this.gbPromo.Controls.Add(this.label101);
            this.gbPromo.Controls.Add(this.label41);
            this.gbPromo.Controls.Add(this.label40);
            this.gbPromo.Controls.Add(this.label39);
            this.gbPromo.Controls.Add(this.label38);
            this.gbPromo.Controls.Add(this.label37);
            this.gbPromo.Controls.Add(this.label36);
            this.gbPromo.Controls.Add(this.label35);
            this.gbPromo.Controls.Add(this.label34);
            this.gbPromo.Controls.Add(this.PromoFriends);
            this.gbPromo.Controls.Add(this.PromoCleaning);
            this.gbPromo.Controls.Add(this.PromoLogic);
            this.gbPromo.Controls.Add(this.PromoCreativity);
            this.gbPromo.Controls.Add(this.PromoCharisma);
            this.gbPromo.Controls.Add(this.PromoBody);
            this.gbPromo.Controls.Add(this.PromoMechanical);
            this.gbPromo.Controls.Add(this.PromoCooking);
            this.gbPromo.Location = new System.Drawing.Point(10, 356);
            this.gbPromo.Name = "gbPromo";
            this.gbPromo.Size = new System.Drawing.Size(960, 231);
            this.gbPromo.TabIndex = 2;
            this.gbPromo.TabStop = false;
            this.gbPromo.Text = "Current Level";
            // 
            // cbTrick
            // 
            this.cbTrick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrick.FormattingEnabled = true;
            this.cbTrick.Items.AddRange(new object[] {
            "None",
            "Stay",
            "Come Here",
            "Play Dead",
            "Speak",
            "Shake",
            "Sit Up",
            "Roll Over"});
            this.cbTrick.Location = new System.Drawing.Point(322, 51);
            this.cbTrick.Name = "cbTrick";
            this.cbTrick.Size = new System.Drawing.Size(157, 24);
            this.cbTrick.TabIndex = 18;
            this.cbTrick.SelectedIndexChanged += new System.EventHandler(this.cbTrick_SelectedIndexChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(184, 54);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(39, 17);
            this.label101.TabIndex = 17;
            this.label101.Text = "Trick";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(184, 25);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(99, 17);
            this.label41.TabIndex = 15;
            this.label41.Text = "Family Friends";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(10, 191);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(63, 17);
            this.label40.TabIndex = 13;
            this.label40.Text = "Cleaning";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(10, 163);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(42, 17);
            this.label39.TabIndex = 11;
            this.label39.Text = "Logic";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 135);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(66, 17);
            this.label38.TabIndex = 9;
            this.label38.Text = "Creativity";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(10, 107);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(67, 17);
            this.label37.TabIndex = 7;
            this.label37.Text = "Charisma";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(10, 79);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(40, 17);
            this.label36.TabIndex = 5;
            this.label36.Text = "Body";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(10, 51);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 17);
            this.label35.TabIndex = 3;
            this.label35.Text = "Mechanical";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 17);
            this.label34.TabIndex = 1;
            this.label34.Text = "Cooking";
            // 
            // PromoFriends
            // 
            this.PromoFriends.Location = new System.Drawing.Point(322, 23);
            this.PromoFriends.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.PromoFriends.Name = "PromoFriends";
            this.PromoFriends.Size = new System.Drawing.Size(57, 22);
            this.PromoFriends.TabIndex = 16;
            this.PromoFriends.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoFriends.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoCleaning
            // 
            this.PromoCleaning.Location = new System.Drawing.Point(104, 189);
            this.PromoCleaning.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoCleaning.Name = "PromoCleaning";
            this.PromoCleaning.Size = new System.Drawing.Size(57, 22);
            this.PromoCleaning.TabIndex = 14;
            this.PromoCleaning.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoCleaning.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoLogic
            // 
            this.PromoLogic.Location = new System.Drawing.Point(104, 161);
            this.PromoLogic.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoLogic.Name = "PromoLogic";
            this.PromoLogic.Size = new System.Drawing.Size(57, 22);
            this.PromoLogic.TabIndex = 12;
            this.PromoLogic.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoLogic.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoCreativity
            // 
            this.PromoCreativity.Location = new System.Drawing.Point(104, 133);
            this.PromoCreativity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoCreativity.Name = "PromoCreativity";
            this.PromoCreativity.Size = new System.Drawing.Size(57, 22);
            this.PromoCreativity.TabIndex = 10;
            this.PromoCreativity.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoCreativity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoCharisma
            // 
            this.PromoCharisma.Location = new System.Drawing.Point(104, 105);
            this.PromoCharisma.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoCharisma.Name = "PromoCharisma";
            this.PromoCharisma.Size = new System.Drawing.Size(57, 22);
            this.PromoCharisma.TabIndex = 8;
            this.PromoCharisma.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoCharisma.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoBody
            // 
            this.PromoBody.Location = new System.Drawing.Point(104, 77);
            this.PromoBody.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoBody.Name = "PromoBody";
            this.PromoBody.Size = new System.Drawing.Size(57, 22);
            this.PromoBody.TabIndex = 6;
            this.PromoBody.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoBody.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoMechanical
            // 
            this.PromoMechanical.Location = new System.Drawing.Point(104, 49);
            this.PromoMechanical.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoMechanical.Name = "PromoMechanical";
            this.PromoMechanical.Size = new System.Drawing.Size(57, 22);
            this.PromoMechanical.TabIndex = 4;
            this.PromoMechanical.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoMechanical.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // PromoCooking
            // 
            this.PromoCooking.Location = new System.Drawing.Point(104, 21);
            this.PromoCooking.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PromoCooking.Name = "PromoCooking";
            this.PromoCooking.Size = new System.Drawing.Size(57, 22);
            this.PromoCooking.TabIndex = 2;
            this.PromoCooking.ValueChanged += new System.EventHandler(this.Promo_ValueChanged);
            this.PromoCooking.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Promo_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbHoursWages);
            this.tabPage2.Controls.Add(this.gbJLHoursWages);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(976, 598);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hours & Wages";
            // 
            // gbHoursWages
            // 
            this.gbHoursWages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbHoursWages.Controls.Add(this.flpHoursWages);
            this.gbHoursWages.Location = new System.Drawing.Point(10, 356);
            this.gbHoursWages.Name = "gbHoursWages";
            this.gbHoursWages.Size = new System.Drawing.Size(960, 231);
            this.gbHoursWages.TabIndex = 2;
            this.gbHoursWages.TabStop = false;
            this.gbHoursWages.Text = "Current Level";
            // 
            // flpHoursWages
            // 
            this.flpHoursWages.AutoSize = true;
            this.flpHoursWages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpHoursWages.Controls.Add(this.flpWork);
            this.flpHoursWages.Controls.Add(this.flpWages);
            this.flpHoursWages.Controls.Add(this.flpWorkDays);
            this.flpHoursWages.Controls.Add(this.gbHWMotives);
            this.flpHoursWages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpHoursWages.Location = new System.Drawing.Point(9, 19);
            this.flpHoursWages.Name = "flpHoursWages";
            this.flpHoursWages.Size = new System.Drawing.Size(943, 204);
            this.flpHoursWages.TabIndex = 1;
            // 
            // flpWork
            // 
            this.flpWork.AutoSize = true;
            this.flpWork.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpWork.Controls.Add(this.lnudWorkStart);
            this.flpWork.Controls.Add(this.lnudWorkHours);
            this.flpWork.Controls.Add(this.lnudWorkFinish);
            this.flpWork.Location = new System.Drawing.Point(3, 3);
            this.flpWork.Name = "flpWork";
            this.flpWork.Size = new System.Drawing.Size(348, 28);
            this.flpWork.TabIndex = 1;
            // 
            // lnudWorkStart
            // 
            this.lnudWorkStart.AutoSize = true;
            this.lnudWorkStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWorkStart.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWorkStart.Label = "Start";
            this.lnudWorkStart.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWorkStart.Location = new System.Drawing.Point(0, 0);
            this.lnudWorkStart.Margin = new System.Windows.Forms.Padding(0);
            this.lnudWorkStart.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.lnudWorkStart.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWorkStart.Name = "lnudWorkStart";
            this.lnudWorkStart.NoLabel = false;
            this.lnudWorkStart.ReadOnly = false;
            this.lnudWorkStart.Size = new System.Drawing.Size(107, 28);
            this.lnudWorkStart.TabIndex = 1;
            this.lnudWorkStart.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWorkStart.ValueSize = new System.Drawing.Size(57, 22);
            this.lnudWorkStart.ValueChanged += new System.EventHandler(this.lnudWork_ValueChanged);
            this.lnudWorkStart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lnudWork_KeyUp);
            // 
            // lnudWorkHours
            // 
            this.lnudWorkHours.AutoSize = true;
            this.lnudWorkHours.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWorkHours.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWorkHours.Label = "Hours";
            this.lnudWorkHours.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWorkHours.Location = new System.Drawing.Point(113, 0);
            this.lnudWorkHours.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lnudWorkHours.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.lnudWorkHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudWorkHours.Name = "lnudWorkHours";
            this.lnudWorkHours.NoLabel = false;
            this.lnudWorkHours.ReadOnly = false;
            this.lnudWorkHours.Size = new System.Drawing.Size(115, 28);
            this.lnudWorkHours.TabIndex = 2;
            this.lnudWorkHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudWorkHours.ValueSize = new System.Drawing.Size(57, 22);
            this.lnudWorkHours.ValueChanged += new System.EventHandler(this.lnudWork_ValueChanged);
            this.lnudWorkHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lnudWork_KeyUp);
            // 
            // lnudWorkFinish
            // 
            this.lnudWorkFinish.AutoSize = true;
            this.lnudWorkFinish.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWorkFinish.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWorkFinish.Label = "Finish";
            this.lnudWorkFinish.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWorkFinish.Location = new System.Drawing.Point(234, 0);
            this.lnudWorkFinish.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lnudWorkFinish.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.lnudWorkFinish.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudWorkFinish.Name = "lnudWorkFinish";
            this.lnudWorkFinish.NoLabel = false;
            this.lnudWorkFinish.ReadOnly = true;
            this.lnudWorkFinish.Size = new System.Drawing.Size(114, 28);
            this.lnudWorkFinish.TabIndex = 0;
            this.lnudWorkFinish.TabStop = false;
            this.lnudWorkFinish.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lnudWorkFinish.ValueSize = new System.Drawing.Size(57, 22);
            // 
            // flpWages
            // 
            this.flpWages.AutoSize = true;
            this.flpWages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpWages.Controls.Add(this.lnudWages);
            this.flpWages.Controls.Add(this.lnudWagesDog);
            this.flpWages.Controls.Add(this.lnudWagesCat);
            this.flpWages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpWages.Location = new System.Drawing.Point(3, 37);
            this.flpWages.Name = "flpWages";
            this.flpWages.Size = new System.Drawing.Size(204, 84);
            this.flpWages.TabIndex = 2;
            // 
            // lnudWages
            // 
            this.lnudWages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWages.AutoSize = true;
            this.lnudWages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWages.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWages.Label = "Wages";
            this.lnudWages.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWages.Location = new System.Drawing.Point(40, 0);
            this.lnudWages.Margin = new System.Windows.Forms.Padding(0);
            this.lnudWages.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lnudWages.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWages.Name = "lnudWages";
            this.lnudWages.NoLabel = false;
            this.lnudWages.ReadOnly = false;
            this.lnudWages.Size = new System.Drawing.Size(164, 28);
            this.lnudWages.TabIndex = 1;
            this.lnudWages.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWages.ValueSize = new System.Drawing.Size(100, 22);
            this.lnudWages.ValueChanged += new System.EventHandler(this.lnudWork_ValueChanged);
            this.lnudWages.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lnudWork_KeyUp);
            // 
            // lnudWagesDog
            // 
            this.lnudWagesDog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWagesDog.AutoSize = true;
            this.lnudWagesDog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWagesDog.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWagesDog.Label = "Wages (Dog)";
            this.lnudWagesDog.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lnudWagesDog.Location = new System.Drawing.Point(0, 28);
            this.lnudWagesDog.Margin = new System.Windows.Forms.Padding(0);
            this.lnudWagesDog.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lnudWagesDog.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWagesDog.Name = "lnudWagesDog";
            this.lnudWagesDog.NoLabel = false;
            this.lnudWagesDog.ReadOnly = false;
            this.lnudWagesDog.Size = new System.Drawing.Size(204, 28);
            this.lnudWagesDog.TabIndex = 2;
            this.lnudWagesDog.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWagesDog.ValueSize = new System.Drawing.Size(100, 22);
            this.lnudWagesDog.ValueChanged += new System.EventHandler(this.lnudWork_ValueChanged);
            this.lnudWagesDog.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lnudWork_KeyUp);
            // 
            // lnudWagesCat
            // 
            this.lnudWagesCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnudWagesCat.AutoSize = true;
            this.lnudWagesCat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lnudWagesCat.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.lnudWagesCat.Label = "Wages (Cat)";
            this.lnudWagesCat.LabelAnchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lnudWagesCat.Location = new System.Drawing.Point(5, 56);
            this.lnudWagesCat.Margin = new System.Windows.Forms.Padding(0);
            this.lnudWagesCat.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lnudWagesCat.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWagesCat.Name = "lnudWagesCat";
            this.lnudWagesCat.NoLabel = false;
            this.lnudWagesCat.ReadOnly = false;
            this.lnudWagesCat.Size = new System.Drawing.Size(199, 28);
            this.lnudWagesCat.TabIndex = 3;
            this.lnudWagesCat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lnudWagesCat.ValueSize = new System.Drawing.Size(100, 22);
            this.lnudWagesCat.ValueChanged += new System.EventHandler(this.lnudWork_ValueChanged);
            this.lnudWagesCat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lnudWork_KeyUp);
            // 
            // flpWorkDays
            // 
            this.flpWorkDays.AutoSize = true;
            this.flpWorkDays.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpWorkDays.Controls.Add(this.WorkMonday);
            this.flpWorkDays.Controls.Add(this.WorkTuesday);
            this.flpWorkDays.Controls.Add(this.WorkWednesday);
            this.flpWorkDays.Controls.Add(this.WorkThursday);
            this.flpWorkDays.Controls.Add(this.WorkFriday);
            this.flpWorkDays.Controls.Add(this.WorkSaturday);
            this.flpWorkDays.Controls.Add(this.WorkSunday);
            this.flpHoursWages.SetFlowBreak(this.flpWorkDays, true);
            this.flpWorkDays.Location = new System.Drawing.Point(3, 127);
            this.flpWorkDays.Name = "flpWorkDays";
            this.flpWorkDays.Size = new System.Drawing.Size(287, 54);
            this.flpWorkDays.TabIndex = 3;
            // 
            // WorkMonday
            // 
            this.WorkMonday.AutoSize = true;
            this.WorkMonday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkMonday.Location = new System.Drawing.Point(3, 3);
            this.WorkMonday.Name = "WorkMonday";
            this.WorkMonday.Size = new System.Drawing.Size(54, 21);
            this.WorkMonday.TabIndex = 1;
            this.WorkMonday.Text = "Mon";
            this.WorkMonday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkTuesday
            // 
            this.WorkTuesday.AutoSize = true;
            this.WorkTuesday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkTuesday.Location = new System.Drawing.Point(63, 3);
            this.WorkTuesday.Name = "WorkTuesday";
            this.WorkTuesday.Size = new System.Drawing.Size(52, 21);
            this.WorkTuesday.TabIndex = 2;
            this.WorkTuesday.Text = "Tue";
            this.WorkTuesday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkWednesday
            // 
            this.WorkWednesday.AutoSize = true;
            this.WorkWednesday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkWednesday.Location = new System.Drawing.Point(121, 3);
            this.WorkWednesday.Name = "WorkWednesday";
            this.WorkWednesday.Size = new System.Drawing.Size(56, 21);
            this.WorkWednesday.TabIndex = 3;
            this.WorkWednesday.Text = "Wed";
            this.WorkWednesday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkThursday
            // 
            this.WorkThursday.AutoSize = true;
            this.WorkThursday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkThursday.Location = new System.Drawing.Point(183, 3);
            this.WorkThursday.Name = "WorkThursday";
            this.WorkThursday.Size = new System.Drawing.Size(52, 21);
            this.WorkThursday.TabIndex = 4;
            this.WorkThursday.Text = "Thu";
            this.WorkThursday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkFriday
            // 
            this.WorkFriday.AutoSize = true;
            this.WorkFriday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.flpWorkDays.SetFlowBreak(this.WorkFriday, true);
            this.WorkFriday.Location = new System.Drawing.Point(241, 3);
            this.WorkFriday.Name = "WorkFriday";
            this.WorkFriday.Size = new System.Drawing.Size(43, 21);
            this.WorkFriday.TabIndex = 5;
            this.WorkFriday.Text = "Fri";
            this.WorkFriday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkSaturday
            // 
            this.WorkSaturday.AutoSize = true;
            this.WorkSaturday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkSaturday.Location = new System.Drawing.Point(3, 30);
            this.WorkSaturday.Name = "WorkSaturday";
            this.WorkSaturday.Size = new System.Drawing.Size(48, 21);
            this.WorkSaturday.TabIndex = 6;
            this.WorkSaturday.Text = "Sat";
            this.WorkSaturday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // WorkSunday
            // 
            this.WorkSunday.AutoSize = true;
            this.WorkSunday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WorkSunday.Location = new System.Drawing.Point(57, 30);
            this.WorkSunday.Name = "WorkSunday";
            this.WorkSunday.Size = new System.Drawing.Size(52, 21);
            this.WorkSunday.TabIndex = 7;
            this.WorkSunday.Text = "Sun";
            this.WorkSunday.CheckedChanged += new System.EventHandler(this.Workday_CheckedChanged);
            // 
            // gbHWMotives
            // 
            this.gbHWMotives.AutoSize = true;
            this.gbHWMotives.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbHWMotives.Controls.Add(this.label27);
            this.gbHWMotives.Controls.Add(this.label24);
            this.gbHWMotives.Controls.Add(this.ComfortHours);
            this.gbHWMotives.Controls.Add(this.HygieneTotal);
            this.gbHWMotives.Controls.Add(this.BladderTotal);
            this.gbHWMotives.Controls.Add(this.label21);
            this.gbHWMotives.Controls.Add(this.WorkBladder);
            this.gbHWMotives.Controls.Add(this.label23);
            this.gbHWMotives.Controls.Add(this.label19);
            this.gbHWMotives.Controls.Add(this.WorkComfort);
            this.gbHWMotives.Controls.Add(this.HungerHours);
            this.gbHWMotives.Controls.Add(this.EnergyHours);
            this.gbHWMotives.Controls.Add(this.label25);
            this.gbHWMotives.Controls.Add(this.label18);
            this.gbHWMotives.Controls.Add(this.WorkPublic);
            this.gbHWMotives.Controls.Add(this.WorkHunger);
            this.gbHWMotives.Controls.Add(this.EnvironmentTotal);
            this.gbHWMotives.Controls.Add(this.BladderHours);
            this.gbHWMotives.Controls.Add(this.ComfortTotal);
            this.gbHWMotives.Controls.Add(this.label22);
            this.gbHWMotives.Controls.Add(this.HungerTotal);
            this.gbHWMotives.Controls.Add(this.HygieneHours);
            this.gbHWMotives.Controls.Add(this.ThirstHours);
            this.gbHWMotives.Controls.Add(this.WorkEnergy);
            this.gbHWMotives.Controls.Add(this.WorkFun);
            this.gbHWMotives.Controls.Add(this.WorkThirst);
            this.gbHWMotives.Controls.Add(this.WorkFamily);
            this.gbHWMotives.Controls.Add(this.WorkEnvironment);
            this.gbHWMotives.Controls.Add(this.PublicHours);
            this.gbHWMotives.Controls.Add(this.label20);
            this.gbHWMotives.Controls.Add(this.FamilyTotal);
            this.gbHWMotives.Controls.Add(this.EnergyTotal);
            this.gbHWMotives.Controls.Add(this.FunTotal);
            this.gbHWMotives.Controls.Add(this.PublicTotal);
            this.gbHWMotives.Controls.Add(this.label33);
            this.gbHWMotives.Controls.Add(this.label32);
            this.gbHWMotives.Controls.Add(this.label31);
            this.gbHWMotives.Controls.Add(this.label30);
            this.gbHWMotives.Controls.Add(this.label29);
            this.gbHWMotives.Controls.Add(this.label28);
            this.gbHWMotives.Controls.Add(this.label26);
            this.gbHWMotives.Controls.Add(this.FunHours);
            this.gbHWMotives.Controls.Add(this.WorkHygiene);
            this.gbHWMotives.Controls.Add(this.FamilyHours);
            this.gbHWMotives.Controls.Add(this.EnvironmentHours);
            this.gbHWMotives.Controls.Add(this.ThirstTotal);
            this.gbHWMotives.Location = new System.Drawing.Point(357, 3);
            this.gbHWMotives.Name = "gbHWMotives";
            this.gbHWMotives.Size = new System.Drawing.Size(583, 198);
            this.gbHWMotives.TabIndex = 4;
            this.gbHWMotives.TabStop = false;
            this.gbHWMotives.Text = "Motives";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(432, 18);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 17);
            this.label27.TabIndex = 64;
            this.label27.Text = "* NoHours";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(129, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 17);
            this.label24.TabIndex = 41;
            this.label24.Text = "* NoHours";
            // 
            // ComfortHours
            // 
            this.ComfortHours.AutoSize = true;
            this.ComfortHours.Enabled = false;
            this.ComfortHours.Location = new System.Drawing.Point(139, 101);
            this.ComfortHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ComfortHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.ComfortHours.Name = "ComfortHours";
            this.ComfortHours.ReadOnly = true;
            this.ComfortHours.Size = new System.Drawing.Size(60, 22);
            this.ComfortHours.TabIndex = 0;
            this.ComfortHours.TabStop = false;
            // 
            // HygieneTotal
            // 
            this.HygieneTotal.AutoSize = true;
            this.HygieneTotal.Enabled = false;
            this.HygieneTotal.Location = new System.Drawing.Point(205, 128);
            this.HygieneTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.HygieneTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.HygieneTotal.Name = "HygieneTotal";
            this.HygieneTotal.ReadOnly = true;
            this.HygieneTotal.Size = new System.Drawing.Size(68, 22);
            this.HygieneTotal.TabIndex = 0;
            this.HygieneTotal.TabStop = false;
            // 
            // BladderTotal
            // 
            this.BladderTotal.AutoSize = true;
            this.BladderTotal.Enabled = false;
            this.BladderTotal.Location = new System.Drawing.Point(205, 155);
            this.BladderTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.BladderTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.BladderTotal.Name = "BladderTotal";
            this.BladderTotal.ReadOnly = true;
            this.BladderTotal.Size = new System.Drawing.Size(68, 22);
            this.BladderTotal.TabIndex = 0;
            this.BladderTotal.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 128);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 17);
            this.label21.TabIndex = 7;
            this.label21.Text = "Hygiene";
            // 
            // WorkBladder
            // 
            this.WorkBladder.AutoSize = true;
            this.WorkBladder.Location = new System.Drawing.Point(72, 155);
            this.WorkBladder.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkBladder.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkBladder.Name = "WorkBladder";
            this.WorkBladder.Size = new System.Drawing.Size(60, 22);
            this.WorkBladder.TabIndex = 10;
            this.WorkBladder.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkBladder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(72, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(61, 17);
            this.label23.TabIndex = 40;
            this.label23.Text = "PerHour";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 46);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(55, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Hunger";
            // 
            // WorkComfort
            // 
            this.WorkComfort.AutoSize = true;
            this.WorkComfort.Location = new System.Drawing.Point(72, 101);
            this.WorkComfort.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkComfort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkComfort.Name = "WorkComfort";
            this.WorkComfort.Size = new System.Drawing.Size(60, 22);
            this.WorkComfort.TabIndex = 6;
            this.WorkComfort.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkComfort.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // HungerHours
            // 
            this.HungerHours.AutoSize = true;
            this.HungerHours.Enabled = false;
            this.HungerHours.Location = new System.Drawing.Point(139, 46);
            this.HungerHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HungerHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.HungerHours.Name = "HungerHours";
            this.HungerHours.ReadOnly = true;
            this.HungerHours.Size = new System.Drawing.Size(60, 22);
            this.HungerHours.TabIndex = 0;
            this.HungerHours.TabStop = false;
            // 
            // EnergyHours
            // 
            this.EnergyHours.AutoSize = true;
            this.EnergyHours.Enabled = false;
            this.EnergyHours.Location = new System.Drawing.Point(442, 46);
            this.EnergyHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.EnergyHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.EnergyHours.Name = "EnergyHours";
            this.EnergyHours.ReadOnly = true;
            this.EnergyHours.Size = new System.Drawing.Size(60, 22);
            this.EnergyHours.TabIndex = 0;
            this.EnergyHours.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(205, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(52, 17);
            this.label25.TabIndex = 42;
            this.label25.Text = "= Total";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 17);
            this.label18.TabIndex = 3;
            this.label18.Text = "Thirst";
            // 
            // WorkPublic
            // 
            this.WorkPublic.AutoSize = true;
            this.WorkPublic.Location = new System.Drawing.Point(375, 101);
            this.WorkPublic.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkPublic.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkPublic.Name = "WorkPublic";
            this.WorkPublic.Size = new System.Drawing.Size(60, 22);
            this.WorkPublic.TabIndex = 16;
            this.WorkPublic.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkPublic.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // WorkHunger
            // 
            this.WorkHunger.AutoSize = true;
            this.WorkHunger.Location = new System.Drawing.Point(72, 46);
            this.WorkHunger.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkHunger.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkHunger.Name = "WorkHunger";
            this.WorkHunger.Size = new System.Drawing.Size(60, 22);
            this.WorkHunger.TabIndex = 2;
            this.WorkHunger.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkHunger.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // EnvironmentTotal
            // 
            this.EnvironmentTotal.AutoSize = true;
            this.EnvironmentTotal.Enabled = false;
            this.EnvironmentTotal.Location = new System.Drawing.Point(509, 155);
            this.EnvironmentTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.EnvironmentTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.EnvironmentTotal.Name = "EnvironmentTotal";
            this.EnvironmentTotal.ReadOnly = true;
            this.EnvironmentTotal.Size = new System.Drawing.Size(68, 22);
            this.EnvironmentTotal.TabIndex = 0;
            this.EnvironmentTotal.TabStop = false;
            // 
            // BladderHours
            // 
            this.BladderHours.AutoSize = true;
            this.BladderHours.Enabled = false;
            this.BladderHours.Location = new System.Drawing.Point(139, 155);
            this.BladderHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.BladderHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.BladderHours.Name = "BladderHours";
            this.BladderHours.ReadOnly = true;
            this.BladderHours.Size = new System.Drawing.Size(60, 22);
            this.BladderHours.TabIndex = 0;
            this.BladderHours.TabStop = false;
            // 
            // ComfortTotal
            // 
            this.ComfortTotal.AutoSize = true;
            this.ComfortTotal.Enabled = false;
            this.ComfortTotal.Location = new System.Drawing.Point(205, 101);
            this.ComfortTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.ComfortTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.ComfortTotal.Name = "ComfortTotal";
            this.ComfortTotal.ReadOnly = true;
            this.ComfortTotal.Size = new System.Drawing.Size(68, 22);
            this.ComfortTotal.TabIndex = 0;
            this.ComfortTotal.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 155);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 17);
            this.label22.TabIndex = 9;
            this.label22.Text = "Bladder";
            // 
            // HungerTotal
            // 
            this.HungerTotal.AutoSize = true;
            this.HungerTotal.Enabled = false;
            this.HungerTotal.Location = new System.Drawing.Point(205, 46);
            this.HungerTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.HungerTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.HungerTotal.Name = "HungerTotal";
            this.HungerTotal.ReadOnly = true;
            this.HungerTotal.Size = new System.Drawing.Size(68, 22);
            this.HungerTotal.TabIndex = 0;
            this.HungerTotal.TabStop = false;
            // 
            // HygieneHours
            // 
            this.HygieneHours.AutoSize = true;
            this.HygieneHours.Enabled = false;
            this.HygieneHours.Location = new System.Drawing.Point(139, 128);
            this.HygieneHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HygieneHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.HygieneHours.Name = "HygieneHours";
            this.HygieneHours.ReadOnly = true;
            this.HygieneHours.Size = new System.Drawing.Size(60, 22);
            this.HygieneHours.TabIndex = 0;
            this.HygieneHours.TabStop = false;
            // 
            // ThirstHours
            // 
            this.ThirstHours.AutoSize = true;
            this.ThirstHours.Enabled = false;
            this.ThirstHours.Location = new System.Drawing.Point(139, 73);
            this.ThirstHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ThirstHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.ThirstHours.Name = "ThirstHours";
            this.ThirstHours.ReadOnly = true;
            this.ThirstHours.Size = new System.Drawing.Size(60, 22);
            this.ThirstHours.TabIndex = 0;
            this.ThirstHours.TabStop = false;
            // 
            // WorkEnergy
            // 
            this.WorkEnergy.AutoSize = true;
            this.WorkEnergy.Location = new System.Drawing.Point(375, 46);
            this.WorkEnergy.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkEnergy.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkEnergy.Name = "WorkEnergy";
            this.WorkEnergy.Size = new System.Drawing.Size(60, 22);
            this.WorkEnergy.TabIndex = 12;
            this.WorkEnergy.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkEnergy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // WorkFun
            // 
            this.WorkFun.AutoSize = true;
            this.WorkFun.Location = new System.Drawing.Point(375, 73);
            this.WorkFun.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkFun.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkFun.Name = "WorkFun";
            this.WorkFun.Size = new System.Drawing.Size(60, 22);
            this.WorkFun.TabIndex = 14;
            this.WorkFun.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkFun.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // WorkThirst
            // 
            this.WorkThirst.AutoSize = true;
            this.WorkThirst.Location = new System.Drawing.Point(72, 73);
            this.WorkThirst.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkThirst.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkThirst.Name = "WorkThirst";
            this.WorkThirst.Size = new System.Drawing.Size(60, 22);
            this.WorkThirst.TabIndex = 4;
            this.WorkThirst.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkThirst.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // WorkFamily
            // 
            this.WorkFamily.AutoSize = true;
            this.WorkFamily.Location = new System.Drawing.Point(375, 128);
            this.WorkFamily.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkFamily.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkFamily.Name = "WorkFamily";
            this.WorkFamily.Size = new System.Drawing.Size(60, 22);
            this.WorkFamily.TabIndex = 18;
            this.WorkFamily.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkFamily.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // WorkEnvironment
            // 
            this.WorkEnvironment.AutoSize = true;
            this.WorkEnvironment.Location = new System.Drawing.Point(375, 155);
            this.WorkEnvironment.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkEnvironment.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkEnvironment.Name = "WorkEnvironment";
            this.WorkEnvironment.Size = new System.Drawing.Size(60, 22);
            this.WorkEnvironment.TabIndex = 20;
            this.WorkEnvironment.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkEnvironment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // PublicHours
            // 
            this.PublicHours.AutoSize = true;
            this.PublicHours.Enabled = false;
            this.PublicHours.Location = new System.Drawing.Point(442, 101);
            this.PublicHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PublicHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.PublicHours.Name = "PublicHours";
            this.PublicHours.ReadOnly = true;
            this.PublicHours.Size = new System.Drawing.Size(60, 22);
            this.PublicHours.TabIndex = 0;
            this.PublicHours.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 101);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 17);
            this.label20.TabIndex = 5;
            this.label20.Text = "Comfort";
            // 
            // FamilyTotal
            // 
            this.FamilyTotal.AutoSize = true;
            this.FamilyTotal.Enabled = false;
            this.FamilyTotal.Location = new System.Drawing.Point(509, 128);
            this.FamilyTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.FamilyTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.FamilyTotal.Name = "FamilyTotal";
            this.FamilyTotal.ReadOnly = true;
            this.FamilyTotal.Size = new System.Drawing.Size(68, 22);
            this.FamilyTotal.TabIndex = 0;
            this.FamilyTotal.TabStop = false;
            // 
            // EnergyTotal
            // 
            this.EnergyTotal.AutoSize = true;
            this.EnergyTotal.Enabled = false;
            this.EnergyTotal.Location = new System.Drawing.Point(509, 46);
            this.EnergyTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.EnergyTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.EnergyTotal.Name = "EnergyTotal";
            this.EnergyTotal.ReadOnly = true;
            this.EnergyTotal.Size = new System.Drawing.Size(68, 22);
            this.EnergyTotal.TabIndex = 0;
            this.EnergyTotal.TabStop = false;
            // 
            // FunTotal
            // 
            this.FunTotal.AutoSize = true;
            this.FunTotal.Enabled = false;
            this.FunTotal.Location = new System.Drawing.Point(509, 73);
            this.FunTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.FunTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.FunTotal.Name = "FunTotal";
            this.FunTotal.ReadOnly = true;
            this.FunTotal.Size = new System.Drawing.Size(68, 22);
            this.FunTotal.TabIndex = 0;
            this.FunTotal.TabStop = false;
            // 
            // PublicTotal
            // 
            this.PublicTotal.AutoSize = true;
            this.PublicTotal.Enabled = false;
            this.PublicTotal.Location = new System.Drawing.Point(509, 101);
            this.PublicTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.PublicTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.PublicTotal.Name = "PublicTotal";
            this.PublicTotal.ReadOnly = true;
            this.PublicTotal.Size = new System.Drawing.Size(68, 22);
            this.PublicTotal.TabIndex = 0;
            this.PublicTotal.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(282, 73);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(32, 17);
            this.label33.TabIndex = 13;
            this.label33.Text = "Fun";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(282, 46);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(53, 17);
            this.label32.TabIndex = 11;
            this.label32.Text = "Energy";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(282, 101);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(88, 17);
            this.label31.TabIndex = 15;
            this.label31.Text = "Social Public";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(282, 128);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(90, 17);
            this.label30.TabIndex = 17;
            this.label30.Text = "Social Family";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(282, 155);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(87, 17);
            this.label29.TabIndex = 19;
            this.label29.Text = "Environment";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(375, 18);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(61, 17);
            this.label28.TabIndex = 63;
            this.label28.Text = "PerHour";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(509, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(52, 17);
            this.label26.TabIndex = 65;
            this.label26.Text = "= Total";
            // 
            // FunHours
            // 
            this.FunHours.AutoSize = true;
            this.FunHours.Enabled = false;
            this.FunHours.Location = new System.Drawing.Point(442, 73);
            this.FunHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FunHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.FunHours.Name = "FunHours";
            this.FunHours.ReadOnly = true;
            this.FunHours.Size = new System.Drawing.Size(60, 22);
            this.FunHours.TabIndex = 0;
            this.FunHours.TabStop = false;
            // 
            // WorkHygiene
            // 
            this.WorkHygiene.AutoSize = true;
            this.WorkHygiene.Location = new System.Drawing.Point(72, 128);
            this.WorkHygiene.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WorkHygiene.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.WorkHygiene.Name = "WorkHygiene";
            this.WorkHygiene.Size = new System.Drawing.Size(60, 22);
            this.WorkHygiene.TabIndex = 8;
            this.WorkHygiene.ValueChanged += new System.EventHandler(this.nudMotive_ValueChanged);
            this.WorkHygiene.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMotive_KeyUp);
            // 
            // FamilyHours
            // 
            this.FamilyHours.AutoSize = true;
            this.FamilyHours.Enabled = false;
            this.FamilyHours.Location = new System.Drawing.Point(442, 128);
            this.FamilyHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FamilyHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.FamilyHours.Name = "FamilyHours";
            this.FamilyHours.ReadOnly = true;
            this.FamilyHours.Size = new System.Drawing.Size(60, 22);
            this.FamilyHours.TabIndex = 0;
            this.FamilyHours.TabStop = false;
            // 
            // EnvironmentHours
            // 
            this.EnvironmentHours.AutoSize = true;
            this.EnvironmentHours.Enabled = false;
            this.EnvironmentHours.Location = new System.Drawing.Point(442, 155);
            this.EnvironmentHours.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.EnvironmentHours.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.EnvironmentHours.Name = "EnvironmentHours";
            this.EnvironmentHours.ReadOnly = true;
            this.EnvironmentHours.Size = new System.Drawing.Size(60, 22);
            this.EnvironmentHours.TabIndex = 0;
            this.EnvironmentHours.TabStop = false;
            // 
            // ThirstTotal
            // 
            this.ThirstTotal.AutoSize = true;
            this.ThirstTotal.Enabled = false;
            this.ThirstTotal.Location = new System.Drawing.Point(205, 73);
            this.ThirstTotal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.ThirstTotal.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.ThirstTotal.Name = "ThirstTotal";
            this.ThirstTotal.ReadOnly = true;
            this.ThirstTotal.Size = new System.Drawing.Size(68, 22);
            this.ThirstTotal.TabIndex = 0;
            this.ThirstTotal.TabStop = false;
            // 
            // gbJLHoursWages
            // 
            this.gbJLHoursWages.AutoSize = true;
            this.gbJLHoursWages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbJLHoursWages.Controls.Add(this.HoursWagesList);
            this.gbJLHoursWages.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbJLHoursWages.Location = new System.Drawing.Point(10, 9);
            this.gbJLHoursWages.Name = "gbJLHoursWages";
            this.gbJLHoursWages.Size = new System.Drawing.Size(960, 323);
            this.gbJLHoursWages.TabIndex = 1;
            this.gbJLHoursWages.TabStop = false;
            this.gbJLHoursWages.Text = "Job Levels";
            // 
            // HoursWagesList
            // 
            this.HoursWagesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HwLvl,
            this.HwStart,
            this.HwHours,
            this.HwEnd,
            this.HwWages,
            this.HwDogWages,
            this.HwCatWages,
            this.HwMon,
            this.HwTue,
            this.HwWed,
            this.HwThu,
            this.HwFri,
            this.HwSat,
            this.HwSun});
            this.HoursWagesList.FullRowSelect = true;
            this.HoursWagesList.GridLines = true;
            this.HoursWagesList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.HoursWagesList.HideSelection = false;
            this.HoursWagesList.Location = new System.Drawing.Point(10, 18);
            this.HoursWagesList.MultiSelect = false;
            this.HoursWagesList.Name = "HoursWagesList";
            this.HoursWagesList.Size = new System.Drawing.Size(944, 300);
            this.HoursWagesList.TabIndex = 1;
            this.HoursWagesList.UseCompatibleStateImageBehavior = false;
            this.HoursWagesList.View = System.Windows.Forms.View.Details;
            this.HoursWagesList.SelectedIndexChanged += new System.EventHandler(this.HoursWagesList_SelectedIndexChanged);
            // 
            // HwLvl
            // 
            this.HwLvl.Text = "Lvl";
            this.HwLvl.Width = 32;
            // 
            // HwStart
            // 
            this.HwStart.Text = "Start";
            this.HwStart.Width = 48;
            // 
            // HwHours
            // 
            this.HwHours.Text = "Hours";
            this.HwHours.Width = 56;
            // 
            // HwEnd
            // 
            this.HwEnd.Text = "End";
            this.HwEnd.Width = 48;
            // 
            // HwWages
            // 
            this.HwWages.Text = "Wages";
            // 
            // HwDogWages
            // 
            this.HwDogWages.Text = "Wages (Dog)";
            this.HwDogWages.Width = 106;
            // 
            // HwCatWages
            // 
            this.HwCatWages.Text = "Wages (Cat)";
            this.HwCatWages.Width = 106;
            // 
            // HwMon
            // 
            this.HwMon.Text = "Mon";
            this.HwMon.Width = 52;
            // 
            // HwTue
            // 
            this.HwTue.Text = "Tue";
            this.HwTue.Width = 52;
            // 
            // HwWed
            // 
            this.HwWed.Text = "Wed";
            this.HwWed.Width = 52;
            // 
            // HwThu
            // 
            this.HwThu.Text = "Thu";
            this.HwThu.Width = 52;
            // 
            // HwFri
            // 
            this.HwFri.Text = "Fri";
            this.HwFri.Width = 52;
            // 
            // HwSat
            // 
            this.HwSat.Text = "Sat";
            this.HwSat.Width = 52;
            // 
            // HwSun
            // 
            this.HwSun.Text = "Sun";
            this.HwSun.Width = 52;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.ItemSize = new System.Drawing.Size(64, 18);
            this.tabControl1.Location = new System.Drawing.Point(1, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 624);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbJobDetails);
            this.tabPage1.Controls.Add(this.gbJLDetails);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(976, 598);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Job Details";
            // 
            // gbJobDetails
            // 
            this.gbJobDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbJobDetails.Controls.Add(this.JobDetailsCopy);
            this.gbJobDetails.Controls.Add(this.jdpFemale);
            this.gbJobDetails.Controls.Add(this.jdpMale);
            this.gbJobDetails.Controls.Add(this.label9);
            this.gbJobDetails.Controls.Add(this.label8);
            this.gbJobDetails.Controls.Add(this.Vehicle);
            this.gbJobDetails.Controls.Add(this.Outfit);
            this.gbJobDetails.Location = new System.Drawing.Point(10, 356);
            this.gbJobDetails.Name = "gbJobDetails";
            this.gbJobDetails.Size = new System.Drawing.Size(960, 231);
            this.gbJobDetails.TabIndex = 2;
            this.gbJobDetails.TabStop = false;
            this.gbJobDetails.Text = "Current Level";
            // 
            // JobDetailsCopy
            // 
            this.JobDetailsCopy.AutoSize = true;
            this.JobDetailsCopy.Location = new System.Drawing.Point(474, 98);
            this.JobDetailsCopy.Name = "JobDetailsCopy";
            this.JobDetailsCopy.Size = new System.Drawing.Size(57, 17);
            this.JobDetailsCopy.TabIndex = 3;
            this.JobDetailsCopy.TabStop = true;
            this.JobDetailsCopy.Text = "Copy ->";
            this.JobDetailsCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.JobDetailsCopy_LinkClicked);
            // 
            // jdpFemale
            // 
            this.jdpFemale.AutoSize = true;
            this.jdpFemale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jdpFemale.DescLabel = "Desc Female";
            this.jdpFemale.DescSize = new System.Drawing.Size(382, 136);
            this.jdpFemale.DescValue = "JobDescFemale";
            this.jdpFemale.Location = new System.Drawing.Point(478, 18);
            this.jdpFemale.Name = "jdpFemale";
            this.jdpFemale.Size = new System.Drawing.Size(478, 170);
            this.jdpFemale.TabIndex = 2;
            this.jdpFemale.TitleLabel = "Title Female";
            this.jdpFemale.TitleValue = "JobTitleFemale";
            this.jdpFemale.TitleValueChanged += new System.EventHandler(this.jdpFemale_TitleValueChanged);
            this.jdpFemale.DescValueChanged += new System.EventHandler(this.jdpFemale_DescValueChanged);
            // 
            // jdpMale
            // 
            this.jdpMale.AutoSize = true;
            this.jdpMale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jdpMale.DescLabel = "Desc Male";
            this.jdpMale.DescSize = new System.Drawing.Size(382, 136);
            this.jdpMale.DescValue = "JobDescMale";
            this.jdpMale.Location = new System.Drawing.Point(6, 18);
            this.jdpMale.Name = "jdpMale";
            this.jdpMale.Size = new System.Drawing.Size(462, 170);
            this.jdpMale.TabIndex = 1;
            this.jdpMale.TitleLabel = "Title Male";
            this.jdpMale.TitleValue = "JobTitleMale";
            this.jdpMale.TitleValueChanged += new System.EventHandler(this.jdpMale_TitleValueChanged);
            this.jdpMale.DescValueChanged += new System.EventHandler(this.jdpMale_DescValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(512, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Vehicle";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Outfit";
            // 
            // Vehicle
            // 
            this.Vehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Vehicle.Location = new System.Drawing.Point(572, 193);
            this.Vehicle.Name = "Vehicle";
            this.Vehicle.Size = new System.Drawing.Size(228, 24);
            this.Vehicle.TabIndex = 7;
            this.Vehicle.SelectedIndexChanged += new System.EventHandler(this.Vehicle_SelectedIndexChanged);
            // 
            // Outfit
            // 
            this.Outfit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Outfit.Location = new System.Drawing.Point(83, 193);
            this.Outfit.Name = "Outfit";
            this.Outfit.Size = new System.Drawing.Size(228, 24);
            this.Outfit.TabIndex = 5;
            this.Outfit.SelectedIndexChanged += new System.EventHandler(this.Outfit_SelectedIndexChanged);
            // 
            // gbJLDetails
            // 
            this.gbJLDetails.AutoSize = true;
            this.gbJLDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbJLDetails.Controls.Add(this.JobDetailList);
            this.gbJLDetails.Location = new System.Drawing.Point(10, 8);
            this.gbJLDetails.Name = "gbJLDetails";
            this.gbJLDetails.Size = new System.Drawing.Size(960, 339);
            this.gbJLDetails.TabIndex = 1;
            this.gbJLDetails.TabStop = false;
            this.gbJLDetails.Text = "Job Levels";
            // 
            // JobDetailList
            // 
            this.JobDetailList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JdLvl,
            this.JdJobTitle,
            this.JdDesc,
            this.JdOutfit,
            this.JdVehicle});
            this.JobDetailList.FullRowSelect = true;
            this.JobDetailList.GridLines = true;
            this.JobDetailList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.JobDetailList.HideSelection = false;
            this.JobDetailList.Location = new System.Drawing.Point(10, 18);
            this.JobDetailList.MultiSelect = false;
            this.JobDetailList.Name = "JobDetailList";
            this.JobDetailList.Size = new System.Drawing.Size(944, 300);
            this.JobDetailList.TabIndex = 1;
            this.JobDetailList.UseCompatibleStateImageBehavior = false;
            this.JobDetailList.View = System.Windows.Forms.View.Details;
            this.JobDetailList.SelectedIndexChanged += new System.EventHandler(this.JobDetailList_SelectedIndexChanged);
            // 
            // JdLvl
            // 
            this.JdLvl.Text = "Lvl";
            this.JdLvl.Width = 32;
            // 
            // JdJobTitle
            // 
            this.JdJobTitle.Text = "Job Title";
            this.JdJobTitle.Width = 160;
            // 
            // JdDesc
            // 
            this.JdDesc.Text = "Job Description";
            this.JdDesc.Width = 320;
            // 
            // JdOutfit
            // 
            this.JdOutfit.Text = "Outfit";
            this.JdOutfit.Width = 160;
            // 
            // JdVehicle
            // 
            this.JdVehicle.Text = "Vehicle";
            this.JdVehicle.Width = 160;
            // 
            // CareerLvls
            // 
            this.CareerLvls.Enabled = false;
            this.CareerLvls.Location = new System.Drawing.Point(368, 7);
            this.CareerLvls.Name = "CareerLvls";
            this.CareerLvls.ReadOnly = true;
            this.CareerLvls.Size = new System.Drawing.Size(57, 22);
            this.CareerLvls.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Career Lvls";
            // 
            // CareerTitle
            // 
            this.CareerTitle.Location = new System.Drawing.Point(95, 6);
            this.CareerTitle.Name = "CareerTitle";
            this.CareerTitle.Size = new System.Drawing.Size(181, 22);
            this.CareerTitle.TabIndex = 2;
            this.CareerTitle.TextChanged += new System.EventHandler(this.CareerTitle_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Career Title";
            // 
            // Language
            // 
            this.Language.Location = new System.Drawing.Point(767, 6);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(218, 24);
            this.Language.TabIndex = 8;
            this.Language.Text = "01 - English";
            this.Language.SelectedIndexChanged += new System.EventHandler(this.Language_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(689, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "Language";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(431, 9);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(56, 17);
            this.label102.TabIndex = 5;
            this.label102.Text = "Reward";
            // 
            // CareerReward
            // 
            this.CareerReward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CareerReward.Location = new System.Drawing.Point(493, 6);
            this.CareerReward.Name = "CareerReward";
            this.CareerReward.Size = new System.Drawing.Size(190, 24);
            this.CareerReward.TabIndex = 6;
            this.CareerReward.SelectedIndexChanged += new System.EventHandler(this.CareerReward_SelectedIndexChanged);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem1});
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miInsertLvl,
            this.miRemoveLvl,
            this.menuItem9});
            this.menuItem6.Text = "&Levels";
            // 
            // miInsertLvl
            // 
            this.miInsertLvl.Index = 0;
            this.miInsertLvl.Text = "&Insert Level";
            this.miInsertLvl.Click += new System.EventHandler(this.miInsertLvl_Click);
            // 
            // miRemoveLvl
            // 
            this.miRemoveLvl.Index = 1;
            this.miRemoveLvl.Text = "&Remove Level";
            this.miRemoveLvl.Click += new System.EventHandler(this.miRemoveLvl_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Enabled = false;
            this.menuItem9.Index = 2;
            this.menuItem9.Text = "&Copy Level...";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.miEnglishOnly,
            this.menuItem4,
            this.menuItem5});
            this.menuItem1.Text = "L&anguages";
            // 
            // menuItem2
            // 
            this.menuItem2.Checked = true;
            this.menuItem2.Enabled = false;
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "&One English";
            // 
            // miEnglishOnly
            // 
            this.miEnglishOnly.Index = 1;
            this.miEnglishOnly.Text = "&English Only";
            this.miEnglishOnly.Click += new System.EventHandler(this.miEnglishOnly_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Enabled = false;
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "&Copy...";
            // 
            // CareerEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(990, 687);
            this.Controls.Add(this.CareerTitle);
            this.Controls.Add(this.label102);
            this.Controls.Add(this.CareerReward);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CareerLvls);
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(998, 716);
            this.Name = "CareerEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Career Editor (by Bidou)";
            this.tabPage4.ResumeLayout(false);
            this.flpChanceCards.ResumeLayout(false);
            this.flpChanceCards.PerformLayout();
            this.flpChanceHeader.ResumeLayout(false);
            this.flpChanceHeader.PerformLayout();
            this.flpChanceText.ResumeLayout(false);
            this.flpChanceText.PerformLayout();
            this.flpCTMale.ResumeLayout(false);
            this.flpCTMale.PerformLayout();
            this.flpCTFemale.ResumeLayout(false);
            this.flpCTFemale.PerformLayout();
            this.tcChanceOutcome.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.gbJLPromo.ResumeLayout(false);
            this.gbPromo.ResumeLayout(false);
            this.gbPromo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PromoFriends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCleaning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCreativity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCharisma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoMechanical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PromoCooking)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbHoursWages.ResumeLayout(false);
            this.gbHoursWages.PerformLayout();
            this.flpHoursWages.ResumeLayout(false);
            this.flpHoursWages.PerformLayout();
            this.flpWork.ResumeLayout(false);
            this.flpWork.PerformLayout();
            this.flpWages.ResumeLayout(false);
            this.flpWages.PerformLayout();
            this.flpWorkDays.ResumeLayout(false);
            this.flpWorkDays.PerformLayout();
            this.gbHWMotives.ResumeLayout(false);
            this.gbHWMotives.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComfortHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HygieneTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BladderTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkBladder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkComfort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HungerHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPublic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkHunger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BladderHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComfortTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HungerTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HygieneHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirstHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkFun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkThirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkFamily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkEnvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublicHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FamilyTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublicTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkHygiene)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FamilyHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnvironmentHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirstTotal)).EndInit();
            this.gbJLHoursWages.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbJobDetails.ResumeLayout(false);
            this.gbJobDetails.PerformLayout();
            this.gbJLDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CareerLvls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion




        #region Global
        private ushort femaleOffset;
        private Str catalogueDesc;
        private SimPe.Packages.GeneratableFile package;
        private Bcon tuning;
        private Bcon lifeScore;
        private Bcon PTO;
        private uint groupId;

        private bool hasReward;
        private Bhav BHavReward;
        private void setRewardFromGUID(byte val1, byte val2, byte val3, byte val4)
        {
            uint guid = ((uint)val4 * 256) + val3;
            guid = ((uint)guid * 256) + val2;
            guid = ((uint)guid * 256) + val1;
            int index = 0;

            if (hasReward)
                while (index < rewardGuids.Length)
                {
                    if (rewardGuids[index] == guid)
                        break;
                    index++;
                }
            else
                while (index < adultGuids.Length)
                {
                    if (adultGuids[index] == guid)
                        break;
                    index++;
                }

            CareerReward.SelectedIndex = index;
        }

        private ushort noLevels;
        private void noLevelsChanged(ushort l)
        {
            noLevels = l;
            femaleOffset = (ushort)(2 * noLevels);
            CareerLvls.Value = noLevels;

            lnudChanceCurrentLevel.Maximum = noLevels;

            miInsertLvl.Enabled = (noLevels < 100);
            miRemoveLvl.Enabled = (noLevels > 1);

            fillJobDetails();
            fillHoursAndWages();
            fillPromotionDetails();
        }

        private ushort currentLevel;
        private bool levelChanging = false;
		private void levelChanged(ushort newLevel)
		{
            if (levelChanging || newLevel == currentLevel) return;
			internalchg = levelChanging = true;

            #region job details
            JobDetailList.SelectedIndices.Clear();
            JobDetailList.SelectedIndices.Add(newLevel - 1);
            gbJobDetails.Text = "Current Level: " + newLevel;

            SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
            jdpMale.TitleValue = items[newLevel * 2 - 1].Title;
            jdpMale.DescValue = items[newLevel * 2].Title;
            jdpFemale.TitleValue = items[newLevel * 2 - 1 + femaleOffset].Title;
            jdpFemale.DescValue = items[newLevel * 2 + femaleOffset].Title;

            setOutfitFromGUID(outfitGUID[newLevel * 2], outfitGUID[newLevel * 2 + 1]);
            setVehicleFromGUID(vehicleGUID[newLevel * 2], vehicleGUID[newLevel * 2 + 1]);
            #endregion

            #region hours & wages
            HoursWagesList.SelectedIndices.Clear();
            HoursWagesList.SelectedIndices.Add(newLevel - 1);
            gbHoursWages.Text = "Current Level: " + newLevel;
            lnudWorkStart.Value = startHour[newLevel];
            lnudWorkHours.Value = hoursWorked[newLevel];
            lnudWorkFinish.Value = (startHour[newLevel] + hoursWorked[newLevel]) % 24;

            if (wages != null)
            {
                lnudWages.Value = wages[newLevel];
                lnudWagesDog.Enabled = lnudWagesCat.Enabled = false;
            }
            else
            {
                lnudWages.Enabled = false;
                lnudWagesDog.Value = wagesDog[newLevel];
                lnudWagesCat.Value = wagesCat[newLevel];
            }

            Boolset dw = getDaysArray(daysWorked[newLevel]);
            WorkMonday.Checked = dw[MONDAY];
            WorkTuesday.Checked = dw[TUESDAY];
            WorkWednesday.Checked = dw[WEDNESDAY];
            WorkThursday.Checked = dw[THURSDAY];
            WorkFriday.Checked = dw[FRIDAY];
            WorkSaturday.Checked = dw[SATURDAY];
            WorkSunday.Checked = dw[SUNDAY];

            WorkHunger.Value = motiveDeltas[HUNGER][newLevel];
            WorkThirst.Value = motiveDeltas[THIRST][newLevel];
            WorkComfort.Value = motiveDeltas[COMFORT][newLevel];
            WorkHygiene.Value = motiveDeltas[HYGIENE][newLevel];
            WorkBladder.Value = motiveDeltas[BLADDER][newLevel];
            WorkEnergy.Value = motiveDeltas[ENERGY][newLevel];
            WorkFun.Value = motiveDeltas[FUN][newLevel];
            WorkPublic.Value = motiveDeltas[PUBLIC][newLevel];
            WorkFamily.Value = motiveDeltas[FAMILY][newLevel];
            WorkEnvironment.Value = motiveDeltas[ENVIRONMENT][newLevel];

            WorkChanged(newLevel);
            #endregion

            #region promotion
            PromoList.SelectedIndices.Clear();
            PromoList.SelectedIndices.Add(newLevel - 1);
            gbPromo.Text = "Current Level: " + newLevel;
            if (skillReq[BODY] != null) // people
            {
                PromoCooking.Value = skillReq[COOKING][newLevel] / 100;
                PromoMechanical.Value = skillReq[MECHANICAL][newLevel] / 100;
                PromoBody.Value = skillReq[BODY][newLevel] / 100;
                PromoCharisma.Value = skillReq[CHARISMA][newLevel] / 100;
                PromoCreativity.Value = skillReq[CREATIVITY][newLevel] / 100;
                PromoLogic.Value = skillReq[LOGIC][newLevel] / 100;
                PromoCleaning.Value = skillReq[CLEANING][newLevel] / 100;
                cbTrick.Enabled = false;
            }
            else // pets
            {
                PromoCooking.Enabled =
                PromoMechanical.Enabled =
                PromoBody.Enabled =
                PromoCharisma.Enabled =
                PromoCreativity.Enabled =
                PromoLogic.Enabled =
                PromoCleaning.Enabled =
                false;
                cbTrick.SelectedIndex = 0;
                for (int i = 0; i * 2 < trick.Count; i++)
                    if (trick[i * 2 + 1] == newLevel)
                        cbTrick.SelectedIndex = trick[i * 2];
            }

            PromoFriends.Value = friends[newLevel];
            #endregion

            //chance cards
			chanceCardsLevelChanged(newLevel);

            currentLevel = newLevel;
            internalchg = levelChanging = false;
		}

        private StrLanguage currentLanguage;
        private ArrayList languageString, languageID;
        private bool oneEnglish, englishOnly;
        private void removeEnglishInternational()
        {
            SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(2);
            SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(l);
            for (int j = 0; j < items.Length; j++)
            {
                items[j].Title = "";
            }

            items = chanceCardsText.LanguageItems(l);
            for (int j = 0; j < items.Length; j++)
            {
                items[j].Title = "";
            }

            items = catalogueDesc.LanguageItems(l);
            for (int j = 0; j < items.Length; j++)
            {
                items[j].Title = "";
            }
        }
        private void removeOtherLanguages()
        {
            for (int i = 0; i < jobTitles.Languages.Length; i++)
                if (jobTitles.Languages[i].Id > 2)
                {
                    SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(jobTitles.Languages[i].Id);
                    SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(l);
                    for (int j = 0; j < items.Length; j++)
                    {
                        items[j].Title = "";
                    }
                }

            for (int i = 0; i < chanceCardsText.Languages.Length; i++)
                if (chanceCardsText.Languages[i].Id > 2)
                {
                    SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(chanceCardsText.Languages[i].Id);
                    SimPe.PackedFiles.Wrapper.StrItemList items = chanceCardsText.LanguageItems(l);
                    for (int j = 0; j < items.Length; j++)
                    {
                        items[j].Title = "";
                    }
                }

            for (int i = 0; i < catalogueDesc.Languages.Length; i++)
                if (catalogueDesc.Languages[i].Id > 2)
                {
                    SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(catalogueDesc.Languages[i].Id);
                    SimPe.PackedFiles.Wrapper.StrItemList items = catalogueDesc.LanguageItems(l);
                    for (int j = 0; j < items.Length; j++)
                    {
                        items[j].Title = "";
                    }
                }
        }

        #endregion


        #region Job Details
        private Str jobTitles;
        private Bcon vehicleGUID;
        private Bcon outfitGUID;

        private void fillJobDetails()
        {
            JobDetailList.Items.Clear();
            SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
            for (ushort i = 1; i <= noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + i, 0);
                item1.SubItems.Add(items[i * 2 - 1].Title);
                item1.SubItems.Add(items[i * 2].Title);
                item1.SubItems.Add(getOutfitTextFromGUID(outfitGUID[i * 2], outfitGUID[i * 2 + 1]));
                item1.SubItems.Add(getVehicleTextFromGUID(vehicleGUID[i * 2], vehicleGUID[i * 2 + 1]));
                JobDetailList.Items.Add(item1);
            }
        }

        private string getOutfitTextFromGUID(short val1, short val2)
        {
            uint guid = ((uint)((ushort)val2) * 65536) + ((ushort)val1);
            int index = 0;

            while (index < outfitGuids.Length)
            {
                if (outfitGuids[index] == guid)
                    break;
                index++;
            }
            return outfitStrings[index];
        }

        private string getVehicleTextFromGUID(short val1, short val2)
        {
            uint guid = ((uint)((ushort)val2) * 65536) + ((ushort)val1);
            int index = 0;

            while (index < vehicleGuids.Length)
            {
                if (vehicleGuids[index] == guid)
                    break;
                index++;
            }
            return vehicleStrings[index];
        }

        private void setOutfitFromGUID(short val1, short val2)
        {
            uint guid = ((uint)((ushort)val2) * 65536) + ((ushort)val1);
            int index = 0;

            while (index < outfitGuids.Length)
            {
                if (outfitGuids[index] == guid)
                    break;
                index++;
            }
            Outfit.SelectedIndex = index;
        }

        private void setVehicleFromGUID(short val1, short val2)
        {
            uint guid = ((uint)((ushort)val2) * 65536) + ((ushort)val1);
            int index = 0;

            while (index < vehicleGuids.Length)
            {
                if (vehicleGuids[index] == guid)
                    break;
                index++;
            }
            Vehicle.SelectedIndex = index;
        }
        #endregion


        #region Hours & Wages
        private Bcon startHour;
        private Bcon hoursWorked;
        private Bcon daysWorked;
        private Bcon wages;
        private Bcon wagesCat;
        private Bcon wagesDog;
        private Bcon[] motiveDeltas;

        private void fillHoursAndWages()
        {
            HoursWagesList.Items.Clear();
            for (ushort i = 1; i <= noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + i, 0);

                item1.SubItems.Add("" + startHour[i]);
                item1.SubItems.Add("" + hoursWorked[i]);
                item1.SubItems.Add("" + (startHour[i] + hoursWorked[i]) % 24);
                if (wages != null)
                {
                    item1.SubItems.Add("" + wages[i]);
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                }
                else
                {
                    item1.SubItems.Add("");
                    item1.SubItems.Add("" + wagesDog[i]);
                    item1.SubItems.Add("" + wagesCat[i]);
                }

                Boolset dw = getDaysArray(daysWorked[i]);
                item1.SubItems.Add("" + dw[MONDAY]);
                item1.SubItems.Add("" + dw[TUESDAY]);
                item1.SubItems.Add("" + dw[WEDNESDAY]);
                item1.SubItems.Add("" + dw[THURSDAY]);
                item1.SubItems.Add("" + dw[FRIDAY]);
                item1.SubItems.Add("" + dw[SATURDAY]);
                item1.SubItems.Add("" + dw[SUNDAY]);

                HoursWagesList.Items.Add(item1);
            }
        }

        private Boolset getDaysArray(short val)
        {
            return new Boolset((byte)((val >= 0) ? val : val + 65536));
        }
        private void WorkChanged(int level)
        {
            HungerHours.Value = hoursWorked[level];
            ThirstHours.Value = hoursWorked[level];
            ComfortHours.Value = hoursWorked[level];
            HygieneHours.Value = hoursWorked[level];
            BladderHours.Value = hoursWorked[level];
            EnergyHours.Value = hoursWorked[level];
            FunHours.Value = hoursWorked[level];
            PublicHours.Value = hoursWorked[level];
            FamilyHours.Value = hoursWorked[level];
            EnvironmentHours.Value = hoursWorked[level];

            HungerTotal.Value = motiveDeltas[HUNGER][level] * hoursWorked[level];
            ThirstTotal.Value = motiveDeltas[THIRST][level] * hoursWorked[level];
            ComfortTotal.Value = motiveDeltas[COMFORT][level] * hoursWorked[level];
            HygieneTotal.Value = motiveDeltas[HYGIENE][level] * hoursWorked[level];
            BladderTotal.Value = motiveDeltas[BLADDER][level] * hoursWorked[level];
            EnergyTotal.Value = motiveDeltas[ENERGY][level] * hoursWorked[level];
            FunTotal.Value = motiveDeltas[FUN][level] * hoursWorked[level];
            PublicTotal.Value = motiveDeltas[PUBLIC][level] * hoursWorked[level];
            FamilyTotal.Value = motiveDeltas[FAMILY][level] * hoursWorked[level];
            EnvironmentTotal.Value = motiveDeltas[ENVIRONMENT][level] * hoursWorked[level];
        }
        #endregion


        #region Promotion
        private Bcon[] skillReq;
        private Bcon friends;
        private Bcon trick;

        private void fillPromotionDetails()
        {
            PromoList.Items.Clear();
            for (ushort i = 1; i <= noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + i, 0);

                if (skillReq[BODY] != null) // people
                {
                    item1.SubItems.Add("" + skillReq[COOKING][i] / 100);
                    item1.SubItems.Add("" + skillReq[MECHANICAL][i] / 100);
                    item1.SubItems.Add("" + skillReq[CHARISMA][i] / 100);
                    item1.SubItems.Add("" + skillReq[BODY][i] / 100);
                    item1.SubItems.Add("" + skillReq[CREATIVITY][i] / 100);
                    item1.SubItems.Add("" + skillReq[LOGIC][i] / 100);
                    item1.SubItems.Add("" + skillReq[CLEANING][i] / 100);
                    item1.SubItems.Add("" + friends[i]);
                    item1.SubItems.Add("");
                }
                else // pets
                {
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("" + friends[i]);
                    String tr = (String)cbTrick.Items[0];
                    for (int j = 0; j * 2 < trick.Count; j++)
                        if (trick[j * 2 + 1] == i)
                            tr = (String)cbTrick.Items[trick[j * 2]];
                    item1.SubItems.Add(tr);
                }

                PromoList.Items.Add(item1);
            }
        }
        #endregion


        #region Chance Cards
        private Str chanceCardsText;
        private Bcon[] chanceASkills;
        private Bcon[] chanceAGood;
        private Bcon[] chanceABad;

        private Bcon[] chanceBSkills;
        private Bcon[] chanceBGood;
        private Bcon[] chanceBBad;

        private Bcon chanceChance;

        private void chanceCardsLevelChanged(ushort newLevel)
        {
            if (currentLevel != 0) chanceCardsToFiles();

            lnudChanceCurrentLevel.Value = newLevel;
            lnudChancePercent.Value = chanceChance[newLevel];

            SimPe.PackedFiles.Wrapper.StrItemList items = chanceCardsText.LanguageItems(currentLanguage);

            cpChoiceA.setValues(true, cpChoiceA.Label, items[newLevel * 12 + 7].Title, chanceASkills, newLevel);
            cpChoiceB.setValues(false, cpChoiceB.Label, items[newLevel * 12 + 8].Title, chanceBSkills, newLevel);

            ChanceTextMale.Text = items[newLevel * 12 + 9].Title;
            ChanceTextFemale.Text = items[newLevel * 12 + 10].Title;

            epPassA.setValues(noLevels, newLevel, chanceAGood, items[newLevel * 12 + 11].Title, items[newLevel * 12 + 12].Title);
            epFailA.setValues(noLevels, newLevel, chanceABad, items[newLevel * 12 + 13].Title, items[newLevel * 12 + 14].Title);
            epPassB.setValues(noLevels, newLevel, chanceBGood, items[newLevel * 12 + 15].Title, items[newLevel * 12 + 16].Title);
            epFailB.setValues(noLevels, newLevel, chanceBBad, items[newLevel * 12 + 17].Title, items[newLevel * 12 + 18].Title);
        }
        private void chanceCardsToFiles()
        {
            SimPe.PackedFiles.Wrapper.StrItemList items = chanceCardsText.LanguageItems(currentLanguage);
            chanceChance[currentLevel] = (short)lnudChancePercent.Value;

            items[currentLevel * 12 + 7].Title = cpChoiceA.Value;
            if (chanceASkills[COOKING] != null)
                cpChoiceA.getValues(chanceASkills, currentLevel);

            items[currentLevel * 12 + 8].Title = cpChoiceB.Value;
            if (chanceBSkills[COOKING] != null)
                cpChoiceB.getValues(chanceBSkills, currentLevel);

            items[currentLevel * 12 + 9].Title = ChanceTextMale.Text;
            items[currentLevel * 12 + 10].Title = ChanceTextFemale.Text;

            string male = "", female = "";
            epPassA.getValues(chanceAGood, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 11].Title = male;
            items[currentLevel * 12 + 12].Title = female;

            epFailA.getValues(chanceABad, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 13].Title = male;
            items[currentLevel * 12 + 14].Title = female;

            epPassB.getValues(chanceBGood, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 15].Title = male;
            items[currentLevel * 12 + 16].Title = female;

            epFailB.getValues(chanceBBad, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 17].Title = male;
            items[currentLevel * 12 + 18].Title = female;
        }
        #endregion


        #region program constants
        private const byte COOKING = 0;
        private const byte MECHANICAL = 1;
        private const byte BODY = 2;
        private const byte CHARISMA = 3;
        private const byte CREATIVITY = 4;
        private const byte LOGIC = 5;
        private const byte GARDENING = 6;
        private const byte CLEANING = 7;

        private const byte MONEY = 8;
        private const byte JOB = 9;

        private const byte HUNGER = 0;
        private const byte THIRST = 1;
        private const byte COMFORT = 2;
        private const byte HYGIENE = 3;
        private const byte BLADDER = 4;
        private const byte ENERGY = 5;
        private const byte FUN = 6;
        private const byte PUBLIC = 7;
        private const byte FAMILY = 8;
        private const byte ENVIRONMENT = 9;
        private const byte MENTAL = 10;

        private const byte MONDAY = 0;
        private const byte TUESDAY = 1;
        private const byte WEDNESDAY = 2;
        private const byte THURSDAY = 3;
        private const byte FRIDAY = 4;
        private const byte SATURDAY = 5;
        private const byte SUNDAY = 6;

        private string[] languageStringsMaster = new string[]
			{
					"0x00 - None",
				"0x01 - English (US)","0x02 - English (Int)","0x03 - French","0x04 - German","0x05 - Italian",
				"0x06 - Spanish","0x07 - Dutch","0x08 - Danish","0x09 - Swedish","0x0A - Norwegian",
				"0x0B - Finish","0x0C - Hebrew","0x0D - Russian","0x0E - Portuguese","0x0F - Japanese",
				"0x10 - Polish","0x11 - SimplifiedChinese","0x12 - TraditionalChinese","0x13 - Thai","0x14 - Korean",
				"0x15 - 21","0x16 - 22","0x17 - 23","0x18 - 24","0x19 - 25",
				"0x1A - 26",
				"0x1B - 27","0x1C - 28","0x1D - 29","0x1E - 30","0x1F - 31","0x20 - 32","0x21 - 33","0x22 - 34",
				"0x23 - Portuguese (Brazil)","0x24 - 36","0x25 - 37","0x26 - 38","0x27 - 39","0x28 - 40",
				"0x29 - 41","0x2A - 42","0x2B - 43","0x2C - 44" };

        private string[] outfitStrings = new string[]
				{
						"None","Astronaut","BlueScrubs","Burglar","CaptHero","CheapSuit","Chef",
					"Clerk","Coach","Cop","EMT","FastFood","Fatigues","GasStation","GreenScrubs",
					"HighRank","LabCoat1","LabCoat2","LeatherJacket","LowRank","MadLab","Mascot",
					"MastEvil","Mayor","MidRank","PowerSuit","Restaurant","Scrubs","SecurityGuard",
					"SlickSuit","SuperChef","Swat","Sweatsuit","Tracksuit","TweedJacket",
		
					"Electrocution", "Maternity", "NPC - Ambulance Driver (Paramedic)","NPC - Bartender",
					"NPC_Burglar", "NPC - BusDriver","NPC Clerk Outfit","NPC Cop","NPC DeliveryPerson",
					"NPC Exterminator","NPC FireFighter","New Gardener Outfit","NPC HandyPerson",
					"NPC HeadMaster","NPC Maid","NPC Mail Outfit","NPC Nanny","NPC PaperDelivery",
					"NPC Pizza Outfit","NPC Repo Outfit","NPC Salesperson","NPC SocialWorker",
					"PrivateSchool","Reaper Lei","Reaper NoLei","SkeletonGlow","SocialBunny Blue",
					"SocialBunny Pink","SocialBunny Yellow","Wedding Outfit",

					"MaternityShirtPants","BodyShirtUntuckedOxford","Outfit - Template",
					
					"Unknown"};

        private uint[] outfitGuids = new uint[]
				{
					0, 0x0CC8FB0A, 0x6CF36A28, 0xACCFBA59, 0x4CC8D355,
					0x6CC1394A, 0x6CDB4D89, 0xACCFB5BA, 0x6CE5E896, 0x2DC106EF,
					0x8CC8D510, 0xACCFB61B, 0xCCC8FA1E, 0x0CCFB4F4, 0x4CF368BE,
					0x8CCFA3D8, 0x8CCFA130, 0x8CCFA223, 0xCCE5E90F, 0x8CCFA318,
					0x2CD4EE5D, 0x8CE5EA26, 0xCCCF9FA7, 0xECE5EB2F, 0x8CCFA387,
					0x6CC13C27, 0xACD4EEE6, 0xACC8EE11, 0x6CC8CFBD, 0x0DCC7C4D,
					0xACCFB97C, 0xACE5EB8C, 0x0CCFA643, 0xECCFA694, 0xECCFB386,

					0xEDED8493, 0x6DD1D04B, 0xECA45D6D, 0x2CA45AE2, 0x0DB8AE38,
					0x8CC13127, 0x2CD89D6B, 0x2DC0FCD7, 0x8DC3893C, 0x0CA45C86,
					0xCDC38723, 0x8CDA36DA, 0xCD65FD9F, 0x6CC13409, 0x6DC38A6C,
					0xECD0F3FC, 0xCCD0F227, 0x6CC1322B, 0xACD0F47C, 0x6CD0F537,
					0x0CA45C32, 0x6CC130A6, 0xECD7C130, 0xCE029001, 0x2E02903A,
					0x6CD4EDE8, 0xEE028B7C, 0x2E028C23, 0xAE028CB9, 0x6D771A13,

					0x4CBC4BB4, 0x8CBC0B19, 0x0C91A937	};

        private uint[] vehicleGuids = new uint[]
				{
					0xAD0AB49C, 0x0D099B93, 0xAC43E058, 0x4CA1487C, 0x6C6CDEC6,
					0xCC69FDA3, 0xEC860630, 0x0CA42373, 0x8C5A4D9E, 0x4D50E553,
					0x4C03451A, 0x6CA4110A, 0x4C413B80, 0x0C85AE14, 0xCD08624E,
					0x4D09B954 };
        private string[] vehicleStrings = new string[]
				{
					"Captain Hero Flyaway","Helicopter - Executive","Helicopter - Army","Town Car",
					"Sports Car - Super","Sports Car - Mid","Sports Car - Low","Sports Team Bus",
					"Sedan","Taxi","Minivan","Limo","HMV","Hatchback","Police","Ambulance",
					"Unknown" };
        private uint[] rewardGuids = new uint[]
				{
					0x0C6E194A,0x8C4D2997,0xD1CD15C8,0xCC58DF85,0x6C2979FB,
					0xCC16D816,0x4C2148B0,0xCC20426A,0x6C6CE31F,0xAC314A3A};
        private string[] rewardStrings = new string[]
			{
				"Biotech Station","Candy Factory","Fingerprint Scanner","Hydroponic Garden","Obstacle Course",
				"Polygraph","Punching Bag","Putting Green","Surgical Dummy","Teleprompter",
				"Unknown" };
        private uint[] adultGuids = new uint[]
			{
				0x2C89E95F, 0x45196555, 0x6C9EBD0E, 0xEC9EBD5F, 0xAC9EBCE3,
				0x0C7761FD, 0x6C9EBD32, 0x2C945B14, 0x0C9EBD47, 0xEC77620B };
        private string[] adultStrings = new string[]
			{
				"Athletic","Business","Criminal","Culinary","Law Enforcement",
				"Medicine","Military","Politics","Science","Slacker",
				"Other..."};
        #endregion


        private bool internalchg = false;
		public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov) 
		{
			bool newpackage = false;

			this.package = (SimPe.Packages.GeneratableFile)package;
			
			
			WaitingScreen.Wait();
			try 
			{
				if (this.package==null) 
				{
					this.package = SimPe.Packages.GeneratableFile.LoadFromFile(CareerTool.DefaultCareerFile);					
					newpackage = true;
					this.package = (SimPe.Packages.GeneratableFile)this.package.Clone();
				}
				loadFiles();
				
				//menuItem2.Checked = oneEnglish;
				miEnglishOnly.Checked = englishOnly;

                languageString = new ArrayList();
				languageID = new ArrayList();
				for (int i = 0; i < catalogueDesc.Languages.Length; i++)
				{
					SimPe.PackedFiles.Wrapper.StrLanguage l = catalogueDesc.Languages[i];
					if (l.Id != 2)
					{
						languageString.Insert(0, languageStringsMaster[l.Id]);
						languageID.Insert(0, l.Id);
					}
				}

                internalchg = true;

                Language.DataSource = languageString;
				Outfit.DataSource = outfitStrings;
				Vehicle.DataSource = vehicleStrings;

                internalchg = false;

                currentLanguage = new SimPe.PackedFiles.Wrapper.StrLanguage(1);

				//SimPe.PackedFiles.Wrapper.StrItem[] items = catalogueDesc.FallbackedLanguageItems(Helper.WindowsRegistry.LanguageCode);
				SimPe.PackedFiles.Wrapper.StrItemList items = catalogueDesc.LanguageItems(currentLanguage);

				CareerTitle.Text = items[0].Title;

				if(BHavReward.FileName == "CT - Career Reward")
				{

					byte a = BHavReward[2].Operands[5];
					byte b = BHavReward[2].Operands[6];
					byte c = BHavReward[2].Operands[7];
					byte d = BHavReward[2].Reserved1[0];

					hasReward = true;
					CareerReward.DataSource = rewardStrings;
					
					setRewardFromGUID(a, b, c, d);
				}
				else
				{
					byte a = BHavReward[1].Operands[0];
					byte b = BHavReward[1].Operands[1];
					byte c = BHavReward[1].Operands[2];
					byte d = BHavReward[1].Operands[3];

					hasReward = false;
					CareerReward.DataSource = adultStrings;
					label102.Text = "Adult";

					setRewardFromGUID(a, b, c, d);
				}

                noLevelsChanged((ushort)tuning[0]);
                currentLevel = 0;

                levelChanged(1);
			}
			catch (Exception e)
			{
				Helper.ExceptionMessage("Error Loading Career", e);
				return new Plugin.ToolResult(false, false);
			} 
			finally 
			{
				WaitingScreen.Stop(this);
			}

			ShowDialog();
			chanceCardsToFiles();

			if (englishOnly)
				removeOtherLanguages();
			if (oneEnglish)
				removeEnglishInternational();

			saveFiles();

			XmlRegistryKey rk = Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("CareerEditor");
			rk.SetValue("oneEnglish", oneEnglish);
			rk.SetValue("englishOnly", englishOnly);

			if (newpackage) package = this.package;
			return new Plugin.ToolResult(true, newpackage);
		}


        private Bcon getBcon(uint instance)
		{
			Interfaces.Files.IPackedFileDescriptor pfd = package.FindFile(0x42434F4E, 0, groupId, instance);
            if (pfd == null) return null;

			Bcon bcon = new Bcon();
			bcon.ProcessData(pfd, package);
			return bcon;
		}
		private SimPe.PackedFiles.Wrapper.Str getStr(uint instance)
		{
			Interfaces.Files.IPackedFileDescriptor pfd  = package.FindFile(0x53545223, 0, groupId, instance);
            if (pfd == null) return null;

            SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
			str.ProcessData(pfd, package);
			return str;
		}
		private void loadFiles()
		{
			Interfaces.Files.IPackedFileDescriptor[] ctss  = package.FindFiles(Data.MetaData.CTSS_FILE);
			groupId = ctss[0].Group;
            catalogueDesc = new SimPe.PackedFiles.Wrapper.Str();
            catalogueDesc.ProcessData(ctss[0], package);

            lifeScore = getBcon(0x100D); // not pets
            PTO = getBcon(0x1054);

            Interfaces.Files.IPackedFileDescriptor pfd = package.FindFile(0x42484156, 0, groupId, 0x1001);
            BHavReward = new Bhav();
            BHavReward.ProcessData(pfd, package);

            tuning = getBcon(0x1019);

            // Job Details
			jobTitles = getStr(0x0093);
			vehicleGUID = getBcon(0x100C);
			outfitGUID = getBcon(0x1055);

            // Promotion
            skillReq = new Bcon[8];
            skillReq[COOKING] = getBcon(0x1004);
            skillReq[MECHANICAL] = getBcon(0x1005);
            skillReq[BODY] = getBcon(0x1006);
            skillReq[CHARISMA] = getBcon(0x1007);
            skillReq[CREATIVITY] = getBcon(0x1008);
            skillReq[LOGIC] = getBcon(0x1009);
            skillReq[GARDENING] = getBcon(0x100A);
            skillReq[CLEANING] = getBcon(0x100B);
            if (skillReq[2] != null) // people
                trick = null;
            else // pets
                trick = getBcon(0x1004);
            friends = getBcon(0x1003);

            // Hours & Wages
			startHour  = getBcon(0x1001);
			hoursWorked = getBcon(0x1002);
            if (trick == null)
            {
                wages = getBcon(0x1000);
                wagesDog = wagesCat = null;
            }
            else
            {
                wagesDog = getBcon(0x1000);
                wagesCat = getBcon(0x1005);
                wages = null;
            }
			daysWorked = getBcon(0x101A);

			motiveDeltas = new Bcon[11];
			motiveDeltas[HUNGER] = getBcon(0x100E);
			motiveDeltas[THIRST] = getBcon(0x100F);
			motiveDeltas[COMFORT] = getBcon(0x1010);
			motiveDeltas[HYGIENE] = getBcon(0x1011);
			motiveDeltas[BLADDER] = getBcon(0x1012);
			motiveDeltas[ENERGY] = getBcon(0x1013);
			motiveDeltas[FUN] = getBcon(0x1014);
			motiveDeltas[PUBLIC] = getBcon(0x1015);
			motiveDeltas[FAMILY] = getBcon(0x1016);
			motiveDeltas[ENVIRONMENT] = getBcon(0x1017);
			motiveDeltas[MENTAL] = getBcon(0x1018);

            // Chance Cards
			chanceCardsText = getStr(0x012D);

            chanceASkills = new Bcon[8]; // not pets
			chanceASkills[COOKING] = getBcon(0x101C);
			chanceASkills[MECHANICAL] = getBcon(0x101D);
			chanceASkills[BODY] = getBcon(0x101E);
			chanceASkills[CHARISMA] = getBcon(0x101F);
			chanceASkills[CREATIVITY] = getBcon(0x1020);
			chanceASkills[LOGIC] = getBcon(0x1021);
			chanceASkills[GARDENING] = getBcon(0x1022);
			chanceASkills[CLEANING] = getBcon(0x1023);

            chanceAGood = new Bcon[10]; // not pets
			chanceAGood[COOKING] = getBcon(0x102E);
			chanceAGood[MECHANICAL] = getBcon(0x102F);
			chanceAGood[BODY] = getBcon(0x1030);
			chanceAGood[CHARISMA] = getBcon(0x1031);
			chanceAGood[CREATIVITY] = getBcon(0x1032);
			chanceAGood[LOGIC] = getBcon(0x1033);
			chanceAGood[GARDENING] = getBcon(0x1034);
			chanceAGood[CLEANING] = getBcon(0x1035);

            // these for pets
			chanceAGood[MONEY] = getBcon(0x102C);
			chanceAGood[JOB] = getBcon(0x102D);

            chanceABad = new Bcon[10]; // not pets
			chanceABad[COOKING] = getBcon(0x1038);
			chanceABad[MECHANICAL] = getBcon(0x1039);
			chanceABad[BODY] = getBcon(0x103A);
			chanceABad[CHARISMA] = getBcon(0x103B);
			chanceABad[CREATIVITY] = getBcon(0x103C);
			chanceABad[LOGIC] = getBcon(0x103D);
			chanceABad[GARDENING] = getBcon(0x103E);
			chanceABad[CLEANING] = getBcon(0x103F);

            // these for pets
			chanceABad[MONEY] = getBcon(0x1036);
			chanceABad[JOB] = getBcon(0x1037);

            chanceBSkills = new Bcon[8]; // not pets
			chanceBSkills[COOKING] = getBcon(0x1024);
			chanceBSkills[MECHANICAL] = getBcon(0x1025);
			chanceBSkills[BODY] = getBcon(0x1026);
			chanceBSkills[CHARISMA] = getBcon(0x1027);
			chanceBSkills[CREATIVITY] = getBcon(0x1028);
			chanceBSkills[LOGIC] = getBcon(0x1029);
			chanceBSkills[GARDENING] = getBcon(0x102A);
			chanceBSkills[CLEANING] = getBcon(0x102B);

			chanceBGood = new Bcon[10];
			chanceBGood[COOKING] = getBcon(0x1042);
			chanceBGood[MECHANICAL] = getBcon(0x1043);
			chanceBGood[BODY] = getBcon(0x1044);
			chanceBGood[CHARISMA] = getBcon(0x1045);
			chanceBGood[CREATIVITY] = getBcon(0x1046);
			chanceBGood[LOGIC] = getBcon(0x1047);
			chanceBGood[GARDENING] = getBcon(0x1048);
			chanceBGood[CLEANING] = getBcon(0x1049);

            // these for pets
			chanceBGood[MONEY] = getBcon(0x1040);
			chanceBGood[JOB] = getBcon(0x1041);

			chanceBBad = new Bcon[10];
			chanceBBad[COOKING] = getBcon(0x104C);
			chanceBBad[MECHANICAL] = getBcon(0x104D);
			chanceBBad[BODY] = getBcon(0x104E);
			chanceBBad[CHARISMA] = getBcon(0x104F);
			chanceBBad[CREATIVITY] = getBcon(0x1050);
			chanceBBad[LOGIC] = getBcon(0x1051);
			chanceBBad[GARDENING] = getBcon(0x1052);
			chanceBBad[CLEANING] = getBcon(0x1053);

            // these for pets
			chanceBBad[MONEY] = getBcon(0x104A);
			chanceBBad[JOB] = getBcon(0x104B);

			chanceChance = getBcon(0x101B);
		}
		private void saveFiles()
		{
			catalogueDesc.SynchronizeUserData();
            if (lifeScore != null)
                lifeScore.SynchronizeUserData();
            PTO.SynchronizeUserData();
            tuning.SynchronizeUserData();

            //if (hasReward)
            BHavReward.SynchronizeUserData();

            jobTitles.SynchronizeUserData();
			vehicleGUID.SynchronizeUserData();
			outfitGUID.SynchronizeUserData();

			startHour.SynchronizeUserData();
			hoursWorked.SynchronizeUserData();
			daysWorked.SynchronizeUserData();

            for (int i = 0; i < 11; i++)
                motiveDeltas[i].SynchronizeUserData();

            if (wages != null)
            {
                wages.SynchronizeUserData();
            }
            else
            {
                wagesDog.SynchronizeUserData();
                wagesCat.SynchronizeUserData();

                trick.Clear();
                for (short i = 0; i < noLevels; i++)
                {
                    ListViewItem item = PromoList.Items[i];
                    short tr = (short)cbTrick.Items.IndexOf(item.SubItems[9].Text);
                    if (tr > 0)
                    {
                        trick.Add(tr);
                        trick.Add((short)(i + 1));
                    }
                }
                trick.SynchronizeUserData();
            }
			friends.SynchronizeUserData();

			chanceCardsText.SynchronizeUserData();
			chanceChance.SynchronizeUserData();
            if (chanceASkills[0] != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    skillReq[i].SynchronizeUserData();
                    chanceASkills[i].SynchronizeUserData();
                    chanceBSkills[i].SynchronizeUserData();
                }
            }
            if (chanceAGood[0] != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    chanceAGood[i].SynchronizeUserData();
                    chanceABad[i].SynchronizeUserData();
                    chanceBGood[i].SynchronizeUserData();
                    chanceBBad[i].SynchronizeUserData();
                }
            }
            for (int i = 8; i < 10; i++)
            {
                chanceAGood[i].SynchronizeUserData();
                chanceABad[i].SynchronizeUserData();
                chanceBGood[i].SynchronizeUserData();
                chanceBBad[i].SynchronizeUserData();
            }
		}


        private void miInsertLvl_Click(object sender, System.EventArgs e)
        {
            SimPe.PackedFiles.Wrapper.StrLanguage us = new SimPe.PackedFiles.Wrapper.StrLanguage(1);
            ushort newNoLevels = (ushort)(noLevels + 1);
            tuning[0] = (short)newNoLevels;

            if (PTO.Count < newNoLevels + 1)
            {
                PTO.Add(15);
                if (lifeScore != null)
                    lifeScore.Add(0);

                // Job Details
                SimPe.PackedFiles.Wrapper.StrItemList usitems = jobTitles.LanguageItems(us);
                usitems[newNoLevels * 2 - 1].Title = "New Male Job Title";
                usitems[newNoLevels * 2].Title = "New Male Job Desc";
                usitems[newNoLevels * 2 - 1 + femaleOffset + 2].Title = "New Female Job Title";
                usitems[newNoLevels * 2 + femaleOffset + 2].Title = "New Female Job Desc";
                for (int i = 0; i < jobTitles.Languages.Length; i++)
                {
                    SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(jobTitles.Languages[i].Id);
                    for (int j = 0; j < 4; j++)
                        jobTitles.Add(new SimPe.PackedFiles.Wrapper.StrToken(j, l, "", ""));
                    if ((l.Id != 2) && (l.Id != 12) && (l.Id != 13))
                    {
                        SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(l);
                        for (int j = noLevels; j > 0; j--)
                        {
                            items[j * 2 + 1 + femaleOffset].Title = items[j * 2 - 1 + femaleOffset].Title;
                            items[j * 2 + 2 + femaleOffset].Title = items[j * 2 + femaleOffset].Title;
                        }
                        items[1 + femaleOffset].Title = "";
                        items[2 + femaleOffset].Title = "";
                    }
                }
                outfitGUID.Add(0);
                outfitGUID.Add(0);
                unchecked
                {
                    vehicleGUID.Add((short)0xAE14);
                    vehicleGUID.Add((short)0x0C85);
                }

                // Hours & Wages
                startHour.Add(0);
                hoursWorked.Add(1);
                if (wages != null)
                {
                    wages.Add(0);
                }
                else
                {
                    wagesDog.Add(0);
                    wagesCat.Add(0);
                }
                daysWorked.Add(0);

                for (int i = 0; i < motiveDeltas.Length; i++)
                    motiveDeltas[i].Add(0);

                // Promotion
                if (wages != null) // people
                    for (int i = 0; i < skillReq.Length; i++)
                        skillReq[i].Add(0);
                // nothing to do for Pets
                friends.Add(0);

                // Chance Cards
                if (chanceASkills[0] != null)
                    for (int i = 0; i < chanceASkills.Length; i++)
                    {
                        skillReq[i].Add(0);
                        chanceASkills[i].Add(0);
                        chanceBSkills[i].Add(0); ;
                    }

                if (chanceAGood[0] != null)
                    for (int i = 0; i < 8; i++)
                    {
                        chanceAGood[i].Add(0);
                        chanceABad[i].Add(0);
                        chanceBGood[i].Add(0);
                        chanceBBad[i].Add(0);
                    }
                for (int i = 8; i < chanceAGood.Length; i++)
                {
                    chanceAGood[i].Add(0);
                    chanceABad[i].Add(0);
                    chanceBGood[i].Add(0);
                    chanceBBad[i].Add(0);
                }
                chanceChance.Add(0);

                for (int i = 0; i < chanceCardsText.Languages.Length; i++)
                {
                    SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(chanceCardsText.Languages[i].Id);
                    for (int j = 0; j < 12; j++)
                        chanceCardsText.Add(new SimPe.PackedFiles.Wrapper.StrToken(j, l, "", ""));
                }
                usitems = chanceCardsText.LanguageItems(us);
                usitems[newNoLevels * 12 + 7].Title = "Choice A";
                usitems[newNoLevels * 12 + 8].Title = "Choice B";
                usitems[newNoLevels * 12 + 9].Title = "Male Text";
                usitems[newNoLevels * 12 + 10].Title = "Female Text";
                usitems[newNoLevels * 12 + 11].Title = "Pass A Male";
                usitems[newNoLevels * 12 + 12].Title = "Pass A Female";
                usitems[newNoLevels * 12 + 13].Title = "Fail A Male";
                usitems[newNoLevels * 12 + 14].Title = "Fail A Female";
                usitems[newNoLevels * 12 + 15].Title = "Pass B Male";
                usitems[newNoLevels * 12 + 16].Title = "Pass B Female";
                usitems[newNoLevels * 12 + 17].Title = "Fail B Male";
                usitems[newNoLevels * 12 + 18].Title = "Fail B Female";

                noLevelsChanged(newNoLevels);
            }
        }
        private void miRemoveLvl_Click(object sender, System.EventArgs e)
        {
            ushort newNoLevels = (ushort)(noLevels - 1);

            tuning[0] = (short)newNoLevels;

            if (PTO.Count > 11)
            {
                PTO.RemoveAt(noLevels);
                lifeScore.RemoveAt(noLevels);

                startHour.RemoveAt(noLevels);
                hoursWorked.RemoveAt(noLevels);
                daysWorked.RemoveAt(noLevels);
                if (wages != null)
                {
                    wages.RemoveAt(noLevels);
                }
                else
                {
                    wagesDog.RemoveAt(noLevels);
                    wagesCat.RemoveAt(noLevels);
                }

                if (chanceASkills[0] != null)
                    for (int i = 0; i < 8; i++)
                    {
                        skillReq[i].RemoveAt(noLevels);
                        chanceASkills[i].RemoveAt(noLevels);
                        chanceBSkills[i].RemoveAt(noLevels);
                    }

                friends.RemoveAt(noLevels);

                for (int i = 0; i < 11; i++)
                    motiveDeltas[i].RemoveAt(noLevels);

                if (chanceAGood[0] != null)
                    for (int i = 0; i < 8; i++)
                    {
                        chanceAGood[i].RemoveAt(noLevels);
                        chanceABad[i].RemoveAt(noLevels);
                        chanceBGood[i].RemoveAt(noLevels);
                        chanceBBad[i].RemoveAt(noLevels);
                    }
                for (int i = 8; i < 10; i++)
                {
                    chanceAGood[i].RemoveAt(noLevels);
                    chanceABad[i].RemoveAt(noLevels);
                    chanceBGood[i].RemoveAt(noLevels);
                    chanceBBad[i].RemoveAt(noLevels);
                }
                chanceChance.RemoveAt(noLevels);

                outfitGUID.RemoveAt(noLevels * 2);
                outfitGUID.RemoveAt(noLevels * 2);
                unchecked
                {
                    vehicleGUID.RemoveAt(noLevels * 2);
                    vehicleGUID.RemoveAt(noLevels * 2);
                }
            }

            for (int i = 0; i < jobTitles.Languages.Length; i++)
            {
                SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(jobTitles.Languages[i].Id);
                SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(l);
                if ((l.Id != 2) && (l.Id != 12) && (l.Id != 13))
                {
                    for (int j = 0; j < noLevels; j++)
                    {
                        items[j * 2 - 1 + femaleOffset].Title = items[j * 2 + 1 + femaleOffset].Title;
                        items[j * 2 + femaleOffset].Title = items[j * 2 + 2 + femaleOffset].Title;
                    }
                }

                for (int j = 0; j < 4; j++)
                    items.Remove(items[items.Length - 1]);
            }

            for (int i = 0; i < chanceCardsText.Languages.Length; i++)
            {
                SimPe.PackedFiles.Wrapper.StrLanguage l = new SimPe.PackedFiles.Wrapper.StrLanguage(chanceCardsText.Languages[i].Id);
                SimPe.PackedFiles.Wrapper.StrItemList items = chanceCardsText.LanguageItems(l);
                for (int j = 0; j < 12; j++)
                    items.Remove(items[items.Length - 1]);
            }

            noLevelsChanged(newNoLevels);
        }
        private void miEnglishOnly_Click(object sender, System.EventArgs e)
        {
            englishOnly = !englishOnly;
            ((System.Windows.Forms.MenuItem)sender).Checked = englishOnly;
        }

		private void CareerTitle_TextChanged(object sender, System.EventArgs e)
		{
			string text = ((System.Windows.Forms.TextBox)sender).Text;
			SimPe.PackedFiles.Wrapper.StrItemList items = catalogueDesc.LanguageItems(currentLanguage);
			items[0].Title = text;
		}
        private void CareerReward_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int index = ((System.Windows.Forms.ComboBox)sender).SelectedIndex;
            uint temp;

            if (index == CareerReward.Items.Count - 1)
                return;

            if (hasReward)
                temp = rewardGuids[index];
            else
                temp = adultGuids[index];

            byte val1 = (byte)(temp % 256);
            temp /= 256;
            byte val2 = (byte)(temp % 256);
            temp /= 256;
            byte val3 = (byte)(temp % 256);
            byte val4 = (byte)(temp / 256);

            if (hasReward)
            {
                BHavReward[2].Operands[5] = val1;
                BHavReward[2].Operands[6] = val2;
                BHavReward[2].Operands[7] = val3;
                BHavReward[2].Reserved1[0] = val4;
            }
            else
            {
                BHavReward[1].Operands[0] = val1;
                BHavReward[1].Operands[1] = val2;
                BHavReward[1].Operands[2] = val3;
                BHavReward[1].Operands[3] = val4;
            }
        }
        private void Language_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            levelChanging = true;

            int index = ((System.Windows.Forms.ComboBox)sender).SelectedIndex;
            currentLanguage = new SimPe.PackedFiles.Wrapper.StrLanguage((byte)languageID[index]);
            JobDetailList.Items.Clear();
            fillJobDetails();

            SimPe.PackedFiles.Wrapper.StrItemList items = catalogueDesc.LanguageItems(currentLanguage);
            CareerTitle.Text = items[0].Title;
            levelChanging = false;

            ushort newLevel = currentLevel;
            currentLevel = 0;
            levelChanged(newLevel);
        }

        private void JobDetailList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (levelChanging) return;
            System.Windows.Forms.ListView.SelectedIndexCollection indices = ((System.Windows.Forms.ListView)sender).SelectedIndices;
			if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
        private void JobDetailsCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            jdpFemale.DescValue = jdpMale.DescValue;
        }
        private void jdpMale_TitleValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpMale.TitleValue;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[1].Text = text;
            SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
            items[currentLevel * 2 - 1].Title = text;
        }
        private void jdpMale_DescValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpMale.DescValue;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[2].Text = text;
            SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
            items[currentLevel * 2].Title = text;
        }
        private void jdpFemale_TitleValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpFemale.TitleValue;
			SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
			items[currentLevel * 2 - 1 + femaleOffset].Title = text;
		}
        private void jdpFemale_DescValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpFemale.DescValue;
			SimPe.PackedFiles.Wrapper.StrItemList items = jobTitles.LanguageItems(currentLanguage);
			items[currentLevel * 2  + femaleOffset].Title = text;
		}
        private void Outfit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            int index = ((System.Windows.Forms.ComboBox)sender).SelectedIndex;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[3].Text = outfitStrings[index];
            if (index == outfitGuids.Length) // unknown
                return;

            ushort val1 = (ushort)(outfitGuids[index] % 65536);
            ushort val2 = (ushort)(outfitGuids[index] / 65536);

            outfitGUID[currentLevel * 2] = (short)val1;
            outfitGUID[currentLevel * 2 + 1] = (short)val2;
        }
        private void Vehicle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            int index = ((System.Windows.Forms.ComboBox)sender).SelectedIndex;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[4].Text = vehicleStrings[index];
            if (index == vehicleGuids.Length) // unknown
                return;

            ushort val1 = (ushort)(vehicleGuids[index] % 65536);
            ushort val2 = (ushort)(vehicleGuids[index] / 65536);

            vehicleGUID[currentLevel * 2] = (short)val1;
            vehicleGUID[currentLevel * 2 + 1] = (short)val2;
        }

        private void PromoList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (levelChanging) return;
            System.Windows.Forms.ListView.SelectedIndexCollection indices = ((System.Windows.Forms.ListView)sender).SelectedIndices;
            if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
		private void Promo_ValueChanged(object sender, System.EventArgs e)
		{
            if (levelChanging) return;

            ArrayList foo = new ArrayList(new NumericUpDown[] {
                PromoCooking, PromoMechanical, PromoBody, PromoCharisma,
                PromoCreativity, PromoLogic, PromoCleaning, PromoFriends
            });
            int i = foo.IndexOf((NumericUpDown)sender);
            if (i == -1) return; // crash!
            ListViewItem item = PromoList.Items[currentLevel - 1];

            short val = (short)((NumericUpDown)sender).Value;
            item.SubItems[i + 1].Text = "" + val;
            if (i < 7)
                skillReq[i][currentLevel] = (short)(val * 100);
            else if (i == 7)
                friends[currentLevel] = val;
        }
		private void Promo_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            Promo_ValueChanged(sender, e);		
		}
        private void cbTrick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (levelChanging) return;
            ListViewItem item = PromoList.Items[currentLevel - 1];
            item.SubItems[9].Text = (String)((ComboBox)sender).SelectedItem;

            List<short[]> lTrick = new List<short[]>();
            for (int i = 0; i < trick.Count / 2; i++)
                lTrick.Add(new short[] { trick[i * 2], trick[i * 2 + 1] });

            short[] result = new short[] { (short)((ComboBox)sender).SelectedIndex, (short)currentLevel };

            int insPtr = 0;
            while (insPtr < lTrick.Count && currentLevel > lTrick[insPtr][1])
                insPtr++;

            if (insPtr < lTrick.Count)
            {
                if (currentLevel == lTrick[insPtr][1])
                    lTrick[insPtr] = result;
                else
                    lTrick.Insert(insPtr, result);
            }
            else
                lTrick.Add(result);

            trick.Clear();
            foreach (short[] pair in lTrick)
            {
                trick.Add(pair[0]);
                trick.Add(pair[1]);
            }
        }

        private void HoursWagesList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (levelChanging) return;
            System.Windows.Forms.ListView.SelectedIndexCollection indices = ((System.Windows.Forms.ListView)sender).SelectedIndices;
			if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
        private void lnudWork_ValueChanged(object sender, System.EventArgs e)
        {
            if (levelChanging || internalchg) return;
            LabelledNumericUpDown nud = (LabelledNumericUpDown)sender;
            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            int i = -1;

            #region Hours
            List<LabelledNumericUpDown> lHours = new List<LabelledNumericUpDown>(new LabelledNumericUpDown[] {
                lnudWorkStart, lnudWorkHours,
            });
            List<Bcon> lbHours = new List<Bcon>(new Bcon[] { startHour, hoursWorked, });
            i = lHours.IndexOf(nud);
            if (i >= 0)
            {
                lbHours[i][currentLevel] = (short)nud.Value;
                item.SubItems[i + 1].Text = "" + nud.Value;
                lnudWorkFinish.Value = (startHour[currentLevel] + hoursWorked[currentLevel]) % 24;
                item.SubItems[3].Text = "" + lnudWorkFinish.Value;
                WorkChanged(currentLevel);
                return;
            }
            #endregion

            #region Wages
            List<LabelledNumericUpDown> lWages = new List<LabelledNumericUpDown>(new LabelledNumericUpDown[] {
                lnudWages, lnudWagesDog, lnudWagesCat,
            });
            List<Bcon> lbWages = new List<Bcon>(new Bcon[] { wages, wagesDog, wagesCat, });
            i = lWages.IndexOf(nud);
            if (i >= 0)
            {
                lbWages[i][currentLevel] = (short)nud.Value;
                item.SubItems[i + 4].Text = "" + nud.Value;
                return;
            }
            #endregion
        }
        private void lnudWork_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            lnudWork_ValueChanged(sender, new EventArgs());
        }
		private void Workday_CheckedChanged(object sender, System.EventArgs e)
		{
            if (levelChanging || internalchg) return;

            List<CheckBox> lcb = new List<CheckBox>(new CheckBox[] {
                WorkMonday, WorkTuesday, WorkWednesday, WorkThursday, WorkFriday, WorkSaturday, WorkSunday, 
            });

            int index = lcb.IndexOf((CheckBox)sender);
            if (index < 0 || index > 6) return; // crash!

            Boolset dw = new Boolset((byte)daysWorked[currentLevel]);
            dw[index] = ((CheckBox)sender).Checked;
            daysWorked[currentLevel] = dw;

            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            item.SubItems[index + 7].Text = "" + ((CheckBox)sender).Checked;
        }
		private void nudMotive_ValueChanged(object sender, System.EventArgs e)
		{
            if (levelChanging || internalchg) return;
            NumericUpDown nud = (NumericUpDown)sender;
            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            int i = -1;

            #region Motives
            List<NumericUpDown> lMotive = new List<NumericUpDown>(new NumericUpDown[] {
                WorkHunger, WorkThirst, WorkComfort, WorkHygiene, WorkBladder,
                WorkEnergy, WorkFun, WorkPublic, WorkFamily, WorkEnvironment,
                //WorkMental
            });
            List<NumericUpDown> lMotiveTotal = new List<NumericUpDown>(new NumericUpDown[] {
                HungerTotal, ThirstTotal, ComfortTotal, HygieneTotal, BladderTotal,
                EnergyTotal, FunTotal, PublicTotal, FamilyTotal, EnvironmentTotal,
                //MentalTotal
            });
            i = lMotive.IndexOf(nud);
            if (i >= 0)
            {
                motiveDeltas[i][currentLevel] = (short)nud.Value;
                lMotiveTotal[i].Value = motiveDeltas[i][currentLevel] * hoursWorked[currentLevel];
                return;
            }
            #endregion
        }
        private void nudMotive_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            nudMotive_ValueChanged(sender, new EventArgs());
		}

        private void lnudChanceCurrentLevel_ValueChanged(object sender, EventArgs e)
        {
            if (levelChanging) return;
            levelChanged((ushort)lnudChanceCurrentLevel.Value);
        }
        private void ChanceCopy_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            ChanceTextFemale.Text = ChanceTextMale.Text;
        }
	}
}