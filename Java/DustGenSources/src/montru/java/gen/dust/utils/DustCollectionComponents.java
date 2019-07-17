package montru.java.gen.dust.utils;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustCollectionComponents extends DustComponents {
	enum DustCollectionTypes implements DustEntityKey {
		MapEntry("Collection;2"),
		Sequence("Collection;0"),
				
		;
        
        private DustCollectionTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCollectionAtts implements DustEntityKey {
				
		;
        
        private DustCollectionAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCollectionLinks implements DustEntityKey {
		SequenceMembers("Collection;1"),
		MapEntryKey("Collection;3"),
				
		;
        
        private DustCollectionLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCollectionValues implements DustEntityKey {
				
		;
        
        private DustCollectionValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCollectionServices implements DustEntityKey {
				
		;
        
        private DustCollectionServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustCollectionMessages implements DustEntityKey {
				
		;
        
        private DustCollectionMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
