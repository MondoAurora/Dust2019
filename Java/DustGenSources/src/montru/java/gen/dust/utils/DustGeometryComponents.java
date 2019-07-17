package montru.java.gen.dust.utils;

/*
 * Created by Montru2018
 */

import montru.java.dust.DustComponents;
import montru.java.dust.DustEntityResolver;

public interface DustGeometryComponents extends DustComponents {
	enum DustGeometryTypes implements DustEntityKey {
		ShapeBox("Geometry;4"),
		GeometricDimension("Geometry;25"),
		RenderTarget("Geometry;31"),
		GeometricInclusion("Geometry;20"),
		GeometricData("Geometry;0"),
		ShapeArc("Geometry;23"),
		ShapeRef("Geometry;38"),
		GeometricDataRole("Geometry;16"),
		ShapeComposite("Geometry;7"),
		ShapePath("Geometry;8"),
		RenderSource("Geometry;37"),
				
		;
        
        private DustGeometryTypes(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGeometryAtts implements DustEntityKey {
		ShapePathClosed("Geometry;11"),
				
		;
        
        private DustGeometryAtts(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGeometryLinks implements DustEntityKey {
		GeometricInclusionTarget("Geometry;19"),
		ShapeArcBegin("Geometry;5"),
		GeometricDataMeasurements("Geometry;22"),
		ShapeBoxSize("Geometry;6"),
		GeometricInclusionParameters("Geometry;21"),
		GeometricDataType("Geometry;15"),
		ShapeArcEnd("Geometry;24"),
				
		;
        
        private DustGeometryLinks(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGeometryValues implements DustEntityKey {
		GeometricDataRoleLocate("Geometry;17"),
		GeometricDimensionCartesianZ("Geometry;26"),
		GeometricDataRoleRotate("Geometry;18"),
		GeometricDimensionCartesianY("Geometry;27"),
		GeometricDimensionCartesianX("Geometry;28"),
		GeometricDataRoleScale("Geometry;14"),
				
		;
        
        private DustGeometryValues(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGeometryServices implements DustEntityKey {
		RenderSourceComposite("Geometry;41"),
		RenderTarget("Geometry;29"),
		RenderSourceSimple("Geometry;40"),
				
		;
        
        private DustGeometryServices(String id) {
            DustEntityResolver.register(id, this);
        }
	}

	enum DustGeometryMessages implements DustEntityKey {
				
		;
        
        private DustGeometryMessages(String id) {
            DustEntityResolver.register(id, this);
        }
	}


}
