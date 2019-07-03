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

		
		Dictionary<DustDataEntity, Object> visitedEntities = new Dictionary<DustDataEntity, Object>();
		List<DustDataReference> callStack = new List<DustDataReference>();
		
		public DustProcVisitProcess(DustSession session, DustInfoTray tray)
		{
			this.session = session;
			this.infoTray = tray;
			this.visitTray = tray as DustVisitTray;
			
			dip = tray.value as DustInfoProcessor;
			dif = tray.value as DustInfoFilter;
			dvp = tray.value as DustVisitor;
		}
		
		private DustVisitTray getCallbackTray()
		{
			if (null == callbackTray) {
				callbackTray = new DustVisitTray(visitTray);
				sendVisitEvent(VisitEvent.beginVisit);
			}
			
			return callbackTray;
		}
		
		private void optCloseVisit()
		{
			if (null != callbackTray) {
				sendVisitEvent(VisitEvent.endVisit);
				callbackTray = null;
			}
		}
		
		public void sendVisitEvent(VisitEvent ve)
		{
			dvp.processVisitEvent(ve, getCallbackTray());
			// process response
		}

		
		public void visitEntity()
		{
			var e = visitTray.entity as DustDataEntity;
			
			if (null != e) {
				if (visitedEntities.ContainsKey(e) && !visitTray.cmd.HasFlag(VisitCommand.recNoCheck)) {
					sendVisitEvent(VisitEvent.revisitItem);
				} else {
					visitedEntities[e] = VISITED;				
			
					foreach (var ec in e.content.Keys) {
						var val = e.content[ec];
				
						if (null != val) {
							var r = val as DustDataReference;
					
							if ((null == r) && visitTray.cmd.HasFlag(VisitCommand.visitAtts)) {
								var wt = getCallbackTray();
								wt.key = ec;
								wt.value = val;
								dip.processInfo(wt);
							} else if ((null != r) && visitTray.cmd.HasFlag(VisitCommand.visitRefs)) {
								visitKey(session, e, ec, false);
							}
						}
					}
				
					if (visitTray.cmd.HasFlag(VisitCommand.recPathOnce)) {
						visitedEntities.Remove(e);
					}
				}
			}
		}
		
		public void visitKey(DustSession session, DustDataEntity ei, DustDataEntity eKey, bool close)
		{
			DustProcCursor cursor = session.optSetCursor(ei, eKey);
			
			if (null != cursor) {
				try {
					bool first = true;
					var pt = callbackTray ?? infoTray;
					foreach (DustDataReference ddr in cursor) {
						pt.value = ddr.eTarget;
									
						if ((null != dif) && !dif.shouldProcessInfo(pt)) {
							continue;
						}

						if (null != dvp) {
							if (first) {
								sendVisitEvent(VisitEvent.enterContext);
								first = false;
							} else {
								sendVisitEvent(VisitEvent.separateItems);
							}
						}
						
						dip.processInfo(pt);
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
			} else {
				
			}
		}
	}
}
