using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static double Value(this RValue rValue)
        {
            if(rValue == null)
            {
                return double.NaN;
            }

            switch(rValue.unit)
            {
                case resistanceUnitEnum.SquareMeterKPerW:
                    return rValue.value;
            }

            throw new System.NotImplementedException();
        }

        public static double Value(this SpecificHeat specificHeat)
        {
            if (specificHeat == null)
            {
                return double.NaN;
            }

            switch (specificHeat.unit)
            {
                case specificHeatEnum.JPerKgK:
                    return specificHeat.value;
            }

            throw new System.NotImplementedException();
        }

        public static double Value(this Density density)
        {
            if (density == null)
            {
                return double.NaN;
            }

            switch (density.unit)
            {
                case densityUnitEnum.KgPerCubicM:
                    return density.value;
            }

            throw new System.NotImplementedException();
        }

        public static double Value(this Conductivity conductivity)
        {
            if (conductivity == null)
            {
                return double.NaN;
            }

            switch (conductivity.unit)
            {
                case conductivityUnitEnum.WPerMeterK:
                    return conductivity.value;
            }

            throw new System.NotImplementedException();
        }

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