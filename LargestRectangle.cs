//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            var histogramTrends = new [] {1, 2, 1, 4, 3, 60, 63, 3, 4};
            var histogramRectangles = new List<HistogramRectangle>();
            var biggestHistogramRectangle = new HistogramRectangle();
            var historyIndex = 0;
            
            for(int idy = 0; idy < histogramTrends.Length; idy += 1)
            {
                // RemoveAll :: if trend falls down or breaks
                histogramRectangles.RemoveAll(r => r.blockHeight > histogramTrends[idy] && r.weightage <= biggestHistogramRectangle.weightage);
                // 
                for (int idx = 0; idx < histogramRectangles.Count; idx += 1)
                {
                    histogramRectangles[idx].endsAtIndex += 1;
                    histogramRectangles[idx].weightage += histogramRectangles[idx].blockHeight <= histogramTrends[idy] ?
                                                            histogramRectangles[idx].blockHeight : 0;
                    
                    // Testify the global biggest Rectangle
                    if(biggestHistogramRectangle.weightage < histogramRectangles[idx].weightage)
                    {
                        biggestHistogramRectangle.weightage = histogramRectangles[idx].weightage;
                        biggestHistogramRectangle.startsAtIndex = histogramRectangles[idx].startsAtIndex;
                        biggestHistogramRectangle.endsAtIndex = histogramRectangles[idx].endsAtIndex;
                        biggestHistogramRectangle.blockHeight = histogramRectangles[idx].blockHeight;
                    }
                }
                
                Console.WriteLine(histogramTrends[idy] + " | " 
                                  + histogramRectangles.FindIndex(r => r.blockHeight == histogramTrends[idy]));
                
                if(histogramRectangles.FindIndex(r => r.blockHeight == histogramTrends[idy]) == -1)
                {
                    histogramRectangles.Add(new HistogramRectangle 
                                        {
                                            blockHeight = histogramTrends[idy],
                                            startsAtIndex = idy,
                                            endsAtIndex = idy,
                                            weightage = histogramTrends[idy]
                                        });
                }
            }
            
            for (int idx = 0; idx < histogramRectangles.Count; idx += 1)
            {
                Console.WriteLine(histogramRectangles[idx].blockHeight + " | " 
                                 +  histogramRectangles[idx].startsAtIndex + " | "
                                 +  histogramRectangles[idx].endsAtIndex + " | "
                                 +  histogramRectangles[idx].weightage);
            }
            
            Console.WriteLine("Biggest histogram rectangle");
            
            Console.WriteLine(biggestHistogramRectangle.blockHeight + " | " 
                                 +  biggestHistogramRectangle.startsAtIndex + " | "
                                 +  biggestHistogramRectangle.endsAtIndex + " | "
                                 +  biggestHistogramRectangle.weightage);
        }
        
        public class HistogramRectangle
        {
            public int blockHeight;
            public int startsAtIndex;
            public int endsAtIndex;
            public int weightage;
        }
    }
}