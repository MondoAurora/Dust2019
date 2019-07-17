package montru.java.gen.dust.utils;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustGenericComponents extends DustComponents {
	enum DustGenericTypes implements DustEntityKey {
		Stream("Generic;3"),
		ContextAware("Generic;10"),
		Connected("Generic;4"),
		Callback("Generic;15"),
		Reference("Generic;13"),
		Identified("Generic;1"),
		Tag("Generic;5"),
				
		;
        
        private DustGenericTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGenericAtts implements DustEntityKey {
		StreamWriter("Generic;14"),
		StreamFileAccess("Generic;7"),
		IdentifiedIdLocal("Generic;0"),
		StreamFileName("Generic;6"),
				
		;
        
        private DustGenericAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGenericLinks implements DustEntityKey {
		ReferencePath("Generic;12"),
		CallbackMessage("Generic;16"),
		ConnectedRequires("Generic;2"),
		ContextAwareEntity("Generic;11"),
		ConnectedExtends("Generic;8"),
		ConnectedOwner("Generic;9"),
				
		;
        
        private DustGenericLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGenericValues implements DustEntityKey {
				
		;
        
        private DustGenericValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGenericServices implements DustEntityKey {
				
		;
        
        private DustGenericServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGenericMessages implements DustEntityKey {
				
		;
        
        private DustGenericMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
