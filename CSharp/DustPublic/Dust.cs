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
		
		public Object value;
		
		public Object dustHint;
		public Object rawHint;
		
		public DustInfoTray()
		{
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
		
		public DustInfoTray(DustEntity owner, Object key, Object value)
		{
			this.entity = owner;
			this.key = key;
			this.value = value;
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
		
		visitAborted
	}
	
	public enum VisitCommand
	{		
		none = 0,
		
		visitAllAtts = 1,
		visitAllRefs = 2,
		visitAll = 3,
		visitMeta = 256, // for serialization
		
		visit = visitAllAtts | visitAllRefs |	visitAll,
		
		recBottomUp = 4,
		recTopDown = 8,
		recFollowPath = 12,
		
		rec = recBottomUp | recTopDown |	recFollowPath,
		
		nextItemSkipped = 16,
		nextLevelDone = 32,
		nextAbort = 48,
		
		next = nextItemSkipped | nextLevelDone | nextAbort,
	}
	
	public class DustVisitTray : DustInfoTray
	{
//		public VisitEvent visitEvent;
		
		public VisitCommand cmd;
		public object result;
		
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
		get,
		set,
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

	public abstract class DustKernel
	{
		public abstract void accessImpl(DustAccessCommand op, DustInfoTray tray);
	}

	public abstract partial class Dust
	{
		protected static DustKernel dustImpl;
				
		public static void access(DustAccessCommand op, DustInfoTray tray)
		{
			dustImpl.accessImpl(op, tray);
		}
	}
}
