/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/07/01
 * Time: 16:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Dust;
using Dust.Kernel;

using Dust.Units.Data;
using Dust.Units.Collection;
using Dust.Units.Generic;
using Dust.Units.Geometry;

namespace DustTest02
{
	interface ShapeSource
	{

	};
	
	class GeometricData : Dictionary<GeometryValues, Double>
	{

	};
	
	class ShapePath : List<GeometricData>, ShapeSource
	{
		public bool closed;
	};
	
	class GeometricInclusion : Dictionary<GeometryValues, GeometricData>
	{
		public ShapeSource target;
	};
	
	class ShapeComposite: List<GeometricInclusion>, ShapeSource
	{
	};
		
	class DrawVisitor : DustVisitor //, DustInfoFilter
	{
		DustUtilKeyFinder kf = new DustUtilKeyFinder(
			                       CollectionLinks.SequenceMembers,
			                       GenericAtts.IdentifiedIdLocal,
			                       DataAtts.VariantValue,
			
			                       GeometryValues.GeometricDimensionCartesianX,
			                       GeometryValues.GeometricDimensionCartesianY,
			                       GeometryValues.GeometricDimensionCartesianZ,
			                       GeometryValues.GeometricDataRoleLocate,
			                       GeometryValues.GeometricDataRoleRotate,
			                       GeometryValues.GeometricDataRoleScale,
			
			                       GeometryLinks.GeometricDataType,
			                       GeometryLinks.GeometricDataMeasurements,
			
			                       GeometryLinks.GeometricInclusionTarget,
			                       GeometryLinks.GeometricInclusionParameters,
			                       
			                       GeometryAtts.ShapePathClosed
		                       );
		
		static readonly	Dictionary<DustEntity, Type> OB_TYPES = new Dictionary<DustEntity, Type> { {
				Dust.Dust.resolveKey(GeometryTypes.GeometricData),
				typeof(GeometricData)
			}, {
				Dust.Dust.resolveKey(GeometryTypes.GeometricInclusion),
				typeof(GeometricInclusion)
			},
			{ Dust.Dust.resolveKey(GeometryTypes.ShapePath), typeof(ShapePath) }, {
				Dust.Dust.resolveKey(GeometryTypes.ShapeComposite),
				typeof(ShapeComposite)
			},
		};
		
		static readonly	HashSet<object> COORDS = new HashSet<object> { 
			GeometryValues.GeometricDimensionCartesianX, 
			GeometryValues.GeometricDimensionCartesianY, 
			GeometryValues.GeometricDimensionCartesianZ, 
		};
		
		static readonly	HashSet<object> ROLES = new HashSet<object> { 
			GeometryValues.GeometricDataRoleLocate, 
			GeometryValues.GeometricDataRoleRotate, 
			GeometryValues.GeometricDataRoleScale, 
		};
		
		GeometryValues readingCoord;
		GeometryValues readingRole;
		GeometricData readingGeoData;
		Object lastReadObject;
		
		public VType getLastObject<VType>() {
			return (VType) lastReadObject;
		}
		
		public void processVisitEvent(VisitEvent visitEvent, DustVisitTray tray)
		{
			int kfi = kf.indexOf((DustEntity)tray.key);
			object kfk = kf.keyOf(kfi);

//			Console.WriteLine("DrawVisitor processVisitEvent {0} - {1}", visitEvent, tray.value);
//			return;
			
			switch (visitEvent) {
				case VisitEvent.entityStartOpt:
					DustEntity pt = DustUtils.getValue(tray.entity, (DustEntity)null, DataLinks.EntityPrimaryType);
					if (OB_TYPES.ContainsKey(pt)) {
						tray.readerObject = Activator.CreateInstance(OB_TYPES[pt]);
						var geo = tray.readerObject as GeometricData;
						if (null != geo) {
							readingGeoData = geo;
						}
					}
					break;
				case VisitEvent.keyStartOpt:
					if (-1 == kfi) {
						tray.resp = VisitResponse.skip;
					} else {
						if (COORDS.Contains(kfk)) {
							readingCoord = (GeometryValues)kfk;
						} else if (ROLES.Contains(kfk)) {
							readingRole = (GeometryValues)kfk;
						}

						Console.WriteLine("Entering {0} ", kf.keyOf(kfi));
					}
					break;
				case VisitEvent.keyEnd:
					if (CollectionLinks.SequenceMembers == kfk) {
						var sp = tray.readerParent as ShapePath;
						if (null != sp) {
							sp.Add((GeometricData)tray.readerObject);
						}
					
						var sc = tray.readerParent as ShapeComposite;
						if (null != sc) {
							sc.Add((GeometricInclusion)tray.readerObject);
						}
					} else if (GeometryLinks.GeometricInclusionTarget == kfk) {
						var gi = tray.readerParent as GeometricInclusion;
						if (null != gi) {
							gi.target = (ShapeSource)tray.readerObject;
						}
					} else if (GeometryLinks.GeometricInclusionParameters == kfk) {
						var gi = tray.readerParent as GeometricInclusion;
						
						if ((null != gi) && (readingRole != null)) {
							gi[readingRole] = (GeometricData)tray.readerObject;
						}
					}
					
					Console.WriteLine("Leaving {0} ", kf.keyOf(kfi));
					break;
				case VisitEvent.entityEnd:
					if ( null != tray.readerObject ) {
						lastReadObject = tray.readerObject;
					}
					break;
			}
		}
		
		public void processInfo(DustInfoTray tray)
		{
			int kfi = kf.indexOf((DustEntity)tray.key);
			if (-1 != kfi) {
				object kfk = kf.keyOf(kfi);
				
				if ( GeometryAtts.ShapePathClosed == kfk ) {
					((ShapePath) tray.readerObject).closed = (bool) tray.value;
				}
					
				if (null != readingGeoData) {
					if (null != readingCoord) {
						readingGeoData[readingCoord] = (Double)tray.value;
						readingCoord = null;
					}
				}
			}

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
			
			DustEntity e1 = DustSystem.getSystem().getEntity(module, "16");
			
			var draw = new DrawVisitor();
			var vt = new DustVisitTray(e, draw);
//			vt.cmd = VisitCommand.visitAtts | VisitCommand.recEntityOnce;
			vt.cmd = VisitCommand.visitAllData | VisitCommand.recPathOnce;
//			vt.cmd = VisitCommand.visitRefs | VisitCommand.recEntityOnce;
//			Console.WriteLine("heh? {0}", vt.cmd);
			Dust.Dust.access(DustAccessCommand.visit, vt);
			
			ShapeSource s = draw.getLastObject<ShapeSource>();
			
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