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

using Dust;
using Dust.Utils.CSharp;

namespace Dust.Kernel
{

	public class DustDataStore : DustUtilFactory<String, DustDataEntity>
	{
		public DustDataStore()
			: base(typeof(DustDataEntity))
		{
		}
	}
		
	public class DustSystem : Dust, DustKernel
	{
		public readonly DustUtilFactory<String, DustDataStore> modules = new DustUtilFactory<String, DustDataStore>(typeof(DustDataStore));

		DustSession rootSession;
		
		public DustSystem()
		{
			rootSession = new DustSession(this);
		}
		
		public DustSession getCurrentSession()
		{
			return rootSession;
		}
		
		public static DustDataEntity getEntity(DustKey key)
		{
			return getSystem().getEntity(key.module, key.key);
		}
		
		public DustDataEntity getEntity(String module, String entity)
		{
			return modules[module][entity];
		}

		public DustEntity resolveKeyImpl(DustKey key)
		{
			return getEntity(key.module, key.key);
		}

		public void accessImpl(DustAccessCommand op, DustInfoTray tray)
		{
			var session = getCurrentSession();
			
			DustDataEntity ei = (tray.entity is DustContext) 
				? session.ctx[((DustContext)tray.entity)]
				: (DustDataEntity)tray.entity;
			
			var eKey = tray.key as DustDataEntity;
			
			if (null == eKey) {
				var key = tray.key as DustKey;
				if (null != key) {
					eKey = modules[key.module][key.key];
				}
			}
			
			switch (op) {
				case DustAccessCommand.read:
					tray.value = ei.getValue(eKey, tray.value, tray.dustHint);
					
					if ((null == tray.value) && DustUtils.isEnumRef(eKey.optValType)) {
						foreach (DustProcCursor drc in rootSession.cursors) {
							if ((drc.owner == ei) && (drc.key == eKey)) {
								tray.value = drc.Current.eTarget;
								return;
							}
						}
					}
					
					break;
				case DustAccessCommand.write:
					tray.value = ei.setValue(eKey, tray.value, tray.dustHint);
					break;
				case DustAccessCommand.visit:
					var vp = new DustProcVisitProcess(session, tray);
					var vt = tray as DustVisitTray;
					
					if (null == vt) {
						vp.visitKey(ei, eKey, true);
					} else {
						vp.visitEntity();
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
			return getSystem().getCurrentSession();
		}
		
		public static void initKernel(string[] args)
		{
			Dust.init(new DustSystem());
		}
	}
}
