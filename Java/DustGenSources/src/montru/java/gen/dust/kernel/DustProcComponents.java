package montru.java.gen.dust.kernel;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustProcComponents extends DustComponents {
	enum DustProcTypes implements DustEntityKey {
		Dispatcher("Proc;56"),
		Change("Proc;5"),
		Iterator("Proc;40"),
		Binary("Proc;1"),
		Session("Proc;7"),
		Task("Proc;24"),
		System("Proc;2"),
		AccessControl("Proc;33"),
		Relay("Proc;44"),
		Scheduler("Proc;23"),
		ValueUpdater("Proc;53"),
		NativeBound("Proc;8"),
				
		;
        
        private DustProcTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustProcAtts implements DustEntityKey {
		ChangeNewValue("Proc;14"),
		BinaryAutoInit("Proc;10"),
		SessionChangeMute("Proc;58"),
		TaskNextRun("Proc;29"),
		ChangeOldValue("Proc;13"),
		TaskRepeatSec("Proc;30"),
		NativeBoundId("Proc;0"),
		BinaryObjectName("Proc;11"),
				
		;
        
        private DustProcAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustProcLinks implements DustEntityKey {
		AccessControlChange("Proc;32"),
		SessionChangeListeners("Proc;19"),
		RelayTarget("Proc;45"),
		TaskInitiator("Proc;37"),
		ChangeSource("Proc;36"),
		ValueUpdaterTarget("Proc;54"),
		ChangeCmd("Proc;16"),
		IteratorMsgStart("Proc;46"),
		SessionRootEntity("Proc;39"),
		ValueUpdaterSource("Proc;55"),
		TaskMessage("Proc;28"),
		IteratorPathMsgTarget("Proc;49"),
		SchedulerTasks("Proc;26"),
		DispatcherTargets("Proc;57"),
		ChangeKey("Proc;15"),
		SessionBinaryAssignments("Proc;18"),
		IteratorMsgEnd("Proc;48"),
		TaskEntity("Proc;27"),
		IteratorMsgSep("Proc;51"),
		AccessControlAccess("Proc;35"),
		BinaryImplementedServices("Proc;12"),
		IteratorLinkLoop("Proc;47"),
		SessionType("Proc;38"),
		IteratorEvalFilter("Proc;50"),
		ChangeEntity("Proc;17"),
				
		;
        
        private DustProcLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustProcValues implements DustEntityKey {
				
		;
        
        private DustProcValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustProcServices implements DustEntityKey {
		Processor("Proc;6"),
		Iterator("Proc;41"),
		Evaluator("Proc;31"),
		Scheduler("Proc;25"),
		AccessControl("Proc;34"),
		ValueUpdater("Proc;52"),
		Listener("Proc;3"),
		Active("Proc;9"),
		Channel("Proc;4"),
				
		;
        
        private DustProcServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustProcMessages implements DustEntityKey {
		ActiveRelease("Proc;22"),
		ActiveInit("Proc;20"),
		EvaluatorEvaluate("Proc;59"),
		ProcessorProcess("Proc;21"),
				
		;
        
        private DustProcMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
