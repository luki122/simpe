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
using System.Reflection;
using System.Collections;
using System.IO;
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.Events;
using System.Windows.Forms;

namespace SimPe
{
    public class MyButtonItem : TD.SandBar.ButtonItem
    {        
        static int counter = 0;

        #region Layout stuff
        public static void GetLayoutInformations(Control b)
        {
            ArrayList list = Helper.WindowsRegistry.Layout.VisibleToolbarButtons;
            GetLayoutInformations(b, list);
        }

        static void GetLayoutInformations(Control b, ArrayList list)
        {
            foreach (Control c in b.Controls)
                GetLayoutInformations(c, list);

            TD.SandBar.ToolBar tb = b as TD.SandBar.ToolBar;
            if (tb != null)
            {
                foreach (object o in tb.Items)
                {
                    MyButtonItem mbi = o as MyButtonItem;
                    if (mbi != null)
                        //if (!mbi.HaveDock)
                            mbi.Visible = list.Contains(mbi.Name);
                }
            }

        }

        public static void SetLayoutInformations(Control b){
            ArrayList list = new ArrayList();
            SetLayoutInformations(b, list);

            Helper.WindowsRegistry.Layout.VisibleToolbarButtons = list;
        }

        static void SetLayoutInformations(Control b, ArrayList list)
        {
            foreach (Control c in b.Controls)
                SetLayoutInformations(c, list);

            TD.SandBar.ToolBar tb = b as TD.SandBar.ToolBar;
            if (tb != null)
            {
                foreach (object o in tb.Items)
                {
                    MyButtonItem mbi = o as MyButtonItem;
                    if (mbi != null)
                        if (mbi.Visible /*&& !mbi.HaveDock*/)
                            list.Add(mbi.Name);
                }
            }
            
        }
        #endregion

        string name;
        public string Name
        {
            get { return name; }
        }

        bool havedock;
        public bool HaveDock
        {
            get { return havedock; }
        }

        public MyButtonItem(string name)
            : this(null, name) { }

        internal MyButtonItem(ToolStripMenuItem item)
            : this(item, null) { }

        ToolStripMenuItem refitem;
        MyButtonItem(ToolStripMenuItem item, string name)
            : base()
        {
            refitem = item;
            if (item != null)
            {
                this.Image = item.Image;
                this.Visible = (item.Image != null);
                if (this.Image == null) this.Text = item.Text;
                this.ToolTipText = item.Text.Replace("&", "");
                this.Enabled = item.Enabled;
                this.Activate += new EventHandler(MyButtonItem_Activate);
                item.CheckedChanged += new EventHandler(item_CheckedChanged);
                item.EnabledChanged += new EventHandler(item_EnabledChanged);
                
                this.ToolTipText = item.Text;
                this.Enabled = item.Enabled;
                this.Checked = item.Checked;

                havedock = false;
                ToolMenuItemExt tmie = item as ToolMenuItemExt;
                if (tmie != null) this.name = tmie.Name;
                else
                {
                    TD.SandDock.DockableWindow dw = item.Tag as TD.SandDock.DockableWindow;

                    if (dw != null)
                    {
                        this.name = dw.Name;
                        havedock = true;
                    }
                    else this.name = "Button_" + (counter++);
                }
            }
            else
            {
                havedock = false;
                this.name = name;
            }


            
        }

        void item_EnabledChanged(object sender, EventArgs e)
        {
            this.Enabled = ((ToolStripMenuItem)sender).Enabled;
        }

        void item_CheckedChanged(object sender, EventArgs e)
        {
            this.Checked = ((ToolStripMenuItem)sender).Checked;
        }

        void MyButtonItem_Activate(object sender, EventArgs e)
        {
            refitem.PerformClick();
        }
    }
	public class ToolMenuItemExt  : ToolStripMenuItem
	{
		/// <summary>
		/// Those delegates can be called when a Tool want's to notify the Host Application
		/// </summary>
		public delegate void ExternalToolNotify(object sender, PackageArg pk);
		IToolPlugin tool;
		

		/// <summary>
		/// Return null, or the stored extended Tool
		/// </summary>
		public IToolExt ToolExt 
		{
			get 
			{
				//if (tool.GetType().GetInterface("SimPe.Interfaces.IToolExt", true) == typeof(SimPe.Interfaces.IToolExt)) return (SimPe.Interfaces.IToolExt)tool;
				if (tool is SimPe.Interfaces.IToolExt) return (SimPe.Interfaces.IToolExt)tool;
				else return null;
			}
		}

		/// <summary>
		/// Return null, or the stored  Tool
		/// </summary>
		public ITool Tool 
		{
			get 
			{
				if (tool is SimPe.Interfaces.ITool) return (SimPe.Interfaces.ITool)tool;
				else return null;
			}
		}

		/// <summary>
		/// Return null, or the stored ToolPlus Item
		/// </summary>
		public IToolPlus ToolPlus 
		{
			get 
			{
				if (tool is SimPe.Interfaces.IToolPlus) return (SimPe.Interfaces.IToolPlus)tool;
				else return null;
			}
		}
		Interfaces.Files.IPackedFileDescriptor pfd;
		Interfaces.Files.IPackageFile package;
		/// <summary>
		/// null or a Function to call when the Pacakge was changed by a Tool Plugin
		/// </summary>
		ExternalToolNotify chghandler;

		ExternalToolNotify ChangeHandler 
		{
			get { return chghandler; }
			set {chghandler = value; }
		}

        string name;
        public string Name
        {
            get { return name; }
        }
		public ToolMenuItemExt(IToolPlus tool, ExternalToolNotify chghnd) : this(tool.ToString(), tool, chghnd)
		{			
		}

		public ToolMenuItemExt(string text, IToolPlugin tool, ExternalToolNotify chghnd) 
		{
			this.tool = tool;
			this.Text = text;
			this.ToolTipText = text;
			Click += new EventHandler(LinkClicked);
			Click += new EventHandler(ClickItem);
			chghandler = chghnd;

            name = tool.GetType().Namespace + "." + tool.GetType().Name;
		}
		
		private void ClickItem(object sender, System.EventArgs e) 
		{
			if (Tool==null) return;
			try 
			{
				if (Tool.IsEnabled(pfd, package)) 
				{
					SimPe.Interfaces.Plugin.IToolResult tr = Tool.ShowDialog(ref pfd, ref package);
					WaitingScreen.Stop();
					if (tr.ChangedAny) 
					{
						PackageArg p = new PackageArg();
						p.Package = package;
						p.FileDescriptor = pfd;
						p.Result = tr;
						if (chghandler!=null) chghandler(this, p);
					}
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("Unable to Start ToolPlugin.", ex);
			}
		}

		#region Event Handler		
		SimPe.Events.ResourceEventArgs lasteventarg;

		/// <summary>
		/// Fired when a Resource was changed by a ToolPlugin and the Enabled state needs to be changed
		/// </summary>
		internal void ChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs e) 
		{
			this.Package = AbstractToolPlus.ExtractPackage(e);
			this.FileDescriptor = AbstractToolPlus.ExtractFileDescriptor(e);

			if (Tool!=null) 
			{
				UpdateEnabledState();
			} 
			else if (ToolPlus!=null) 
			{
				lasteventarg = e;
				this.Enabled = ToolPlus.ChangeEnabledStateEventHandler(sender, e);					
			}
		}

		/// <summary>
		/// Fired when a Link is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkClicked(object sender, EventArgs e)
		{
			if (ToolPlus==null) return;
			if (lasteventarg.LoadedPackage!=null) lasteventarg.LoadedPackage.PauseIndexChangedEvents();
			ToolPlus.Execute(sender, lasteventarg);
			if (lasteventarg.LoadedPackage!=null) lasteventarg.LoadedPackage.RestartIndexChangedEvents();
		}
		#endregion

		public override string ToString()
		{
			try 
			{
				return tool.ToString();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("Unable to Load ToolPlugin.", ex);
			}

			return "Plugin Error";
		}

		public Interfaces.Files.IPackedFileDescriptor FileDescriptor
		{
			get { return pfd; }
			set { pfd = value; }
		}

		public Interfaces.Files.IPackageFile Package
		{
			get { return package; }
			set { package = value; }
		}

		void UpdateEnabledState()
		{
			try 
			{
				Enabled = Tool.IsEnabled(pfd, package);
			} 
			catch (Exception)
			{
				Enabled = false;
			}
		}
	}

	/// <summary>
	/// Class that can be used to Load external Filewrappers int the given Registry
	/// </summary>
	public class LoadFileWrappersExt : LoadFileWrappers
	{
		/// <summary>
		/// Constructor of The class
		/// </summary>
		/// <param name="registry">
		/// Registry the External Data should be added to
		/// </param>
		/// <param name="toolreg">Registry the tools should be added to</param>
		public LoadFileWrappersExt() :base(FileTable.WrapperRegistry, FileTable.ToolRegistry)
		{
		}

		/// <summary>
		/// Add one single MenuItem (and all needed Parents)
		/// </summary>
		/// <param name="item"></param>
		/// <param name="parts"></param>
		public static void AddMenuItem(ref SimPe.Events.ChangedResourceEvent ev, ToolStripItemCollection parent, ToolMenuItemExt item, string[] parts)
		{
			System.Reflection.Assembly a = typeof(LoadFileWrappersExt).Assembly;

			for (int i=0; i<parts.Length-1; i++) 
			{
				string name = SimPe.Localization.GetString(parts[i]);
				ToolStripMenuItem mi = null;
				//find an existing Menu Item
				if (parent!=null) 
				{
					foreach (ToolStripMenuItem oi in parent) 
					{
						if (oi.Text.ToLower().Trim()==name.ToLower().Trim()) 
						{
							mi = oi;
							break;
						}
					}
				}
				if (mi==null) 
				{
					mi = new ToolStripMenuItem(name);
					
					if (parent!=null) 
					{						
						System.IO.Stream imgstr = a.GetManifestResourceStream("SimPe."+parts[i]+".png");
						if (imgstr!=null) mi.Image = System.Drawing.Image.FromStream(imgstr);
						parent.Insert(0, mi);
					}
					
				}

				parent = mi.DropDownItems;
			}

			if (item.ToolExt!=null) 
			{
				item.ShortcutKeys = Helper.ToKeys(item.ToolExt.Shortcut);                
				item.Image = item.ToolExt.Icon;			
				//item.ToolTipText = item.ToolExt.ToString();
			}

			parent.Add(item);			
			ev += new SimPe.Events.ChangedResourceEvent(item.ChangeEnabledStateEventHandler);
			item.ChangeEnabledStateEventHandler(item, new SimPe.Events.ResourceEventArgs(null));			
		}

		/// <summary>
		/// Build a ToolBar that matches the Content of a MenuItem
		/// </summary>
		/// <param name="tb"></param>
		/// <param name="mi"></param>
		/// <param name="exclude">List of <see cref="TD.SandBar.MenuButtonItem"/> that should be excluded</param>
		public static void BuildToolBar(TD.SandBar.ToolBar tb, ToolStripItemCollection mi, ArrayList exclude)
		{
			System.Collections.Generic.List<ToolStripItemCollection> submenus = new System.Collections.Generic.List<ToolStripItemCollection>();
            System.Collections.Generic.List<ToolStripMenuItem> items = new System.Collections.Generic.List<ToolStripMenuItem>();
            System.Collections.Generic.List<ToolStripMenuItem> starters = new System.Collections.Generic.List<ToolStripMenuItem>();

			for (int i=mi.Count-1; i>=0; i--)
			{
                ToolStripMenuItem tsmi = mi[i] as ToolStripMenuItem;
                if (tsmi == null)
                {
                    if (i < mi.Count - 1)
                        starters.Add(mi[i + 1] as ToolStripMenuItem);
                    continue;
                }
                if (tsmi.DropDownItems.Count > 0) submenus.Add(tsmi.DropDownItems);
				else 
				{
                    ToolStripMenuItem item = tsmi;
					if (exclude.Contains(item)) continue;
					if (item.Image==null) items.Add(item);
					else items.Insert(0, item);
				}
			}

			for (int i=0; i<items.Count; i++)
			{
                ToolStripMenuItem item = items[i];				
				TD.SandBar.ButtonItem bi = new MyButtonItem(item);
                bi.BeginGroup = (i == 0 && tb.Items.Count > 0) || starters.Contains(item); ;				                

				tb.Items.Add(bi);
			}
			

			for (int i=0; i<submenus.Count; i++)
				BuildToolBar(tb, submenus[i], exclude);			
		}

        ToolStripMenuItem mi;
		/// <summary>
		/// Adds the Tool Plugins to the passed menu
		/// </summary>
		/// <param name="mi">The Menu you want to add Items to</param>
		/// <param name="chghandler">A Function to call when the Package was chaged by a Tool</param>
        public static void AddMenuItems(IToolExt[] toolsp, ref SimPe.Events.ChangedResourceEvent ev, ToolStripMenuItem mi, ToolMenuItemExt.ExternalToolNotify chghandler) 
		{			
			foreach (IToolExt tool in toolsp)
			{		
				string name = tool.ToString();
				string[] parts = name.Split("\\".ToCharArray());
				name = SimPe.Localization.GetString(parts[parts.Length-1]);
				ToolMenuItemExt item = new ToolMenuItemExt(name, tool, chghandler);

				AddMenuItem(ref ev, mi.DropDownItems, item, parts);
			}
		}
		/// <summary>
		/// Adds the Tool Plugins to the passed menu
		/// </summary>
		/// <param name="mi">The Menu you want to add Items to</param>
		/// <param name="chghandler">A Function to call when the Package was chaged by a Tool</param>
		public void AddMenuItems(ref SimPe.Events.ChangedResourceEvent ev, ToolStripMenuItem mi, TD.SandBar.ToolBar tb, ToolMenuItemExt.ExternalToolNotify chghandler) 
		{
			this.mi = mi;
			IToolExt[] toolsp = (IToolExt[])FileTable.ToolRegistry.ToolsPlus;
			AddMenuItems(toolsp, ref ev, mi, chghandler);

			ITool[] tools = FileTable.ToolRegistry.Tools;
			
			foreach (ITool tool in tools)
			{
				string name = tool.ToString().Trim();
				if (name=="") continue;

				string[] parts = name.Split("\\".ToCharArray());
				name = SimPe.Localization.GetString(parts[parts.Length-1]);
				ToolMenuItemExt item = new ToolMenuItemExt(name, tool, chghandler);

                AddMenuItem(ref ev, mi.DropDownItems, item, parts);
			}



            BuildToolBar(tb, mi.DropDownItems, new ArrayList());
			//EnableMenuItems(null);
		}

		/// <summary>
		/// Link all Listeners with the GUI Control
		/// </summary>
		/// <param name="ev"></param>
		public void AddListeners(ref SimPe.Events.ChangedResourceEvent ev)
		{
			//load Listeners
			foreach (IListener item in  FileTable.ToolRegistry.Listeners)
			{
				ev += new SimPe.Events.ChangedResourceEvent(item.SelectionChangedHandler);
				item.SelectionChangedHandler(item, new SimPe.Events.ResourceEventArgs(null));
			}
		}

		/*/// <summary>
		/// Used to determin the enabled State of a Tool
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ActiveDocumentChanged(object sender, TD.SandDock.ActiveDocumentEventArgs e)
		{
			if (e==null) EnableMenuItems(null);
			else if (e.NewActiveDocument==null) EnableMenuItems(null);
			else if (e.NewActiveDocument.Tag is SimPe.Interfaces.Plugin.IFileWrapper) 
				EnableMenuItems((SimPe.Interfaces.Plugin.IFileWrapper)e.NewActiveDocument.Tag);
		}

		/// <summary>
		/// Used to determin the Enabled state of a Tool
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ChangedPackage(LoadedPackage sender) 
		{
			EnableMenuItems(null, sender.Package);
		}*/

		/*/// <summary>
		/// Set the Enabled state of the Tool MenuItems
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="wrapper"></param>
		void EnableMenuItems(SimPe.Interfaces.Plugin.IFileWrapper wrapper)
		{
			if (wrapper==null) EnableMenuItems(null, null);
			else EnableMenuItems(wrapper.FileDescriptor, wrapper.Package);
		}

		/// <summary>
		/// Set the Enabled state of the Tool MenuItems
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="pfd"></param>
		/// <param name="package"></param>
		void EnableMenuItems(TD.SandBar.MenuItemBase.MenuItemCollection parent, SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
		{
			foreach(TD.SandBar.MenuButtonItem item in parent) 
			{
				try 
				{
					if (item is ToolMenuItemExt) 
					{
						ToolMenuItemExt tmi = (ToolMenuItemExt)item;
						tmi.Package = package;
						tmi.FileDescriptor = pfd;
						tmi.UpdateEnabledState();
					} 
					else 
					{
						EnableMenuItems(item.Items, pfd, package);
					}
				} 
				catch (Exception) {}
			}
		}

		/// <summary>
		/// Set the Enabled state of the Tool MenuItems
		/// </summary>
		/// <param name="mi"></param>
		/// <param name="wrapper"></param>
		void EnableMenuItems(SimPe.Interfaces.Files.IPackedFileDescriptor pfd, SimPe.Interfaces.Files.IPackageFile package)
		{
			EnableMenuItems(mi.Items, pfd, package);
		}
		*/
		
	}
}