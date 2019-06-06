/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Collection
{
	public class CollectionTypes : DustKey
	{
		private CollectionTypes(string module, int key) : base(module, key) { }

		public static CollectionTypes Sequence = new CollectionTypes("Collection", 0);
	}

	public class CollectionAtts : DustKey
	{
		private CollectionAtts(string module, int key) : base(module, key) { }

	}

	public class CollectionLinks : DustKey
	{
		private CollectionLinks(string module, int key) : base(module, key) { }

		public static CollectionLinks SequenceMembers = new CollectionLinks("Collection", 1);
	}

}
