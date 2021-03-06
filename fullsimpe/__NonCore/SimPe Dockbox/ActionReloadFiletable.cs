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

namespace SimPe.Plugin.Tool.Action
{
	/// <summary>
	/// The ReloadFileTable Action
	/// </summary>
	public class ActionReloadFiletable : SimPe.Interfaces.IToolAction
	{
		
		#region IToolAction Member

		public virtual bool ChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs es)
		{
			return true;								
		}

		public void ExecuteEventHandler(object sender, SimPe.Events.ResourceEventArgs e)
		{
			if (!ChangeEnabledStateEventHandler(null, e)) return;
			
            // Once you reload the filetable, you're no longer in local mode
            //bool old = SimPe.Helper.LocalMode;
            SimPe.Helper.LocalMode = false;
			SimPe.FileTable.FileIndex.ForceReload();
            //SimPe.Helper.LocalMode = old;
		}

		#endregion		

		
		#region IToolPlugin Member
		public override string ToString()
		{
			return "Reload FileTable";
		}
		#endregion

		#region IToolExt Member
		public System.Windows.Forms.Shortcut Shortcut
		{
			get
			{
				return System.Windows.Forms.Shortcut.None;
			}
		}

		public System.Drawing.Image Icon
		{
			get
			{
				return System.Drawing.Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.recur.png"));
			}
		}

		public virtual bool Visible 
		{
			get {return true;}
		}

		#endregion
	}
}
