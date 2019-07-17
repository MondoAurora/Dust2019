package montru.java.gen.dust.utils;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustLayoutComponents extends DustComponents {
	enum DustLayoutTypes implements DustEntityKey {
		LayoutClient("Layout;22"),
		LayoutManager("Layout;23"),
		AlignmentValue("Layout;7"),
		LayoutCartesianTabular("Layout;26"),
		TabCellInfo("Layout;19"),
		AlignmentMode("Layout;4"),
		PlacementAxisInfo("Layout;13"),
		TabAxisInfo("Layout;16"),
				
		;
        
        private DustLayoutTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustLayoutAtts implements DustEntityKey {
		cellICount("Layout;15"),
		gapBegin("Layout;12"),
		weight("Layout;11"),
		cellIndex("Layout;17"),
		size("Layout;10"),
		gapEnd("Layout;14"),
				
		;
        
        private DustLayoutAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustLayoutLinks implements DustEntityKey {
		AlignmentValue("Layout;9"),
		LegendAxisInfo("Layout;25"),
		AxisInfoSequence("Layout;24"),
		Client("Layout;20"),
		LayoutManager("Layout;21"),
		AxisInfo("Layout;18"),
		AlignmentMode("Layout;8"),
				
		;
        
        private DustLayoutLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustLayoutValues implements DustEntityKey {
				
		;
        
        private DustLayoutValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustLayoutServices implements DustEntityKey {
				
		;
        
        private DustLayoutServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustLayoutMessages implements DustEntityKey {
				
		;
        
        private DustLayoutMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
