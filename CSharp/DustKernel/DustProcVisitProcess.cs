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
		
		public VisitContext( DustVisitTray tray ) {
			this.tray = tray;
		}
	}
	
	public class DustProcVisitProcess
	{
		readonly DustSession session;
		readonly DustVisitTray visitTray;
		
		Dictionary<DustDataEntity, Object> visitedEntities = new Dictionary<DustDataEntity, Object>();
		List<DustDataReference> callStack = new List<DustDataReference>();
		
		public DustProcVisitProcess(DustSession session, DustVisitTray visitTray)
		{
			this.session = session;
			this.visitTray = visitTray;
		}
		
		public void startVisit()
		{
			
		}
		
		public static void sendVisitEvent(DustVisitor visitor, VisitEvent ve, DustVisitTray dvt, DustInfoTray src)
		{
			// init dvt
//			dvt.visitEvent = VisitEvent.beginVisit;
			visitor.processVisitEvent(ve, dvt);
			// process response
		}

		public static void visitKey(DustSession session, DustDataEntity ei, DustDataEntity eKey, DustInfoTray tray)
		{
			DustProcCursor cursor = session.optSetCursor(ei, eKey);
			if (null != cursor) {
				try {
					var dip = tray.value as DustInfoProcessor;
					var dif = tray.value as DustInfoFilter;
					var dvp = tray.value as DustVisitor;
							
					DustVisitTray dvt = null;
					var pt = tray;
					foreach (DustDataReference ddr in cursor) {
						pt.value = ddr.eTarget;
									
						if ((null != dif) && !dif.shouldProcessInfo(pt)) {
							continue;
						}

						if (null != dvp) {
							if (null == dvt) {
								pt = dvt = new DustVisitTray(tray);
								sendVisitEvent(dvp, VisitEvent.beginVisit, dvt, tray);
								sendVisitEvent(dvp, VisitEvent.enterContext, dvt, tray);
							} else {
								sendVisitEvent(dvp, VisitEvent.separateItems, dvt, tray);
							}
						}
						dip.processInfo(pt);
					}
								
					if (null != dvt) {
						sendVisitEvent(dvp, VisitEvent.leaveContext, dvt, tray);
						sendVisitEvent(dvp, VisitEvent.endVisit, dvt, tray);
					} 
								
				} finally {
					session.cursors.Remove(cursor);						
				}
			}
		}
	}
}
