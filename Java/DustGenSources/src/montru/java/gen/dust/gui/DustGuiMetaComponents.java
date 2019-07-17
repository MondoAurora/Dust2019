package montru.java.gen.dust.gui;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustGuiMetaComponents extends DustComponents {
	enum DustGuiMetaTypes implements DustEntityKey {
		GuiWidgetLabel("GuiMeta;6"),
		GuiContainerContextAware("GuiMeta;12"),
		GuiContainerMapped("GuiMeta;4"),
		GuiWidgetAnchor("GuiMeta;8"),
		GuiPanelEntity("GuiMeta;0"),
		GuiWidgetCommand("GuiMeta;7"),
		GuiWidgetActive("GuiMeta;10"),
		GuiWidgetAttEditor("GuiMeta;5"),
		GuiWidgetAttToggle("GuiMeta;11"),
		GuiComponent("GuiMeta;1"),
		GuiComponentContextAware("GuiMeta;3"),
		GuiComponentIdentified("GuiMeta;2"),
		GuiComponentContextRef("GuiMeta;9"),
				
		;
        
        private DustGuiMetaTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGuiMetaAtts implements DustEntityKey {
				
		;
        
        private DustGuiMetaAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGuiMetaLinks implements DustEntityKey {
				
		;
        
        private DustGuiMetaLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGuiMetaValues implements DustEntityKey {
				
		;
        
        private DustGuiMetaValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGuiMetaServices implements DustEntityKey {
				
		;
        
        private DustGuiMetaServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGuiMetaMessages implements DustEntityKey {
				
		;
        
        private DustGuiMetaMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
