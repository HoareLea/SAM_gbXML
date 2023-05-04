using Grasshopper.Kernel;
using SAM.Analytical.Grasshopper;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;

namespace SAM.Geometry.Grasshopper
{
    /// <summary>
    /// A Grasshopper component that converts a gbXML file into a SAM Analytical Model.
    /// </summary>
    public class FromgbXML : GH_SAMComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("3ea384d3-850b-4606-b200-758ad4f8c4b2");

        /// <summary>
        /// The latest version of this component
        /// </summary>
        public override string LatestComponentVersion => "1.0.0";

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.SAM_gbXML_out3;

        /// <summary>
        /// Initializes a new instance of the SAM_point3D class.
        /// </summary>
        public FromgbXML()
          : base("FromgbXML", "FromgbXML",
              "From gbXML file To SAM Analytical Model",
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
            // Set the default value of the second output parameter to false.
            dataAccess.SetData(1, false);

            bool run = false;
            // Get the value of the second input parameter, and display an error message if it's invalid.
            if (!dataAccess.GetData(2, ref run))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                return;
            }

            // If the value of the second input parameter is false, return without executing the rest of the code.
            if (!run)
                return;

            string path = null;
            // Get the value of the first input parameter, and display an error message if it's invalid.
            if (!dataAccess.GetData(0, ref path) || string.IsNullOrWhiteSpace(path))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                return;
            }

            double tolerance = 0.00001;
            // Get the value of the third input parameter, and display an error message if it's invalid.
            if (!dataAccess.GetData(1, ref tolerance) || double.IsNaN(tolerance))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                return;
            }

            // Create an AnalyticalModel object using the gbXML.Create.AnalyticalModel method.
            // The method takes a file path, a MacroDistance object, and a tolerance value as input parameters.
            Analytical.AnalyticalModel analyticalModel = Analytical.gbXML.Create.AnalyticalModel(path, Core.Tolerance.MacroDistance, tolerance);

            // Get the adjacency cluster from the AnalyticalModel object, if it exists.
            Analytical.AdjacencyCluster adjacencyCluster = analyticalModel?.AdjacencyCluster;
            if (adjacencyCluster != null)
            {
                // Update the area and volume properties of the adjacency cluster.
                Analytical.Modify.UpdateAreaAndVolume(adjacencyCluster, false);
                // Create a new AnalyticalModel object using the modified adjacency cluster.
                analyticalModel = new Analytical.AnalyticalModel(analyticalModel, adjacencyCluster);
            }

            // Set the first output parameter to a GooAnalyticalModel object that wraps the AnalyticalModel object.
            dataAccess.SetData(0, new GooAnalyticalModel(analyticalModel));
            // Set the second output parameter to true.
            dataAccess.SetData(1, true);

            // This line is commented out, but it's a good practice to add comments for debugging purposes.
            //AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Cannot split segments");
        }
    }
}