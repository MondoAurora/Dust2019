/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Comm
{
	public class CommTypes : DustKey
	{
		private CommTypes(string module, int key) : base(module, key) { }

		public static CommTypes Domain = new CommTypes("Comm", 11);
		public static CommTypes Unit = new CommTypes("Comm", 4);
		public static CommTypes Persistent = new CommTypes("Comm", 3);
		public static CommTypes RemoteRef = new CommTypes("Comm", 17);
		public static CommTypes Reference = new CommTypes("Comm", 13);
		public static CommTypes Store = new CommTypes("Comm", 8);
	}

	public class CommAtts : DustKey
	{
		private CommAtts(string module, int key) : base(module, key) { }

		public static CommAtts RemoteRefItemModuleId = new CommAtts("Comm", 19);
		public static CommAtts ReferenceModuleCommitId = new CommAtts("Comm", 16);
		public static CommAtts PersistentCommitId = new CommAtts("Comm", 0);
		public static CommAtts RemoteRefModuleName = new CommAtts("Comm", 20);
		public static CommAtts RemoteRefModuleCommitId = new CommAtts("Comm", 18);
		public static CommAtts ReferenceItemModuleId = new CommAtts("Comm", 14);
		public static CommAtts PersistentEntityId = new CommAtts("Comm", 2);
		public static CommAtts ReferenceModuleName = new CommAtts("Comm", 15);
		public static CommAtts UnitNextEntityId = new CommAtts("Comm", 5);
	}

	public class CommLinks : DustKey
	{
		private CommLinks(string module, int key) : base(module, key) { }

		public static CommLinks UnitDomain = new CommLinks("Comm", 12);
		public static CommLinks PersistentStoreWith = new CommLinks("Comm", 7);
		public static CommLinks UnitMainEntities = new CommLinks("Comm", 21);
		public static CommLinks UnitEntities = new CommLinks("Comm", 6);
		public static CommLinks PersistentContainingUnit = new CommLinks("Comm", 1);
	}

}
