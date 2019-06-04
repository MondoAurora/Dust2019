/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/27
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;

using Dust;
using Dust.Utils.CSharp;

namespace Dust.Kernel
{

	public class DustEntityStore : DustUtilFactory<String, DustDataEntity>
	{
		public DustEntityStore()
			: base(typeof(DustDataEntity))
		{
		}
	}
	
	public class DustRefCursor : DustUtilsIterator<DustDataReference>
	{
		public readonly DustDataEntity owner;
		public readonly DustDataEntity key;
		
		public static DustRefCursor optSet(DustDataEntity owner, DustDataEntity key)
		{
			DustDataReference ddr = owner.getRef(key);
			return (null == ddr) ? null : new DustRefCursor(ddr);
		}
		
		DustRefCursor(DustDataReference ddr)
			: base(ddr.getMembers())
		{
			this.owner = ddr.eSrc;
			this.key = ddr.eLinkDef;
		}
	}
	
	public class DustSession
	{
		public readonly DustSession parent;
		
		public readonly DustUtilFactory<String, DustEntityStore> modules = new DustUtilFactory<String, DustEntityStore>(typeof(DustEntityStore));
		public readonly Dictionary<DustContext, DustDataEntity> ctx = new Dictionary<DustContext, DustDataEntity>();

		public readonly HashSet<DustDataEntity> managedInstances = new HashSet<DustDataEntity>();
		public readonly HashSet<DustDataReference> managedReferences = new HashSet<DustDataReference>();
		
		public readonly List<DustRefCursor> cursors = new List<DustRefCursor>();

		public DustSession()
			: this(null)
		{
		}
		
		public DustSession(DustSession p)
		{
			this.parent = p;
		}
		
		public DustDataEntity getEntity(String module, String entity)
		{
			return modules[module][entity];
		}
		
	}
	
	public class DustSystem : Dust
	{
		DustSession rootSession = new DustSession();

		override	public void accessImpl(DustOperation op, DustInfoTray tray)
		{
			DustDataEntity ei = (tray.owner is DustContext) 
				? rootSession.ctx[((DustContext)tray.owner)]
				: (DustDataEntity)tray.owner;
			
			var eKey = tray.key as DustDataEntity;
			
			if ( null == eKey ) {
				var key = (DustKey)tray.key;
				eKey = rootSession.modules[key.module][key.key];
			}
			
			switch (op) {
				case DustOperation.get:
					if ( DustUtils.isEnumRef(eKey.optValType) ) {
						foreach ( DustRefCursor drc in rootSession.cursors ) {
							if ( (drc.owner == tray.owner) && (drc.key == tray.key)) {
								tray.value = drc.Current.eTarget;
								return;
							}
						}
					}
					tray.value = ei.getValue(eKey, tray.value, tray.dustHint);
					break;
				case DustOperation.set:
					tray.value = ei.setValue(eKey, tray.value, tray.dustHint);
					break;
				case DustOperation.visit:
					DustRefCursor cursor = DustRefCursor.optSet(ei, eKey);
					if (null != cursor) {
						try {
							rootSession.cursors.Insert(0, cursor);
							
							var dvt = new DustVisitorTray(tray);
							foreach (DustDataReference ddr in cursor) {
								dvt.value = ddr.eTarget;
								((DustInfoProcessor) tray.value).processInfo(dvt);
							}
						} finally {
							rootSession.cursors.Remove(cursor);						
						}
					}
					break;
			}
		}

		public static DustSystem getSystem()
		{
			return (DustSystem)Dust.dustImpl;
		}
		
		public static DustSession getSession()
		{
			return getSystem().rootSession;
		}
		
		public static void initKernel(string[] args)
		{
			Dust.init(new DustSystem());
		}
	}
}
