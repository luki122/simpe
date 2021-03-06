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
using SimPe.Plugin.Tool.Action;

namespace SimPe.Plugin
{
	/// <summary>
	/// Lists all Plugins (=FileType Wrappers) available in this Package
	/// </summary>
	/// <remarks>
	/// GetWrappers() has to return a list of all Plugins provided by this Library. 
	/// If a Plugin isn't returned, SimPe won't recognize it!
	/// </remarks>
	public class WrapperFactory : SimPe.Interfaces.Plugin.AbstractWrapperFactory, SimPe.Interfaces.Plugin.IToolFactory
	{
		#region AbstractWrapperFactory Member


		/// <summary>
		/// Returns a List of all available Plugins in this Package
		/// </summary>
		/// <returns>A List of all provided Plugins (=FileType Wrappers)</returns>
		public override SimPe.Interfaces.IWrapper[] KnownWrappers
		{
			get 
			{
                SimPe.PackedFiles.Wrapper.SdscFreetime.RegisterAsAspirationEditor(new SimPe.Plugin.SimAspirationEditor());
				FileTable.ProviderRegistry.LotProvider.LoadingLot += new SimPe.Interfaces.Providers.LoadLotData(LotProvider_LoadingLot);
                IWrapper[] wrappers = {
										  new Plugin.EnhancedNgbh(),
										  new Plugin.Ngbh(this.LinkedProvider),
										  new Plugin.Ltxt(this.LinkedProvider),
										  new Plugin.Want(this.LinkedProvider),
										  new Plugin.XWant(),
										  new Plugin.Idno(),
									      new Plugin.RoadTexture(),
										  new Plugin.Tatt(),		
										  new Plugin.Bnfo(),
										  new Plugin.Nhtr(),
                                          new Plugin.Lot(),
									  };
				return wrappers;
			}
		}

		#endregion

		#region IToolFactory Member


		public IToolPlugin[] KnownTools
		{
			get
			{
				System.Collections.ArrayList tools = new System.Collections.ArrayList();				
				if (Helper.WindowsRegistry.HiddenMode) 
				{
					tools.Add(new Plugin.FixUidTool());
					tools.Add(new ActionIntriguedNeighborhood());
				}

				if (Helper.QARelease) tools.Add(new ActionDeleteSim());
				
				IToolPlugin[] ret = new IToolPlugin[tools.Count];
				tools.CopyTo(ret);
				return ret;
			}
		}		


		#endregion

		private void LotProvider_LoadingLot(object sender, SimPe.Interfaces.Providers.ILotItem item)
		{
			SimPe.Interfaces.Files.IPackageFile pkg = FileTable.ProviderRegistry.SimDescriptionProvider.BasePackage;
			if (pkg!=null)
			{
				SimPe.Providers.LotProvider.LotItem li = item as SimPe.Providers.LotProvider.LotItem;
				//SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x0BF999E7, 0, Data.MetaData.LOCAL_GROUP, item.Instance);
				if (item.LtxtFileIndexItem!=null)
				{
					SimPe.Plugin.Ltxt ltxt = new Ltxt();
					ltxt.ProcessData(item.LtxtFileIndexItem);
					item.Tags.Add(ltxt);
					li.Owner = ltxt.OwnerInstance;
				}
			}
		}
	}
}
