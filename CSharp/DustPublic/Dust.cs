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
		
		public DustInfoTray(DustEntity owner)
			:this(owner, null)
		{
		}
		
		public DustInfoTray(DustEntity owner, Object value, params Object[] keys)
		{
			this.entity = owner;
			this.keys = keys;
			if (0 < keys.Length) {
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
		visitStart,
		processValue,
		keyStartOpt,
		refSep,
		entityStartOpt,
		entityEnd,
		entityRevisit,
		keyEnd,
		visitEnd,
		
		visitAborted = -1,
		visitInternalError = -2
	}
	
	[Flags]
	public enum VisitCommand
	{
		none = 0,
		
		visitAtts = 2 << 0,
		visitRefs = 2 << 1,
		visitAllData = visitAtts | visitRefs,
		visitMeta = 2 << 2,
		// for serialization
		
		visit = visitAtts | visitRefs |	visitMeta,
		
		recEntityOnce = 2 << 3,
		recPathOnce = 2 << 4,
		recNoCheck = 2 << 5,
		
		rec = recEntityOnce | recPathOnce |	recNoCheck,
	}
	
	public enum VisitResponse
	{
		success,
		
		skip,
		
		abort,
	}
	
	public class DustVisitTray : DustInfoTray
	{
		public DustVisitor visitor;
		
		public VisitCommand cmd;
		public object result;
		public VisitResponse resp;
		
		public DustVisitTray(DustInfoTray src, DustVisitor visitor)
			: base(src)
		{
			this.visitor = visitor;
			value = visitor;
		}
		
		public DustVisitTray(DustVisitTray src)
		{
			loadSrc(src);
		}
		
		public void loadSrc(DustVisitTray src)
		{
			base.loadSrc(src);
			
			this.visitor = src.visitor;
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
		NotSet = 0,
		AttDefIdentifier = 1,
		AttDefBool = 2,
		AttDefLong = 3,
		AttDefDouble = 4,
		AttDefRaw = 5,
		LinkDefSingle = -1,
		LinkDefSet = -2,
		LinkDefArray = -3,
		LinkDefMap = -4
	}
	
	public class DustException : Exception
{
    public DustException() : base() { }
    public DustException(string message) : base(message) { }
    public DustException(string message, System.Exception inner) : base(message, inner) { }
}

	public interface DustKernel
	{
		void accessImpl(DustAccessCommand op, DustInfoTray tray);
		DustEntity resolveKeyImpl(DustKey key);
	}

	public abstract partial class Dust
	{
		protected static DustKernel dustImpl;
				
		public static void access(DustAccessCommand op, DustInfoTray tray)
		{
			dustImpl.accessImpl(op, tray);
		}
		
		public static DustEntity resolveKey(DustKey key)
		{
			return dustImpl.resolveKeyImpl(key);
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
