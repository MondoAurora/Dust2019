package montru.java.gen.dust.kernel;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustCommComponents extends DustComponents {
	enum DustCommTypes implements DustEntityKey {
		Store("Comm;8"),
		Reference("Comm;13"),
		RemoteRef("Comm;17"),
		Persistent("Comm;3"),
		Domain("Comm;11"),
		Unit("Comm;4"),
				
		;
        
        private DustCommTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCommAtts implements DustEntityKey {
		RemoteRefModuleName("Comm;20"),
		ReferenceModuleName("Comm;15"),
		PersistentCommitId("Comm;0"),
		RemoteRefItemModuleId("Comm;19"),
		PersistentEntityId("Comm;2"),
		RemoteRefModuleCommitId("Comm;18"),
		ReferenceItemModuleId("Comm;14"),
		ReferenceModuleCommitId("Comm;16"),
		UnitNextEntityId("Comm;5"),
				
		;
        
        private DustCommAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCommLinks implements DustEntityKey {
		PersistentContainingUnit("Comm;1"),
		UnitEntities("Comm;6"),
		UnitDomain("Comm;12"),
		PersistentStoreWith("Comm;7"),
		UnitMainEntities("Comm;21"),
				
		;
        
        private DustCommLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCommValues implements DustEntityKey {
				
		;
        
        private DustCommValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCommServices implements DustEntityKey {
		Store("Comm;10"),
				
		;
        
        private DustCommServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCommMessages implements DustEntityKey {
				
		;
        
        private DustCommMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
