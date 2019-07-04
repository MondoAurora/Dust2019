/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/06
 * Time: 11:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Dust;

namespace Dust.Kernel
{
	class VisitContext
	{
		public DustVisitTray tray;
		
		public VisitContext(DustVisitTray tray)
		{
			this.tray = tray;
		}
	}
	
	public class DustProcVisitProcess
	{
		static Object VISITED = new Object();
		readonly DustSession session;
		readonly DustInfoTray infoTray;
		readonly DustVisitTray visitTray;
		
		DustVisitTray callbackTray;
		
		readonly DustInfoProcessor dip;
		readonly DustInfoFilter dif;
		readonly DustVisitor dvp;
		
		bool empty;

		Dictionary<DustDataEntity, Object> visitedEntities = new Dictionary<DustDataEntity, Object>();
		List<DustDataReference> callStack = new List<DustDataReference>();
		
		public DustProcVisitProcess(DustSession session, DustInfoTray tray)
		{
			this.session = session;
			this.infoTray = tray;
			this.visitTray = tray as DustVisitTray;
			callbackTray = (null == visitTray) ? new DustVisitTray(tray) : new DustVisitTray(visitTray);
			callbackTray.entity = session.resolveEntity(tray.entity);
			
			empty = true;
			
			dip = tray.value as DustInfoProcessor;
			dif = tray.value as DustInfoFilter;
			dvp = tray.value as DustVisitor;
		}
				
		private void optCloseVisit()
		{
			if (!empty) {
				sendVisitEvent(VisitEvent.endVisit);
				callbackTray = null;
			}
		}
		
		public void sendVisitEvent(VisitEvent ve)
		{
			if (empty) {
				empty = false;
				sendVisitEvent(VisitEvent.beginVisit);
			}

			dvp.processVisitEvent(ve, callbackTray);
			// process response
		}

		public void visitEntity()
		{
			var e = session.resolveEntity(callbackTray.entity);
			
			if (null != e) {
				bool found = visitedEntities.ContainsKey(e);
				bool nochk = callbackTray.cmd.HasFlag(VisitCommand.recNoCheck);
				if (visitedEntities.ContainsKey(e) && !callbackTray.cmd.HasFlag(VisitCommand.recNoCheck)) {
					sendVisitEvent(VisitEvent.revisitItem);
				} else {
					visitedEntities[e] = VISITED;				
			
					foreach (var ec in e.content.Keys) {
						var val = e.content[ec];
				
						if (null != val) {
							var r = val as DustDataReference;
					
							if ((null == r) && callbackTray.cmd.HasFlag(VisitCommand.visitAtts)) {
								callbackTray.key = ec;
								callbackTray.value = val;
								dip.processInfo(callbackTray);
							} else if ((null != r) && callbackTray.cmd.HasFlag(VisitCommand.visitRefs)) {
								visitKey(e, ec, false);
							}
						}
					}
				
					if (callbackTray.cmd.HasFlag(VisitCommand.recPathOnce)) {
						visitedEntities.Remove(e);
					}
				}
			}
		}
		
		public void visitKey(DustDataEntity ei, DustDataEntity eKey, bool close)
		{
			DustProcCursor cursor = session.optSetCursor(ei, eKey);
			
			if (null != cursor) {
				try {
					bool first = true;
					foreach (DustDataReference ddr in cursor) {
						callbackTray.entity = ei;
						callbackTray.value = ddr.eTarget;
						callbackTray.key = eKey;
									
						if ((null != dif) && !dif.shouldProcessInfo(callbackTray)) {
							continue;
						}

						if (null != dvp) {
							if (first) {
								first = false;
								sendVisitEvent(VisitEvent.enterContext);
							} else {
								sendVisitEvent(VisitEvent.separateItems);
							}
						}
						
						dip.processInfo(callbackTray);
						
						if (null != dvp) {
							bool map = DustValType.LinkDefMap == ddr.eLinkDef.optValType;

							if (map) {
								callbackTray.key = ddr.getId();
								sendVisitEvent(VisitEvent.enterContext);
							}
							
							callbackTray.entity = ddr.eTarget;
							visitEntity();
							
							if (map) {
								callbackTray.key = ddr.getId();
								sendVisitEvent(VisitEvent.leaveContext);
							}
						}
					}
								
					if (!first) {
						sendVisitEvent(VisitEvent.leaveContext);
						if (close) {
							sendVisitEvent(VisitEvent.endVisit);
						}
					} 
				} finally {
					session.cursors.Remove(cursor);						
				}
			}
		}
	}
}
