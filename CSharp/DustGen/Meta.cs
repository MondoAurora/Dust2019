/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Meta
{
	public class MetaTypes : DustKey
	{
		private MetaTypes(string module, int key) : base(module, key) { }

		public static MetaTypes Type = new MetaTypes("Meta", 3);
		public static MetaTypes LinkDef = new MetaTypes("Meta", 7);
		public static MetaTypes MeasurementUnit = new MetaTypes("Meta", 34);
		public static MetaTypes Constant = new MetaTypes("Meta", 10);
		public static MetaTypes AttDef = new MetaTypes("Meta", 0);
		public static MetaTypes Command = new MetaTypes("Meta", 16);
		public static MetaTypes Meta = new MetaTypes("Meta", 28);
		public static MetaTypes Service = new MetaTypes("Meta", 5);
	}

	public class MetaAtts : DustKey
	{
		private MetaAtts(string module, int key) : base(module, key) { }

	}

	public class MetaLinks : DustKey
	{
		private MetaLinks(string module, int key) : base(module, key) { }

		public static MetaLinks LinkDefItemTypePrimary = new MetaLinks("Meta", 32);
		public static MetaLinks TypeAttDefs = new MetaLinks("Meta", 2);
		public static MetaLinks LinkDefParent = new MetaLinks("Meta", 6);
		public static MetaLinks AttDefMeasurementUnit = new MetaLinks("Meta", 35);
		public static MetaLinks AttDefParent = new MetaLinks("Meta", 1);
		public static MetaLinks TypeLinkDefs = new MetaLinks("Meta", 4);
		public static MetaLinks LinkDefType = new MetaLinks("Meta", 8);
		public static MetaLinks AttDefType = new MetaLinks("Meta", 19);
		public static MetaLinks LinkDefItemTypeAdditional = new MetaLinks("Meta", 33);
		public static MetaLinks CommandRetValType = new MetaLinks("Meta", 30);
		public static MetaLinks LinkDefReverse = new MetaLinks("Meta", 20);
		public static MetaLinks TypeLinkedServices = new MetaLinks("Meta", 21);
		public static MetaLinks MetaAccessControl = new MetaLinks("Meta", 29);
	}

	public class MetaValues : DustKey
	{
		private MetaValues(string module, int key) : base(module, key) { }

		public static MetaValues AttDefIdentifier = new MetaValues("Meta", 15);
		public static MetaValues LinkDefSet = new MetaValues("Meta", 9);
		public static MetaValues AttDefRaw = new MetaValues("Meta", 31);
		public static MetaValues AttDefDouble = new MetaValues("Meta", 18);
		public static MetaValues LinkDefSingle = new MetaValues("Meta", 12);
		public static MetaValues AttDefBool = new MetaValues("Meta", 14);
		public static MetaValues LinkDefArray = new MetaValues("Meta", 11);
		public static MetaValues LinkDefFactory = new MetaValues("Meta", 36);
		public static MetaValues LinkDefMap = new MetaValues("Meta", 17);
		public static MetaValues AttDefLong = new MetaValues("Meta", 13);
	}

}
