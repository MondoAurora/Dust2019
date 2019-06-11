/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Geometry
{
	public class GeometryTypes : DustKey
	{
		private GeometryTypes(string module, int key) : base(module, key) { }

		public static GeometryTypes CartesianVector = new GeometryTypes("Geometry", 0);
	}

	public class GeometryAtts : DustKey
	{
		private GeometryAtts(string module, int key) : base(module, key) { }

		public static GeometryAtts Z = new GeometryAtts("Geometry", 1);
		public static GeometryAtts Y = new GeometryAtts("Geometry", 3);
		public static GeometryAtts X = new GeometryAtts("Geometry", 2);
	}

	public class GeometryLinks : DustKey
	{
		private GeometryLinks(string module, int key) : base(module, key) { }

	}

}
