using gbXMLSerializer;
using Grasshopper.Kernel;
using SAM.Analytical;
using SAM.Analytical.gbXML;
using SAM.Analytical.Grasshopper;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;

namespace SAM.Geometry.Grasshopper
{
    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public class TogbXML : GH_SAMComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("0928ad5f-eae6-4bb5-b098-b40a627e4e75");

        /// <summary>
        /// The latest version of this component
        /// </summary>
        public override string LatestComponentVersion => "1.0.1";

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.SAM_gbXML3;

        /// <summary>
        /// Initializes a new instance of the SAM_point3D class.
        /// </summary>
        public TogbXML()
          : base("TogbXML", "TogbXML",
              "SAMAnalytical Model To gbXML",
              "SAM", "gbXML")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager inputParamManager)
        {
            inputParamManager.AddParameter(new global::Grasshopper.Kernel.Parameters.Param_GenericObject(), "_analyticalModel", "_analyticalModel", "SAM Analytical Object", GH_ParamAccess.item);
            inputParamManager.AddTextParameter("_path", "_path", "File Path with extension .xml", GH_ParamAccess.item);
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
            dataAccess.SetData(1, false); // Set output data parameter 1 to false by default

            bool run = false;
            if (!dataAccess.GetData(3, ref run)) // Attempt to retrieve input data parameter 3 and store it in the 'run' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails
                return;
            }
            if (!run) // If the 'run' variable is false, skip the rest of the code and return
                return;

            Core.SAMObject sAMObject = null;
            if (!dataAccess.GetData(0, ref sAMObject) || sAMObject == null) // Attempt to retrieve input data parameter 0 and store it in the 'sAMObject' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null
                return;
            }

            string path = null;
            if (!dataAccess.GetData(1, ref path) || string.IsNullOrWhiteSpace(path)) // Attempt to retrieve input data parameter 1 and store it in the 'path' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null or whitespace
                return;
            }

            double tolerance = 0.00001;
            if (!dataAccess.GetData(2, ref tolerance) || double.IsNaN(tolerance)) // Attempt to retrieve input data parameter 2 and store it in the 'tolerance' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null or NaN
                return;
            }

            gbXML gbXML = null; // Declare a null gbXML object
            if (sAMObject is AnalyticalModel) // If the retrieved data is an AnalyticalModel object
            {
                gbXML = ((AnalyticalModel)sAMObject).TogbXML(Core.Tolerance.MacroDistance, tolerance); // Convert the AnalyticalModel object to gbXML format
            }
            else if (sAMObject is BuildingModel) // If the retrieved data is a BuildingModel object
            {
                gbXML = ((BuildingModel)sAMObject).TogbXML(Core.Tolerance.MacroDistance, tolerance); // Convert the BuildingModel object to gbXML format
            }

            if (gbXML == null) // If gbXML conversion failed, skip the rest of the code and return
                return;

            bool result = Core.gbXML.Create.gbXML(gbXML, path); // Create a gbXML file at the specified path using the gbXML object

            dataAccess.SetData(0, Core.gbXML.Convert.ToString(gbXML)); // Set output data parameter 0 to a string representation of the gbXML object
            dataAccess.SetData(1, result); // Set output data parameter 1 to the result of the gbXML creation process
        }

    }

}