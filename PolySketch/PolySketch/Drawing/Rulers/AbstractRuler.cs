using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public abstract class AbstractRuler : IRuler
    {
        public AbstractRuler()
        {
            Weight = 1.0F;
        }

        public float Weight { get; set; }

        /// <summary>
        /// Get the calculated data, and generate the new weighted control points
        /// </summary>
        /// <param name="lastData">Ruled data, are needed for reference if the ruler depends on them</param>
        /// <param name="data">Unruled data, the calculaction will be based on this data</param>
        /// <returns>List of the calculated and weighted data, same length as the second parameter</returns>
        public List<PVector> UpdateWithRuler( List<PVector> lastData , List<PVector> data )
        {
            var newData = CalculateData(lastData , data);
            var retVal = new List<PVector>();
            for ( int i = 0 ; i < data.Count ; i++ )
            {
                var vec = PVector.Sub(data[i] , newData[i]);
                vec.Mult(Weight);
                vec.Add(data[i]);
                retVal.Add(vec);
            }
            return retVal;
        }

        /// <summary>
        /// Use this method to calculate the given data according to the ruler
        /// </summary>
        /// <param name="lastData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract List<PVector> CalculateData( List<PVector> lastData , List<PVector> data );
    }
}