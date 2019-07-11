/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Layout
{
	public class LayoutTypes : DustKey
	{
		private LayoutTypes(string module, int key) : base(module, key) { }

		public static LayoutTypes TabCellInfo = new LayoutTypes("Layout", 19);
		public static LayoutTypes AlignmentValue = new LayoutTypes("Layout", 7);
		public static LayoutTypes LayoutCartesianTabular = new LayoutTypes("Layout", 26);
		public static LayoutTypes TabAxisInfo = new LayoutTypes("Layout", 16);
		public static LayoutTypes LayoutClient = new LayoutTypes("Layout", 22);
		public static LayoutTypes AlignmentMode = new LayoutTypes("Layout", 4);
		public static LayoutTypes LayoutManager = new LayoutTypes("Layout", 23);
		public static LayoutTypes PlacementAxisInfo = new LayoutTypes("Layout", 13);
	}

	public class LayoutAtts : DustKey
	{
		private LayoutAtts(string module, int key) : base(module, key) { }

		public static LayoutAtts cellIndex = new LayoutAtts("Layout", 17);
		public static LayoutAtts cellICount = new LayoutAtts("Layout", 15);
		public static LayoutAtts size = new LayoutAtts("Layout", 10);
		public static LayoutAtts gapBegin = new LayoutAtts("Layout", 12);
		public static LayoutAtts gapEnd = new LayoutAtts("Layout", 14);
		public static LayoutAtts weight = new LayoutAtts("Layout", 11);
	}

	public class LayoutLinks : DustKey
	{
		private LayoutLinks(string module, int key) : base(module, key) { }

		public static LayoutLinks AxisInfoSequence = new LayoutLinks("Layout", 24);
		public static LayoutLinks LegendAxisInfo = new LayoutLinks("Layout", 25);
		public static LayoutLinks Client = new LayoutLinks("Layout", 20);
		public static LayoutLinks AxisInfo = new LayoutLinks("Layout", 18);
		public static LayoutLinks AlignmentValue = new LayoutLinks("Layout", 9);
		public static LayoutLinks AlignmentMode = new LayoutLinks("Layout", 8);
		public static LayoutLinks LayoutManager = new LayoutLinks("Layout", 21);
	}

	public class LayoutValues : DustKey
	{
		private LayoutValues(string module, int key) : base(module, key) { }

	}

}
