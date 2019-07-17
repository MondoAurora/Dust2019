package montru.java.gen.dust.kernel;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustMetaComponents extends DustComponents {
	enum DustMetaTypes implements DustEntityKey {
		Constant("Meta;10"),
		LinkDef("Meta;7"),
		Service("Meta;5"),
		Command("Meta;16"),
		Type("Meta;3"),
		Meta("Meta;28"),
		AttDef("Meta;0"),
		MeasurementUnit("Meta;34"),
				
		;
        
        private DustMetaTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustMetaAtts implements DustEntityKey {
				
		;
        
        private DustMetaAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustMetaLinks implements DustEntityKey {
		LinkDefReverse("Meta;20"),
		LinkDefItemTypeAdditional("Meta;33"),
		AttDefParent("Meta;1"),
		LinkDefType("Meta;8"),
		AttDefMeasurementUnit("Meta;35"),
		LinkDefParent("Meta;6"),
		AttDefType("Meta;19"),
		MetaAccessControl("Meta;29"),
		TypeLinkedServices("Meta;21"),
		LinkDefItemTypePrimary("Meta;32"),
		TypeLinkDefs("Meta;4"),
		CommandRetValType("Meta;30"),
		TypeAttDefs("Meta;2"),
				
		;
        
        private DustMetaLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustMetaValues implements DustEntityKey {
		LinkDefArray("Meta;11"),
		LinkDefFactory("Meta;36"),
		LinkDefSet("Meta;9"),
		LinkDefMap("Meta;17"),
		LinkDefSingle("Meta;12"),
		AttDefRaw("Meta;31"),
		AttDefBool("Meta;14"),
		AttDefIdentifier("Meta;15"),
		AttDefDouble("Meta;18"),
		AttDefLong("Meta;13"),
				
		;
        
        private DustMetaValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustMetaServices implements DustEntityKey {
				
		;
        
        private DustMetaServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustMetaMessages implements DustEntityKey {
				
		;
        
        private DustMetaMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
