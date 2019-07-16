/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Generic
{
	public class GenericTypes : DustKey
	{
		private GenericTypes(string module, int key) : base(module, key) { }

		public static GenericTypes Tag = new GenericTypes("Generic", 5);
		public static GenericTypes Identified = new GenericTypes("Generic", 1);
		public static GenericTypes Connected = new GenericTypes("Generic", 4);
		public static GenericTypes ContextAware = new GenericTypes("Generic", 10);
		public static GenericTypes Stream = new GenericTypes("Generic", 3);
		public static GenericTypes Reference = new GenericTypes("Generic", 13);
		public static GenericTypes Callback = new GenericTypes("Generic", 15);
	}

	public class GenericAtts : DustKey
	{
		private GenericAtts(string module, int key) : base(module, key) { }

		public static GenericAtts StreamWriter = new GenericAtts("Generic", 14);
		public static GenericAtts StreamFileAccess = new GenericAtts("Generic", 7);
		public static GenericAtts IdentifiedIdLocal = new GenericAtts("Generic", 0);
		public static GenericAtts StreamFileName = new GenericAtts("Generic", 6);
	}

	public class GenericLinks : DustKey
	{
		private GenericLinks(string module, int key) : base(module, key) { }

		public static GenericLinks ReferencePath = new GenericLinks("Generic", 12);
		public static GenericLinks CallbackMessage = new GenericLinks("Generic", 16);
		public static GenericLinks ConnectedExtends = new GenericLinks("Generic", 8);
		public static GenericLinks ContextAwareEntity = new GenericLinks("Generic", 11);
		public static GenericLinks ConnectedRequires = new GenericLinks("Generic", 2);
		public static GenericLinks ConnectedOwner = new GenericLinks("Generic", 9);
	}

	public class GenericValues : DustKey
	{
		private GenericValues(string module, int key) : base(module, key) { }

	}

}
