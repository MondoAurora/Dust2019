/*
 * Created by Montru2018
 */
 
using System;
using Dust;

namespace Dust.Units.Proc
{
	public class ProcTypes : DustKey
	{
		private ProcTypes(string module, int key) : base(module, key) { }

		public static ProcTypes Change = new ProcTypes("Proc", 5);
		public static ProcTypes Iterator = new ProcTypes("Proc", 40);
		public static ProcTypes Relay = new ProcTypes("Proc", 44);
		public static ProcTypes ValueUpdater = new ProcTypes("Proc", 53);
		public static ProcTypes System = new ProcTypes("Proc", 2);
		public static ProcTypes NativeBound = new ProcTypes("Proc", 8);
		public static ProcTypes AccessControl = new ProcTypes("Proc", 33);
		public static ProcTypes Dispatcher = new ProcTypes("Proc", 56);
		public static ProcTypes Task = new ProcTypes("Proc", 24);
		public static ProcTypes Binary = new ProcTypes("Proc", 1);
		public static ProcTypes Scheduler = new ProcTypes("Proc", 23);
		public static ProcTypes Session = new ProcTypes("Proc", 7);
	}

	public class ProcAtts : DustKey
	{
		private ProcAtts(string module, int key) : base(module, key) { }

		public static ProcAtts NativeBoundId = new ProcAtts("Proc", 0);
		public static ProcAtts SessionChangeMute = new ProcAtts("Proc", 58);
		public static ProcAtts TaskRepeatSec = new ProcAtts("Proc", 30);
		public static ProcAtts TaskNextRun = new ProcAtts("Proc", 29);
		public static ProcAtts BinaryObjectName = new ProcAtts("Proc", 11);
		public static ProcAtts ChangeOldValue = new ProcAtts("Proc", 13);
		public static ProcAtts ChangeNewValue = new ProcAtts("Proc", 14);
		public static ProcAtts BinaryAutoInit = new ProcAtts("Proc", 10);
	}

	public class ProcLinks : DustKey
	{
		private ProcLinks(string module, int key) : base(module, key) { }

		public static ProcLinks ValueUpdaterSource = new ProcLinks("Proc", 55);
		public static ProcLinks TaskEntity = new ProcLinks("Proc", 27);
		public static ProcLinks ValueUpdaterTarget = new ProcLinks("Proc", 54);
		public static ProcLinks SchedulerTasks = new ProcLinks("Proc", 26);
		public static ProcLinks SessionRootEntity = new ProcLinks("Proc", 39);
		public static ProcLinks IteratorPathMsgTarget = new ProcLinks("Proc", 49);
		public static ProcLinks IteratorEvalFilter = new ProcLinks("Proc", 50);
		public static ProcLinks IteratorLinkLoop = new ProcLinks("Proc", 47);
		public static ProcLinks SessionChangeListeners = new ProcLinks("Proc", 19);
		public static ProcLinks DispatcherTargets = new ProcLinks("Proc", 57);
		public static ProcLinks IteratorMsgEnd = new ProcLinks("Proc", 48);
		public static ProcLinks IteratorMsgSep = new ProcLinks("Proc", 51);
		public static ProcLinks ChangeKey = new ProcLinks("Proc", 15);
		public static ProcLinks TaskMessage = new ProcLinks("Proc", 28);
		public static ProcLinks ChangeSource = new ProcLinks("Proc", 36);
		public static ProcLinks BinaryImplementedServices = new ProcLinks("Proc", 12);
		public static ProcLinks ChangeCmd = new ProcLinks("Proc", 16);
		public static ProcLinks SessionBinaryAssignments = new ProcLinks("Proc", 18);
		public static ProcLinks AccessControlChange = new ProcLinks("Proc", 32);
		public static ProcLinks IteratorMsgStart = new ProcLinks("Proc", 46);
		public static ProcLinks SessionType = new ProcLinks("Proc", 38);
		public static ProcLinks ChangeEntity = new ProcLinks("Proc", 17);
		public static ProcLinks AccessControlAccess = new ProcLinks("Proc", 35);
		public static ProcLinks RelayTarget = new ProcLinks("Proc", 45);
		public static ProcLinks TaskInitiator = new ProcLinks("Proc", 37);
	}

	public class ProcValues : DustKey
	{
		private ProcValues(string module, int key) : base(module, key) { }

	}

}
