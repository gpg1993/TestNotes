using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    /// <summary>
    /// 植物特征
    /// </summary>
    public interface IPlantTrait
    {
        /// <summary>
        /// 光合作用
        /// </summary>
        void Photosynthesis();
        /// <summary>
        /// 产生果实
        /// </summary>
        void CreateFruit();
    }

    public class PlantTrait : IPlantTrait
    {
        public void CreateFruit()
        {
            Console.WriteLine("产生果实");
        }

        public void Photosynthesis()
        {
            Console.WriteLine("光合作用");
        }
    }
}
