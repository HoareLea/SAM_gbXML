// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020–2026 Michal Dengusiak & Jakub Ziolkowski and contributors

using Grasshopper.Kernel;
using SAM.Analytical.Grasshopper;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;
using System.Collections.Generic;

namespace SAM.Geometry.Grasshopper
{
    /// <summary>
    /// A Grasshopper component that converts a gbXML file into a SAM Analytical Model.
    /// </summary>
    public class FromgbXML : GH_SAMVariableOutputParameterComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("3ea384d3-850b-4606-b200-758ad4f8c4b2");

        /// <summary>
        /// The latest version of this component
        /// </summary>
        public override string LatestComponentVersion => "1.0.1";

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
        protected override GH_SAMParam[] Inputs
        {
            get
            {
                List<GH_SAMParam> result = new List<GH_SAMParam>();
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_String() { Name = "_path", NickName = "_path", Description = "File Path", Access = GH_ParamAccess.item }, ParamVisibility.Binding));

                global::Grasshopper.Kernel.Parameters.Param_Number param_Number = new global::Grasshopper.Kernel.Parameters.Param_Number() { Name = "_tolerance_", NickName = "_tolerance_", Description = "Tolerance", Access = GH_ParamAccess.item };
                param_Number.SetPersistentData(0.00001);
                result.Add(new GH_SAMParam(param_Number, ParamVisibility.Binding));

                global::Grasshopper.Kernel.Parameters.Param_Boolean param_Boolean = new global::Grasshopper.Kernel.Parameters.Param_Boolean() { Name = "_run_", NickName = "_run_", Description = "Run", Access = GH_ParamAccess.item };
                param_Boolean.SetPersistentData(false);
                result.Add(new GH_SAMParam(param_Boolean, ParamVisibility.Binding));

                return result.ToArray();
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override GH_SAMParam[] Outputs
        {
            get
            {
                List<GH_SAMParam> result = new List<GH_SAMParam>();
                result.Add(new GH_SAMParam(new GooAnalyticalModelParam() { Name = "AnalyticalModel", NickName = "AnalyticalModel", Description = "AnalyticalModel", Access = GH_ParamAccess.item }, ParamVisibility.Binding));
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_Boolean() { Name = "Successful", NickName = "Successful", Description = "Correctly imported?", Access = GH_ParamAccess.item }, ParamVisibility.Binding));
                return result.ToArray();
            }
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="dataAccess">
        /// The DA object is used to retrieve from inputs and store in outputs.
        /// </param>
        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {
            int index;

            // Set the default value of the second output parameter to false.
            index = Params.IndexOfOutputParam("Successful");
            if (index != -1)
            {
                dataAccess.SetData(index, false);
            }

            bool run = false;
            // Get the value of the second input parameter, and display an error message if it's invalid.
            index = Params.IndexOfInputParam("_run_");
            if (index == -1 || !dataAccess.GetData(index, ref run))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                return;
            }

            // If the value of the second input parameter is false, return without executing the rest of the code.
            if (!run)
                return;

            string path = null;
            // Get the value of the first input parameter, and display an error message if it's invalid.
            index = Params.IndexOfInputParam("_path");
            if (index == -1 || !dataAccess.GetData(index, ref path) || string.IsNullOrWhiteSpace(path))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data");
                return;
            }

            double tolerance = 0.00001;
            // Get the value of the third input parameter, and display an error message if it's invalid.
            index = Params.IndexOfInputParam("_tolerance_");
            if (index == -1 || !dataAccess.GetData(index, ref tolerance) || double.IsNaN(tolerance))
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
            index = Params.IndexOfOutputParam("AnalyticalModel");
            if (index != -1)
            {
                dataAccess.SetData(index, new GooAnalyticalModel(analyticalModel));
            }

            // Set the second output parameter to true.
            index = Params.IndexOfOutputParam("Successful");
            if (index != -1)
            {
                dataAccess.SetData(index, true);
            }

            // This line is commented out, but it's a good practice to add comments for debugging purposes.
            //AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Cannot split segments");
        }
    }
}
