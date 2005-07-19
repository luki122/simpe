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
using SimPe.Interfaces;

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Tghe Object Workshop as new Dockable Tool
	/// </summary>
	public class ObectWorkshopDockTool : SimPe.Interfaces.IDockableTool
	{		
		ObjectWorkshopDock rd;
		public ObectWorkshopDockTool(ObjectWorkshopDock rd)
		{
			this.rd = rd;
		}

		#region IDockableTool Member

		public TD.SandDock.DockControl GetDockableControl()
		{
			return rd.dcObjectWorkshop;
		}		

		public event SimPe.Events.ChangedResourceEvent ShowNewResource;

		public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es)
		{
			
		}

		#endregion

		#region IToolPlugin Member

		public override string ToString()
		{
			return rd.dcObjectWorkshop.Text;
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
				return rd.dcObjectWorkshop.TabImage;
			}
		}

		#endregion
	}
}
