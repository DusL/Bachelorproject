using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLESMonitor;

namespace CLESMonitor.Model
{
    public class FuzzyCalculate
    {
        public FuzzyCalculate()
        {
           
        }

        public double lowGSRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double rightBoudary = mean - 1.5 * SD;

            // If the normalised value falls within the boundaries, calculate the value
            if (normalised <= rightBoudary)
            {
                value = (rightBoudary - normalised) / rightBoudary;
            }

            return value;
        }

        public double midLowGSRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double leftBoundary = mean - 2 * SD;
            double rightBoundary = mean;

            // Since the midLow fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalised && normalised <= (mean - SD))
            {
                //(GSRMean - GSRStandardDeviation) - leftBoundary = -3 * GSRStandardDeviation
                value = (normalised - leftBoundary) / ((mean - SD) - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalised >= (mean - SD) && rightBoundary >= normalised)
            {
                // (rightBoundary - (GSRMean - GSRStandardDeviation) = GSRMean - GSRMean - GSRStandardDeviation = GSRStandardDeviation
                value = (rightBoundary - normalised) / (rightBoundary - (mean - SD));
            }

            return value;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRMidHigh
        /// </summary>
        /// <returns></returns>
        public double midHighGSRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double leftBoundary = mean - SD;
            double rightBoundary = mean + SD;

            // Since the midHigh fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalised && normalised <= mean)
            {
                value = (normalised - leftBoundary) / (mean - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalised >= mean && rightBoundary >= normalised)
            {
                value = (rightBoundary - normalised) / (rightBoundary - mean);
            }

            return value;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRHigh
        /// </summary>
        /// <returns></returns>
        public double highGSRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double leftBoundary = mean;
            /* If the normalised value is not greater than the mean +1SD but within the boundaries of "high"
               calculate the value*/
            if (normalised >= leftBoundary && normalised <= (mean + SD))
            {
                value = (normalised - leftBoundary) / ((mean + SD) - leftBoundary);
            }
            // If the value is greater the mean + 1SD, the value high = 1
            else if (normalised >= (mean + SD))
            {
                value = 1;
            }

            return value;
        }

        /// <summary>
        /// Calculates the fuzzy value for the 'low' level of HR
        /// </summary>
        /// <returns>The truth value of 'low' (double)</returns>
        public double lowHRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double rightBoudary = mean - SD;

            // If the normalised value falls within the boundaries, calculate the value
            if (normalised <= rightBoudary)
            {
                value = (rightBoudary - normalised) / rightBoudary;
            }

            return value;
        }

        /// <summary>
        /// Calculates the fuzzy value for the 'mid' level of HR
        /// </summary>
        /// <returns>The truth value of 'mid' (double)</returns>
        public double midHRValue(double mean, double SD, double normalised)
        {
            double value = 0;
            double leftBoundary = mean - 2 * SD;
            double rightBoundary = mean + 2 * SD;

            // Since the midLow fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalised && normalised <= (mean - leftBoundary))
            {
                value = (normalised - leftBoundary) / (mean - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalised >= mean && rightBoundary >= normalised)
            {
                value = (rightBoundary - normalised) / (rightBoundary - mean);
            }

            return value;
        }

        /// <summary>
        /// Calculates the fuzzy value for the 'high' level of HR
        /// </summary>
        /// <returns>The truth value of 'high' (double)</returns>
        public double highHRValue(double mean, double normalised)
        {
            double value = 0;
            double leftBoundary = mean;

            // If the normalised value falls withing teh boundaries of high
            if (normalised >= leftBoundary)
            {
                //The maximum value HRNormalised can get = 100;
                value = (normalised - leftBoundary) / (100 - leftBoundary);
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HRValue"></param>
        /// <returns>The normalised hartrate (double)</returns>
        public double normalisedHR(double HRValue, double HRMin, double HRMax)
        {
            return ((HRValue - HRMin) / (HRMax - HRMin)) * 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GSRValue"></param>
        /// <returns>The normalised skin conductance (double)</returns>
        public double normalisedGSR(double GSRValue, double GSRMin, double GSRMax)
        {
            return ((GSRValue - GSRMin) / (GSRMax - GSRMin)) * 100;
        }
    }
}
