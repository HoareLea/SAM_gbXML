﻿using gbXMLSerializer;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SAM.Analytical.gbXML;
using SAM.Analytical.Grasshopper;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;
using System.Collections.Generic;

namespace SAM.Geometry.Grasshopper
{
    public class SAMAnalyticalTogbXML : GH_SAMComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("0928ad5f-eae6-4bb5-b098-b40a627e4e75");

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.SAM_gbXML_in;

        /// <summary>
        /// Initializes a new instance of the SAM_point3D class.
        /// </summary>
        public SAMAnalyticalTogbXML()
          : base("SAMAnalytical.TogbXML", "SAMAnalytical.TogbXML",
              "SAMAnalytical To gbXML",
              "SAM", "gbXML")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager inputParamManager)
        {
            inputParamManager.AddParameter(new GooAnalyticalModelParam(), "_analyticalModel", "_analyticalModel", "AnalyticalModel", GH_ParamAccess.item);
            inputParamManager.AddTextParameter("_path", "_path", "File Path", GH_ParamAccess.item);
            inputParamManager.AddNumberParameter("_tolerance_", "_tolerance_", "Tolerance", GH_ParamAccess.item, 0.00001);
            inputParamManager.AddBooleanParameter("_run_", "_run_", "Run", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager outputParamManager)
        {
            outputParamManager.AddGenericParameter("String", "String", "String", GH_ParamAccess.item);
            outputParamManager.AddBooleanParameter("Successful", "Successful", "Correctly imported?", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="dataAccess">
        /// The DA object is used to retrieve from inputs and store in outputs.
        /// </param>
        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {
            bool run = false;
            if (!dataAccess.GetData(3, ref run))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }
            if (!run)
                return;

            Analytical.AnalyticalModel analyticalModel = null;
            if (!dataAccess.GetData(0, ref analyticalModel) || analyticalModel == null)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }

            string path = null;
            if (!dataAccess.GetData(1, ref path) || string.IsNullOrWhiteSpace(path))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }

            double tolerance = 0.00001;
            if (!dataAccess.GetData(2, ref tolerance) || double.IsNaN(tolerance))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }

            gbXML gbXML = analyticalModel.TogbXML(Core.Tolerance.MacroDistance, tolerance);
            if(gbXML == null)
            {
                dataAccess.SetData(0, null);
                dataAccess.SetData(1, false);
            }

            bool result = Core.gbXML.Create.gbXML(gbXML, path);

            dataAccess.SetData(0, Core.gbXML.Convert.ToString(gbXML));
            dataAccess.SetData(1, result);
        }
    }
}