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
using System.Collections;

namespace SimPe.Interfaces.Providers
{
	/// <summary>
	/// Interface to obtain all SimDescriptions available in a Package
	/// </summary>
	public interface ISimDescriptions : ICommonPackage
	{
		/// <summary>
		/// Find the Description of a Sim using the Instance Number
		/// </summary>
		/// <param name="instance">The Instance Id of the sim</param>
		/// <returns>null or a ISDesc Object</returns>
		Wrapper.ISDesc FindSim(ushort instance);

		/// <summary>
		/// Find the Description of a Sim using the Sim ID
		/// </summary>
		/// <param name="simid">The Sim ID</param>
		/// <returns>null or a ISDesc Object</returns>
		Wrapper.ISDesc FindSim(uint simid);

		/// <summary>
		/// returns the Instance Id for the given Sim
		/// </summary>
		/// <param name="simid">ID of the Sim</param>
		/// <returns>0xffff or a valid Instance Number</returns>
		ushort GetInstance(uint simid);

		/// <summary>
		/// returns the Sim Id for the given Sim
		/// </summary>
		/// <param name="instance">Instance Number of the Sim</param>
		/// <returns>0xffffffff or a valid Sim ID</returns>
		uint GetSimId(ushort instance);

		/// <summary>
		/// Returns availabl SDSC Files by SimGUID
		/// </summary>
		Hashtable SimGuidMap
		{
			get;
		}

		/// <summary>
		/// Returns availabl SDSC Files by Instance
		/// </summary>
		Hashtable SimInstance
		{
			get;
		}

		/// <summary>
		/// Returns a List containing all Household Names
		/// </summary>
		/// <returns></returns>
		ArrayList GetHouseholdNames();

		/// <summary>
		/// Returns a List containing all Household Names
		/// </summary>
		/// <param name="firstcustom">Returns the name of the first household with a custom Sim in it</param>
		/// <returns></returns>
		ArrayList GetHouseholdNames(out string firstcustom);

		#region Nightlife
		/// <summary>
		/// Returns the name of a Turnon/Turnoff
		/// </summary>		
		/// <param name="val1">stored Number for TurnOns1</param>
		/// <param name="val1">stored Number for TurnOns2</param>
		/// <returns></returns>
		string GetTurnOnName(ushort val1, ushort val2);

		/// <summary>
		/// Create the Index from the passed Numbers
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		uint BuildTurnOnIndex(ushort val1, ushort val2);

		/// <summary>
		/// Invers Operation to <see cref="BuildTurnOnIndex"/>
		/// </summary>
		/// <param name="index"></param>
		/// <param name="nr"></param>
		/// <returns>val1 and val2</returns>
		ushort[] GetFromTurnOnIndex(uint index);

		/// <summary>
		/// Returns a List of all available TurnOns
		/// </summary>
		/// <returns></returns>
		SimPe.Interfaces.IAlias[] GetAllTurnOns();
		#endregion
	}
}