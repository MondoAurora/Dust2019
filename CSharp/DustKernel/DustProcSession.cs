/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/06
 * Time: 11:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Dust;
using Dust.Utils.CSharp;

namespace Dust.Kernel
{
	public class DustSession
	{
		public readonly DustSystem system;
		public readonly DustSession parent;
		
		public readonly Dictionary<DustContext, DustDataEntity> ctx = new Dictionary<DustContext, DustDataEntity>();

		public readonly HashSet<DustDataEntity> managedInstances = new HashSet<DustDataEntity>();
		public readonly HashSet<DustDataReference> managedReferences = new HashSet<DustDataReference>();
		
		public readonly List<DustProcCursor> cursors = new List<DustProcCursor>();

		public DustSession(DustSystem system)
		{
			this.system = system;
			this.parent = null;
		}
		
		public DustSession(DustSession p)
		{
			this.parent = p;
			this.system = p.system;
		}
		
		public DustDataEntity getEntity(String module, String entity)
		{
			return system.getEntity(module, entity);
		}
		
		public DustDataEntity resolveEntity(Object de)
		{
			var ei = de as DustDataEntity;
			if (null != ei) {
				return ei;				
			}
			
			var dc = de as DustContext;
			if (null != dc) {
				return ctx[dc];
			}
			
			var dk = de as DustKey;
			if (null != dk) {
				return system.getEntity(dk.module, dk.key);
			}
			
			return null;
		}

		public DustProcCursor optSetCursor(DustDataEntity ei, DustDataEntity eKey)
		{
			DustProcCursor cursor = DustProcCursor.optSet(ei, eKey);
			if (null != cursor) {
				cursors.Insert(0, cursor);
			}

			return cursor;
		}
		
		public void dropCursor(DustProcCursor cursor)
		{
			if (null != cursor) {
				cursors.Remove(cursor);
			}
		}		
	}

}
