/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.GuiMeta
{
	public class GuiMetaTypes : DustKey
	{
		private GuiMetaTypes(string module, int key) : base(module, key) { }

		public static GuiMetaTypes GuiComponentContextAware = new GuiMetaTypes("GuiMeta", 3);
		public static GuiMetaTypes GuiComponentContextRef = new GuiMetaTypes("GuiMeta", 9);
		public static GuiMetaTypes GuiWidgetAttToggle = new GuiMetaTypes("GuiMeta", 11);
		public static GuiMetaTypes GuiWidgetLabel = new GuiMetaTypes("GuiMeta", 6);
		public static GuiMetaTypes GuiWidgetAttEditor = new GuiMetaTypes("GuiMeta", 5);
		public static GuiMetaTypes GuiContainerMapped = new GuiMetaTypes("GuiMeta", 4);
		public static GuiMetaTypes GuiComponent = new GuiMetaTypes("GuiMeta", 1);
		public static GuiMetaTypes GuiWidgetAnchor = new GuiMetaTypes("GuiMeta", 8);
		public static GuiMetaTypes GuiWidgetActive = new GuiMetaTypes("GuiMeta", 10);
		public static GuiMetaTypes GuiComponentIdentified = new GuiMetaTypes("GuiMeta", 2);
		public static GuiMetaTypes GuiContainerContextAware = new GuiMetaTypes("GuiMeta", 12);
		public static GuiMetaTypes GuiWidgetCommand = new GuiMetaTypes("GuiMeta", 7);
		public static GuiMetaTypes GuiPanelEntity = new GuiMetaTypes("GuiMeta", 0);
	}

	public class GuiMetaAtts : DustKey
	{
		private GuiMetaAtts(string module, int key) : base(module, key) { }

	}

	public class GuiMetaLinks : DustKey
	{
		private GuiMetaLinks(string module, int key) : base(module, key) { }

	}

	public class GuiMetaValues : DustKey
	{
		private GuiMetaValues(string module, int key) : base(module, key) { }

	}

}
