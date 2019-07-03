/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/27
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Dust
{
	public class DustEntity
	{
		
	}
	
	public class DustContext : DustEntity
	{
		public static DustContext SELF = new DustContext();
		public static DustContext MESSAGE = new DustContext();
		
		private DustContext()
		{
		}
	}
	
	public class DustInfoTray
	{
		public DustEntity entity;
		public Object key;
		Object[] keys;
		
		public Object value;
		
		public Object dustHint;
		public Object rawHint;
		
		public DustInfoTray()
		{
		}
		
		public DustInfoTray(DustEntity owner, Object value, params Object[] keys)
		{
			this.entity = owner;
			this.keys = keys;
			if ( 0 < keys.Length ) {
				this.key = keys[0];
			}
			this.value = value;
		}

		public DustInfoTray(DustInfoTray src)
		{
			loadSrc(src);
		}
		
		public void loadSrc(DustInfoTray src)
		{
			this.entity = src.entity;
			this.key = src.key;
			this.value = src.value;
			this.dustHint = src.dustHint;
			this.rawHint = src.rawHint;
		}		
	}
	
	public enum VisitEvent
	{
		beginVisit,
		enterContext,
		separateItems,
		revisitItem,
		leaveContext,
		endVisit,
		
		visitAborted // called when lower layers aborted aka. finally
	}
	
	[Flags]
	public enum VisitCommand
	{
		none = 0,
		
		visitAtts = 2 >> 0,
		visitRefs = 2 >> 1,
		visitAllData = visitAtts | visitRefs,
		visitMeta = 2 >> 2,
		// for serialization
		
		visit = visitAtts | visitRefs |	visitMeta,
		
		recEntityOnce = 2 >> 3,
		recPathOnce = 2 >> 4,
		recNoCheck = 2 >> 5,
		
		rec = recEntityOnce | recPathOnce |	recNoCheck,
	}
	
	public enum VisitResponse
	{
		itemProcessed,
		itemSkipped,
		levelDone,
		abort,
	}
	
	public class DustVisitTray : DustInfoTray
	{
		//		public VisitEvent visitEvent;
		
		public VisitCommand cmd;
		public object result;
		public VisitResponse resp;
		
		public DustVisitTray(DustInfoTray src)
			: base(src)
		{
		}
		
		public DustVisitTray(DustVisitTray src)
		{
			loadSrc(src);
		}
		
		public void loadSrc(DustVisitTray src)
		{
			base.loadSrc(src);
			
//			this.visitEvent = src.visitEvent;
			this.cmd = src.cmd;
			this.result = src.result;
		}

	}

	public enum DustAccessCommand
	{
		read,
		write,
		visit
	}
	
	public interface DustInfoFilter
	{
		bool shouldProcessInfo(DustInfoTray tray);
	}
	
	public interface DustInfoProcessor
	{
		void processInfo(DustInfoTray tray);
	}
	
	public interface DustVisitor : DustInfoProcessor
	{
		void processVisitEvent(VisitEvent visitEvent, DustVisitTray tray);
	}
	

	public enum DustValType
	{
		AttDefIdentifier = 0,
		AttDefBool = 1,
		AttDefLong = 2,
		AttDefDouble = 3,
		AttDefRaw = 4,
		LinkDefSingle = -1,
		LinkDefSet = -2,
		LinkDefArray = -3,
		LinkDefMap = -4
	}

	public interface DustKernel
	{
		void accessImpl(DustAccessCommand op, DustInfoTray tray);
	}

	public abstract partial class Dust
	{
		protected static DustKernel dustImpl;
				
		public static void access(DustAccessCommand op, DustInfoTray tray)
		{
			dustImpl.accessImpl(op, tray);
		}
		
//		public static void propagateKernel(Assembly a)
//		{
//			var tDM = a.GetType("Dust.Module.DustModule");
//			var mIK = tDM.GetMethod("initKernel");
//			
//			mIK.Invoke(null, new Object [] { dustImpl });
//		}
	}
}
