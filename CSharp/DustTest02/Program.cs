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

namespace DustTest02
{
	class DrawVisitor : DustVisitor
	{
		public void processVisitEvent(VisitEvent visitEvent, DustVisitTray tray)
		{
			Console.WriteLine("DrawVisitor processVisitEvent {0}: {1} = {2}", visitEvent, tray.key, tray.value);
		}
		
		public void processInfo(DustInfoTray tray)
		{
			Console.WriteLine("DrawVisitor processInfo {0} = {1}", tray.key, tray.value);
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
			
			var tray = new DustInfoTray(DustContext.SELF, new DrawVisitor());
			var vt = new DustVisitTray(tray);
			vt.cmd = VisitCommand.visitRefs | VisitCommand.recPathOnce;
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