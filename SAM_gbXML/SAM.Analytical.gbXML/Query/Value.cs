using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This class contains extension methods for getting values from gbXML serializer classes.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Gets the R value from gbXML serializer class.
        /// </summary>
        /// <param name="rValue">The RValue instance to extract value from.</param>
        /// <returns>The R value.</returns>
        public static double Value(this RValue rValue)
        {
            // If RValue is null, return NaN
            if (rValue == null)
            {
                return double.NaN;
            }

            // Check the unit of the RValue and return the value accordingly
            switch (rValue.unit)
            {
                case resistanceUnitEnum.SquareMeterKPerW:
                    return rValue.value;
            }

            // Throw an exception if the unit is not implemented
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the specific heat value from gbXML serializer class.
        /// </summary>
        /// <param name="specificHeat">The SpecificHeat instance to extract value from.</param>
        /// <returns>The specific heat value.</returns>
        public static double Value(this SpecificHeat specificHeat)
        {
            // If SpecificHeat is null, return NaN
            if (specificHeat == null)
            {
                return double.NaN;
            }

            // Check the unit of the SpecificHeat and return the value accordingly
            switch (specificHeat.unit)
            {
                case specificHeatEnum.JPerKgK:
                    return specificHeat.value;
            }

            // Throw an exception if the unit is not implemented
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the density value from gbXML serializer class.
        /// </summary>
        /// <param name="density">The Density instance to extract value from.</param>
        /// <returns>The density value.</returns>
        public static double Value(this Density density)
        {
            if (density == null)
            {
                return double.NaN;
            }

            // Check the unit of the Density and return the value accordingly
            switch (density.unit)
            {
                case densityUnitEnum.KgPerCubicM:
                    return density.value;
            }

            // Throw an exception if the unit is not implemented
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the conductivity value from gbXML serializer class.
        /// </summary>
        /// <param name="conductivity">The Conductivity instance to extract value from.</param>
        /// <returns>The conductivity value.</returns>
        public static double Value(this Conductivity conductivity)
        {
            // Check the unit of the Conductivity and return the value accordingly
            switch (conductivity.unit)
            {
                case conductivityUnitEnum.WPerMeterK:
                    return conductivity.value;
            }

            // Throw an exception if the unit is not implemented
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the thickness value from gbXML serializer class.
        /// </summary>
        /// <param name="thickness">The Thickness instance to extract value from.</param>
        /// <returns>The thickness value.</returns>
        public static double Value(this Thickness thickness)
        {
            if (thickness == null)
            {
                return double.NaN;
            }

            switch (thickness.unit)
            {
                case lengthUnitEnum.Meters:
                    return thickness.value;
            }

            throw new System.NotImplementedException();
        }
    }
}
