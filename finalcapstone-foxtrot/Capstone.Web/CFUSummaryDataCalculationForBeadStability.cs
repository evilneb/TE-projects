using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models.DataExplorationModels;

namespace Capstone.Web
{
    public static class CFUSummaryDataCalculationForBeadStability
    {
        public static int FindLengthOfSublistAmountsPerSublist(List<BeadStabilityRowData> bigListOfSummaryData)
        {
            string currentRep = "0";
            int sublistLength = 0;
            foreach(BeadStabilityRowData row in bigListOfSummaryData)
            {
                if(Convert.ToInt32(row.Rep) > Convert.ToInt32(currentRep))
                {
                    sublistLength++;
                    currentRep = row.Rep;
                }
                else if(Convert.ToInt32(row.Rep) == Convert.ToInt32(currentRep))
                {
                    sublistLength++;
                }
                else
                {
                    break;
                }
            }
            return sublistLength;
        }

        public static int FindRepitionAmountPerSublist(List<BeadStabilityRowData> bigListOfSummaryData)
        {
            string currentRep = "0";
            int repCount = 0;
            foreach (BeadStabilityRowData row in bigListOfSummaryData)
            {
                if (Convert.ToInt32(row.Rep) > Convert.ToInt32(currentRep))
                {
                    repCount++;
                    currentRep = row.Rep;
                }
                else if (Convert.ToInt32(row.Rep) == Convert.ToInt32(currentRep))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            return repCount;
        }

        public static List<List<BeadStabilityRowData>> GenerateChunkListOfSummaryBeadStabilityData(List<BeadStabilityRowData> bigListOfSummaryBeadStabilityData)
        {
            int subListLength = FindLengthOfSublistAmountsPerSublist(bigListOfSummaryBeadStabilityData);
            List<List<BeadStabilityRowData>> chunkList = new List<List<BeadStabilityRowData>>();

            for (int i = 0; i < bigListOfSummaryBeadStabilityData.Count; i += subListLength)
            {
                List<BeadStabilityRowData> currentList = new List<BeadStabilityRowData>();
                for (int j = i; j < subListLength + i; j++)
                {
                    currentList.Add(bigListOfSummaryBeadStabilityData[j]);
                }
                currentList.Add(new BeadStabilityRowData());
                chunkList.Add(currentList);
            }
            return chunkList;
        }

        public static List<List<BeadStabilityRowData>> CalculateAverageCFUValueToEachSublist(List<List<BeadStabilityRowData>> chunkListOfBeadStabilityData)
        {
            int lastRowIndexValue = FindLengthOfSublistAmountsPerSublist(chunkListOfBeadStabilityData[0]);

            List<bool> dil_zeroIsNumber = new List<bool>();
            List<bool> dil_oneIsNumber = new List<bool>();
            List<bool> dil_twoIsNumber = new List<bool>();
            List<bool> dil_threeIsNumber = new List<bool>();
            List<bool> dil_fourIsNumber = new List<bool>();
            List<bool> dil_fiveIsNumber = new List<bool>();
            List<bool> dil_sixIsNumber = new List<bool>();
            List<bool> dil_sevenIsNumber = new List<bool>();

            foreach (List<BeadStabilityRowData> subList in chunkListOfBeadStabilityData)
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
                    if (Int32.TryParse(subList[i].Dil_1HR_Zero, out n) && Convert.ToInt32(subList[i].Dil_1HR_Zero) != 0)
                    {
                        dil_zeroIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_One, out n) && Convert.ToInt32(subList[i].Dil_1HR_One) != 0)
                    {
                        dil_oneIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Two, out n) && Convert.ToInt32(subList[i].Dil_1HR_Two) != 0)
                    {
                        dil_twoIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Three, out n) && Convert.ToInt32(subList[i].Dil_1HR_Three) != 0)
                    {
                        dil_threeIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Four, out n) && Convert.ToInt32(subList[i].Dil_1HR_Four) != 0)
                    {
                        dil_fourIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Five, out n) && Convert.ToInt32(subList[i].Dil_1HR_Five) != 0)
                    {
                        dil_fiveIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Six, out n) && Convert.ToInt32(subList[i].Dil_1HR_Six) != 0)
                    {
                        dil_sixIsNumber.Add(true);
                    }
                    if (Int32.TryParse(subList[i].Dil_1HR_Seven, out n) && Convert.ToInt32(subList[i].Dil_1HR_Seven) != 0)
                    {
                        dil_sevenIsNumber.Add(true);
                    }
                }
                if (!dil_zeroIsNumber.Contains(false) && dil_zeroIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Zero);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (double)(200 * averageColonySize * Math.Pow(10, 0));
                    subList[lastRowIndexValue].Dil_1HR_Zero = cfu.ToString();
                }
                if (!dil_oneIsNumber.Contains(false) && dil_oneIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_One);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 1));
                    subList[lastRowIndexValue].Dil_1HR_One = cfu.ToString();
                }
                if (!dil_twoIsNumber.Contains(false) && dil_twoIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Two);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 2));
                    subList[lastRowIndexValue].Dil_1HR_Two = cfu.ToString();
                }
                if (!dil_threeIsNumber.Contains(false) && dil_threeIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Three);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 3));
                    subList[lastRowIndexValue].Dil_1HR_Three = cfu.ToString();
                }
                if (!dil_fourIsNumber.Contains(false) && dil_fourIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Four);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 4));
                    subList[lastRowIndexValue].Dil_1HR_Four = cfu.ToString();
                }
                if (!dil_fiveIsNumber.Contains(false) && dil_fiveIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Five);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 5));
                    subList[lastRowIndexValue].Dil_1HR_Five = cfu.ToString();
                }
                if (!dil_sixIsNumber.Contains(false) && dil_sixIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Six);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 6));
                    subList[lastRowIndexValue].Dil_1HR_Six = cfu.ToString();
                }
                if (!dil_sevenIsNumber.Contains(false) && dil_sevenIsNumber.Count >= lastRowIndexValue)
                {
                    int colonyTotal = 0;
                    for (int i = 0; i < lastRowIndexValue; i++)
                    {
                        colonyTotal += Convert.ToInt32(subList[i].Dil_1HR_Seven);
                    }
                    double averageColonySize = (double)colonyTotal / lastRowIndexValue;
                    double cfu = (200 * averageColonySize * Math.Pow(10, 7));
                    subList[lastRowIndexValue].Dil_1HR_Seven = cfu.ToString();
                }
            }
            return chunkListOfBeadStabilityData;
        }
    }
}