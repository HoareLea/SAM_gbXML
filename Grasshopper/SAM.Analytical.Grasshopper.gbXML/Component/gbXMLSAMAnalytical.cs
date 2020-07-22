using gbXMLSerializer;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SAM.Analytical.gbXML;
using SAM.Analytical.Grasshopper;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;
using System.Collections.Generic;
using System.IO;

namespace SAM.Geometry.Grasshopper
{
    public class gbXMLSAMAnalytical : GH_SAMComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("3ea384d3-850b-4606-b200-758ad4f8c4b2");

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.SAM_gbXML;

        /// <summary>
        /// Initializes a new instance of the SAM_point3D class.
        /// </summary>
        public gbXMLSAMAnalytical()
          : base("gbXML.SAMAnalytical", "gbXML.SAMAnalytical",
              "gbXML To SAM Analytical",
              "SAM", "gbXML")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager inputParamManager)
        {
            inputParamManager.AddTextParameter("_path", "_path", "File Path", GH_ParamAccess.item);
            inputParamManager.AddNumberParameter("_tolerance_", "_tolerance_", "Tolerance", GH_ParamAccess.item, 0.00001);
            inputParamManager.AddBooleanParameter("_run_", "_run_", "Run", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager outputParamManager)
        {
            outputParamManager.AddParameter(new GooAnalyticalModelParam(), "AnalyticalModel", "AnalyticalModel", "AnalyticalModel", GH_ParamAccess.item);
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
            if (!dataAccess.GetData(2, ref run))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }
            if (!run)
                return;

            string path = null;
            if (!dataAccess.GetData(0, ref path) || string.IsNullOrWhiteSpace(path))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }

            double tolerance = 0.00001;
            if (!dataAccess.GetData(1, ref tolerance) || double.IsNaN(tolerance))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                dataAccess.SetData(1, false);
                return;
            }

            throw new NotImplementedException();



            //dataAccess.SetDataList(0, result);
            dataAccess.SetData(1, true);

            //AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Cannot split segments");
        }
    }
}