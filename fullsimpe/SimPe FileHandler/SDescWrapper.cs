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
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Files;
using SimPe.Data;
using SimPe.PackedFiles.Wrapper.Supporting;
using System.IO;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// known Versions for SDSC Files
	/// </summary>
	public enum SDescVersions : int
	{
		Unknown = 0,
		OriginalGame = 0x20,
		University = 0x22
	}

	#region Ghost Flags
	/// <summary>
	/// Ghost Flag class
	/// </summary>
	public class GhostFlags : FlagBase
	{
		public GhostFlags(ushort flags) : base(flags) {}
		public GhostFlags() : base(0) {}

		public bool IsGhost
		{
			get { return GetBit(0); }
			set { SetBit(0, value); }
		}

		public bool CanPassThroughObjects
		{
			get { return GetBit(1); }
			set { SetBit(1, value); }
		}

		public bool CanPassThroughWalls
		{
			get { return GetBit(2); }
			set { SetBit(2, value); }
		}

		public bool CanPassThroughPeople
		{
			get { return GetBit(3); }
			set { SetBit(3, value); }
		}

		public bool IgnoreTraversalCosts
		{
			get { return GetBit(4); }
			set { SetBit(4, value); }
		}
	}
	#endregion

	#region Body Flags
	/// <summary>
	/// Ghost Flag class
	/// </summary>
	public class BodyFlags : FlagBase
	{
		public BodyFlags(ushort flags) : base(flags) {}
		public BodyFlags() : base(0) {}

		public bool Fat
		{
			get { return GetBit(0); }
			set { SetBit(0, value); }
		}

		public bool PregnantFull
		{
			get { return GetBit(1); }
			set { SetBit(1, value); }
		}

		public bool PregnantHalf
		{
			get { return GetBit(2); }
			set { SetBit(2, value); }
		}

		public bool PregnantHidden
		{
			get { return GetBit(3); }
			set { SetBit(3, value); }
		}

		public bool Fit
		{
			get { return GetBit(4); }
			set { SetBit(4, value); }
		}
	}
	#endregion

	#region CharacterDescription
	/// <summary>
	/// Holds some descriptive Properties about a Character
	/// </summary>
	public class CharacterDescription 
	{
		GhostFlags ghostflags;
		public GhostFlags GhostFlag 
		{
			get { return ghostflags; }
			set { ghostflags = value; }
		}

		BodyFlags bodyflags;
		public BodyFlags BodyFlag 
		{
			get { return bodyflags; }
			set { bodyflags = value; }
		}

		public CharacterDescription() 
		{
			ghostflags = new GhostFlags();
			bodyflags = new BodyFlags();
		}

		ushort autonomy;
		public ushort AutonomyLevel 
		{
			get { return autonomy; }
			set { autonomy = value; }
		}

		ushort npc;
		public ushort NPCType 
		{
			get { return npc; }
			set { npc = value; }
		}

		ushort voice;
		public ushort VoiceType 
		{
			get { return voice; }
			set { voice = value; }
		}

		Data.MetaData.SchoolTypes schooltype;
		public Data.MetaData.SchoolTypes SchoolType
		{
			get { return schooltype; }			
			set { schooltype = value; }
		}

		Data.MetaData.Grades grade;
		public Data.MetaData.Grades Grade
		{
			get { return grade; }			
			set { grade = value; }
		}

		short careerperformance;
		public short CareerPerformance
		{
			get { return careerperformance; }			
			set { careerperformance = value; }
		}		


		private MetaData.Careers career;
		public MetaData.Careers Career 
		{
			get { 
				/*if ((LifeSection == Data.MetaData.LifeSections.Adult) 
					|| (LifeSection == Data.MetaData.LifeSections.Teen) 
					|| (LifeSection == Data.MetaData.LifeSections.Elder) )
				{
					return career;
				} 
				else 
				{
					return MetaData.Careers.Unemployed;
				}*/
				return career;
			}
			set 
			{
				career = value;				
			}
		}

		private ushort careerlevel;
		public ushort CareerLevel 
		{
			get { 
				/*if (LifeSection == Data.MetaData.LifeSections.Adult) 
				{
					 return (ushort)Math.Min(10, (int)careerlevel); 
				} 
				else if (LifeSection == Data.MetaData.LifeSections.Teen) 
				{
					 return (ushort)Math.Min(3, (int)careerlevel); 
				} 
				else if (LifeSection == Data.MetaData.LifeSections.Elder) 
				{
					 return (ushort)Math.Min(10, (int)careerlevel);
				} 
				else 
				{
					return 0;
				}*/
				return careerlevel;
			}
			set 
			{
				//careerlevel = (ushort)Math.Min(10, (int)value); 				
				careerlevel = value;
			}
		}

		private MetaData.ZodiacSignes zodiac;
		public MetaData.ZodiacSignes ZodiacSign
		{
			get { return zodiac; }			
			set { zodiac = value; }
		}

		private MetaData.AspirationTypes aspiration;
		public MetaData.AspirationTypes Aspiration 
		{
			get { return aspiration; }
			set { aspiration = value; }
		}

		private MetaData.Gender gender;
		public MetaData.Gender Gender 
		{
			get { return gender; }
			set { gender = value;}
		}

		private MetaData.LifeSections lifesection;
		public MetaData.LifeSections LifeSection 
		{
			get { return lifesection; }
			set {lifesection = value; }
		}


		private ushort age;	
		public ushort Age
		{
			get { return age; }			
			set { age = value; }
		}

		private ushort prevage;	
		public ushort PrevAgeDays
		{
			get { return prevage; }			
			set { prevage = value; }
		}

		private ushort agedur;	
		public ushort AgeDuration
		{
			get { return agedur; }			
			set { agedur = value; }
		}

		private ushort clifeline;
		public ushort BlizLifelinePoints 
		{
			get { return (ushort)Math.Min(1200, (uint)clifeline); }
			set { clifeline = (ushort)Math.Min(1200, (uint)value); }
		}

		private short lifeline;
		public short LifelinePoints 
		{
			get { return (short)Math.Min(600, (int)(lifeline)); }
			set { lifeline = (short)Math.Min(600, (int)(value)); }
		}

		private ushort lifelinescore;
		public uint LifelineScore 
		{
			get { return (uint)(lifelinescore * (ushort)10); }
			set { lifelinescore = (ushort)(Math.Min(short.MaxValue, value / 10)); }
		}
	}
	#endregion

	#region CharacterAttributes
	/// <summary>
	/// Stores character Attributes
	/// </summary>
	public class CharacterAttributes 
	{				
		

		private ushort neat;
		public ushort Neat 
		{
			get 
			{
				return (ushort)Math.Min(1000, (uint)neat);
			}
			set 
			{
				neat = (ushort)Math.Min(1000, (uint)value);
			}
		}

		private ushort outgoing;
		public ushort Outgoing 
		{
			get 
			{
				return (ushort)Math.Min(1000, (uint)outgoing);
			}
			set 
			{
				outgoing = (ushort)Math.Min(1000, (uint)value);
			}
		}

		private ushort active;
		public ushort Active 
		{
			get 
			{
				return (ushort)Math.Min(1000, (uint)active);
			}
			set 
			{
				active = (ushort)Math.Min(1000, (uint)value);
			}
		}

		private ushort playful;
		public ushort Playful 
		{
			get 
			{
				return (ushort)Math.Min(1000, (uint)playful);
			}
			set 
			{
				playful = (ushort)Math.Min(1000, (uint)value);
			}
		}

		private ushort nice;
		public ushort Nice 
		{
			get 
			{
				return (ushort)Math.Min(1000, (uint)nice);
			}
			set 
			{
				nice = (ushort)Math.Min(1000, (uint)value);
			}
		}		
	}
	#endregion

	#region Decay
	/// <summary>
	/// Decay Values of a Sim
	/// </summary>
	public class SimDecay
	{
		private short hunger;
		public short Hunger 
		{
			get { return hunger; }
			set { hunger = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short comfort;
		public short Comfort 
		{
			get { return comfort; }
			set { comfort = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short bladder;
		public short Bladder 
		{
			get { return bladder; }
			set { bladder = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short energy;
		public short Energy 
		{
			get { return energy; }
			set { energy = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short hygiene;
		public short Hygiene 
		{
			get { return hygiene; }
			set { hygiene = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short social;
		public short Social 
		{
			get { return social; }
			set { social = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}

		private short fun;
		public short Fun 
		{
			get { return fun; }
			set { fun = Math.Min((short)1000, Math.Max((short)-1000, value)); }
		}
	}
	#endregion

	#region SkillAttributes
	/// <summary>
	/// Skill Attributes of a Sim
	/// </summary>
	public class SkillAttributes 
	{
		private ushort romance;
		public ushort Romance 
		{
			get { return (ushort)Math.Min(1000, (uint)romance); }
			set { romance = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort fatness;
		public ushort Fatness 
		{
			get { return (ushort)Math.Min(1000, (uint)fatness); }
			set { fatness = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort cooking;
		public ushort Cooking 
		{
			get	{ return (ushort)Math.Min(1000, (uint)cooking); }
			set { cooking = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort mechanical;
		public ushort Mechanical 
		{
			get	{ return (ushort)Math.Min(1000, (uint)mechanical); }
			set { mechanical = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort charisma;
		public ushort Charisma 
		{
			get	{ return (ushort)Math.Min(1000, (uint)charisma); }
			set { charisma = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort body;
		public ushort Body 
		{
			get	{ return (ushort)Math.Min(1000, (uint)body); }
			set { body = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort logic;
		public ushort Logic 
		{
			get	{ return (ushort)Math.Min(1000, (uint)logic); }
			set { logic = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort creativity;
		public ushort Creativity 
		{
			get	{ return (ushort)Math.Min(1000, (uint)creativity); }
			set { creativity = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort cleaning;
		public ushort Cleaning 
		{
			get	{ return (ushort)Math.Min(1000, (uint)cleaning); }
			set { cleaning = (ushort)Math.Min(1000, (uint)value); }
		}
	}
	#endregion

	#region Interests
	/// <summary>
	/// What a Sim is interessted in
	/// </summary>
	public class InterestAttributes 
	{
		private ushort politics;
		public ushort Politics 
		{
			get	{ return (ushort)Math.Min(1000, (uint)politics); }
			set { politics = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort money;
		public ushort Money 
		{
			get	{ return (ushort)Math.Min(1000, (uint)money); }
			set { money = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort crime;
		public ushort Crime 
		{
			get	{ return (ushort)Math.Min(1000, (uint)crime); }
			set { crime = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort environment;
		public ushort Environment 
		{
			get	{ return (ushort)Math.Min(1000, (uint)environment); }
			set { environment = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort entertainment;
		public ushort Entertainment 
		{
			get	{ return (ushort)Math.Min(1000, (uint)entertainment); }
			set { entertainment = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort culture;
		public ushort Culture 
		{
			get	{ return (ushort)Math.Min(1000, (uint)culture); }
			set { culture = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort food;
		public ushort Food 
		{
			get	{ return (ushort)Math.Min(1000, (uint)food); }
			set { food = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort health;
		public ushort Health 
		{
			get	{ return (ushort)Math.Min(1000, (uint)health); }
			set { health = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort fashion;
		public ushort Fashion 
		{
			get	{ return (ushort)Math.Min(1000, (uint)fashion); }
			set { fashion = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort sports;
		public ushort Sports 
		{
			get	{ return (ushort)Math.Min(1000, (uint)sports); }
			set { sports = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort paranormal;
		public ushort Paranormal 
		{
			get	{ return (ushort)Math.Min(1000, (uint)paranormal); }
			set { paranormal = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort travel;
		public ushort Travel 
		{
			get	{ return (ushort)Math.Min(1000, (uint)travel); }
			set { travel = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort work;
		public ushort Work 
		{
			get	{ return (ushort)Math.Min(1000, (uint)work); }
			set { work = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort weather;
		public ushort Weather 
		{
			get	{ return (ushort)Math.Min(1000, (uint)weather); }
			set { weather = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort animals;
		public ushort Animals 
		{
			get	{ return (ushort)Math.Min(1000, (uint)animals); }
			set { animals = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort school;
		public ushort School 
		{
			get	{ return (ushort)Math.Min(1000, (uint)school); }
			set { school = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort toys;
		public ushort Toys 
		{
			get	{ return (ushort)Math.Min(1000, (uint)toys); }
			set { toys = (ushort)Math.Min(1000, (uint)value); }
		}

		private ushort scifi;
		public ushort Scifi 
		{
			get	{ return (ushort)Math.Min(1000, (uint)scifi); }
			set { scifi = (ushort)Math.Min(1000, (uint)value); }
		}	
	
		private short woman;
		public short Woman
		{
			get	{ return woman; }
			set { woman = (short)Math.Max(-1000, Math.Min(1000, (int)value)); }
		}

		private short man;
		public short Man
		{
			get	{ return man; }
			set { man = (short)Math.Max(-1000, Math.Min(1000, (int)value)); }
		}
	}
	#endregion

	#region Relationships

	/// <summary>
	/// A List of all Sims known to the current one
	/// </summary>
	public class SimRelationAttribute 
	{		
		SDesc parent;
		internal SimRelationAttribute (SDesc parent) 
		{
			this.parent = parent;
			siminstance = new ushort[0];
		}

		private ushort[] siminstance;
		public ushort[] SimInstances
		{
			get	{ return siminstance; }
			set { 
				siminstance = value; 				
			}
		}

		/// <summary>
		/// Returns the SimDescription of the Sim with the passed Instance
		/// </summary>
		/// <param name="instance">Instance Number</param>
		/// <returns>The SimDescription Object or null if none was found</returns>
		public SDesc GetSimDescription(ushort instance)
		{
			if (instance==parent.FileDescriptor.Instance) return null;			
			IPackedFileDescriptor pfd = parent.Package.FindFile(
				Data.MetaData.SIM_DESCRIPTION_FILE,
				0,
				parent.FileDescriptor.Group,
				instance
				);
			
			
			SDesc sdesc = new SDesc(parent.nameprovider, parent.familynameprovider, parent.sdescprovider);
			if (pfd!=null) sdesc.ProcessData(pfd, parent.Package);

			return sdesc;
		}

		/// <summary>
		/// Returns the Relationship Files for the given instance
		/// </summary>
		/// <param name="instance">The instance of the related Sim</param>
		/// <returns>
		///		null or a SimRelations Object 
		///	</returns>
		public SimRelations GetSimRelationships(ushort instance)
		{
			if (instance==parent.FileDescriptor.Instance) return null;			
			IPackedFileDescriptor pfd1 = parent.Package.FindFile(
				Data.MetaData.RELATION_FILE,
				0,
				parent.FileDescriptor.Group,
				(uint)((instance << 16) + parent.FileDescriptor.Instance)
				);

			IPackedFileDescriptor pfd2 = parent.Package.FindFile(
				Data.MetaData.RELATION_FILE,
				0,
				parent.FileDescriptor.Group,
				(uint)((parent.FileDescriptor.Instance << 16) + instance)
				);
			
			
			SRel[] rels = new SRel[2];
			rels[0] = new SRel();
			rels[1] = new SRel();

			if (pfd1!=null) rels[1].ProcessData(pfd1, parent.Package);
			if (pfd2!=null) rels[0].ProcessData(pfd2, parent.Package);

			return new SimRelations(rels);
		}
	}
	#endregion

	#region SdscUniversity
	/// <summary>
	/// University specific Data
	/// </summary>
	public class SdscUniversity
	{
		internal SdscUniversity()
		{
			major = Data.Majors.Undeclared;
			time = 72;
			semester = 1;
		}

		ushort effort;
		public ushort Effort
		{
			get { return effort; }			
			set { effort = value; }
		}

		
		ushort grade;
		public ushort Grade
		{
			get { return grade; }			
			set { grade = value; }
		}

		
		ushort time;
		public ushort Time
		{
			get { return time; }			
			set { time = value; }
		}

		
		ushort semester;
		public ushort Semester
		{
			get { return semester; }			
			set { semester = value; }
		}

		
		ushort oncampus;
		public ushort OnCampus
		{
			get { return oncampus; }			
			set { oncampus = value; }
		}

		
		ushort influence;
		public ushort Influence
		{
			get { return influence; }			
			set { influence = value; }
		}
		

		Data.Majors major;
		public Data.Majors Major
		{
			get { return major; }			
			set { major = value; }
		}

		internal void Serialize(BinaryReader reader)
		{
			reader.BaseStream.Seek(0x014, SeekOrigin.Begin);
			effort = reader.ReadUInt16();

			reader.BaseStream.Seek(0x0b2, SeekOrigin.Begin);
			grade = reader.ReadUInt16();

			reader.BaseStream.Seek(0x160, SeekOrigin.Begin);
			major = (Data.Majors)reader.ReadUInt32();
			time = reader.ReadUInt16();
			reader.BaseStream.Seek(0x2, SeekOrigin.Current);
			semester = reader.ReadUInt16();
			oncampus = reader.ReadUInt16();
			reader.BaseStream.Seek(0x4, SeekOrigin.Current);
			influence = reader.ReadUInt16();			
		}

		internal void Unserialize(BinaryWriter writer)
		{
			writer.BaseStream.Seek(0x014, SeekOrigin.Begin);
			writer.Write(effort);

			writer.BaseStream.Seek(0x0b2, SeekOrigin.Begin);
			writer.Write(grade);

			writer.BaseStream.Seek(0x160, SeekOrigin.Begin);
			writer.Write((uint)major);
			writer.Write(time);
			writer.BaseStream.Seek(0x2, SeekOrigin.Current);
			writer.Write(semester);
			writer.Write(oncampus);
			writer.BaseStream.Seek(0x4, SeekOrigin.Current);
			writer.Write(influence);			
		}
	}
	#endregion

	/// <summary>
	/// Represents a PackedFile in SDsc Format
	/// </summary>
	public class SDesc : AbstractWrapper, SimPe.Interfaces.Plugin.IFileWrapper, SimPe.Interfaces.Plugin.IFileWrapperSaveExtension, SimPe.Interfaces.Wrapper.ISDesc
	{
		#region Local Attributes
		/// <summary>
		/// Number of teh assigned Instance
		/// </summary>
		private ushort instancenumber;

		/// <summary>
		/// The Id of the related sim
		/// </summary>
		private uint simid; 

		/// <summary>
		/// Instance Number of Family
		/// </summary>
		ushort familyinstance;

		/// <summary>
		/// Version of the package
		/// </summary>
		int version;
		/// <summary>
		/// Teh Version of this File
		/// </summary>
		public SDescVersions Version 
		{
			get { return (SDescVersions)version; }
		}

		/// <summary>
		/// Spaces of unknown Data
		/// </summary>
		private byte[] reserved_01;
		/// <summary>
		/// Spaces of unknown Data
		/// </summary>
		//private byte[] reserved_02;
		/// <summary>
		/// Spaces of unknown Data
		/// </summary>
		private byte[] reserved_03;

		/// <summary>
		/// Decay Values of the Sim
		/// </summary>
		private SimDecay decay;

		ushort unlinked;
		/// <summary>
		/// True if this Sim is only available for Memory Reasons
		/// </summary>
		public ushort Unlinked 
		{
			get {return unlinked; }
			set {unlinked = value;}
		}
		#endregion

		#region External Attributes
		//returns / sets the Instance of the Family the Sim lives in
		public ushort FamilyInstance 
		{
			get { return familyinstance; }
			set {familyinstance = value; }
		}

		/// <summary>
		/// Returns / Sets the Decay Values
		/// </summary>
		public SimDecay Decay 
		{
			get { return decay; }
			set { decay = value; }
		}

		/// <summary>
		/// Holds Sim Relationships
		/// </summary>
		private SimRelationAttribute relations;

		/// <summary>
		/// Returns the Relationships a sim has
		/// </summary>
		public SimRelationAttribute Relations 
		{
			get { return relations; }
		}

		/// <summary>
		/// Some Description about the Characters Life
		/// </summary>
		private CharacterDescription description;

		/// <summary>
		/// Returns the Description of the Character
		/// </summary>
		public CharacterDescription CharacterDescription 
		{
			get 
			{
				return description;
			}			
		}

		SdscUniversity uni;
		/// <summary>
		/// Returns University Specific Data
		/// </summary>
		/// <remarks>Only valid if Version == SDescVersions.University</remarks>
		public SdscUniversity University
		{
			get { return uni; }
		}


		/// <summary>
		/// Character Attributes
		/// </summary>
		private CharacterAttributes character;

		/// <summary>
		/// Returns the Character Attributes
		/// </summary>
		public CharacterAttributes Character
		{
			get 
			{
				return character;
			}			
		}

		/// <summary>
		/// Character Attributes
		/// </summary>
		private CharacterAttributes gencharacter;

		/// <summary>
		/// Returns the Character Attributes
		/// </summary>
		public CharacterAttributes GeneticCharacter
		{
			get 
			{
				return gencharacter;
			}			
		}

		

		/// <summary>
		/// Skill Attributes
		/// </summary>
		private SkillAttributes skills;

		/// <summary>
		/// Returns the Skill Attributes
		/// </summary>
		public SkillAttributes Skills
		{
			get 
			{
				return skills;
			}			
		}

		/// <summary>
		/// A Sims Interests
		/// </summary>
		private InterestAttributes interests;

		/// <summary>
		/// Returns the Interests of a Sim
		/// </summary>
		public InterestAttributes Interests
		{
			get 
			{
				return interests;
			}
		}
		#endregion

		#region Local Getters/Setters
				
		/// <summary>
		/// Returns the Name Provider
		/// </summary>
		internal SimPe.Interfaces.Providers.ISimNames NameProvider 
		{
			get { return nameprovider; }
		}

		/// <summary>
		/// Returns the Description Provider
		/// </summary>
		internal SimPe.Interfaces.Providers.ISimDescriptions DescriptionProvider 
		{
			get { return sdescprovider; }
		}

		/// <summary>
		/// Returns/Sets the Sim Id
		/// </summary>
		public uint SimId
		{
			get 
			{
				return simid;
			}
			set 
			{
				simid = value;
			}
		}

		/// <summary>
		/// Returns the FirstName of a Sim 
		/// </summary>
		/// <remarks>If no SimName Provider is available, '---' will be delivered</remarks>
		public string SimName 
		{
			get 
			{
				if (nameprovider!=null) 
				{
					return nameprovider.FindName(SimId).Name;
				} 
				else 
				{
					return "---";
				}
			}
		}

		/// <summary>
		/// true if an Image for this Sim is available
		/// </summary>
		public bool HasImage 
		{
			get 
			{
				if (nameprovider!=null) 
				{
					object o = nameprovider.FindName(SimId).Tag;
					if (o!=null) 
					{
						object[] tags = (object[])o;
						if ((System.Drawing.Image)tags[1]!=null)
							return true;
					}
				} 

				return false;
				
			}
		}

		/// <summary>
		/// Returns the Image stored for a specific Sim
		/// </summary>
		public System.Drawing.Image Image 
		{
			get 
			{
				if (nameprovider!=null) 
				{
					object o = nameprovider.FindName(SimId).Tag;
					if (o!=null) 
					{
						object[] tags = (object[])o;
						if ((System.Drawing.Image)tags[1]!=null)
							return (System.Drawing.Image)tags[1];
					}
				} 

				return new System.Drawing.Bitmap(1,1);
				
			}
		}

		/// <summary>
		/// Returns the Name of the File the Character is stored in
		/// </summary>
		/// <remarks>null, if no File was found</remarks>
		public string CharacterFileName
		{
			get 
			{
				if (nameprovider!=null) 
				{
					object o = nameprovider.FindName(SimId).Tag;
					if (o!=null) 
					{
						object[] tags = (object[])o;
						return Helper.ToString(tags[0]);
					}
				} 
				return null;
			}
		}

		/// <summary>
		/// Returns or Sets the Instance Number
		/// </summary>
		public ushort Instance 
		{
			get 
			{
				return instancenumber;
			}
			set
			{
				instancenumber = value;
			}
		}

		/// <summary>
		/// Returns the FamilyName of a Sim 
		/// </summary>
		/// <remarks>If no SimFamilyName Provider is available, '---' will be delivered</remarks>
		public string SimFamilyName 
		{
			get 
			{
				if (nameprovider!=null) 
				{
					object[] o = nameprovider.FindName(SimId).Tag;
					if (o!=null) 
					{
						if (o.Length>2) 
						{
							if (o[2]!=null) return o[2].ToString();
						}
					}
				} 
				return Localization.Manager.GetString("Unknown");
				
			}
		}

		/// <summary>
		/// Returns the FamilyName of a Sim that is stored in the current Package
		/// </summary>
		/// <remarks>If no SimFamilyName Provider is available, '---' will be delivered</remarks>
		public string HouseholdName
		{
			get 
			{
				if (familynameprovider!=null) 
				{
					return familynameprovider.FindName(SimId).Name;
				} 
				else 
				{
					return "---";
				}
			}
		}

		/// <summary>
		/// True if the Character File contains Character Data (AgeData, 3dMesh...)
		/// </summary>
		public bool AvailableCharacterData 
		{
			get 
			{
				if (nameprovider!=null) 
				{
					object[] o = nameprovider.FindName(SimId).Tag;
					if (o!=null) 
					{
						if (o.Length>3) 
						{
							if (o[3]!=null) return (bool)o[3];
						}
					}
				} 
				return false;
			}
		}
		#endregion

		#region SDesc specific Methods/Data
		/// <summary>
		/// Stores null or a valid Name Provider
		/// </summary>
		internal SimPe.Interfaces.Providers.ISimNames nameprovider;

		/// <summary>
		/// Stores null or a valid FamilyName Provider
		/// </summary>
		internal SimPe.Interfaces.Providers.ISimFamilyNames familynameprovider;

		/// <summary>
		/// Stores null or a valid SimDescription provider
		/// </summary>
		internal SimPe.Interfaces.Providers.ISimDescriptions sdescprovider;

		/// <summary>
		/// Scans the passed Package for a Description File containing the SimId
		/// </summary>
		/// <param name="simid">id of your Sim</param>
		/// <param name="package">the package you want to search</param>
		/// <returns>null if none was found, or the Description file describing the Sim</returns>
		/// 
		public static SDesc FindForSimId(uint simid, IPackageFile package) 
		{
			IPackedFileDescriptor[] files = package.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);

			SDesc sdesc = new SDesc(null, null, null);
			foreach(IPackedFileDescriptor pfd in files)
			{				
				sdesc.ProcessData(pfd, package);
				if (sdesc.SimId == simid) 
				{
					return sdesc;
				}
			}

			return null;
		}
		#endregion

		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Sim Description Wrapper",
				"Quaxi",
				"---",
				8
				); 
		}

		protected override string GetResourceName(SimPe.Data.TypeAlias ta)
		{
			if (!this.Processed) ProcessData(FileDescriptor, Package);
			return this.SimName + " " + this.SimFamilyName;
		}
		#endregion

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new SimPe.PackedFiles.UserInterface.SDesc();
		}

		/// <summary>
		/// Change the links to the Providers
		/// </summary>
		/// <param name="prov">A Provider Registry</param>
		public void SetProviders(SimPe.Interfaces.IProviderRegistry prov) 
		{
			nameprovider = prov.SimNameProvider;
			familynameprovider = prov.SimFamilynameProvider;
			sdescprovider = prov.SimDescriptionProvider;
		}

		/// <summary>
		/// Creates a new Instance
		/// </summary>
		/// <param name="names">null, or a Sim Name Provider</param>
		/// <param name="famnames">null or a Sim Familyname Provider</param>
		/// <param name="sdesc">nullor a SimD</param>
		public SDesc(SimPe.Interfaces.Providers.ISimNames names, SimPe.Interfaces.Providers.ISimFamilyNames famnames, SimPe.Interfaces.Providers.ISimDescriptions sdesc) : base()
		{
			reserved_01 = new byte[0xC2];
			//reserved_02 = new byte[0x9A];
			reserved_03 = new byte[0x9D];

			reserved_01[0x00] = 0x70;
			reserved_01[0x04] = 0x20;
			reserved_01[0x08] = 0x20;			
			reserved_03[0x00] = 0x02;
			reserved_03[0x08] = 0x01;

			nameprovider = names;
			familynameprovider = famnames;
			sdescprovider = sdesc;

			decay = new SimDecay();
			character = new CharacterAttributes();
			gencharacter = new CharacterAttributes();
			skills = new SkillAttributes();
			interests = new InterestAttributes();
			description = new CharacterDescription();	
			relations = new SimRelationAttribute(this);
			uni = new SdscUniversity();

			description.Aspiration = MetaData.AspirationTypes.Romance;
			description.ZodiacSign = MetaData.ZodiacSignes.Virgo;
			description.LifeSection = MetaData.LifeSections.Adult;
			description.Gender = MetaData.Gender.Female;
			description.LifelinePoints = 500;
			

			interests.Woman = 50;
			interests.Man = 50;

			skills.Fatness = 500;
			version = 0x20;
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
		{							
			long startpos = reader.BaseStream.Position;
			reserved_01 = reader.ReadBytes(0xC2);						
			description.Age = reader.ReadUInt16();
			description.PrevAgeDays = reader.ReadUInt16();
			//reserved_02= reader.ReadBytes(0x9A);
			//instancenumber = reader.ReadUInt16();
			//simid = reader.ReadUInt32();
			reserved_03 = reader.ReadBytes((int)(reader.BaseStream.Length - (reader.BaseStream.Position)));
			long endpos = reader.BaseStream.Position;

			///
			/// TODO: This needs to be done more efficient, but for now it will work!
			///
			reader.BaseStream.Seek(startpos + 0x04, System.IO.SeekOrigin.Begin);
			version = reader.ReadInt32();

			if (version>=(int)SDescVersions.University) 
			{
				reader.BaseStream.Seek(startpos + 0x172, System.IO.SeekOrigin.Begin);
				instancenumber = reader.ReadUInt16();
				simid = reader.ReadUInt32();
			} 
			else 
			{
				reader.BaseStream.Seek(startpos + 0x160, System.IO.SeekOrigin.Begin);
				instancenumber = reader.ReadUInt16();
				simid = reader.ReadUInt32();
			}
 
			//decay			
			reader.BaseStream.Seek(startpos + 0xC6, System.IO.SeekOrigin.Begin);
			decay.Hunger = reader.ReadInt16();
			decay.Comfort = reader.ReadInt16();
			decay.Bladder = reader.ReadInt16();
			decay.Energy = reader.ReadInt16();
			decay.Hygiene = reader.ReadInt16();
			reader.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			decay.Social = reader.ReadInt16();
			reader.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			decay.Fun = reader.ReadInt16();
			
			//skills
			reader.BaseStream.Seek(startpos + 0x1E, System.IO.SeekOrigin.Begin);
			skills.Cleaning = reader.ReadUInt16();
			skills.Cooking = reader.ReadUInt16();
			skills.Charisma = reader.ReadUInt16();
			skills.Mechanical = reader.ReadUInt16();
			reader.BaseStream.Seek(0x04, System.IO.SeekOrigin.Current);
			skills.Creativity = reader.ReadUInt16();
			reader.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			skills.Body = reader.ReadUInt16();
			skills.Logic = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0xEA, System.IO.SeekOrigin.Begin);
			skills.Romance = reader.ReadUInt16();


			//character (Genetic)
			reader.BaseStream.Seek(startpos + 0x10, System.IO.SeekOrigin.Begin);
			character.Nice = reader.ReadUInt16();
			character.Active = reader.ReadUInt16();
			reader.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			character.Playful = reader.ReadUInt16();
			character.Outgoing = reader.ReadUInt16();
			character.Neat = reader.ReadUInt16();

			//random Data
			reader.BaseStream.Seek(startpos + 0x68, System.IO.SeekOrigin.Begin);
			description.Aspiration = (MetaData.AspirationTypes)reader.ReadUInt16();	
			reader.BaseStream.Seek(startpos + 0xBC, System.IO.SeekOrigin.Begin);
			description.VoiceType = reader.ReadUInt16();	
			reader.BaseStream.Seek(startpos + 0x7C, System.IO.SeekOrigin.Begin);
			description.Grade = (Data.MetaData.Grades)reader.ReadUInt16();
			description.CareerLevel = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x80, System.IO.SeekOrigin.Begin);
			description.LifeSection = (MetaData.LifeSections)reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x86, System.IO.SeekOrigin.Begin);
			familyinstance = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x8A, System.IO.SeekOrigin.Begin);
			description.CareerPerformance = reader.ReadInt16();
			reader.BaseStream.Seek(startpos + 0x8E, System.IO.SeekOrigin.Begin);
			description.Gender = (MetaData.Gender)reader.ReadUInt16();		
			reader.BaseStream.Seek(startpos + 0x94, System.IO.SeekOrigin.Begin);
			description.GhostFlag.Value = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x98, System.IO.SeekOrigin.Begin);
			description.ZodiacSign = (Data.MetaData.ZodiacSignes)reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0xAE, System.IO.SeekOrigin.Begin);
			description.BodyFlag.Value = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0xB0, System.IO.SeekOrigin.Begin);
			skills.Fatness = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0xBE, System.IO.SeekOrigin.Begin);
			description.Career = (Data.MetaData.Careers)reader.ReadUInt32();
			reader.BaseStream.Seek(startpos + 0xE2, System.IO.SeekOrigin.Begin);
			description.SchoolType = (Data.MetaData.SchoolTypes)reader.ReadUInt32();
			reader.BaseStream.Seek(startpos + 0x14C, System.IO.SeekOrigin.Begin);
			description.LifelinePoints = reader.ReadInt16();
			description.LifelineScore = (uint)(reader.ReadUInt16() * 10);
			description.BlizLifelinePoints = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x142, System.IO.SeekOrigin.Begin);
			description.NPCType = reader.ReadUInt16();
			description.AgeDuration = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x54, System.IO.SeekOrigin.Begin);
			description.AutonomyLevel = reader.ReadUInt16();
			reader.BaseStream.Seek(startpos + 0x156, System.IO.SeekOrigin.Begin);
			unlinked = reader.ReadUInt16();

			//available Relationships
			reader.BaseStream.Seek(startpos + 0x16A, System.IO.SeekOrigin.Begin);
			if (version>=(int)SDescVersions.University) reader.BaseStream.Seek(0x12, System.IO.SeekOrigin.Current);
			relations.SimInstances = new ushort[reader.ReadUInt16()];

			int ct = 0;
			for (int i=0; i<relations.SimInstances.Length; i++)
			{
				if (reader.BaseStream.Length - reader.BaseStream.Position < 4) continue;
				reader.ReadUInt16();			//yet unknown
				relations.SimInstances[i] = reader.ReadUInt16();
				ct++;
			}

			//something went wrong while reading the SimInstances
			if (ct != relations.SimInstances.Length) 
			{
				ushort[] old = relations.SimInstances;
				relations.SimInstances = new ushort[ct];
				for (int i=0; i<ct; i++) relations.SimInstances[i] = old[i];
			}

			//character (Genetic)
			reader.BaseStream.Seek(startpos + 0x6A, System.IO.SeekOrigin.Begin);
			gencharacter.Neat = reader.ReadUInt16();
			gencharacter.Nice = reader.ReadUInt16();
			gencharacter.Active = reader.ReadUInt16();
			gencharacter.Outgoing = reader.ReadUInt16();
			gencharacter.Playful = reader.ReadUInt16();

			
			

			//interests
			reader.BaseStream.Seek(startpos + 0x038, System.IO.SeekOrigin.Begin);			
			interests.Woman = reader.ReadInt16();
			interests.Man = reader.ReadInt16();
			reader.BaseStream.Seek(startpos + 0x104, System.IO.SeekOrigin.Begin);
			interests.Politics = reader.ReadUInt16();
			interests.Money = reader.ReadUInt16();
			interests.Environment = reader.ReadUInt16();
			interests.Crime = reader.ReadUInt16();
			interests.Entertainment = reader.ReadUInt16();
			interests.Culture = reader.ReadUInt16();
			interests.Food = reader.ReadUInt16();
			interests.Health = reader.ReadUInt16();
			interests.Fashion = reader.ReadUInt16();
			interests.Sports = reader.ReadUInt16();
			interests.Paranormal = reader.ReadUInt16();
			interests.Travel = reader.ReadUInt16();
			interests.Work = reader.ReadUInt16();
			interests.Weather = reader.ReadUInt16();
			interests.Animals = reader.ReadUInt16();
			interests.School = reader.ReadUInt16();
			interests.Toys = reader.ReadUInt16();
			interests.Scifi = reader.ReadUInt16();

			//university only Items
			if (version>=(int)SDescVersions.University) 
			{				
				uni.Serialize(reader);
			}

			reader.BaseStream.Seek(endpos, System.IO.SeekOrigin.Begin);
		}

		protected override void Serialize(System.IO.BinaryWriter writer) 
		{		
			long startpos = writer.BaseStream.Position;
			writer.Write(reserved_01);					
			writer.Write(description.Age);
			writer.Write(description.PrevAgeDays);
			//writer.Write(reserved_02);
			//writer.Write(instancenumber);
			//writer.Write(simid);
			writer.Write(reserved_03);
			while (writer.BaseStream.Length < 0x16D) writer.Write((byte)0);
			long endpos = writer.BaseStream.Position;			
			
			///
			/// TODO: This needs to be done more efficient, but for now it will work!
			///			 			
			writer.BaseStream.Seek(startpos + 0x04, System.IO.SeekOrigin.Begin);
			writer.Write(version); //Version Number

			if (version>=(int)SDescVersions.University) 
			{
				writer.BaseStream.Seek(startpos + 0x172, System.IO.SeekOrigin.Begin);
				writer.Write(instancenumber);
				writer.Write(simid); 
			} 
			else 
			{
				writer.BaseStream.Seek(startpos + 0x160, System.IO.SeekOrigin.Begin);
				writer.Write(instancenumber);
				writer.Write(simid); 
			}

			//character 
			writer.BaseStream.Seek(startpos + 0x10, System.IO.SeekOrigin.Begin);
			writer.Write(character.Nice); //Nice
			writer.Write(character.Active); //Active
			writer.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			writer.Write(character.Playful); //Playful
			writer.Write(character.Outgoing); //Outgoing
			writer.Write(character.Neat); //Neat

			//random Data			
			writer.BaseStream.Seek(startpos + 0x68, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.Aspiration);
			writer.BaseStream.Seek(startpos + 0xBC, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.VoiceType);
			writer.BaseStream.Seek(startpos + 0x7C, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.Grade);
			writer.Write(description.CareerLevel);
			writer.BaseStream.Seek(startpos + 0x80, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.LifeSection);
			writer.BaseStream.Seek(startpos + 0x86, System.IO.SeekOrigin.Begin);
			writer.Write(familyinstance);
			writer.BaseStream.Seek(startpos + 0x8A, System.IO.SeekOrigin.Begin);
			writer.Write(description.CareerPerformance);
			writer.BaseStream.Seek(startpos + 0x8E, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.Gender);	
			writer.BaseStream.Seek(startpos + 0x94, System.IO.SeekOrigin.Begin);
			writer.Write(description.GhostFlag.Value);
			writer.BaseStream.Seek(startpos + 0x98, System.IO.SeekOrigin.Begin);
			writer.Write((ushort)description.ZodiacSign);
			writer.BaseStream.Seek(startpos + 0xAE, System.IO.SeekOrigin.Begin);
			writer.Write(description.BodyFlag.Value);
			writer.BaseStream.Seek(startpos + 0xB0, System.IO.SeekOrigin.Begin);
			writer.Write(skills.Fatness);
			writer.BaseStream.Seek(startpos + 0xBE, System.IO.SeekOrigin.Begin);
			writer.Write((uint)description.Career);
			writer.BaseStream.Seek(startpos + 0xE2, System.IO.SeekOrigin.Begin);
			writer.Write((uint)description.SchoolType);
			writer.BaseStream.Seek(startpos + 0x14C, System.IO.SeekOrigin.Begin);
			writer.Write(description.LifelinePoints);
			writer.Write((ushort)(description.LifelineScore / 10));
			writer.Write(description.BlizLifelinePoints);
			writer.BaseStream.Seek(startpos + 0x142, System.IO.SeekOrigin.Begin);
			writer.Write(description.NPCType);
			writer.Write(description.AgeDuration);
			writer.BaseStream.Seek(startpos + 0x54, System.IO.SeekOrigin.Begin);
			writer.Write(description.AutonomyLevel);
			writer.BaseStream.Seek(startpos + 0x156, System.IO.SeekOrigin.Begin);
			writer.Write(unlinked);

			//decay
			writer.BaseStream.Seek(startpos + 0xC6, System.IO.SeekOrigin.Begin);
			writer.Write(decay.Hunger);
			writer.Write(decay.Comfort);
			writer.Write(decay.Bladder);
			writer.Write(decay.Energy);
			writer.Write(decay.Hygiene);
			writer.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			writer.Write(decay.Social);
			writer.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			writer.Write(decay.Fun);

			//available Relationships
			writer.BaseStream.Seek(startpos + 0x16A, System.IO.SeekOrigin.Begin);
			if (version>=(int)SDescVersions.University) writer.BaseStream.Seek(0x12, System.IO.SeekOrigin.Current);
			writer.Write((uint)relations.SimInstances.Length);

			for (int i=0; i<relations.SimInstances.Length; i++)
			{								
				writer.Write((uint)relations.SimInstances[i]);
			}
			writer.Write((byte)0x01);

			//skills
			writer.BaseStream.Seek(startpos + 0x1E, System.IO.SeekOrigin.Begin);
			writer.Write(skills.Cleaning);
			writer.Write(skills.Cooking);
			writer.Write(skills.Charisma);
			writer.Write(skills.Mechanical);
			writer.BaseStream.Seek(0x04, System.IO.SeekOrigin.Current);
			writer.Write(skills.Creativity);
			writer.BaseStream.Seek(0x02, System.IO.SeekOrigin.Current);
			writer.Write(skills.Body);
			writer.Write(skills.Logic);
			writer.BaseStream.Seek(startpos + 0xEA, System.IO.SeekOrigin.Begin);
			writer.Write(skills.Romance);

			//character (Genetic)
			writer.BaseStream.Seek(startpos + 0x6A, System.IO.SeekOrigin.Begin);
			writer.Write(gencharacter.Neat);
			writer.Write(gencharacter.Nice);
			writer.Write(gencharacter.Active);
			writer.Write(gencharacter.Outgoing);
			writer.Write(gencharacter.Playful);

			//interests
			writer.BaseStream.Seek(startpos + 0x038, System.IO.SeekOrigin.Begin);			
			writer.Write(interests.Woman);
			writer.Write(interests.Man);
			writer.BaseStream.Seek(startpos + 0x104, System.IO.SeekOrigin.Begin);
			writer.Write(interests.Politics);
			writer.Write(interests.Money);
			writer.Write(interests.Environment);
			writer.Write(interests.Crime);
			writer.Write(interests.Entertainment);
			writer.Write(interests.Culture);
			writer.Write(interests.Food);
			writer.Write(interests.Health);
			writer.Write(interests.Fashion);
			writer.Write(interests.Sports);
			writer.Write(interests.Paranormal);
			writer.Write(interests.Travel);
			writer.Write(interests.Work);
			writer.Write(interests.Weather);
			writer.Write(interests.Animals);
			writer.Write(interests.School);
			writer.Write(interests.Toys);
			writer.Write(interests.Scifi);

			//university only Items
			if (version>=(int)SDescVersions.University) 
			{				
				uni.Unserialize(writer);
			}

			writer.BaseStream.Seek(endpos, System.IO.SeekOrigin.Begin);
		}
		#endregion

		#region IPackedFileWrapper Member
		public override string Description
		{
			get
			{
				return "GUID=0x"+Helper.HexString(this.SimId)+", Filename="+this.CharacterFileName+", Name="+this.SimName+" "+this.SimFamilyName+", Age="+this.CharacterDescription.LifeSection.ToString()+", Job="+this.CharacterDescription.Career.ToString()+", Zodiac="+this.CharacterDescription.ZodiacSign.ToString()+", Major="+this.University.Major+", Grade="+this.CharacterDescription.Grade.ToString();
			}
		}

		public uint[] AssignableTypes
		{
			get 
			{
				uint[] Types = {
								   0xAACE2EFB
							   };
				return Types;
			}
		}


		public Byte[] FileSignature
		{
			get 
			{
				Byte[] sig = {					 
							 };
				return sig;
			}
		}		

		

		#endregion

		#region static values
		static SimPe.Interfaces.IAlias[] addoncarrer;
		/// <summary>
		/// Returns a List of Userdefined AddOnCareers
		/// </summary>
		public static SimPe.Interfaces.IAlias[] AddonCarrers
		{
			get 
			{
				if (addoncarrer==null) addoncarrer = Data.Alias.LoadFromXml(System.IO.Path.Combine(Helper.SimPeDataPath, "additional_careers.xml"));
				return addoncarrer;
			}
		}

		static SimPe.Interfaces.IAlias[] addonmajor;
		/// <summary>
		/// Returns a List of Userdefined AddOnCareers
		/// </summary>
		public static SimPe.Interfaces.IAlias[] AddonMajors
		{
			get 
			{
				if (addonmajor==null) addonmajor = Data.Alias.LoadFromXml(System.IO.Path.Combine(Helper.SimPeDataPath, "additional_majors.xml"));
				return addonmajor;
			}
		}

		static SimPe.Interfaces.IAlias[] addonschool;
		/// <summary>
		/// Returns a List of Userdefined AddOnCareers
		/// </summary>
		public static SimPe.Interfaces.IAlias[] AddonSchools
		{
			get 
			{
				if (addonschool==null) addonschool = Data.Alias.LoadFromXml(System.IO.Path.Combine(Helper.SimPeDataPath, "additional_schools.xml"));
				return addonschool;
			}
		}
		#endregion
	}
}
