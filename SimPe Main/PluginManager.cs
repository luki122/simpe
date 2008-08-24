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

namespace SimPe
{
	/// <summary>
	/// This class manages the initialization of Various Plugins
	/// </summary>
	public class PluginManager : Ambertation.Threading.StoppableThread
	{
		LoadFileWrappersExt wloader;
		SimPe.LoadHelpTopics lht;
		internal PluginManager(
            System.Windows.Forms.ToolStripMenuItem toolmenu, 
			System.Windows.Forms.ToolStrip tootoolbar,
			TD.SandDock.TabControl dc, 
			LoadedPackage lp,
			SteepValley.Windows.Forms.ThemedControls.XPTaskBox defaultactiontaskbox,
            System.Windows.Forms.ContextMenuStrip defaultactionmenu,
			SteepValley.Windows.Forms.ThemedControls.XPTaskBox toolactiontaskbox, 
			SteepValley.Windows.Forms.ThemedControls.XPTaskBox extactiontaskbox,
			System.Windows.Forms.ToolStrip actiontoolbar,
			Ambertation.Windows.Forms.DockContainer docktooldc,
            System.Windows.Forms.ToolStripMenuItem helpmenu)
            : base(true)
		{
			SimPe.PackedFiles.TypeRegistry tr = new SimPe.PackedFiles.TypeRegistry();

			FileTable.ProviderRegistry = tr;
			FileTable.ToolRegistry = tr;
			FileTable.WrapperRegistry = tr;
			FileTable.HelpTopicRegistry = tr;
			FileTable.SettingsRegistry = tr;

			wloader = new LoadFileWrappersExt();

            Splash.Screen.SetMessage("Loading Dynamic Wrappers");
            this.LoadDynamicWrappers();
            Splash.Screen.SetMessage("Loading Static Wrappers");
			this.LoadStaticWrappers();

            Splash.Screen.SetMessage("Loading Menu Items");
			wloader.AddMenuItems(ref ChangedGuiResourceEvent, toolmenu, tootoolbar, new ToolMenuItemExt.ExternalToolNotify(ClosedToolPluginHandler));
            Splash.Screen.SetMessage("Loading Listeners");
			wloader.AddListeners(ref ChangedGuiResourceEvent);
			//dc.ActiveDocumentChanged += new TD.SandDock.ActiveDocumentEventHandler(wloader.ActiveDocumentChanged);
			//lp.AfterFileLoad += new SimPe.Events.PackageFileLoadedEvent(wloader.ChangedPackage);


            Splash.Screen.SetMessage("Loading Default Actions");
            LoadActionTools(defaultactiontaskbox, actiontoolbar, defaultactionmenu, GetDefaultActions());
            Splash.Screen.SetMessage("Loading External Tools");
			LoadActionTools(toolactiontaskbox, actiontoolbar, defaultactionmenu, LoadExternalTools());
            Splash.Screen.SetMessage("Loading ...");
			LoadActionTools(extactiontaskbox, actiontoolbar, null, null);

            Splash.Screen.SetMessage("Loading Docks");
			LoadDocks(docktooldc, lp);
            Splash.Screen.SetMessage("Loading Help Topics");
			lht = new LoadHelpTopics(helpmenu);

            Splash.Screen.SetMessage("");
		}

		/// <summary>
		/// firede whenever a (classic) Tool was closed
		/// </summary>
		public event ToolMenuItemExt.ExternalToolNotify ClosedToolPlugin;

		/// <summary>
		/// Event Wrapper
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="pk"></param>
		void ClosedToolPluginHandler(object sender, PackageArg pk)
		{
			if (ClosedToolPlugin!=null) 
				ClosedToolPlugin(sender, pk);			
		}

		

		/// <summary>
		/// Load all Static FileWrappers (theese Wrappers are allways available!)
		/// </summary>
		void LoadStaticWrappers()
		{
            Splash.Screen.SetMessage("Loading SettingsFactory");
            FileTable.WrapperRegistry.Register(new SimPe.Custom.SettingsFactory());
            Splash.Screen.SetMessage("Loading SimFactory");
            FileTable.WrapperRegistry.Register(new SimPe.PackedFiles.Wrapper.Factory.SimFactory());
            Splash.Screen.SetMessage("Loading ExtendedWrapperFactory");
            FileTable.WrapperRegistry.Register(new SimPe.PackedFiles.Wrapper.Factory.ExtendedWrapperFactory());
            Splash.Screen.SetMessage("Loading DefaultWrapperFactory");
            FileTable.WrapperRegistry.Register(new SimPe.PackedFiles.Wrapper.Factory.DefaultWrapperFactory());
            //FileTable.WrapperRegistry.Register(new SimPe.PackedFiles.Wrapper.Factory.GenericWrapperFactory());
            Splash.Screen.SetMessage("Loading ScenegraphWrapperFactory");
            FileTable.WrapperRegistry.Register(new SimPe.Plugin.ScenegraphWrapperFactory());
            FileTable.ToolRegistry.Register(new SimPe.Plugin.ScenegraphWrapperFactory());
            Splash.Screen.SetMessage("Loading RefFileFactory");
            FileTable.WrapperRegistry.Register(new SimPe.Plugin.RefFileFactory());
            Splash.Screen.SetMessage("Loading ClstWrapperFactory");
            FileTable.WrapperRegistry.Register(new SimPe.PackedFiles.Wrapper.Factory.ClstWrapperFactory());
        }

		/// <summary>
		/// Load all Wrappers found in the Plugins Folder
		/// </summary>
		void LoadDynamicWrappers()
		{			
			wloader.Scan(Helper.SimPePluginPath);
			//wloader.AddMenuItems(this.miPlugins, new EventHandler(ToolChangePacakge));
		}

		#region Action Tools			
		event SimPe.Events.ChangedResourceEvent ChangedGuiResourceEvent;

		object thsender;
		SimPe.Events.ResourceEventArgs the;
		protected override void StartThread()
		{			
			System.Delegate[] dls = ChangedGuiResourceEvent.GetInvocationList();
			foreach (System.Delegate d in dls) 
			{
				if (this.HaveToStop) 
					break;				
				((SimPe.Events.ChangedResourceEvent)d)(thsender, the);
			}
		}

		/// <summary>
		/// Fires with the same arguments that were used during 
		/// the last Time <see cref="ChangedGuiResourceEventHandler"/> was called
		/// </summary>
		public void ChangedGuiResourceEventHandler()
		{
			if (the!=null) ChangedGuiResourceEventHandler(thsender, the);
		}
		
		/// <summary>
		/// Fired when a Resource was changed by a ToolPlugin and the Enabled state needs to be changed
		/// </summary>
		public void ChangedGuiResourceEventHandler(object sender, SimPe.Events.ResourceEventArgs e)
		{
            RemoteControl.FireResourceListSelectionChangedHandler(sender, e);
			if (ChangedGuiResourceEvent!=null) 
			{
				thsender = sender;
				the = e;
				
				//this.ExecuteThread(System.Threading.ThreadPriority.Normal, "ActionTool notification");
				
				//ChangedGuiResourceEvent(sender, e);

				System.Delegate[] dls = ChangedGuiResourceEvent.GetInvocationList();
				foreach (System.Delegate d in dls) 
				{
					if (d.Target is SimPe.Interfaces.IToolExt) 
						if (!((SimPe.Interfaces.IToolExt)d.Target).Visible) continue;

					((SimPe.Events.ChangedResourceEvent)d)(sender, e);
				}
			}
		}

		/// <summary>
		/// Returns a List of Builtin Actions
		/// </summary>
		/// <returns></returns>
		SimPe.Interfaces.IToolAction[] GetDefaultActions()
		{
			return new SimPe.Interfaces.IToolAction[] {
														  new SimPe.Actions.Default.AddAction(),
														  new SimPe.Actions.Default.ExportAction(),
														  new SimPe.Actions.Default.ReplaceAction(),
														  new SimPe.Actions.Default.DeleteAction(),
														  new SimPe.Actions.Default.RestoreAction(),
														  new SimPe.Actions.Default.CloneAction(),
														  new SimPe.Actions.Default.CreateAction()
													  };
		}

		/// <summary>
		/// Load all available Action Tools
		/// </summary>
		void LoadActionTools(
			SteepValley.Windows.Forms.ThemedControls.XPTaskBox taskbox, 
			System.Windows.Forms.ToolStrip tb,
            System.Windows.Forms.ContextMenuStrip mi, 
			SimPe.Interfaces.IToolAction[] tools)
		{			
			if (tools==null) tools = FileTable.ToolRegistry.Actions;

			int top =  4 + taskbox.DockPadding.Top;
			if (taskbox.Tag!=null) top = (int)taskbox.Tag;

			bool tfirst = true;
			bool mfirst = true;
			foreach (SimPe.Interfaces.IToolAction tool in tools) 
			{
				ActionToolDescriptor atd = new ActionToolDescriptor(tool);
				ChangedGuiResourceEvent += new SimPe.Events.ChangedResourceEvent(atd.ChangeEnabledStateEventHandler);				

				if (taskbox!=null) 
				{
					atd.LinkLabel.Top = top;
					atd.LinkLabel.Left = 12;
					top += atd.LinkLabel.Height;
					atd.LinkLabel.Parent = taskbox;
					atd.LinkLabel.Visible = taskbox.IsExpanded;
					atd.LinkLabel.AutoSize = true;
				}

				if (mi!=null) 
				{
                    bool beggrp = (mfirst && mi.Items.Count != 0);
                    if (beggrp) mi.Items.Add("-");
                    mi.Items.Add(atd.MenuButton);
                    

					mfirst = false;
				}

				if (tb!=null && atd.ToolBarButton!=null)
				{
                    ////RECHECK
					//atd.ToolBarButton.BeginGroup = (tfirst && tb.Items.Count!=0);
                    
					
                    if (tfirst && tb.Items.Count != 0)
                        tb.Items.Add(new System.Windows.Forms.ToolStripSeparator());
                    tb.Items.Add(atd.ToolBarButton);
                    
					tfirst = false;
				}
				
			}
			taskbox.Height = top + 4;
			taskbox.Tag = top;
		}
		#endregion

		#region External Program Tools
		SimPe.Interfaces.IToolAction[] LoadExternalTools()
		{
   			ToolLoaderItemExt[] items = ToolLoaderExt.Items;
			SimPe.Interfaces.IToolAction[] tools = new SimPe.Interfaces.IToolAction[items.Length];
			for (int i=0; i<items.Length; i++)
				tools[i] = new SimPe.Actions.Default.StartExternalToolAction(items[i]);
			
			return tools;
		}
		#endregion

		#region dockable Tools
		void LoadDocks(Ambertation.Windows.Forms.DockContainer dc, LoadedPackage lp)
		{
			foreach (SimPe.Interfaces.IDockableTool idt in FileTable.ToolRegistry.Docks)
			{
				Ambertation.Windows.Forms.DockPanel dctrl = idt.GetDockableControl();

                
				if (dctrl!=null) 
				{
                    dctrl.Name = "dc."+idt.GetType().Namespace + "." + idt.GetType().Name;
					dctrl.Manager = dc.Manager;
                    dc.Controls.Add(dctrl);
					//dctrl.DockNextTo(dc);

					ChangedGuiResourceEvent += new SimPe.Events.ChangedResourceEvent(idt.RefreshDock);
					dctrl.Tag = idt.Shortcut;
					idt.RefreshDock(this, new SimPe.Events.ResourceEventArgs(lp));
				}
			}
		}
		#endregion
	}
}
