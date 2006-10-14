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
using System.Data;
using SimPe.Events;

namespace SimPe
{
    partial class MainForm
    {
        private void SetupMainForm()
        {
            manager.Visible = false;
            createdmenus = false;
            WaitBarControl wbc = new WaitBarControl(this);
            Wait.Bar = wbc;

            package = new LoadedPackage();
            package.BeforeFileLoad += new PackageFileLoadEvent(BeforeFileLoad);
            package.AfterFileLoad += new PackageFileLoadedEvent(AfterFileLoad);
            package.BeforeFileSave += new PackageFileSaveEvent(BeforeFileSave);
            package.AfterFileSave += new PackageFileSavedEvent(AfterFileSave);
            package.IndexChanged += new EventHandler(ChangedActiveIndex);
            package.AddedResource += new EventHandler(AddedRemovedIndexResource);
            package.RemovedResource += new EventHandler(AddedRemovedIndexResource);

            filter = new ViewFilter();
            treebuilder = new TreeBuilder(package, filter);
            resloader = new ResourceLoader(dc, package);
            remote = new RemoteHandler(this, package, resloader, miWindow);

            plugger = new PluginManager(
                miTools,
                tbTools,
                dc,
                package,
                tbDefaultAction,
                miAction,
                tbExtAction,
                tbPlugAction,
                tbAction,
                dockBottom,
                this.mbiTopics);
            plugger.ClosedToolPlugin += new ToolMenuItemExt.ExternalToolNotify(ClosedToolPlugin);
            remote.SetPlugger(plugger);


            remote.LoadedResource += new ChangedResourceEvent(rh_LoadedResource);

            SetupResourceViewToolBar();
            package.UpdateRecentFileMenu(this.miRecent);

            InitTheme();

            dockBottom.Height = ((this.Height * 3) / 4);
            this.Text += " (Version " + Helper.SimPeVersion.ProductVersion + ")";
            
            TD.SandDock.SandDockManager sdm2 = new TD.SandDock.SandDockManager();
            sdm2.OwnerForm = this;
            ThemeManager.Global.AddControl(sdm2);

            this.dc.Manager = sdm2;

            lv.SmallImageList = FileTable.WrapperRegistry.WrapperImageList;
            this.tvType.ImageList = FileTable.WrapperRegistry.WrapperImageList;


            InitMenuItems();
            Ambertation.Windows.Forms.ToolStripRuntimeDesigner.Add(tbContainer);
            Ambertation.Windows.Forms.ToolStripRuntimeDesigner.LineUpToolBars(tbContainer);

            Ambertation.Windows.Forms.Serializer.Global.Register(tbContainer);
            Ambertation.Windows.Forms.Serializer.Global.Register(manager);

            manager.ForceCleanUp();
            //this.dcResource.BringToFront();
            //this.dcResourceList.BringToFront();
        }

        void LoadForm(object sender, System.EventArgs e)
        {
            if (Helper.WindowsRegistry.PreviousVersion == 0) About.ShowWelcome();

            dcFilter.Collapse(false);

            if (!Helper.WindowsRegistry.HiddenMode)
            {
                lv.Columns.RemoveAt(lv.Columns.Count - 1);
                lv.Columns.RemoveAt(lv.Columns.Count - 1);
            }

            cbsemig.Items.Add("[Group Filter]");
            cbsemig.Items.Add(new SimPe.Data.SemiGlobalAlias(true, 0x7FD46CD0, "Globals"));
            cbsemig.Items.Add(new SimPe.Data.SemiGlobalAlias(true, 0x7FE59FD0, "Behaviour"));
            foreach (Data.SemiGlobalAlias sga in Data.MetaData.SemiGlobals)
                if (sga.Known) this.cbsemig.Items.Add(sga);
            if (cbsemig.Items.Count > 0) cbsemig.SelectedIndex = 0;

            ReloadLayout();

            if (Helper.WindowsRegistry.CheckForUpdates)
                About.ShowUpdate();

            //load Files passed on the commandline
            package.LoadOrImportFiles(pargs, true);

            //Set the Lock State of the Docks
            MakeFloatable(!Helper.WindowsRegistry.LockDocks);
            
            manager.Visible = true;

            
        }
    }
}
