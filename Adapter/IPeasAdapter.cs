using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    /// <summary>
    /// 豌豆人
    /// </summary>
    public interface IPeaser
    {
        /// <summary>
        /// 攻击
        /// </summary>
        void attacking();
        /// <summary>
        /// 光合作用
        /// </summary>
        void Photosynthesis();
        /// <summary>
        /// 产生果实
        /// </summary>
        void CreateFruit();
    }

    public class PeaserOperation: IPeaser
    {
        private IPlantTrait plantTrait;
        private ICreatureFeature creatureFeature;
        public PeaserOperation()
        {
            plantTrait = new PlantTrait();
            creatureFeature = new CreatureFeature();
        }

        public void attacking()
        {
            creatureFeature.attacking();
        }

        public void CreateFruit()
        {
            plantTrait.CreateFruit();
        }

        public void Photosynthesis()
        {
            plantTrait.Photosynthesis();
        }
    }
}
