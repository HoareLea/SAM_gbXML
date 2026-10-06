// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020–2026 Michal Dengusiak & Jakub Ziolkowski and contributors

using Grasshopper.Kernel;
using SAM.Analytical;
using SAM.Analytical.gbXML;
using SAM.Analytical.Grasshopper.gbXML.Properties;
using SAM.Core.Grasshopper;
using System;
using System.Collections.Generic;

namespace SAM.Geometry.Grasshopper
{
    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public class TogbXML : GH_SAMVariableOutputParameterComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("0928ad5f-eae6-4bb5-b098-b40a627e4e75");

        /// <summary>
        /// The latest version of this component
        /// </summary>
        public override string LatestComponentVersion => "1.0.2";

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
        protected override GH_SAMParam[] Inputs
        {
            get
            {
                List<GH_SAMParam> result = new List<GH_SAMParam>();
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_GenericObject() { Name = "_analyticalModel", NickName = "_analyticalModel", Description = "SAM Analytical Object", Access = GH_ParamAccess.item }, ParamVisibility.Binding));
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_String() { Name = "_path", NickName = "_path", Description = "File Path with extension .xml", Access = GH_ParamAccess.item }, ParamVisibility.Binding));

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
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_GenericObject() { Name = "String", NickName = "String", Description = "String", Access = GH_ParamAccess.item }, ParamVisibility.Binding));
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

            index = Params.IndexOfOutputParam("Successful");
            if (index != -1)
            {
                dataAccess.SetData(index, false); // Set output data parameter 1 to false by default
            }

            bool run = false;
            index = Params.IndexOfInputParam("_run_");
            if (index == -1 || !dataAccess.GetData(index, ref run)) // Attempt to retrieve input data parameter 3 and store it in the 'run' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails
                return;
            }
            if (!run) // If the 'run' variable is false, skip the rest of the code and return
                return;

            Core.SAMObject sAMObject = null;
            index = Params.IndexOfInputParam("_analyticalModel");
            if (index == -1 || !dataAccess.GetData(index, ref sAMObject) || sAMObject == null) // Attempt to retrieve input data parameter 0 and store it in the 'sAMObject' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null
                return;
            }

            string path = null;
            index = Params.IndexOfInputParam("_path");
            if (index == -1 || !dataAccess.GetData(index, ref path) || string.IsNullOrWhiteSpace(path)) // Attempt to retrieve input data parameter 1 and store it in the 'path' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null or whitespace
                return;
            }

            double tolerance = 0.00001;
            index = Params.IndexOfInputParam("_tolerance_");
            if (index == -1 || !dataAccess.GetData(index, ref tolerance) || double.IsNaN(tolerance)) // Attempt to retrieve input data parameter 2 and store it in the 'tolerance' variable
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid data"); // Display error message if data retrieval fails or the retrieved data is null or NaN
                return;
            }

            gbXMLSerializer.gbXML gbXML = null; // Declare a null gbXML object
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

            index = Params.IndexOfOutputParam("String");
            if (index != -1)
            {
                dataAccess.SetData(index, Core.gbXML.Convert.ToString(gbXML)); // Set output data parameter 0 to a string representation of the gbXML object
            }

            index = Params.IndexOfOutputParam("Successful");
            if (index != -1)
            {
                dataAccess.SetData(index, result); // Set output data parameter 1 to the result of the gbXML creation process
            }
        }

    }

}
