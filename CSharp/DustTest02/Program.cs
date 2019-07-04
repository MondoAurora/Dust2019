/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/07/01
 * Time: 16:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Threading.Tasks;

using Dust;
using Dust.Kernel;

using Dust.Units.Generic;
using Dust.Units.Geometry;

namespace DustTest02
{
	class DrawVisitor : DustVisitor, DustInfoFilter
	{
		public bool shouldProcessInfo(DustInfoTray tray)
		{
			return true;
		}
		
		public void processVisitEvent(VisitEvent visitEvent, DustVisitTray tray)
		{
//			Console.WriteLine("DrawVisitor processVisitEvent {0}: {1} = {2}", visitEvent, tray.key, tray.value);
		}
		
		public void processInfo(DustInfoTray tray)
		{
			switch (DustUtils.indexOf((DustEntity)tray.key,
				GenericAtts.IdentifiedIdLocal, 
				GeometryValues.GeometricDimensionCartesianY, 
				GeometryValues.GeometricDimensionCartesianZ)) {
				case 0:
					Console.WriteLine("Id: {0}", tray.value);
					break;
			}
			
			if (tray.value is Double) {
				Console.WriteLine("Double value {0}", tray.value);

			}
//			Console.WriteLine("DrawVisitor processInfo {0} = {1} {2}", tray.key, tray.value, ((null == tray.value) ? "null" : tray.value.GetType().ToString()));
		}
	}

	class Program
	{
		public static void Main(string[] args)
		{
			DustSystem.initKernel(args);
						
			var module = "GeoTest02";
			var entityId = "23";
			
			Console.WriteLine("Connecting to server...");
			
			Task<DustEntity> t = DustCommConnectorHttp.loadRemote("localhost", module, entityId);
			t.Wait();
			DustEntity e = t.Result;
			
			DustEntity e1 = DustSystem.getSystem().getEntity(module, "16");
			
//			var tray = new DustInfoTray(DustContext.SELF, new DrawVisitor());
			var tray = new DustInfoTray(e, new DrawVisitor());
			var vt = new DustVisitTray(tray);
//			vt.cmd = VisitCommand.visitAtts | VisitCommand.recEntityOnce;
			vt.cmd = VisitCommand.visitAllData | VisitCommand.recEntityOnce;
//			vt.cmd = VisitCommand.visitRefs | VisitCommand.recEntityOnce;
//			Console.WriteLine("heh? {0}", vt.cmd);
			Dust.Dust.access(DustAccessCommand.visit, vt);
			
			var a = Assembly.LoadFrom("bin\\csharp\\DustGui.dll");
			var tt = a.GetType("Dust.Gui.MyClass");
			var mc = Activator.CreateInstance(tt);
			
			var m = tt.GetMethod("testFunction");
			m.Invoke(mc, null);

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}