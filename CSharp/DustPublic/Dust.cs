/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/27
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

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
		public DustEntity owner;
		public Object key;
		
		public Object value;
		
		public Object dustHint;
		public Object rawHint;
		
		public DustInfoTray()
		{
		}
		
		public DustInfoTray(DustInfoTray src)
		{
			this.owner = src.owner;
			this.key = src.key;
			this.value = src.value;
			this.dustHint = src.dustHint;
			this.rawHint = src.rawHint;
		}
		
		public DustInfoTray(DustEntity owner, Object key, Object value)
		{
			this.owner = owner;
			this.key = key;
			this.value = value;
		}
	}
	
	public enum VisitAction
	{
		beginVisit,
		enterContext,
		separateItems,
		leaveContext,
		endVisit
	}
	
	public class DustVisitorTray : DustInfoTray
	{
		public VisitAction visitAction;
		public bool recursive;
		
		public DustVisitorTray(DustInfoTray src)
			: base(src)
		{
		}
		
		public DustVisitorTray(DustVisitorTray src)
			: base(src)
		{
			this.visitAction = src.visitAction;
			this.recursive = src.recursive;
		}

	}

	public enum DustOperation
	{
		get,
		set,
		visit
	}
	
	public interface DustInfoProcessor
	{
		void processInfo(DustInfoTray tray);
	}
	
	public interface DustVisitor : DustInfoProcessor
	{
		void processVisitEvent(DustVisitorTray tray);
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
		public abstract void accessImpl(DustOperation op, DustInfoTray tray);
	}

	public abstract partial class Dust
	{
		protected static DustKernel dustImpl;
				
		public static void access(DustOperation op, DustInfoTray tray)
		{
			dustImpl.accessImpl(op, tray);
		}
	}
}
