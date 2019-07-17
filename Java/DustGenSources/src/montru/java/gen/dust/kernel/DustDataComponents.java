package montru.java.gen.dust.kernel;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustDataComponents extends DustComponents {
	enum DustDataTypes implements DustEntityKey {
		Variant("Data;13"),
		Entity("Data;2"),
		Message("Data;5"),
				
		;
        
        private DustDataTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustDataAtts implements DustEntityKey {
		MessageReturn("Data;10"),
		VariantValue("Data;15"),
		EntityBinaries("Data;6"),
				
		;
        
        private DustDataAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustDataLinks implements DustEntityKey {
		MessageTarget("Data;12"),
		EntityTags("Data;3"),
		VariantValueType("Data;14"),
		EntityModels("Data;1"),
		MessageCommand("Data;8"),
		EntityPrimaryType("Data;0"),
		EntityAccessControl("Data;9"),
		EntityServices("Data;7"),
		MessageSource("Data;11"),
				
		;
        
        private DustDataLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustDataValues implements DustEntityKey {
				
		;
        
        private DustDataValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustDataServices implements DustEntityKey {
				
		;
        
        private DustDataServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustDataMessages implements DustEntityKey {
				
		;
        
        private DustDataMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
