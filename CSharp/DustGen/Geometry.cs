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

		public static GeometryTypes RenderTarget = new GeometryTypes("Geometry", 31);
		public static GeometryTypes ShapeArc = new GeometryTypes("Geometry", 23);
		public static GeometryTypes GeometricDataRole = new GeometryTypes("Geometry", 16);
		public static GeometryTypes ShapePath = new GeometryTypes("Geometry", 8);
		public static GeometryTypes ShapeRef = new GeometryTypes("Geometry", 38);
		public static GeometryTypes ShapeComposite = new GeometryTypes("Geometry", 7);
		public static GeometryTypes GeometricDimension = new GeometryTypes("Geometry", 25);
		public static GeometryTypes GeometricData = new GeometryTypes("Geometry", 0);
		public static GeometryTypes RenderSource = new GeometryTypes("Geometry", 37);
		public static GeometryTypes ShapeBox = new GeometryTypes("Geometry", 4);
		public static GeometryTypes GeometricInclusion = new GeometryTypes("Geometry", 20);
	}

	public class GeometryAtts : DustKey
	{
		private GeometryAtts(string module, int key) : base(module, key) { }

		public static GeometryAtts ShapePathClosed = new GeometryAtts("Geometry", 11);
	}

	public class GeometryLinks : DustKey
	{
		private GeometryLinks(string module, int key) : base(module, key) { }

		public static GeometryLinks GeometricDataMeasurements = new GeometryLinks("Geometry", 22);
		public static GeometryLinks GeometricInclusionTarget = new GeometryLinks("Geometry", 19);
		public static GeometryLinks ShapeArcBegin = new GeometryLinks("Geometry", 5);
		public static GeometryLinks GeometricInclusionParameters = new GeometryLinks("Geometry", 21);
		public static GeometryLinks ShapeBoxSize = new GeometryLinks("Geometry", 6);
		public static GeometryLinks ShapeArcEnd = new GeometryLinks("Geometry", 24);
		public static GeometryLinks GeometricDataType = new GeometryLinks("Geometry", 15);
	}

	public class GeometryValues : DustKey
	{
		private GeometryValues(string module, int key) : base(module, key) { }

		public static GeometryValues GeometricDimensionCartesianX = new GeometryValues("Geometry", 28);
		public static GeometryValues GeometricDimensionCartesianY = new GeometryValues("Geometry", 27);
		public static GeometryValues GeometricDimensionCartesianZ = new GeometryValues("Geometry", 26);
		public static GeometryValues GeometricDataRoleRotate = new GeometryValues("Geometry", 18);
		public static GeometryValues GeometricDataRoleLocate = new GeometryValues("Geometry", 17);
		public static GeometryValues GeometricDataRoleScale = new GeometryValues("Geometry", 14);
	}

}
