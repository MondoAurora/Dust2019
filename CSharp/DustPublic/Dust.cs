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
	
	public abstract class DustKernel
	{
		public abstract double getValDoubleImpl(DustEntity entity, DustKey key, double defVal);
	}
	
	public abstract class DustEntityProcessorBase
	{
		public abstract void processEntity(DustEntity entity);
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

	public abstract class Dust : DustKernel
	{
		private static readonly object sysLock = new object();

		protected static DustKernel dustImpl;
		
		protected static void init(DustKernel system)
		{ 
			lock (sysLock) { 
				if (null == Dust.dustImpl) {
					Dust.dustImpl = system;
				}
			}
		}
		
		public static double getValDouble(DustEntity entity, DustKey key, double defVal)
		{
			return dustImpl.getValDoubleImpl(entity, key, defVal);
		}		
	}
}
