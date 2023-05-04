using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace SAM.Analytical.Grasshopper.gbXML
{
    /// <summary>
    /// This class defines the assembly information for the SAM.Analytical.Grasshopper.gbXML toolkit.
    /// </summary>
    public class AssemblyInfo : GH_AssemblyInfo
    {
        /// <summary>
        /// Gets the name of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override string Name
        {
            get
            {
                return "SAM";
            }
        }

        /// <summary>
        /// Gets the icon of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override Bitmap Icon
        {
            get
            {
                // Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.SAM_Small;
            }
        }

        /// <summary>
        /// Gets the assembly icon of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override Bitmap AssemblyIcon
        {
            get
            {
                // Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.SAM_Small;
            }
        }

        /// <summary>
        /// Gets the description of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override string Description
        {
            get
            {
                // Return a short string describing the purpose of this GHA library.
                return "SAM.Analytical.Grasshopper.gbXML Toolkit, please explore";
            }
        }

        /// <summary>
        /// Gets the ID of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override Guid Id
        {
            get
            {
                return new Guid("1f06ad12-36df-41cf-b0f5-b4351748dda7");
            }
        }

        /// <summary>
        /// Gets the author name of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override string AuthorName
        {
            get
            {
                // Return a string identifying you or your company.
                return "Michal Dengusiak & Jakub Ziolkowski at Hoare Lea";
            }
        }

        /// <summary>
        /// Gets the contact details of the author of the SAM.Analytical.Grasshopper.gbXML toolkit.
        /// </summary>
        public override string AuthorContact
        {
            get
            {
                // Return a string representing your preferred contact details.
                return "Michal Dengusiak -> michaldengusiak@hoarelea.com and Jakub Ziolkowski -> jakubziolkowski@hoarelea.com";
            }
        }
    }
}
