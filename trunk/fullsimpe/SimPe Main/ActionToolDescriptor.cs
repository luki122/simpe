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
	/// Zusammenfassung f�r LoadActionTool.
	/// </summary>
	internal class ActionToolDescriptor
	{
		SimPe.Interfaces.IToolAction tool;
		SteepValley.Windows.Forms.XPLinkedLabelIcon ll;
		LoadedPackage lp;

		SimPe.Events.ResourceEventArgs lasteventarg;
		
		/// <summary>
		/// Returns the generated LinkLabel
		/// </summary>
		public SteepValley.Windows.Forms.XPLinkedLabelIcon LinkLabel 
		{
			get {return ll; }
		}

		TD.SandBar.ButtonItem bi;
		/// <summary>
		/// Returns the generated ToolBar ButtonItem (can be null)
		/// </summary>
		public TD.SandBar.ButtonItem ToolBarButton
		{
			get {return bi; }
		}

		TD.SandBar.MenuButtonItem mi;
		/// <summary>
		/// Returns the generated MenuButtonItem 
		/// </summary>
		public TD.SandBar.MenuButtonItem MenuButton
		{
			get {return mi; }
		}

		/// <summary>
		/// Create a new Instance
		/// </summary>
		/// <param name="tool"></param>
		public ActionToolDescriptor(SimPe.Interfaces.IToolAction tool)
		{
			//this.lp = lp;
			this.tool = tool;

			ll = new SteepValley.Windows.Forms.XPLinkedLabelIcon();
			ll.Name = tool.ToString();
			if (tool.Icon!=null)
				if (tool.Icon is System.Drawing.Bitmap)
					ll.Icon = System.Drawing.Icon.FromHandle(((System.Drawing.Bitmap)tool.Icon).GetHicon());
			ll.Text = SimPe.Localization.GetString(tool.ToString());
			ll.LinkArea = new System.Windows.Forms.LinkArea(0, ll.Text.Length);
			ll.Font = new System.Drawing.Font("Verdana", ll.Font.Size, System.Drawing.FontStyle.Bold);
			ll.Height = 16;
			ll.AutoSize = true;

			ll.LinkClicked += new EventHandler(LinkClicked);

			mi = new TD.SandBar.MenuButtonItem(ll.Text);
			mi.Activate += new EventHandler(LinkClicked);
			mi.Image = tool.Icon;
			mi.Shortcut = tool.Shortcut;

			if (tool.Icon!=null) 
			{
				bi = new MyButtonItem("action."+tool.GetType().Namespace+"."+tool.GetType().Name);
				bi.Text = "";
				bi.ToolTipText = ll.Text;
				bi.Image = tool.Icon;
				bi.BuddyMenu = mi;

				//bi.Activate += new EventHandler(LinkClicked);
			}

			//Make Sure the Action is disabled on StartUp
			ChangeEnabledStateEventHandler(null, new SimPe.Events.ResourceEventArgs(lp));
		}

		/// <summary>
		/// Fired when a Resource was changed by a ToolPlugin and the Enabled state needs to be changed
		/// </summary>
		public void ChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs e) 
		{
			lp = e.LoadedPackage;
			ll.Enabled = tool.ChangeEnabledStateEventHandler(sender, e);
			//if (bi!=null) 
			mi.Enabled = ll.Enabled;

			lasteventarg = e;
		}

		/// <summary>
		/// Fired when a Link is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkClicked(object sender, EventArgs e)
		{

			if (lp!=null) lp.PauseIndexChangedEvents();
			tool.ExecuteEventHandler(sender, lasteventarg);
			if (lp!=null) lp.RestartIndexChangedEvents();
		}
	}	
}
