using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models.DataExplorationModels;

namespace Capstone.Web
{
    public static class CFUSummaryDataCalculationForStorageStability
    {
        public static int FindMaxNumberOfRepetitionFromSummaryData(List<StorageStabilityExperimentData> summaryStorageStabilityData)
        {
            return Convert.ToInt32(summaryStorageStabilityData[summaryStorageStabilityData.Count - 1].Rep);
        }

        public static List<List<StorageStabilityExperimentData>> ChunkListGeneration(List<StorageStabilityExperimentData> summaryStorageStabilityData)
        {
            int subListLength = FindMaxNumberOfRepetitionFromSummaryData(summaryStorageStabilityData);
            List<List<StorageStabilityExperimentData>> chunkList = new List<List<StorageStabilityExperimentData>>();

            for (int i = 0; i < summaryStorageStabilityData.Count; i += subListLength)
            {
                List<StorageStabilityExperimentData> currentList = new List<StorageStabilityExperimentData>();
                for (int j = i; j < subListLength + i; j++)
                {
                    currentList.Add(summaryStorageStabilityData[j]);
                }
                currentList.Add(new StorageStabilityExperimentData());
                chunkList.Add(currentList);
            }
            return chunkList;
        }

        public static List<List<StorageStabilityExperimentData>> CalcualteAndAddAverageCFUValueToEachSubListAndReturnThatChunkList(List<List<StorageStabilityExperimentData>> chunkList)
        {
            int lastRowIndexValue = chunkList[0].Count - 1;

            List<bool> dil_zeroIsNumber = new List<bool>();
            List<bool> dil_oneIsNumber = new List<bool>();
            List<bool> dil_twoIsNumber = new List<bool>();
            List<bool> dil_threeIsNumber = new List<bool>();
            List<bool> dil_fourIsNumber = new List<bool>();
            List<bool> dil_fiveIsNumber = new List<bool>();
            List<bool> dil_sixIsNumber = new List<bool>();
            List<bool> dil_sevenIsNumber = new List<bool>();

            foreach (List<StorageStabilityExperimentData> subList in chunkList)
            {
                dil_zeroIsNumber.Clear();
                dil_oneIsNumber.Clear();
                dil_twoIsNumber.Clear();
                dil_threeIsNumber.Clear();
                dil_fourIsNumber.Clear();
                dil_fiveIsNumber.Clear();
                dil_sixIsNumber.Clear();
                dil_sevenIsNumber.Clear();

                for (int i = 0; i < subList.Count; i++)
                {
                    int n;
                    if (Int32.TryParse(subList[i].Dil_Zero, out n) && Convert.ToInt32(subList[i].Dil_Zero) != 0)
                    {
                        dil_zeroIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_One, out n) && Convert.ToInt32(subList[i].Dil_One) != 0)
                    {
                        dil_oneIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Two, out n) && Convert.ToInt32(subList[i].Dil_Two) != 0)
                    {
                        dil_twoIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Three, out n) && Convert.ToInt32(subList[i].Dil_Three) != 0)
                    {
                        dil_threeIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Four, out n) && Convert.ToInt32(subList[i].Dil_Four) != 0)
                    {
                        dil_fourIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Five, out n) && Convert.ToInt32(subList[i].Dil_Five) != 0)
                    {
                        dil_fiveIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Six, out n) && Convert.ToInt32(subList[i].Dil_Six) != 0)
                    {
                        dil_sixIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_Seven, out n) && Convert.ToInt32(subList[i].Dil_Seven) != 0)
                    {
                        dil_sevenIsNumber.Add(true);
                    }
                }
                if (!dil_zeroIsNumber.Contains(false) && dil_zeroIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Zero);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (double)(200 * averageColonySize * Math.Pow(10, 0));
                    subList[lastRowIndexValue].Dil_Zero = cfu.ToString();
                }
                if (!dil_oneIsNumber.Contains(false) && dil_oneIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_One);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 1));
                    subList[lastRowIndexValue].Dil_One = cfu.ToString();
                }
                if (!dil_twoIsNumber.Contains(false) && dil_twoIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Two);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 2));
                    subList[lastRowIndexValue].Dil_Two = cfu.ToString();
                }
                if (!dil_threeIsNumber.Contains(false) && dil_threeIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Three);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 3));
                    subList[lastRowIndexValue].Dil_Three = cfu.ToString();
                }
                if (!dil_fourIsNumber.Contains(false) && dil_fourIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Four);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 4));
                    subList[lastRowIndexValue].Dil_Four = cfu.ToString();
                }
                if (!dil_fiveIsNumber.Contains(false) && dil_fiveIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Five);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 5));
                    subList[lastRowIndexValue].Dil_Five = cfu.ToString();
                }
                if (!dil_sixIsNumber.Contains(false) && dil_sixIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Six);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 6));
                    subList[lastRowIndexValue].Dil_Six = cfu.ToString();
                }
                if (!dil_sevenIsNumber.Contains(false) && dil_sevenIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_Seven);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 7));
                    subList[lastRowIndexValue].Dil_Seven = cfu.ToString();
                }
            }
            return chunkList;
        }
    }
}