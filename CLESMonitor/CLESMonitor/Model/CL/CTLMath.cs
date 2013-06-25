using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.CL
{
    public static class CTLMath
    {
        /// <summary>
        /// Sets the array of domains of the new tasks to be the combined domains of both task1 and task2
        /// Domains that occur in both tasks are only added once.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>An array of informationDomains</returns>
        public static List<int> multitaskDomain(CTLTask task1, CTLTask task2)
        {
            List<int> newDomain = null;
            if (task1 != null && task2 != null)
            {
                if (task1.informationDomains != null && task2.informationDomains != null)
                {
                    newDomain = task1.informationDomains;
                    List<int> tempDomain = task2.informationDomains;
                    for (int i = 0; i <= tempDomain.Count - 1; i++)
                    {
                        if (!newDomain.Contains(tempDomain[i]))
                        {
                            newDomain.Add(tempDomain[i]);
                        }
                    }
                }
            }
            return newDomain;
        }

        /// <summary>
        /// Sets the MO-value of a multitaks to be the sum of the MO-values of the original tasks when this sum is greater than 1.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>A double representing the MO value of the new multitask (value between 1 and 2)</returns>
        public static double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = 0.0;
            double MO2 = 0.0;
            double returnMO = 0.0;

            if (task1 != null && task2 != null)
            {
                MO1 = task1.moValue;
                MO2 = task2.moValue;
                returnMO = Math.Max(MO1 + MO2, 1);
            }
            return returnMO;
        }

        /// <summary>
        /// Sets the Lip-value of a multitask: the largest of the two lip-values of the original tasks
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>The Lip value for a new task</returns>
        public static int multitaskLip(CTLTask task1, CTLTask task2)
        {
            int lip1 = 0;
            int lip2 = 0;
            int returnLip = 0;

            if (task1 != null && task2 != null)
            {
                lip1 = task1.lipValue;
                lip2 = task2.lipValue;
                returnLip = Math.Max(lip1, lip2);
            }
            return returnLip;
        }

        /// <summary>
        /// Implements the overall Level of Information Processing (LIP) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>Average Lip-value (not rounded). 
        /// It can attain values between 1 and 3 (only without overlapping tasks!).</returns>
        public static double calculateOverallLip(List<CTLTask> tasks, TimeSpan lengthTimeframe)
        {
            double lipValue = 0;

            for (int i = 0; i < tasks.Count; i++)
            {
                lipValue += tasks[i].lipValue * tasks[i].duration.TotalSeconds;
            }
            lipValue = lipValue / lengthTimeframe.TotalSeconds;

            return lipValue;
        }

        /// <summary>
        /// Implements the overall Mental occupancy (MO) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>The normalized MO-value across 1 time frame. 
        /// It can attain values between 0 and 1 (only without overlapping tasks!).</returns>
        public static double calculateOverallMo(List<CTLTask> tasks, TimeSpan lengthTimeframe)
        {
            double moValue = 0;

            for (int i = 0; i < tasks.Count; i++)
            {
                moValue += tasks[i].moValue * tasks[i].duration.TotalSeconds;
            }
            moValue = moValue / lengthTimeframe.TotalSeconds;

            return moValue;
        }

        /// <summary>
        /// Implements the overall Task Set Switching (TSS) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">The list of tasks to use</param>
        /// <returns>The calculated TSS-value. 
        /// It can attain values between 0 and (tasks.Count-1) (only without overlapping tasks!).</returns>
        public static double calculateTSS(List<CTLTask> tasks)
        {
            double tssValue = 0;

            for (int i = 0; i < tasks.Count - 1; i++)
            {
                int unionCount = tasks[i].informationDomains.Union(tasks[i + 1].informationDomains).Count();
                int intersectionCount = tasks[i].informationDomains.Intersect(tasks[i + 1].informationDomains).Count();

                tssValue += (unionCount - intersectionCount) / unionCount;
            }

            return tssValue;
        }

        /// <summary>
        /// Implements the mental workload (MWL) formula as defined in the scientific literature
        /// </summary>
        /// <param name="lipValue">The level of information processing</param>
        /// <param name="moValue">The mental occupancy</param>
        /// <param name="tssValue">The task set switches</param>
        /// <returns>The calculated MWL-value</returns>
        public static double calculateMentalWorkLoad(double lipValue, double moValue, double tssValue, List<CTLTask> tasksInCalculationFrame)
        {
            //TODO: Moet nog getest??

            double mwlValue = 0;
            double normalizedTssValue = 0;

            // Define the bounds as tuples (lower bound, upper bound)
            Tuple<int, int> lipBounds = Tuple.Create(0, 3);
            Tuple<int, int> moBounds = Tuple.Create(0, 1);
            Tuple<int, int> tssBounds = Tuple.Create(0, Math.Max(tasksInCalculationFrame.Count - 1, 0));

            // Project all metrics on the [0, 1] interval
            double normalizedLipValue = (lipValue - lipBounds.Item1) / lipBounds.Item2;
            double normalizedMoValue = (moValue - moBounds.Item1) / moBounds.Item2;
            // Avoid NaN values!
            if (tssBounds.Item2 != 0)
            {
                normalizedTssValue = (tssValue - tssBounds.Item1) / tssBounds.Item2;
            }

            Vector diagonalVector = new Vector(1.0, 1.0, 1.0);
            Vector mwlVector = new Vector(normalizedLipValue, normalizedMoValue, normalizedTssValue);

            // The distance to the origin (the length of the input vector)
            // For now we use this as MWL value, since the formula appeared to be unuseable
            double distanceToOrigin = mwlVector.length();

            Vector mwlProjDiagonal = mwlVector.orthogonalProjection(diagonalVector);

            Vector zVector = mwlVector - mwlProjDiagonal;

            double distanceToDiagonal = zVector.length();
            mwlValue = distanceToOrigin - (distanceToDiagonal/2);

            return mwlValue;
        }
    }
}
